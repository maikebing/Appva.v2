// <copyright file="AccountRolesViewModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Web.ViewModels
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class AccountRolesViewModel
    {
        [DisplayName("Roller")]
        public IList<SelectListItem> Roles
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Roller får ej vara tom.")]
        public string[] SelectedRoles
        {
            get;
            set;
        }
    }
}