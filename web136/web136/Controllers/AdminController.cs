namespace Web136.Controllers
{
    using System.Web.Mvc;

    public class AdminController : Controller
    {
        public ActionResult Index(int id)
        {
            ViewBag.id = id;
            return this.View();
        }

        public ActionResult Edit(int id)
        {
            ViewBag.id = id;
            return this.View();
        }

        public ActionResult StudentList()
        {
            return this.View();
        }

        public ActionResult InstructorList()
        {
            return this.View();
        }

        public ActionResult CreateInstructor()
        {
            return this.View();
        }

        public ActionResult EditInstructor(int id)
        {
            ViewBag.Id = id;
            return this.View();
        }

        public ActionResult ScheduleDayList()
        {
            return this.View();
        }

        public ActionResult ScheduleTimeList()
        {
            return this.View();
        }

        public ActionResult EditScheduleDay(int id)
        {
            ViewBag.Id = id;
            return this.View();
        }

        public ActionResult EditScheduleTime(int id)
        {
            ViewBag.Id = id;
            return this.View();
        }

        public ActionResult CreateScheduleDay()
        {
            return this.View();
        }

        public ActionResult CreateScheduleTime()
        {
            return this.View();
        }
    }
}
