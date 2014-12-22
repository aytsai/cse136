namespace IRepository
{
    using System.Collections.Generic;

    using POCO;

    public interface IScheduleRepository
    {
        Schedule GetSchedule(int id, ref List<string> errors);

        List<Schedule> GetScheduleList(string year, string quarter, ref List<string> errors);

        void InsertSchedule(Schedule sched, ref List<string> errors);

        void UpdateSchedule(Schedule sched, ref List<string> errors);

        void DeleteSchedule(int id, ref List<string> errors);

        List<ScheduleDay> GetScheduleDayList(ref List<string> errors);

        List<ScheduleTime> GetScheduleTimeList(ref List<string> errors);

        ScheduleDay GetScheduleDay(int id, ref List<string> errors);

        ScheduleTime GetScheduleTime(int id, ref List<string> errors);

        void UpdateScheduleDay(ScheduleDay sched, ref List<string> errors);

        void UpdateScheduleTime(ScheduleTime sched, ref List<string> errors);

        void DeleteScheduleDay(int id, ref List<string> errors);

        void DeleteScheduleTime(int id, ref List<string> errors);

        void InsertScheduleDay(ScheduleDay sched, ref List<string> errors);

        void InsertScheduleTime(ScheduleTime sched, ref List<string> errors);
    }
}
