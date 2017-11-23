// <copyright file="UpdateRolesForm.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using Appva.Mcss.Web.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
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
        [DisplayName("Roller")]
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

        /// <summary>
        /// The taxon which the user has permission to
        /// </summary>
        public Guid? Taxon
        {
            get;
            set;
        }

        /// <summary>
        /// Available taxons
        /// </summary>
        public IList<TaxonViewModel> Taxons
        {
            get;
            set;
        }

        /// <summary>
        /// If the current user is allowed to update the account taxon
        /// Eg. If current users location is child of accounts current taxon
        /// </summary>
        public bool CanUpdateTaxonPermission
        {
            get;
            set;
        }
    }
}