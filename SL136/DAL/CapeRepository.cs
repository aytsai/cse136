namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    using IRepository;

    using POCO;

    public class CapeRepository : BaseRepository, ICapeRepository
    {
        private const string InsCape = "ins_cape";
        private const string UpdCape = "upd_cape";
        private const string DelCape = "del_cape";
        private const string SelCapeStudent = "sel_cape_student";
        private const string SelCapeInstructor = "sel_cape_instructor";

        public void InsertCape(Cape cape, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            try
            {
                var adapter = new SqlDataAdapter(InsCape, conn)
                {
                    SelectCommand =
                    {
                        CommandType = CommandType.StoredProcedure
                    }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@student_id", SqlDbType.VarChar, 50));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@schedule_id", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@review", SqlDbType.VarChar, 50));

                adapter.SelectCommand.Parameters["@student_id"].Value = cape.StudentId;
                adapter.SelectCommand.Parameters["@schedule_id"].Value = cape.Schedule.ScheduleId;
                adapter.SelectCommand.Parameters["@review"].Value = cape.Review;

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

        public void UpdateCape(Cape cape, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            try
            {
                var adapter = new SqlDataAdapter(UpdCape, conn)
                {
                    SelectCommand = { CommandType = CommandType.StoredProcedure }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@cape_id", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@student_id", SqlDbType.VarChar, 50));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@schedule_id", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@review", SqlDbType.VarChar, 50));

                adapter.SelectCommand.Parameters["@cape_id"].Value = cape.CapeId;
                adapter.SelectCommand.Parameters["@student_id"].Value = cape.StudentId;
                adapter.SelectCommand.Parameters["@schedule_id"].Value = cape.Schedule.ScheduleId;
                adapter.SelectCommand.Parameters["@review"].Value = cape.Review;

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

        public void DeleteCape(int id, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);

            try
            {
                var adapter = new SqlDataAdapter(DelCape, conn)
                {
                    SelectCommand =
                    {
                        CommandType =
                            CommandType
                            .StoredProcedure
                    }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@cape_id", SqlDbType.Int));

                adapter.SelectCommand.Parameters["@cape_id"].Value = id;

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

        public Cape ViewCape(string id1, int id2, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            Cape cape = null;

            try
            {
                var adapter = new SqlDataAdapter(SelCapeStudent, conn)
                {
                    SelectCommand = { CommandType = CommandType.StoredProcedure }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@schedule_id", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@student_id", SqlDbType.VarChar, 20));

                adapter.SelectCommand.Parameters["@schedule_id"].Value = id2;
                adapter.SelectCommand.Parameters["@student_id"].Value = id1;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);

                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    return null;
                }

                cape = new Cape
                {
                    CapeId = Convert.ToInt32(dataSet.Tables[0].Rows[0]["cape_id"].ToString()),
                    StudentId = dataSet.Tables[0].Rows[0]["student_id"].ToString(),
                    Schedule = new Schedule
                    {
                        ScheduleId = Convert.ToInt32(dataSet.Tables[0].Rows[0]["schedule_id"].ToString())
                    },
                    Review = dataSet.Tables[0].Rows[0]["review"].ToString()
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

            return cape;
        }

        public List<Cape> ViewStudentCape(int id, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            var capeList = new List<Cape>();

            try
            {
                var adapter = new SqlDataAdapter(SelCapeInstructor, conn)
                {
                    SelectCommand =
                    {
                        CommandType = CommandType.StoredProcedure
                    }
                };

                adapter.SelectCommand.Parameters.Add(new SqlParameter("@instructor_id", SqlDbType.Int));
                adapter.SelectCommand.Parameters["@instructor_id"].Value = id;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);

                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    return null;
                }

                for (var i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    var cape = new Cape
                    {
                        Schedule = new Schedule
                        {
                            ScheduleId = Convert.ToInt32(dataSet.Tables[0].Rows[i]["schedule_id"].ToString()),
                            Quarter = dataSet.Tables[0].Rows[i]["quarter"].ToString(),
                            Year = dataSet.Tables[0].Rows[i]["year"].ToString(),
                            Course = new Course
                            {
                                Title = dataSet.Tables[0].Rows[i]["course_title"].ToString()
                            }
                        },
                        Review = dataSet.Tables[0].Rows[i]["review"].ToString()
                    };
                    capeList.Add(cape);
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

            return capeList;
        }
    }
}
