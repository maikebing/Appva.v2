// <copyright file="UpdateRoleRoleHandler.cs" company="Appva AB">
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
    using Appva.Mcss.Admin.Areas.Roles.Models;
    using Appva.Mcss.Admin.Areas.Roles.Roles.Create;
    using Appva.Mcss.Admin.Areas.Roles.Roles.Update;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Models;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Mvc;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class UpdateRoleRoleHandler : RequestHandler<Identity<UpdateRoleRole>, UpdateRoleRole>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IRoleService"/>.
        /// </summary>
        private readonly IRoleService roleService;

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistence;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateRoleRoleHandler"/> class.
        /// </summary>
        /// <param name="roleService">The <see cref="IRoleService"/>.</param>
        /// <param name="persistence">The <see cref="IPersistenceContext"/>.</param>
        public UpdateRoleRoleHandler(IRoleService roleService, IPersistenceContext persistence)
        {
            this.roleService = roleService;
            this.persistence = persistence;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override UpdateRoleRole Handle(Identity<UpdateRoleRole> message)
        {
            var role  = this.roleService.Find(message.Id);
            var roles = this.persistence.QueryOver<Role>()
                .Where(x => x.IsActive == true)
                .OrderBy(x => x.Name).Asc.ThenBy(x => x.Weight).Asc
                .List();
            return new UpdateRoleRole
            {
                AdminRoles = this.Merge(roles.Where(x => x.IsVisible == false).ToList(), role.Roles),
                UserRoles  = this.Merge(roles.Where(x => x.IsVisible == true).ToList(), role.Roles),
                RoleName   = role.Name
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
        private IList<Tickable> Merge(IList<Role> items, IList<Role> selected)
        {
            var selections  = selected.Select(x => x.Id).ToList();
            var permissions = items.Select(x => new Tickable
            {
                Id    = x.Id,
                Label = x.Name,
            }).ToList();
            foreach (var permission in permissions)
            {
                if (selections.Contains(permission.Id))
                {
                    permission.IsSelected = true;
                }
            }
            return permissions.OrderBy(x => x.Label).ToList();
        }

        #endregion
    }
}