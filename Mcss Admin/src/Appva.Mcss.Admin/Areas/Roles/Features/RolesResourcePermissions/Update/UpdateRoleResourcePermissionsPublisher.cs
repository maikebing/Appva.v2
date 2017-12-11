// <copyright file="UpdateRoleResourcePermissionsPublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Roles.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Areas.Roles.Models;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class UpdateRoleResourcePermissionsPublisher : RequestHandler<UpdateRoleResourcePermissions, Parameterless<IList<Role>>>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IRoleService"/>
        /// </summary>
        private readonly IRoleService roleService;

        /// <summary>
        /// The <see cref="IPermissionService"/>
        /// </summary>
        private readonly IPermissionService permissions;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateRoleResourcePermissionsPublisher"/> class.
        /// </summary>
        public UpdateRoleResourcePermissionsPublisher(
            IRoleService roleService,
            IPermissionService permissions)
        {
            this.roleService = roleService;
            this.permissions = permissions;
        }

        #endregion

        #region RequestHandler overrides

        /// <inheritdoc />
        public override Parameterless<IList<Role>> Handle(UpdateRoleResourcePermissions message)
        {
            var role = this.roleService.Find(message.RoleId);

            var permissionList = message.Permissions.SelectMany(x => x.Value);
            var unseleceted = this.permissions.ListAllIn(permissionList.Where(x => x.IsSelected == false).Select(x => new Guid(x.Id)).ToArray());
            var selected    = this.permissions.ListAllIn(permissionList.Where(x => x.IsSelected == true).Select(x => new Guid(x.Id)).ToArray());

            foreach (var u in unseleceted)
            {
                role.Permissions.Remove(u);
            }

            foreach (var s in selected)
            {
                if (!role.Permissions.Contains(s))
                {
                    role.Permissions.Add(s);
                }
            }

            this.roleService.Update(role);
            return new Parameterless<IList<Role>>();
        }

        #endregion

        
    }
}