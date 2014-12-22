namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    using IRepository;

    using POCO;

    public class CourseRepository : BaseRepository, ICourseRepository
    {
        private const string GetCourseListProcedure = "spGetCourseList";
        private const string InsertPrereq = "ins_prereq";
        private const string UpdatePrereq = "upd_prereq";
        private const string ViewPrereq = "sel_prereq";
        private const string DeletePrereq = "del_prereq";

        public void InsertPrerequisite(Prereq prereq, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            try
            {
                var adapter = new SqlDataAdapter(InsertPrereq, conn)
                {
                    SelectCommand =
                    {
                        CommandType = CommandType.StoredProcedure
                    }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@course_id", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@p_course_id", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@alt_p_course_id", SqlDbType.Int));

                adapter.SelectCommand.Parameters["@course_id"].Value = prereq.CourseID;
                adapter.SelectCommand.Parameters["@p_course_id"].Value = prereq.PCourseID;
                adapter.SelectCommand.Parameters["@p_course_id"].Value = prereq.AltPCourseID;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
            finally
            {
                conn.Dispose();
            }
        }

        public void UpdatePrerequisite(Prereq prereq, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            try
            {
                var adapter = new SqlDataAdapter(UpdatePrereq, conn)
                {
                    SelectCommand = { CommandType = CommandType.StoredProcedure }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@course_id", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@p_course_id", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@alt_p_course_id", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@prereq_id", SqlDbType.Int));

                adapter.SelectCommand.Parameters["@course_id"].Value = prereq.CourseID;
                adapter.SelectCommand.Parameters["@p_course_id"].Value = prereq.PCourseID;
                adapter.SelectCommand.Parameters["@p_course_id"].Value = prereq.AltPCourseID;
                adapter.SelectCommand.Parameters["@p_course_id"].Value = prereq.PrereqID;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
            finally
            {
                conn.Dispose();
            }
        }

        public void DeletePrerequisite(int id, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);

            try
            {
                var adapter = new SqlDataAdapter(DeletePrereq, conn)
                {
                    SelectCommand =
                    {
                        CommandType =
                            CommandType
                            .StoredProcedure
                    }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@prereq_id", SqlDbType.Int));

                adapter.SelectCommand.Parameters["@prereq_id"].Value = id;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
            finally
            {
                conn.Dispose();
            }
        }

        public Prereq ViewPrerequisite(int id, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            Prereq prereq = null;

            try
            {
                var adapter = new SqlDataAdapter(ViewPrereq, conn)
                {
                    SelectCommand = { CommandType = CommandType.StoredProcedure }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@course_id", SqlDbType.Int));

                adapter.SelectCommand.Parameters["@course_id"].Value = id;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);

                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    return null;
                }

                prereq = new Prereq
                {
                    CourseID = Convert.ToInt32(dataSet.Tables[0].Rows[0]["course_id"].ToString()),
                    PCourseID = Convert.ToInt32(dataSet.Tables[0].Rows[0]["course_id"].ToString()),
                    AltPCourseID = Convert.ToInt32(dataSet.Tables[0].Rows[0]["course_id"].ToString()),
                    PrereqID = Convert.ToInt32(dataSet.Tables[0].Rows[0]["prereq_id"].ToString())
                };
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
            finally
            {
                conn.Dispose();
            }

            return prereq;
        }

        public List<Course> GetCourseList(ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            var courseList = new List<Course>();

            try
            {
                var adapter = new SqlDataAdapter(GetCourseListProcedure, conn)
                                  {
                                      SelectCommand =
                                          {
                                              CommandType = CommandType.StoredProcedure
                                          }
                                  };

                var dataSet = new DataSet();
                adapter.Fill(dataSet);

                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    return null;
                }

                for (var i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    var course = new Course
                                     {
                                         CourseId = Convert.ToInt32(dataSet.Tables[0].Rows[i]["course_id"].ToString()),
                                         Title = dataSet.Tables[0].Rows[i]["course_title"].ToString(),
                                         CourseLevel =
                                             (CourseLevel)
                                             Enum.Parse(
                                                 typeof(CourseLevel),
                                                 dataSet.Tables[0].Rows[i]["course_level"].ToString()),
                                         Description = dataSet.Tables[0].Rows[i]["course_description"].ToString()
                                     };
                    courseList.Add(course);
                }
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
            finally
            {
                conn.Dispose();
            }

            return courseList;
        }
    }
}
