// <copyright file="UpdateRolesForm.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class UpdateRolesForm : Identity<ListAccount>
    {
        /// <summary>
        /// The roles available.
        /// </summary>
        [Display(Name = "Roller", ResourceType = typeof(Resources.Language))]
        public IList<SelectListItem> Roles
        {
            get;
            set;
        }

        /// <summary>
        /// The selected roles.
        /// </summary>
        public string[] SelectedRoles
        {
            get;
            set;
        }
    }
}