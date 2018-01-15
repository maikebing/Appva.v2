// <copyright file="PractitionerRoleViewModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using System.Collections.Generic;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class PractitionerRoleViewModel
    {
        #region Properties.

        /// <summary>
        /// The role id.
        /// </summary>
        public string Id
        {
            get;
            set;
        }

        /// <summary>
        /// The label.
        /// </summary>
        public string Label
        {
            get;
            set;
        }

        /// <summary>
        /// The roles.
        /// </summary>
        public IList<SelectListItem> Roles
        {
            get;
            set;
        }

        #endregion
    }
}