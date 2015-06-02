// <copyright file="OwinAuthenticationManager.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Security
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Mcss.Admin.Domain.Entities;

    #endregion

    /// <summary>
    /// Handles user account authentication.
    /// </summary>
    public interface IOwinAuthentication
    {
        /// <summary>
        /// Signs in a user account to the web application.
        /// </summary>
        /// <param name="account">The user account</param>
        /// <param name="isPersistent">Whether or not persistent cookies are enabled, defaults to false</param>
        void SignIn(Account account, bool isPersistent = false);

        /// <summary>
        /// Signs out the current user account from the web application.
        /// </summary>
        void SignOut();
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public abstract class OwinAuthenticationManager : IOwinAuthentication
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="OwinAuthenticationManager"/> class.
        /// </summary>
        public OwinAuthenticationManager()
        {
        }

        #endregion
    }
}