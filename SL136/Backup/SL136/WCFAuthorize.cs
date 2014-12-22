// <copyright file="WCFAuthorize.cs" company="University of California San Diego">
// University of California San Diego. CSE 136
// </copyright>

namespace SL136
{
    using System.Collections.Generic;
    using IRepository;
    using POCO;
    using Repository;
    using Service;

    /// <summary>
    /// service layer for "schedule" methods
    /// </summary>
    public class WCFAuthorize : IAuthorize
    {
        /// <summary>
        /// authenticate based on the email and password
        /// </summary>
        /// <param name="email">email address</param>
        /// <param name="password">password to authenticate</param>
        /// <param name="errors">error list</param>
        /// <returns>logon object</returns>
        public Logon Authenticate(string email, string password, ref List<string> errors)
        {
            IAuthorizeRepository authorize = new AuthorizeRepository();
            return new AuthorizeService(authorize).Authenticate(email, password, ref errors);
        }
    }
}
