// <copyright file="UpdateRoleCategoryPermissionsHandler.cs" company="Appva AB">
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
    using System;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class UpdateRoleCategoryPermissionsHandler : RequestHandler<Identity<UpdateRoleCategoryPermissions>, UpdateRoleCategoryPermissions>
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

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateRoleCategoryPermissionsHandler"/> class.
        /// </summary>
        /// <param name="roleService">The <see cref="IRoleService"/>.</param>
        /// <param name="persistence">The <see cref="IPersistenceContext"/>.</param>
        public UpdateRoleCategoryPermissionsHandler(IRoleService roleService, IPersistenceContext persistence)
        {
            this.roleService = roleService;
            this.persistence = persistence;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override UpdateRoleCategoryPermissions Handle(Identity<UpdateRoleCategoryPermissions> message)
        {
            var role = this.roleService.Find(message.Id);
            var categories = this.persistence.QueryOver<Category>()
                    .OrderBy(x => x.Name).Asc
                        .List();

            return new UpdateRoleCategoryPermissions
            {
                Id = message.Id,
                Categories = this.Merge(categories, role.ArticleCategories),
                DeviceCategories = this.Merge(categories, role.DeviceArticleCategories)
            };
        }

        #endregion

        #region Private methods.

        /// <inheritdoc />
        private IList<Tickable> Merge(IList<Category> items, IList<Category> selected)
        {
            var selections = selected.Select(x => x.Id).ToList();
            var permissions = items.Select(x => new Tickable
            {
                Id = x.Id.ToString(),
                Label = x.Name,
                HelpText = x.IsActive == false ? "Listan är inaktiverad" : ""
            }).ToList();

            foreach (var permission in permissions)
            {
                if (selections.Contains(new Guid(permission.Id)))
                {
                    permission.IsSelected = true;
                }
            }

            return permissions;
        }

        #endregion
    }
}