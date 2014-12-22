﻿namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    using IRepository;

    using POCO;

    public class StudentRepository : BaseRepository, IStudentRepository
    {
        private const string InsertStudentInfoProcedure = "spInsertStudentInfo";
        private const string UpdateStudentInfoProcedure = "spUpdateStudentInfo";
        private const string DeleteStudentInfoProcedure = "spDeleteStudentInfo";
        private const string GetStudentListProcedure = "spGetStudentList";
        private const string GetStudentInfoProcedure = "spGetStudentInfo";
        private const string InsertStudentScheduleProcedure = "spInsertStudentSchedule";
        private const string DeleteStudentScheduleProcedure = "spDeleteStudentSchedule";
        private const string GetEnrollListProcedure = "sel_enrollment";
        private const string SelGrade = "sel_grade";

        public void InsertStudent(Student student, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            try
            {
                var adapter = new SqlDataAdapter(InsertStudentInfoProcedure, conn)
                                  {
                                      SelectCommand =
                                          {
                                              CommandType = CommandType.StoredProcedure
                                          }
                                  };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@student_id", SqlDbType.VarChar, 20));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@ssn", SqlDbType.VarChar, 9));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@first_name", SqlDbType.VarChar, 50));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@last_name", SqlDbType.VarChar, 50));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@email", SqlDbType.VarChar, 64));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@password", SqlDbType.VarChar, 64));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@shoe_size", SqlDbType.Float));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@weight", SqlDbType.Int));

                adapter.SelectCommand.Parameters["@student_id"].Value = student.StudentId;
                adapter.SelectCommand.Parameters["@ssn"].Value = student.SSN;
                adapter.SelectCommand.Parameters["@first_name"].Value = student.FirstName;
                adapter.SelectCommand.Parameters["@last_name"].Value = student.LastName;
                adapter.SelectCommand.Parameters["@email"].Value = student.Email;
                adapter.SelectCommand.Parameters["@password"].Value = student.Password;
                adapter.SelectCommand.Parameters["@shoe_size"].Value = student.ShoeSize;
                adapter.SelectCommand.Parameters["@weight"].Value = student.Weight;

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

        public void UpdateStudent(Student student, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            try
            {
                var adapter = new SqlDataAdapter(UpdateStudentInfoProcedure, conn)
                {
                    SelectCommand = { CommandType = CommandType.StoredProcedure }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@student_id", SqlDbType.VarChar, 20));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@ssn", SqlDbType.VarChar, 9));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@first_name", SqlDbType.VarChar, 50));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@last_name", SqlDbType.VarChar, 50));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@email", SqlDbType.VarChar, 64));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@password", SqlDbType.VarChar, 64));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@shoe_size", SqlDbType.Float));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@weight", SqlDbType.Int));

                adapter.SelectCommand.Parameters["@student_id"].Value = student.StudentId;
                adapter.SelectCommand.Parameters["@ssn"].Value = student.SSN;
                adapter.SelectCommand.Parameters["@first_name"].Value = student.FirstName;
                adapter.SelectCommand.Parameters["@last_name"].Value = student.LastName;
                adapter.SelectCommand.Parameters["@email"].Value = student.Email;
                adapter.SelectCommand.Parameters["@password"].Value = student.Password;
                adapter.SelectCommand.Parameters["@shoe_size"].Value = student.ShoeSize;
                adapter.SelectCommand.Parameters["@weight"].Value = student.Weight;

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

        public void DeleteStudent(string id, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);

            try
            {
                var adapter = new SqlDataAdapter(DeleteStudentInfoProcedure, conn)
                                  {
                                      SelectCommand =
                                          {
                                              CommandType =
                                                  CommandType
                                                  .StoredProcedure
                                          }
                                  };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@student_id", SqlDbType.VarChar, 20));

                adapter.SelectCommand.Parameters["@student_id"].Value = id;

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

        public Student GetStudentDetail(string id, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            Student student = null;

            try
            {
                var adapter = new SqlDataAdapter(GetStudentInfoProcedure, conn)
                {
                    SelectCommand = { CommandType = CommandType.StoredProcedure }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@student_id", SqlDbType.VarChar, 20));

                adapter.SelectCommand.Parameters["@student_id"].Value = id;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);

                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    return null;
                }

                student = new Student
                              {
                                  StudentId = dataSet.Tables[0].Rows[0]["student_id"].ToString(),
                                  FirstName = dataSet.Tables[0].Rows[0]["first_name"].ToString(),
                                  LastName = dataSet.Tables[0].Rows[0]["last_name"].ToString(),
                                  SSN = dataSet.Tables[0].Rows[0]["ssn"].ToString(),
                                  Email = dataSet.Tables[0].Rows[0]["email"].ToString(),
                                  Password = dataSet.Tables[0].Rows[0]["password"].ToString(),
                                  ShoeSize =
                                      (float)Convert.ToDouble(dataSet.Tables[0].Rows[0]["shoe_size"].ToString()),
                                  Weight = Convert.ToInt32(dataSet.Tables[0].Rows[0]["weight"].ToString())
                              };

                if (dataSet.Tables[1] != null)
                {
                    student.Enrolled = new List<Schedule>();
                    for (var i = 0; i < dataSet.Tables[1].Rows.Count; i++)
                    {
                        var schedule = new Schedule();
                        var course = new Course
                                         {
                                             CourseId = Convert.ToInt32(dataSet.Tables[1].Rows[i]["course_id"].ToString()),
                                             Title = dataSet.Tables[1].Rows[i]["course_title"].ToString(),
                                             Description =
                                                 dataSet.Tables[1].Rows[i]["course_description"].ToString()
                                         };
                        schedule.Course = course;

                        schedule.Quarter = dataSet.Tables[1].Rows[i]["quarter"].ToString();
                        schedule.Year = dataSet.Tables[1].Rows[i]["year"].ToString();
                        schedule.Session = dataSet.Tables[1].Rows[i]["session"].ToString();
                        schedule.ScheduleId = Convert.ToInt32(dataSet.Tables[1].Rows[i]["schedule_id"].ToString());
                        student.Enrolled.Add(schedule);
                    }
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

            return student;
        }

        public List<Student> GetStudentList(ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            var studentList = new List<Student>();

            try
            {
                var adapter = new SqlDataAdapter(GetStudentListProcedure, conn)
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
                    var student = new Student
                                      {
                                          StudentId = dataSet.Tables[0].Rows[i]["student_id"].ToString(),
                                          FirstName = dataSet.Tables[0].Rows[i]["first_name"].ToString(),
                                          LastName = dataSet.Tables[0].Rows[i]["last_name"].ToString(),
                                          SSN = dataSet.Tables[0].Rows[i]["ssn"].ToString(),
                                          Email = dataSet.Tables[0].Rows[i]["email"].ToString(),
                                          Password = dataSet.Tables[0].Rows[i]["password"].ToString(),
                                          ShoeSize =
                                              (float)
                                              Convert.ToDouble(dataSet.Tables[0].Rows[i]["shoe_size"].ToString()),
                                          Weight = Convert.ToInt32(dataSet.Tables[0].Rows[i]["weight"].ToString())
                                      };
                    studentList.Add(student);
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

            return studentList;
        }

        public void EnrollSchedule(Enrollment enroll, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);

            try
            {
                var adapter = new SqlDataAdapter(InsertStudentScheduleProcedure, conn)
                                  {
                                      SelectCommand =
                                          {
                                              CommandType
                                                  =
                                                  CommandType
                                                  .StoredProcedure
                                          }
                                  };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@student_id", SqlDbType.VarChar, 20));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@schedule_id", SqlDbType.Int));

                adapter.SelectCommand.Parameters["@student_id"].Value = enroll.StudentId;
                adapter.SelectCommand.Parameters["@schedule_id"].Value = enroll.Schedule.ScheduleId;

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

        public void DropEnrolledSchedule(Enrollment enroll, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);

            try
            {
                var adapter = new SqlDataAdapter(DeleteStudentScheduleProcedure, conn)
                                  {
                                      SelectCommand =
                                          {
                                              CommandType
                                                  =
                                                  CommandType
                                                  .StoredProcedure
                                          }
                                  };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@student_id", SqlDbType.VarChar, 20));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@schedule_id", SqlDbType.Int));

                adapter.SelectCommand.Parameters["@student_id"].Value = enroll.StudentId;
                adapter.SelectCommand.Parameters["@schedule_id"].Value = enroll.Schedule.ScheduleId;

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

        public List<Enrollment> GetEnrollments(string studentId, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            var enrollList = new List<Enrollment>();

            try
            {
                var adapter = new SqlDataAdapter(GetEnrollListProcedure, conn)
                {
                    SelectCommand =
                    {
                        CommandType = CommandType.StoredProcedure
                    }
                };

                adapter.SelectCommand.Parameters.Add(new SqlParameter("@student_id", SqlDbType.VarChar, 20));

                adapter.SelectCommand.Parameters["@student_id"].Value = studentId;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);

                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    return null;
                }

                for (var i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    var enroll = new Enrollment
                    {
                        StudentId = dataSet.Tables[0].Rows[i]["student_id"].ToString(),
                        Schedule = new Schedule
                        {
                            ScheduleId = Convert.ToInt32(dataSet.Tables[0].Rows[i]["schedule_id"].ToString()),
                            Year = dataSet.Tables[0].Rows[i]["year"].ToString(),
                            Quarter = dataSet.Tables[0].Rows[i]["quarter"].ToString(),
                            Session = dataSet.Tables[0].Rows[i]["session"].ToString(),
                            Unit = Convert.ToInt32(dataSet.Tables[0].Rows[i]["unit"].ToString()),
                            ScheduleDay = new ScheduleDay
                            {
                                ScheduleDayId = Convert.ToInt32(dataSet.Tables[0].Rows[i]["schedule_day_id"].ToString()),
                                SchedDay = dataSet.Tables[0].Rows[i]["schedule_day"].ToString()
                            },
                            ScheduleTime = new ScheduleTime
                            {
                                ScheduleTimeId = Convert.ToInt32(dataSet.Tables[0].Rows[i]["schedule_time_id"].ToString()),
                                SchedTime = dataSet.Tables[0].Rows[i]["schedule_time"].ToString()
                            },
                            Instructor = new Instructor
                            {
                                InstructorId = Convert.ToInt32(dataSet.Tables[0].Rows[i]["instructor_id"].ToString()),
                                FirstName = dataSet.Tables[0].Rows[i]["first_name"].ToString(),
                                LastName = dataSet.Tables[0].Rows[i]["last_name"].ToString(),
                                Title = dataSet.Tables[0].Rows[i]["title"].ToString()
                            },
                            Course = new Course
                            {
                                CourseId = Convert.ToInt32(dataSet.Tables[0].Rows[i]["course_id"].ToString()),
                                Title = dataSet.Tables[0].Rows[i]["course_title"].ToString(),
                                Description = dataSet.Tables[0].Rows[i]["course_description"].ToString(),
                            }
                        },
                        Grade = DBNull.Value.Equals(dataSet.Tables[0].Rows[i]) ? string.Empty : dataSet.Tables[0].Rows[i]["grade"].ToString()
                    };
                    enrollList.Add(enroll);
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

            return enrollList;
        }

        public Enrollment ViewGrade(string id1, int id2, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            Enrollment enroll = null;

            try
            {
                var adapter = new SqlDataAdapter(SelGrade, conn)
                {
                    SelectCommand = { CommandType = CommandType.StoredProcedure }
                };

                adapter.SelectCommand.Parameters.Add(new SqlParameter("@student_id", SqlDbType.VarChar, 20));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@schedule_id", SqlDbType.Int));

                adapter.SelectCommand.Parameters["@student_id"].Value = id1;
                adapter.SelectCommand.Parameters["@schedule_id"].Value = id2;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);

                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    return null;
                }

                enroll = new Enrollment
                {
                    StudentId = dataSet.Tables[0].Rows[0]["student_id"].ToString(),
                    Schedule = new Schedule
                    {
                        ScheduleId = Convert.ToInt32(dataSet.Tables[0].Rows[0]["schedule_id"].ToString())
                    },
                    Grade = dataSet.Tables[0].Rows[0]["grade"].ToString()
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

            return enroll;
        }
    }
}
