// <copyright file="PractitionerRolesModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Appva.Cqrs;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class PractitionerRolesModel : IRequest<Guid>
    {
        #region Properties.

        /// <summary>
        /// The file id.
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// The imported roles.
        /// </summary>
        public IList<string> ImportedRoles
        {
            get;
            set;
        }

        /// <summary>
        /// A list of selected roles.
        /// </summary>
        public IList<Guid> SelectedRoles
        {
            get;
            set;
        }

        /// <summary>
        /// A list of <see cref="bool"/> indicating if HSA id is required.
        /// </summary>
        public IList<bool> SelectedHsaRequirements
        {
            get;
            set;
        }

        /// <summary>
        /// A collection of <see cref="PractitionerRoleViewModel"/>.
        /// </summary>
        public IEnumerable<PractitionerRoleViewModel> Roles
        {
            get;
            set;
        }

        #endregion
    }
}