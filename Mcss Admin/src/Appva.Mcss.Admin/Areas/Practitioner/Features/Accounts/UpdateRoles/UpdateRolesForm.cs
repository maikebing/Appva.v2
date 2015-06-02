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
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using Appva.Cqrs;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class UpdateRolesForm : Identity<ListAccount>
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