// <copyright file="ISchedule.cs" company="University of California San Diego">
// University of California San Diego. CSE 136
// </copyright>

namespace SL136
{
    using System.Collections.Generic;
    using System.ServiceModel;
    using POCO;

    /// <summary>
    /// service layer interface for "schedule" methods
    /// </summary>
    [ServiceContract]
    public interface ISchedule
    {
        /// <summary>
        /// Get schedule list
        /// </summary>
        /// <param name="year">selected year</param>
        /// <param name="quarter">selected quarter</param>
        /// <param name="errors">error list</param>
        /// <returns>list of schedule</returns>
        [OperationContract]
        List<Schedule> GetScheduleList(string year, string quarter, ref List<string> errors);
    }
}
