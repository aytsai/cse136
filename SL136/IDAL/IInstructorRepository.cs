namespace IRepository
{
    using System.Collections.Generic;

    using POCO;

    public interface IInstructorRepository
    {
        void InsertInstructor(Instructor instr, ref List<string> errors);

        void UpdateInstructor(Instructor instr, ref List<string> errors);

        void DeleteInstructor(int id, ref List<string> errors);

        Instructor ViewInstructor(int id, ref List<string> errors);

        List<Instructor> ViewAllInstructors(ref List<string> errors);

        void ViewEnrollmentNumber(int id, ref List<string> errors);

        void UpdateGrade(Enrollment enroll, ref List<string> errors);
    }
}
