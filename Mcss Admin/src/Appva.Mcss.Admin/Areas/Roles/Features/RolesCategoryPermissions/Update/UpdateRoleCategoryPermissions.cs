// <copyright file="UpdateRoleCategoryPermissions.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Roles.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Appva.Cqrs;
    using Appva.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class UpdateRoleCategoryPermissions : IRequest<bool>
    {
        #region Properties.

        /// <summary>
        /// The role <see cref="Guid"/>.
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// A list of categories the role can view in admin.
        /// </summary>
        public IList<Tickable> Categories
        {
            get;
            set;
        }

        /// <summary>
        /// A list of categories the role can view in a device.
        /// </summary>
        public IList<Tickable> DeviceCategories
        {
            get;
            set;
        }

        #endregion
    }
}