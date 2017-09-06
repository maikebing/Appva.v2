// <copyright file="UpdateRoleResourcePermissions.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Roles.Models
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Appva.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class UpdateRoleResourcePermissions : IRequest<Parameterless<IList<Role>>>
    {
        #region Properties.

        /// <summary>
        /// The role id
        /// </summary>
        public Guid RoleId
        {
            get;
            set;
        }

        /// <summary>
        /// The role-name
        /// </summary>
        public string Role
        {
            get;
            set;
        }

        /// <summary>
        /// The permissions
        /// </summary>
        public Dictionary<string, List<Tickable>> Permissions
        {
            get;
            set;
        }

        #endregion
    }
}