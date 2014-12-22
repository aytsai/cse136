// <copyright file="IStudent.cs" company="University of California San Diego">
// University of California San Diego. CSE 136
// </copyright>

namespace SL136
{
    using System.Collections.Generic;
    using System.ServiceModel;
    using POCO; 

    /// <summary>
    /// service layer interface for "student" methods
    /// </summary>
    [ServiceContract]
    public interface IStudent
    {
        /// <summary>
        /// Get student info
        /// </summary>
        /// <param name="id">student id</param>
        /// <param name="errors">error list</param>
        /// <returns>student object</returns>
        [OperationContract]
        Student GetStudent(string id, ref List<string> errors);

        /// <summary>
        /// insert a new student
        /// </summary>
        /// <param name="student">student object</param>
        /// <param name="errors">error list</param>
        [OperationContract]
        void InsertStudent(Student student, ref List<string> errors);

        /// <summary>
        /// update student info
        /// </summary>
        /// <param name="student">student info</param>
        /// <param name="errors">error list</param>
        [OperationContract]
        void UpdateStudent(Student student, ref List<string> errors);

        /// <summary>
        /// delete an existing student
        /// </summary>
        /// <param name="id">student id</param>
        /// <param name="errors">error list</param>
        [OperationContract]
        void DeleteStudent(string id, ref List<string> errors);

        /// <summary>
        /// get a list of all students in the database
        /// </summary>
        /// <param name="errors">error list</param>
        /// <returns>list of students</returns>
        [OperationContract]
        List<Student> GetStudentList(ref List<string> errors);

        /// <summary>
        /// student enrolling in a schedule course
        /// </summary>
        /// <param name="student_id">student id</param>
        /// <param name="schedule_id">schedule id</param>
        /// <param name="errors">error list</param>
        [OperationContract]
        void EnrollSchedule(string student_id, int schedule_id, ref List<string> errors);

        /// <summary>
        /// student dropping a scheduled course
        /// </summary>
        /// <param name="student_id">student id</param>
        /// <param name="schedule_id">schedule id</param>
        /// <param name="errors">error list</param>
        [OperationContract]
        void DropEnrolledSchedule(string student_id, int schedule_id, ref List<string> errors);
    }
}
