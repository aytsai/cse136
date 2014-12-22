namespace Web136.Controllers
{
    using System.Web.Mvc;

    public class StaffController : Controller
    {
        public ActionResult Index(int id)
        {
            ViewBag.id = id;
            return this.View();
        }

        public ActionResult StudentList(int id)
        {
            ViewBag.id = id;
            return this.View();
        }

        public ActionResult CreateStudent()
        {
            return this.View();
        }

        public ActionResult EditStudent(string id)
        {
            ViewBag.Id = id;
            return this.View();
        }

        public ActionResult EditGrade(string id, int sched)
        {
            ViewBag.Id = id;
            ViewBag.Sched = sched;
            return this.View();
        }

        public ActionResult StudentDetail(string id)
        {
            ViewBag.Id = id;
            return this.View();
        }

        public ActionResult CapeList(int id)
        {
            ViewBag.id = id;
            return this.View();
        }
    }
}
