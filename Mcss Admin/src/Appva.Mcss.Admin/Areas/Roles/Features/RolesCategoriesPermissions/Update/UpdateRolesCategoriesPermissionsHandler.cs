// <copyright file="UpdateRolesCategoriesPermissionsHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Roles.Roles.List
{
    #region Imports.

    using System.Collections.Generic;
    using System.Linq;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Areas.Roles.Models;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Models;
    using Appva.Mvc;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class UpdateRolesCategoriesPermissionsHandler : RequestHandler<Identity<UpdateRolesCategoriesPermissions>, UpdateRolesCategoriesPermissions>
    {
        #region Fields.

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
        /// Initializes a new instance of the <see cref="UpdateRolesCategoriesPermissionsHandler"/> class.
        /// </summary>
        /// <param name="roleService">The <see cref="IRoleService"/>.</param>
        /// <param name="persistence">The <see cref="IPersistenceContext"/>.</param>
        public UpdateRolesCategoriesPermissionsHandler(IRoleService roleService, IPersistenceContext persistence)
        {
            this.roleService = roleService;
            this.persistence = persistence;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override UpdateRolesCategoriesPermissions Handle(Identity<UpdateRolesCategoriesPermissions> message)
        {
            var role = this.roleService.Find(message.Id);
            var categories = this.persistence.QueryOver<ArticleCategory>()
                .Where(x => x.IsActive == true)
                    .OrderBy(x => x.Name).Asc
                        .List();

            return new UpdateRolesCategoriesPermissions
            {
                Id = message.Id,
                Categories = this.Merge(categories, role.ArticleCategories)
            };
        }

        #endregion

        #region Private methods.

        /// <inheritdoc />
        private IList<Tickable> Merge(IList<ArticleCategory> items, IList<ArticleCategory> selected)
        {
            var selections = selected.Select(x => x.Id).ToList();
            var permissions = items.Select(x => new Tickable
            {
                Id = x.Id,
                Label = x.Name
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