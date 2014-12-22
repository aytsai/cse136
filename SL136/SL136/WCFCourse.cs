// <copyright file="WCFCourse.cs" company="University of California San Diego">
// University of California San Diego. CSE 136
// </copyright>

namespace SL136
{
    using System.Collections.Generic;
    using POCO;
    using Repository;
    using Service;

    /// <summary>
    /// service layer for "course" methods
    /// </summary>
    public class WCFCourse : ICourse
    {
        /// <summary>
        /// Get a list of available courses
        /// </summary>
        /// <param name="errors">error list</param>
        /// <returns>list of courses</returns>
        public List<Course> GetCourseList(ref List<string> errors)
        {
            var service = new CourseService(new CourseRepository());
            return service.GetCourseList(ref errors);
        }
    }
}
