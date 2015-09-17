// <copyright file="AbstractPassword.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using System.ComponentModel.DataAnnotations;
    using DataAnnotationsExtensions;

    #endregion

    /// <summary>
    /// Abstract base class for change password post requests.
    /// </summary>
    public abstract class AbstractPassword
    {
        /// <summary>
        /// The new password.
        /// </summary>
        [Required(ErrorMessage = "Nytt lösenord måste fyllas i.")]
        [MinLength(8, ErrorMessage = "Nytt lösenord måste vara minst 8 tecken långt.")]
        [MaxLength(255, ErrorMessage = "Nytt lösenord måste vara maximalt 255 tecken långt.")]
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