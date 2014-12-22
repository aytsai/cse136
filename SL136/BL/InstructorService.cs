namespace Service
{
    using System;
    using System.Collections.Generic;
    using IRepository;
    using POCO;

    public class InstructorService
    {
        private readonly IInstructorRepository repository;

        public InstructorService(IInstructorRepository repository)
        {
            this.repository = repository;
        }

        public void InsertInstructor(Instructor instr, ref List<string> errors)
        {
            if (instr == null)
            {
                errors.Add("Intructor cannot be null");
                throw new ArgumentException();
            }

            this.repository.InsertInstructor(instr, ref errors);
        }

        public void UpdateInstructor(Instructor instr, ref List<string> errors)
        {
            if (instr == null)
            {
                errors.Add("Intructor cannot be null");
                throw new ArgumentException();
            }

            this.repository.UpdateInstructor(instr, ref errors);
        }

        public void DeleteInstructor(int id, ref List<string> errors)
        {
            if (id == 0)
            {
                errors.Add("Invalid instructor id");
                throw new ArgumentException();
            }

            this.repository.DeleteInstructor(id, ref errors);
        }

        public Instructor ViewInstructor(int id, ref List<string> errors)
        {
            if (id == 0)
            {
                errors.Add("Invalid instructor id");
                throw new ArgumentException();
            }

            return this.repository.ViewInstructor(id, ref errors);
        }

        public List<Instructor> ViewAllInstructors(ref List<string> errors)
        {
            return this.repository.ViewAllInstructors(ref errors);
        }

        public void ViewEnrollmentNumber(int id, ref List<string> errors)
        {
            this.repository.ViewEnrollmentNumber(id, ref errors);
        }

        public void UpdateGrade(Enrollment enroll, ref List<string> errors)
        {
            if (enroll == null)
            {
                errors.Add("Enrollment cannot be null");
                throw new ArgumentException();
            }

            this.repository.UpdateGrade(enroll, ref errors);
        }
    }
}
