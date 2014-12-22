namespace Service
{
    using System;

    using System.Collections.Generic;

    using IRepository;

    using POCO;

    public class ScheduleService
    {
        private readonly IScheduleRepository repository;

        public ScheduleService(IScheduleRepository repository)
        {
            this.repository = repository;
        }

        public Schedule GetSchedule(int id, ref List<string> errors)
        {
            return this.repository.GetSchedule(id, ref errors);
        }

        public List<Schedule> GetScheduleList(string year, string quarter, ref List<string> errors)
        {
            if (year == null && quarter == null)
            {
                return this.repository.GetScheduleList(string.Empty, string.Empty, ref errors);
            }
            else if (year == null)
            {
                return this.repository.GetScheduleList(string.Empty, quarter, ref errors);
            }
            else if (quarter == null)
            {
                return this.repository.GetScheduleList(year, string.Empty, ref errors);
            }
            else
            {
                return this.repository.GetScheduleList(year, quarter, ref errors);
            }
        }

        public void InsertSchedule(Schedule sched, ref List<string> errors)
        {
            if (sched == null)
            {
                errors.Add("Schedule cannot be null");
                throw new ArgumentException();
            }

            this.repository.InsertSchedule(sched, ref errors);
        }

        public void UpdateSchedule(Schedule sched, ref List<string> errors)
        {
            if (sched == null)
            {
                errors.Add("Schedule cannot be null");
                throw new ArgumentException();
            }

            this.repository.UpdateSchedule(sched, ref errors);
        }

        public void DeleteSchedule(int id, ref List<string> errors)
        {
            if (id == 0)
            {
                errors.Add("Invalid schedule id");
                throw new ArgumentException();
            }

            this.repository.DeleteSchedule(id, ref errors);
        }

        public List<ScheduleDay> GetScheduleDayList(ref List<string> errors)
        {
            return this.repository.GetScheduleDayList(ref errors);
        }

        public List<ScheduleTime> GetScheduleTimeList(ref List<string> errors)
        {
            return this.repository.GetScheduleTimeList(ref errors);
        }

        public ScheduleDay GetScheduleDay(int id, ref List<string> errors)
        {
            return this.repository.GetScheduleDay(id, ref errors);
        }

        public ScheduleTime GetScheduleTime(int id, ref List<string> errors)
        {
            return this.repository.GetScheduleTime(id, ref errors);
        }

        public void UpdateScheduleDay(ScheduleDay sched, ref List<string> errors)
        {
            if (sched == null)
            {
                errors.Add("Schedule day cannot be null");
                throw new ArgumentException();
            }

            this.repository.UpdateScheduleDay(sched, ref errors);
        }

        public void UpdateScheduleTime(ScheduleTime sched, ref List<string> errors)
        {
            if (sched == null)
            {
                errors.Add("Schedule time cannot be null");
                throw new ArgumentException();
            }

            this.repository.UpdateScheduleTime(sched, ref errors);
        }

        public void DeleteScheduleDay(int id, ref List<string> errors)
        {
            if (id == 0)
            {
                errors.Add("Invalid schedule id");
                throw new ArgumentException();
            }

            this.repository.DeleteScheduleDay(id, ref errors);
        }

        public void DeleteScheduleTime(int id, ref List<string> errors)
        {
            if (id == 0)
            {
                errors.Add("Invalid schedule id");
                throw new ArgumentException();
            }

            this.repository.DeleteScheduleTime(id, ref errors);
        }

        public void InsertScheduleDay(ScheduleDay sched, ref List<string> errors)
        {
            if (sched == null)
            {
                errors.Add("Schedule day cannot be null");
                throw new ArgumentException();
            }

            this.repository.InsertScheduleDay(sched, ref errors);
        }

        public void InsertScheduleTime(ScheduleTime sched, ref List<string> errors)
        {
            if (sched == null)
            {
                errors.Add("Schedule time cannot be null");
                throw new ArgumentException();
            }

            this.repository.InsertScheduleTime(sched, ref errors);
        }
    }
}
