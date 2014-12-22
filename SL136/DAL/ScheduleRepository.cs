namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    using IRepository;

    using POCO;

    public class ScheduleRepository : BaseRepository, IScheduleRepository
    {
        private const string GetScheduleListProcedure = "spGetScheduleList";
        private const string InsSchedule = "ins_course_schedule";
        private const string DelSchedule = "del_course_schedule";
        private const string UpdSchedule = "upd_course_schedule";
        private const string GetSched = "sel_course_schedule";
        private const string GetDay = "sel_scheduledaylist";
        private const string GetTime = "sel_scheduletimelist";
        private const string GetSDay = "sel_scheduleday";
        private const string GetSTime = "sel_scheduletime";
        private const string UpdDay = "upd_scheduleday";
        private const string UpdTime = "upd_scheduletime";
        private const string DelDay = "del_scheduleday";
        private const string DelTime = "del_scheduletime";
        private const string InsDay = "ins_scheduleday";
        private const string InsTime = "ins_scheduletime";

        public Schedule GetSchedule(int id, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            Schedule schedule = null;

            try
            {
                var adapter = new SqlDataAdapter(GetSched, conn)
                {
                    SelectCommand = { CommandType = CommandType.StoredProcedure }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@schedule_id", SqlDbType.Int));

                adapter.SelectCommand.Parameters["@schedule_id"].Value = id;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);

                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    return null;
                }

                schedule = new Schedule
                {
                    ScheduleId = Convert.ToInt32(dataSet.Tables[0].Rows[0]["schedule_id"].ToString()),
                    Year = dataSet.Tables[0].Rows[0]["year"].ToString(),
                    Quarter = dataSet.Tables[0].Rows[0]["quarter"].ToString(),
                    Session = dataSet.Tables[0].Rows[0]["session"].ToString(),
                    Unit = Convert.ToInt32(dataSet.Tables[0].Rows[0]["unit"].ToString()),
                    ScheduleDay = new ScheduleDay
                    {
                        ScheduleDayId = Convert.ToInt32(dataSet.Tables[0].Rows[0]["schedule_day_id"].ToString())
                    },
                    ScheduleTime = new ScheduleTime
                    {
                        ScheduleTimeId = Convert.ToInt32(dataSet.Tables[0].Rows[0]["schedule_time_id"].ToString())
                    },
                    Instructor = new Instructor
                    {
                        InstructorId = Convert.ToInt32(dataSet.Tables[0].Rows[0]["instructor_id"].ToString())
                    },
                    Course = new Course
                    {
                        CourseId = Convert.ToInt32(dataSet.Tables[0].Rows[0]["course_id"].ToString())
                    }
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

            return schedule;
        }

        public List<Schedule> GetScheduleList(string year, string quarter, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            var scheduleList = new List<Schedule>();

            try
            {
                var adapter = new SqlDataAdapter(GetScheduleListProcedure, conn);

                if (year.Length > 0)
                {
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@year", SqlDbType.Int));
                    adapter.SelectCommand.Parameters["@year"].Value = year;
                }

                if (quarter.Length > 0)
                {
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@quarter", SqlDbType.VarChar, 25));
                    adapter.SelectCommand.Parameters["@quarter"].Value = quarter;
                }

                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);

                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    return null;
                }

                for (var i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    var schedule = new Schedule
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
                    };
                    scheduleList.Add(schedule);
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

            return scheduleList;
        }

        public void InsertSchedule(Schedule sched, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            try
            {
                var adapter = new SqlDataAdapter(InsSchedule, conn)
                {
                    SelectCommand =
                    {
                        CommandType = CommandType.StoredProcedure
                    }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@schedule_id", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@course_id", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@year", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@quarter", SqlDbType.VarChar, 50));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@session", SqlDbType.VarChar, 64));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@unit", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@schedule_day_id", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@schedule_time_id", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@instructor_id", SqlDbType.Int));

                adapter.SelectCommand.Parameters["@schedule_id"].Value = sched.ScheduleId;
                adapter.SelectCommand.Parameters["@course_id"].Value = sched.Course.CourseId;
                adapter.SelectCommand.Parameters["@year"].Value = sched.Year;
                adapter.SelectCommand.Parameters["@quarter"].Value = sched.Quarter;
                adapter.SelectCommand.Parameters["@session"].Value = sched.Session;
                adapter.SelectCommand.Parameters["@unit"].Value = sched.Unit;
                adapter.SelectCommand.Parameters["@schedule_day_id"].Value = sched.ScheduleDay.ScheduleDayId;
                adapter.SelectCommand.Parameters["@schedule_time_id"].Value = sched.ScheduleTime.ScheduleTimeId;
                adapter.SelectCommand.Parameters["@instructor_id"].Value = sched.Instructor.InstructorId;

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

        public void UpdateSchedule(Schedule sched, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            try
            {
                var adapter = new SqlDataAdapter(UpdSchedule, conn)
                {
                    SelectCommand = { CommandType = CommandType.StoredProcedure }
                };

                adapter.SelectCommand.Parameters.Add(new SqlParameter("@schedule_id", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@course_id", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@year", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@quarter", SqlDbType.VarChar, 50));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@session", SqlDbType.VarChar, 50));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@unit", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@schedule_day_id", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@schedule_time_id", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@instructor_id", SqlDbType.Int));

                adapter.SelectCommand.Parameters["@schedule_id"].Value = sched.ScheduleId;
                adapter.SelectCommand.Parameters["@course_id"].Value = sched.Course.CourseId;
                adapter.SelectCommand.Parameters["@year"].Value = sched.Year;
                adapter.SelectCommand.Parameters["@quarter"].Value = sched.Quarter;
                adapter.SelectCommand.Parameters["@session"].Value = sched.Session;
                adapter.SelectCommand.Parameters["@unit"].Value = sched.Unit;
                adapter.SelectCommand.Parameters["@schedule_day_id"].Value = sched.ScheduleDay.ScheduleDayId;
                adapter.SelectCommand.Parameters["@schedule_time_id"].Value = sched.ScheduleTime.ScheduleTimeId;
                adapter.SelectCommand.Parameters["@instructor_id"].Value = sched.Instructor.InstructorId;

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

        public void DeleteSchedule(int id, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);

            try
            {
                var adapter = new SqlDataAdapter(DelSchedule, conn)
                {
                    SelectCommand =
                    {
                        CommandType =
                            CommandType
                            .StoredProcedure
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

        public List<ScheduleDay> GetScheduleDayList(ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            var scheduleDayList = new List<ScheduleDay>();

            try
            {
                var adapter = new SqlDataAdapter(GetDay, conn);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);

                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    return null;
                }

                for (var i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    var scheduleDay = new ScheduleDay
                    {
                        ScheduleDayId = Convert.ToInt32(dataSet.Tables[0].Rows[i]["schedule_day_id"].ToString()),
                        SchedDay = dataSet.Tables[0].Rows[i]["schedule_day"].ToString()
                    };
                    scheduleDayList.Add(scheduleDay);
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

            return scheduleDayList;
        }

        public List<ScheduleTime> GetScheduleTimeList(ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            var scheduleTimeList = new List<ScheduleTime>();

            try
            {
                var adapter = new SqlDataAdapter(GetTime, conn);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);

                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    return null;
                }

                for (var i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    var scheduleTime = new ScheduleTime
                    {
                        ScheduleTimeId = Convert.ToInt32(dataSet.Tables[0].Rows[i]["schedule_time_id"].ToString()),
                        SchedTime = dataSet.Tables[0].Rows[i]["schedule_time"].ToString()
                    };
                    scheduleTimeList.Add(scheduleTime);
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

            return scheduleTimeList;
        }

        public ScheduleDay GetScheduleDay(int id, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            ScheduleDay scheduleDay = null;

            try
            {
                var adapter = new SqlDataAdapter(GetSDay, conn)
                {
                    SelectCommand = { CommandType = CommandType.StoredProcedure }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@schedule_day_id", SqlDbType.Int));

                adapter.SelectCommand.Parameters["@schedule_day_id"].Value = id;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);

                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    return null;
                }

                scheduleDay = new ScheduleDay
                {
                    ScheduleDayId = Convert.ToInt32(dataSet.Tables[0].Rows[0]["schedule_day_id"].ToString()),
                    SchedDay = dataSet.Tables[0].Rows[0]["schedule_day"].ToString()
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

            return scheduleDay;
        }

        public ScheduleTime GetScheduleTime(int id, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            ScheduleTime scheduleTime = null;

            try
            {
                var adapter = new SqlDataAdapter(GetSTime, conn)
                {
                    SelectCommand = { CommandType = CommandType.StoredProcedure }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@schedule_time_id", SqlDbType.Int));

                adapter.SelectCommand.Parameters["@schedule_time_id"].Value = id;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);

                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    return null;
                }

                scheduleTime = new ScheduleTime
                {
                    ScheduleTimeId = Convert.ToInt32(dataSet.Tables[0].Rows[0]["schedule_time_id"].ToString()),
                    SchedTime = dataSet.Tables[0].Rows[0]["schedule_time"].ToString()
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

            return scheduleTime;
        }

        public void UpdateScheduleDay(ScheduleDay sched, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            try
            {
                var adapter = new SqlDataAdapter(UpdDay, conn)
                {
                    SelectCommand = { CommandType = CommandType.StoredProcedure }
                };

                adapter.SelectCommand.Parameters.Add(new SqlParameter("@schedule_day_id", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@schedule_day", SqlDbType.VarChar, 50));

                adapter.SelectCommand.Parameters["@schedule_day_id"].Value = sched.ScheduleDayId;
                adapter.SelectCommand.Parameters["@schedule_day"].Value = sched.SchedDay;

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

        public void UpdateScheduleTime(ScheduleTime sched, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            try
            {
                var adapter = new SqlDataAdapter(UpdTime, conn)
                {
                    SelectCommand = { CommandType = CommandType.StoredProcedure }
                };

                adapter.SelectCommand.Parameters.Add(new SqlParameter("@schedule_time_id", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@schedule_time", SqlDbType.VarChar, 50));

                adapter.SelectCommand.Parameters["@schedule_time_id"].Value = sched.ScheduleTimeId;
                adapter.SelectCommand.Parameters["@schedule_time"].Value = sched.SchedTime;

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

        public void DeleteScheduleDay(int id, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);

            try
            {
                var adapter = new SqlDataAdapter(DelDay, conn)
                {
                    SelectCommand =
                    {
                        CommandType =
                            CommandType
                            .StoredProcedure
                    }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@schedule_day_id", SqlDbType.Int));

                adapter.SelectCommand.Parameters["@schedule_day_id"].Value = id;

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

        public void DeleteScheduleTime(int id, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);

            try
            {
                var adapter = new SqlDataAdapter(DelTime, conn)
                {
                    SelectCommand =
                    {
                        CommandType =
                            CommandType
                            .StoredProcedure
                    }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@schedule_time_id", SqlDbType.Int));

                adapter.SelectCommand.Parameters["@schedule_time_id"].Value = id;

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

        public void InsertScheduleDay(ScheduleDay sched, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            try
            {
                var adapter = new SqlDataAdapter(InsDay, conn)
                {
                    SelectCommand =
                    {
                        CommandType = CommandType.StoredProcedure
                    }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@schedule_day", SqlDbType.VarChar, 50));

                adapter.SelectCommand.Parameters["@schedule_day"].Value = sched.SchedDay;

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

        public void InsertScheduleTime(ScheduleTime sched, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            try
            {
                var adapter = new SqlDataAdapter(InsTime, conn)
                {
                    SelectCommand =
                    {
                        CommandType = CommandType.StoredProcedure
                    }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@schedule_time", SqlDbType.VarChar, 50));

                adapter.SelectCommand.Parameters["@schedule_time"].Value = sched.SchedTime;

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
