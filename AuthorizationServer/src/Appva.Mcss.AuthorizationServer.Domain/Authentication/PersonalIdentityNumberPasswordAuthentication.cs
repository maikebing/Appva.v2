// <copyright file="PersonalIdentityNumberPasswordAuthentication.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>

using Appva.Mcss.AuthorizationServer.Domain.Entities;

namespace Appva.Mcss.AuthorizationServer.Domain.Authentication
{
    #region Imports.

    using System;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// FIXME: Real implementation!
    /// </summary>
    public sealed class PersonalIdentityNumberPasswordAuthentication : IAuthentication
    {
        #region Public Properties.

        public string Password
        {
            get; set; 
        }

        public DateTime LastLoginAt
        {
            get;
            set;
        }

        public bool ForcePasswordChange
        {
            get;
            set;
        }
        #endregion

    }
}