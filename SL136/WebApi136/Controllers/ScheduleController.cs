namespace WebApi136.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;

    using POCO;

    using Repository;

    using Service;

    public class ScheduleController : ApiController
    {
        private readonly ScheduleService service = new ScheduleService(new ScheduleRepository());

        private List<string> errors = new List<string>();

        [HttpGet]
        public Schedule GetSchedule(int id)
        {
            // var service = new ScheduleService(new ScheduleRepository());
            // var errors = new List<string>();
            // return service.GetScheduleList(year, quarter, ref errors);
            return this.service.GetSchedule(id, ref this.errors);
        }

        [HttpGet]
        public List<Schedule> GetScheduleList(string year, string quarter)
        {
            // var service = new ScheduleService(new ScheduleRepository());
            // var errors = new List<string>();
            // return service.GetScheduleList(year, quarter, ref errors);
            return this.service.GetScheduleList(year, quarter, ref this.errors);
        }

        [HttpPost]
        public string InsertSchedule(Schedule sched)
        {
            this.service.InsertSchedule(sched, ref this.errors);
            return this.errors.Count == 0 ? "ok" : "Error occurred";
        }

        [HttpPost]
        public string UpdateSchedule(Schedule sched)
        {
            this.service.UpdateSchedule(sched, ref this.errors);
            return this.errors.Count == 0 ? "ok" : "Error occurred";
        }

        [HttpPost]
        public string DeleteSchedule(int id)
        {
            this.service.DeleteSchedule(id, ref this.errors);
            return this.errors.Count == 0 ? "ok" : "Error occurred";
        }

        [HttpGet]
        public List<ScheduleDay> GetScheduleDayList()
        {
            return this.service.GetScheduleDayList(ref this.errors);
        }

        [HttpGet]
        public List<ScheduleTime> GetScheduleTimeList()
        {
            return this.service.GetScheduleTimeList(ref this.errors);
        }

        [HttpGet]
        public ScheduleDay GetScheduleDay(int id)
        {
            return this.service.GetScheduleDay(id, ref this.errors);
        }

        [HttpGet]
        public ScheduleTime GetScheduleTime(int id)
        {
            return this.service.GetScheduleTime(id, ref this.errors);
        }

        [HttpPost]
        public string UpdateScheduleDay(ScheduleDay sched)
        {
            this.service.UpdateScheduleDay(sched, ref this.errors);
            return this.errors.Count == 0 ? "ok" : "Error occurred";
        }

        [HttpPost]
        public string UpdateScheduleTime(ScheduleTime sched)
        {
            this.service.UpdateScheduleTime(sched, ref this.errors);
            return this.errors.Count == 0 ? "ok" : "Error occurred";
        }

        [HttpPost]
        public string DeleteScheduleDay(int id)
        {
            this.service.DeleteScheduleDay(id, ref this.errors);
            return this.errors.Count == 0 ? "ok" : "Error occurred";
        }

        [HttpPost]
        public string DeleteScheduleTime(int id)
        {
            this.service.DeleteScheduleTime(id, ref this.errors);
            return this.errors.Count == 0 ? "ok" : "Error occurred";
        }

        [HttpPost]
        public string InsertScheduleDay(ScheduleDay sched)
        {
            this.service.InsertScheduleDay(sched, ref this.errors);
            return this.errors.Count == 0 ? "ok" : "Error occurred";
        }

        [HttpPost]
        public string InsertScheduleTime(ScheduleTime sched)
        {
            this.service.InsertScheduleTime(sched, ref this.errors);
            return this.errors.Count == 0 ? "ok" : "Error occurred";
        }
    }
}