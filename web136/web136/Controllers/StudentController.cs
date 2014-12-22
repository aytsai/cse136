namespace Web136.Controllers
{
    using System.Web.Mvc;

    public class StudentController : Controller
    {
        public ActionResult Index(string id)
        {
            ViewBag.Id = id;
            return this.View();
        }

        public ActionResult Edit(string id)
        {
            ViewBag.Id = id;
            return this.View();
        }

        public ActionResult CreateEnrollment(string id)
        {
            ViewBag.Id = id;
            return this.View();
        }

        public ActionResult CreateCape(string id, int id2)
        {
            ViewBag.Id = id;
            ViewBag.Id2 = id2;
            return this.View();
        }

        public ActionResult ViewCape(string id, int id2)
        {
            ViewBag.Id = id;
            ViewBag.Id2 = id2;
            return this.View();
        }
    }
}
