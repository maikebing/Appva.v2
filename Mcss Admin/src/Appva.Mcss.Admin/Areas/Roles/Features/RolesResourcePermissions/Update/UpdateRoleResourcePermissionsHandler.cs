// <copyright file="UpdateRoleResourcePermissions.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Roles.Handlers
{
    #region Imports.

    using Appva.Core.Extensions;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Areas.Roles.Models;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Models;
    using Appva.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class UpdateRoleResourcePermissionsHandler : RequestHandler<Identity<UpdateRoleResourcePermissions>, UpdateRoleResourcePermissions> 
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IPermissionService"/>.
        /// </summary>
        private readonly IPermissionService permissions;

        /// <summary>
        /// The <see cref="IRoleService"/>.
        /// </summary>
        private readonly IRoleService roleService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateRoleResourcePermissionsHandler"/> class.
        /// </summary>
        /// <param name="permissions"></param>
        public UpdateRoleResourcePermissionsHandler(
            IPermissionService permissions,
            IRoleService roleService)
        {
            this.permissions = permissions;
            this.roleService = roleService;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override UpdateRoleResourcePermissions Handle(Identity<UpdateRoleResourcePermissions> message)
        {
            var role = this.roleService.Find(message.Id);
            var selected = this.permissions.ListByRoles(new List<Role>() { role });
            var schema = Permissions.DeviceSchema;
            var permissions = this.permissions.List(schema)
                .GroupBy(
                    x =>  x.Resource.Replace(schema, "").Split('/').First(y => y.IsNotEmpty()),
                    x => new Tickable
                    {
                        Id = x.Id.ToString(),
                        Label = x.Name,
                        HelpText = x.Description,
                        IsSelected = selected.Contains(x)
                    })
                .ToDictionary(x => x.Key, x => x.ToList());

            return new UpdateRoleResourcePermissions
            {
                RoleId      = message.Id,
                Role        = role.Name,
                Permissions = permissions
            };
        }

        #endregion
    }
}