namespace IRepository
{
    using System.Collections.Generic;

    using POCO;

    public interface ICourseRepository
    {
        List<Course> GetCourseList(ref List<string> errors);

        void InsertPrerequisite(Prereq prereq, ref List<string> errors);

        void UpdatePrerequisite(Prereq prereq, ref List<string> errors);

        void DeletePrerequisite(int id, ref List<string> errors);

        Prereq ViewPrerequisite(int id, ref List<string> errors);
    }
}
