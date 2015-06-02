// <copyright file="ForgotPassword.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Features.Authentication.Forgot
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Appva.Mcss.Admin.Domain.Entities;
    using DataAnnotationsExtensions;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class ForgotPassword
    {
        /// <summary>
        /// The E-mail address.
        /// </summary>
        [Required(ErrorMessage = "E-post måste anges")]
        [Display(Name = "E-post")]
        [Email(ErrorMessage = "E-postadress måste anges i korrekt format, t. ex. namn.efternamn@foretag.se.")]
        public string Email
        {
            get;
            set;
        }

        /// <summary>
        /// The personal identity number.
        /// </summary>
        [Required(ErrorMessage = "Personnummer måste anges")]
        [Appva.Mvc.PersonalIdentityNumber(ErrorMessage = "Personnummer måste anges")]
        [Display(Name = "Personnummer")]
        [Appva.Mvc.PlaceHolder("T.ex. 19010101-0001")]
        public PersonalIdentityNumber PersonalIdentityNumber
        {
            get;
            set;
        }

        /// <summary>
        /// The current tenant.
        /// </summary>
        public string Tenant
        {
            get;
            set;
        }
    }
}