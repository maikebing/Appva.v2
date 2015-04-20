// <copyright file="RoleFormViewModel.cs" company="Appva AB">
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
    public class RoleFormViewModel
    {
        [DisplayName("Rollnamn")]
        [Required(ErrorMessage = "Rollnamn måste fyllas i.")]
        public string Name
        {
            get;
            set;
        }

        [DisplayName("Beskrivning")]
        [Required(ErrorMessage = "Beskrivning måste fyllas i.")]
        public string Description
        {
            get;
            set;
        }

        [DisplayName("Behörigheter")]
        public IList<SelectListItem> Permissions
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Behörigheter får ej vara tom.")]
        public string[] SelectedPermissions
        {
            get;
            set;
        }
    }
}