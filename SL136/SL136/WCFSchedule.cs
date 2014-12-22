// <copyright file="WCFSchedule.cs" company="University of California San Diego">
// University of California San Diego. CSE 136
// </copyright>

namespace SL136
{
    using System.Collections.Generic;
    using POCO;
    using Repository;
    using Service;

    /// <summary>
    /// service layer for "schedule" methods
    /// </summary>
    public class WCFSchedule : ISchedule
    {
        /// <summary>
        /// Get a list of schedule with filtering
        /// </summary>
        /// <param name="year">year selected</param>
        /// <param name="quarter">quarter selected</param>
        /// <param name="errors">error list</param>
        /// <returns>list of schedule</returns>
        public List<Schedule> GetScheduleList(string year, string quarter, ref List<string> errors)
        {
            var service = new ScheduleService(new ScheduleRepository());
            return service.GetScheduleList(year, quarter, ref errors);
        }
    }
}
