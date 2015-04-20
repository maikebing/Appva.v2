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

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class ForgotPassword
    {
        [Required(ErrorMessage = "E-post måste anges")]
        [Display(Name = "E-post")]
        //[Email(ErrorMessage = "E-postadress måste anges i korrekt format, t. ex. namn.efternamn@foretag.se.")]
        public string Email
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Personnummer måste anges")]
        //[UniqueIdentifier(ErrorMessage = "Personnummer måste anges")]
        [Display(Name = "Personnummer")]
        //[PlaceHolder("T.ex. 19010101-0001")]
        public string UniqueIdentifier
        {
            get;
            set;
        }

        public string Tenant
        {
            get;
            set;
        }
    }

}