// <copyright file="WCFStudent.cs" company="University of California San Diego">
// University of California San Diego. CSE 136
// </copyright>

namespace SL136
{
    using System.Collections.Generic;
    using POCO;
    using Repository;
    using Service;

    /// <summary>
    /// service layer for "student" methods
    /// </summary>
    public class WCFStudent : IStudent
    {
        /// <summary>
        /// student service
        /// </summary>
        private StudentService service = new StudentService(new StudentRepository());

        /// <summary>
        /// Get student info
        /// </summary>
        /// <param name="id">student id</param>
        /// <param name="errors">error list</param>
        /// <returns>student object</returns>
        public Student GetStudent(string id, ref List<string> errors)
        {
            return this.service.GetStudent(id, ref errors);
        }

        /// <summary>
        /// insert a new student
        /// </summary>
        /// <param name="student">student object</param>
        /// <param name="errors">error list</param>
        public void InsertStudent(Student student, ref List<string> errors)
        {
            this.service.InsertStudent(student, ref errors);
        }

        /// <summary>
        /// update student info
        /// </summary>
        /// <param name="student">student info</param>
        /// <param name="errors">error list</param>
        public void UpdateStudent(Student student, ref List<string> errors)
        {
            this.service.UpdateStudent(student, ref errors);
        }

        /// <summary>
        /// delete an existing student
        /// </summary>
        /// <param name="id">student id</param>
        /// <param name="errors">error list</param>
        public void DeleteStudent(string id, ref List<string> errors)
        {
            this.service.DeleteStudent(id, ref errors);
        }

        /// <summary>
        /// get a list of all students in the database
        /// </summary>
        /// <param name="errors">error list</param>
        /// <returns>list of students</returns>
        public List<Student> GetStudentList(ref List<string> errors)
        {
            return this.service.GetStudentList(ref errors);
        }

        /// <summary>
        /// student enrolling in a schedule course
        /// </summary>
        /// <param name="student_id">student id</param>
        /// <param name="schedule_id">schedule id</param>
        /// <param name="errors">error list</param>
        public void EnrollSchedule(string student_id, int schedule_id, ref List<string> errors)
        {
            this.service.EnrollSchedule(student_id, schedule_id, ref errors);
        }

        /// <summary>
        /// student dropping a scheduled course
        /// </summary>
        /// <param name="student_id">student id</param>
        /// <param name="schedule_id">schedule id</param>
        /// <param name="errors">error list</param>
        public void DropEnrolledSchedule(string student_id, int schedule_id, ref List<string> errors)
        {
            this.service.DropEnrolledSchedule(student_id, schedule_id, ref errors);
        }
    }
}
