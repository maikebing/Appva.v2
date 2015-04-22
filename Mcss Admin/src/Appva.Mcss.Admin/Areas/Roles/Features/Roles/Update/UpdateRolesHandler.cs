// <copyright file="CreateRolesHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Roles.Roles.List
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Areas.Roles.Roles.Create;
    using Appva.Mcss.Admin.Areas.Roles.Roles.Update;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Models;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Mvc.Html.Models;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class UpdateRolesHandler : RequestHandler<Identity<UpdateRole>, UpdateRole>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IRoleService"/>.
        /// </summary>
        private readonly IRoleService roleService;

        /// <summary>
        /// The <see cref="IPermissionService"/>.
        /// </summary>
        private readonly IPermissionService permissionService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateRolesHandler"/> class.
        /// </summary>
        public UpdateRolesHandler(IRoleService roleService, IPermissionService permissionService)
        {
            this.roleService = roleService;
            this.permissionService = permissionService;
        }

        #endregion

        #region RequestHandler<Id<UpdateRole>, UpdateRole> Overrides.

        /// <inheritdoc />
        public override UpdateRole Handle(Identity<UpdateRole> message)
        {
            var role = this.roleService.Find(message.Id);
            var permissions = this.permissionService.List();
            return new UpdateRole
            {
                Name = role.Name,
                Description = role.Description,
                Permissions = this.Merge(permissions, role.Permissions)
            };
        }

        #endregion

        #region Private Methods.

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <param name="selected"></param>
        /// <returns></returns>
        private IList<Tickable> Merge(IList<Permission> items, IList<Permission> selected)
        {
            var selections = selected.Select(x => x.Id).ToList();
            var permissions = items.Select(x => new Tickable
            {
                Id = x.Id,
                Label = x.Name,
                HelpText = x.Description
            }).ToList();
            foreach (var permission in permissions)
            {
                if (selections.Contains(permission.Id))
                {
                    permission.IsSelected = true;
                }
            }
            return permissions;
        }

        #endregion
    }
}