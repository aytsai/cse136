namespace WebApi136.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;

    using POCO;

    using Repository;

    using Service;

    public class CourseController : ApiController
    {
        private readonly CourseService service = new CourseService(new CourseRepository());

        private List<string> errors = new List<string>();

        [HttpGet]
        public List<Course> GetCourseList()
        {
            var service = new CourseService(new CourseRepository());
            var errors = new List<string>();

            //// we could log the errors here if there are any...
            return service.GetCourseList(ref errors);
        }

        //// you can add more [HttpGet] and [HttpPost] methods as you need
        [HttpPost]
        public string InsertPrerequisite(Prereq prereq)
        {
            this.service.InsertPrerequisite(prereq, ref this.errors);
            return this.errors.Count == 0 ? "ok" : "Error occurred";
        }

        [HttpPost]   
        public string UpdatePrerequisite(Prereq prereq)
        {
            this.service.UpdatePrerequisite(prereq, ref this.errors);
            return this.errors.Count == 0 ? "ok" : "Error occurred";
        }

        [HttpPost]
        public string DeletePrerequisite(int id)
        {
            this.service.DeletePrerequisite(id, ref this.errors);
            return this.errors.Count == 0 ? "ok" : "Error occurred";
        }

        [HttpGet]
        public Prereq ViewPrerequisite(int id)
        {
            return this.service.ViewPrerequisite(id, ref this.errors);
        }
    }
}