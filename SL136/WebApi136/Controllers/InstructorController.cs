namespace WebApi136.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;

    using POCO;

    using Repository;

    using Service;

    public class InstructorController : ApiController
    {
        private readonly InstructorService service = new InstructorService(new InstructorRepository());

        private List<string> errors = new List<string>();

        [HttpPost]
        public string InsertInstructor(Instructor instr)
        {
            this.service.InsertInstructor(instr, ref this.errors);
            return this.errors.Count == 0 ? "ok" : "Error occurred";
        }

        [HttpPost]
        public string UpdateInstructor(Instructor instr)
        {
            this.service.UpdateInstructor(instr, ref this.errors);
            return this.errors.Count == 0 ? "ok" : "Error occurred";
        }

        [HttpPost]
        public string DeleteInstructor(int id)
        {
            this.service.DeleteInstructor(id, ref this.errors);
            return this.errors.Count == 0 ? "ok" : "Error occurred";
        }

        [HttpGet]
        public Instructor ViewInstructor(int id)
        {
            return this.service.ViewInstructor(id, ref this.errors);
        }

        [HttpGet]
        public List<Instructor> ViewAllInstructors()
        {
            return this.service.ViewAllInstructors(ref this.errors);
        }

        /* public void ViewEnrollmentNumber(int id, ref List<string> errors) -- not implementing anymore = 21 features */

        [HttpPost]
        public string UpdateGrade(Enrollment enroll)
        {
            this.service.UpdateGrade(enroll, ref this.errors);
            return this.errors.Count == 0 ? "ok" : "Error occurred";
        }
    }
}