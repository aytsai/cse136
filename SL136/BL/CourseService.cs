namespace Service
{
    using System;
    using System.Collections.Generic;

    using IRepository;

    using POCO;

    public class CourseService
    {
        private readonly ICourseRepository repository;

        public CourseService(ICourseRepository repository)
        {
            this.repository = repository;
        }

        public List<Course> GetCourseList(ref List<string> errors)
        {
            return this.repository.GetCourseList(ref errors);
        }

        public void InsertPrerequisite(Prereq prereq, ref List<string> errors)
        {
            if (prereq == null)
            {
                errors.Add("Courses cannot be null");
                throw new ArgumentException();
            }

            this.repository.InsertPrerequisite(prereq, ref errors);
        }

        public void UpdatePrerequisite(Prereq prereq, ref List<string> errors)
        {
            if (prereq == null)
            {
                errors.Add("Courses cannot be null");
                throw new ArgumentException();
            }

            this.repository.UpdatePrerequisite(prereq, ref errors);
        }

        public void DeletePrerequisite(int id, ref List<string> errors)
        {
            if (id == 0)
            {
                errors.Add("Invalid prereq id");
                throw new ArgumentException();
            }

            this.repository.DeletePrerequisite(id, ref errors);
        }

        public Prereq ViewPrerequisite(int id, ref List<string> errors)
        {
            if (id == 0)
            {
                errors.Add("Invalid prereq id");
                throw new ArgumentException();
            }

            return this.repository.ViewPrerequisite(id, ref errors);
        }
    }
}
