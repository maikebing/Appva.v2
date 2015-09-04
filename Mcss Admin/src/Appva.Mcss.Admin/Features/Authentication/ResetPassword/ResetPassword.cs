// <copyright file="ResetPassword.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using System.ComponentModel.DataAnnotations;
    using Appva.Cqrs;
    using DataAnnotationsExtensions;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ResetPassword : IRequest<bool>
    {
        /// <summary>
        /// The new password.
        /// </summary>
        [Required(ErrorMessage = "Nytt lösenord måste fyllas i.")]
        [MinLength(10, ErrorMessage = "Nytt lösenord måste vara minst 10 tecken långt.")]
        public string NewPassword
        {
            get;
            set;
        }

        /// <summary>
        /// The new password confirmation.
        /// </summary>
        [Required(ErrorMessage = "Upprepa lösenord måste fyllas i.")]
        [EqualTo("NewPassword", ErrorMessage = "Nytt lösenord måste stämma överens med upprepat lösenord.")]
        public string ConfirmPassword
        {
            get;
            set;
        }
    }
}