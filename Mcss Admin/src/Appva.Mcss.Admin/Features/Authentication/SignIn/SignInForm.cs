// <copyright file="SignInForm.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Appva.Cqrs;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class SignInForm : IRequest<bool>
    {
        /// <summary>
        /// The user name.
        /// </summary>
        [Required]
        [StringLength(255)]
        public string UserName
        {
            get;
            set;
        }

        /// <summary>
        /// The password credentials.
        /// </summary>
        [Required]
        [StringLength(255)]
        public string Password
        {
            get;
            set;
        }

        /// <summary>
        /// The return URL.
        /// </summary>
        public string ReturnUrl
        {
            get;
            set;
        }
    }
}