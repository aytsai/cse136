namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    using IRepository;

    using POCO;

    public class InstructorRepository : BaseRepository, IInstructorRepository
    {
        private const string InsInstructor = "ins_instructor";
        private const string UpdInstructor = "upd_instructor";
        private const string DelInstructor = "del_instructor";
        private const string SelInstructor = "sel_instructor";
        private const string SelAllInstructor = "sel_all_instructor";
        private const string ViewEnrollment = "sel_enrollment_num";
        private const string UpdGrade = "upd_grade";

        public void InsertInstructor(Instructor instr, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            try
            {
                var adapter = new SqlDataAdapter(InsInstructor, conn)
                {
                    SelectCommand =
                    {
                        CommandType = CommandType.StoredProcedure
                    }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@first_name", SqlDbType.VarChar, 50));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@last_name", SqlDbType.VarChar, 50));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@title", SqlDbType.VarChar, 50));

                adapter.SelectCommand.Parameters["@first_name"].Value = instr.FirstName;
                adapter.SelectCommand.Parameters["@last_name"].Value = instr.LastName;
                adapter.SelectCommand.Parameters["@title"].Value = instr.Title;

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

        public void UpdateInstructor(Instructor instr, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            try
            {
                var adapter = new SqlDataAdapter(UpdInstructor, conn)
                {
                    SelectCommand = { CommandType = CommandType.StoredProcedure }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@instructor_id", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@first_name", SqlDbType.VarChar, 50));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@last_name", SqlDbType.VarChar, 50));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@title", SqlDbType.VarChar, 50));

                adapter.SelectCommand.Parameters["@instructor_id"].Value = instr.InstructorId;
                adapter.SelectCommand.Parameters["@first_name"].Value = instr.FirstName;
                adapter.SelectCommand.Parameters["@last_name"].Value = instr.LastName;
                adapter.SelectCommand.Parameters["@title"].Value = instr.Title;

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

        public void DeleteInstructor(int id, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);

            try
            {
                var adapter = new SqlDataAdapter(DelInstructor, conn)
                {
                    SelectCommand =
                    {
                        CommandType =
                            CommandType
                            .StoredProcedure
                    }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@instructor_id", SqlDbType.Int));

                adapter.SelectCommand.Parameters["@instructor_id"].Value = id;

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

        public Instructor ViewInstructor(int id, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            Instructor instr = null;

            try
            {
                var adapter = new SqlDataAdapter(SelInstructor, conn)
                {
                    SelectCommand = { CommandType = CommandType.StoredProcedure }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@instructor_id", SqlDbType.Int));

                adapter.SelectCommand.Parameters["@instructor_id"].Value = id;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);

                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    return null;
                }

                instr = new Instructor
                {
                    InstructorId = Convert.ToInt32(dataSet.Tables[0].Rows[0]["instructor_id"].ToString()),
                    FirstName = dataSet.Tables[0].Rows[0]["first_name"].ToString(),
                    LastName = dataSet.Tables[0].Rows[0]["last_name"].ToString(),
                    Title = dataSet.Tables[0].Rows[0]["title"].ToString()
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

            return instr;
        }

        public List<Instructor> ViewAllInstructors(ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            var instrList = new List<Instructor>();

            try
            {
                var adapter = new SqlDataAdapter(SelAllInstructor, conn)
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
                    var instr = new Instructor
                    {
                        InstructorId = Convert.ToInt32(dataSet.Tables[0].Rows[i]["instructor_id"].ToString()),
                        FirstName = dataSet.Tables[0].Rows[i]["first_name"].ToString(),
                        LastName = dataSet.Tables[0].Rows[i]["last_name"].ToString(),
                        Title = dataSet.Tables[0].Rows[i]["title"].ToString()
                    };
                    instrList.Add(instr);
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

            return instrList;
        }

        public void ViewEnrollmentNumber(int id, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            try
            {
                var adapter = new SqlDataAdapter(ViewEnrollment, conn)
                {
                    SelectCommand =
                    {
                        CommandType = CommandType.StoredProcedure
                    }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@schedule_id", SqlDbType.Int));

                adapter.SelectCommand.Parameters["@schedule_id"].Value = id;

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

        public void UpdateGrade(Enrollment enroll, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            try
            {
                var adapter = new SqlDataAdapter(UpdGrade, conn)
                {
                    SelectCommand = { CommandType = CommandType.StoredProcedure }
                };

                adapter.SelectCommand.Parameters.Add(new SqlParameter("@student_id", SqlDbType.VarChar, 20));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@schedule_id", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@grade", SqlDbType.VarChar, 20));

                adapter.SelectCommand.Parameters["@student_id"].Value = enroll.StudentId;
                adapter.SelectCommand.Parameters["@schedule_id"].Value = enroll.Schedule.ScheduleId;
                adapter.SelectCommand.Parameters["@grade"].Value = enroll.Grade;

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
    }
}
