// <copyright file="SignIn.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Features.Shared.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class SignIn
    {
        [Required]
        [StringLength(50)]
        [Display(Name = "Användarnamn")]
        public string UserName
        {
            get;
            set;
        }

        [Required]
        [StringLength(50)]
        [Display(Name = "Lösenord")]
        public string Password
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