// <copyright file="IAuthorize.cs" company="University of California San Diego">
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
    public interface IAuthorize
    {
        /// <summary>
        /// authenticate based on email and password
        /// </summary>
        /// <param name="email">email address</param>
        /// <param name="password">password data</param>
        /// <param name="errors">error list</param>
        /// <returns>logon object</returns>
        [OperationContract]
        Logon Authenticate(string email, string password, ref List<string> errors);
    }
}
