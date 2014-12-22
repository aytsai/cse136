// <copyright file="ICourse.cs" company="University of California San Diego">
// University of California San Diego. CSE 136
// </copyright>

namespace SL136
{
    using System.Collections.Generic;
    using System.ServiceModel;
    using POCO;

    /// <summary>
    /// service layer interface for "authentication" method
    /// </summary>
    [ServiceContract]
    public interface ICourse
    {
        /// <summary>
        /// Get all course list
        /// </summary>
        /// <param name="errors">error list</param>
        /// <returns>list of courses</returns>
        [OperationContract]
        List<Course> GetCourseList(ref List<string> errors);
    }
}
