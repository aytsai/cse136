namespace WebApi136.Controllers
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.Web.Http;

    using POCO;

    using Repository;

    using Service;

    public class StudentController : ApiController
    {
        private readonly StudentService service = new StudentService(new StudentRepository());

        private List<string> errors = new List<string>();

        [HttpGet]
        public Student GetStudent(string id)
        {
            return this.service.GetStudent(id, ref this.errors);
        }

        [HttpPost]
        public string InsertStudent(Student student)
        {
            this.service.InsertStudent(student, ref this.errors);
            return this.errors.Count == 0 ? "ok" : "Error occurred";
        }

        [HttpPost]
        public string UpdateStudent(Student student)
        {
            this.service.UpdateStudent(student, ref this.errors);
            return this.errors.Count == 0 ? "ok" : "Error occurred";
        }

        [HttpPost]
        public string DeleteStudent(string id)
        {
            this.service.DeleteStudent(id, ref this.errors);
            return this.errors.Count == 0 ? "ok" : "Error occurred";
        }

        [HttpGet]
        public List<Student> GetStudentList()
        {
            return this.service.GetStudentList(ref this.errors);
        }

        [HttpPost]
        public string EnrollSchedule(Enrollment enroll)
        {
            this.service.EnrollSchedule(enroll, ref this.errors);
            return this.errors.Count == 0 ? "ok" : "Error occurred";
        }

        [HttpPost]
        public string DropEnrolledSchedule(Enrollment enroll)
        {
            this.service.DropEnrolledSchedule(enroll, ref this.errors);
            return this.errors.Count == 0 ? "ok" : "Error occurred";
        }

        [HttpGet]
        public Enrollment ViewGrade(string id1, int id2)
        {
            return this.service.ViewGrade(id1, id2, ref this.errors);
        }

        [HttpGet]
        public List<Enrollment> GetEnrollments(string studentId)
        {
            return this.service.GetEnrollments(studentId, ref this.errors);
        }
    }
}