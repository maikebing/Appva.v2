﻿// <copyright file="UserCredentialsModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Practitioner.Models
{
    #region Imports.

    using Appva.Mcss.Admin.Areas.Practitioner.Features.Accounts.GetCredentials;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class UserCredentialsModel : Credentials
    {
        /// <summary>
        /// The account id
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }   
    }
}