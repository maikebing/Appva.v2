// <copyright file="ChangePassword.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Features.Accounts.ChangePassword
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
using Appva.Cqrs;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class ChangePassword : IRequest<ChangePassword>
    {
        [DisplayName("Nuvarande lösenord")]
        [Required(ErrorMessage = "Nuvarande lösenord måste fyllas i.")]
        [DataType(DataType.Password)]
        public string CurrentPassword
        {
            get;
            set;
        }

        [DisplayName("Nytt lösenord")]
        [Required(ErrorMessage = "Nytt lösenord måste fyllas i.")]
        [MinLength(8, ErrorMessage = "Nytt lösenord måste vara minst 8 tecken långt.")]
        [DataType(DataType.Password)]
        public string NewPassword
        {
            get;
            set;
        }

        [DisplayName("Upprepa nytt lösenord")]
        [Required(ErrorMessage = "Upprepa lösenord måste fyllas i.")]
        //[EqualTo("NewPassword", ErrorMessage = "Lösenord måste stämma överens.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword
        {
            get;
            set;
        }
    }
}