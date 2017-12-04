// <copyright file="UpdateRoleCategoryPermissionsPublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Roles.Handlers
{
    #region Imports.

    using System.Linq;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Areas.Roles.Models;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Persistence;
    using System;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class UpdateRoleCategoryPermissionsPublisher : RequestHandler<UpdateRoleCategoryPermissions, bool>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistence;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateRoleCategoryPermissionsPublisher"/> class.
        /// </summary>
        /// <param name="persistence">The <see cref="IPersistenceContext"/>.</param>
        public UpdateRoleCategoryPermissionsPublisher(IPersistenceContext persistence)
        {
            this.persistence = persistence;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override bool Handle(UpdateRoleCategoryPermissions message)
        {
            var categories = this.persistence.QueryOver<Category>()
                .AndRestrictionOn(x => x.Id)
                    .IsIn(message.Categories.Where(x => x.IsSelected).Select(x => new Guid(x.Id)).ToArray())
                        .List();

            var deviceCategories = this.persistence.QueryOver<Category>()
                .AndRestrictionOn(x => x.Id)
                    .IsIn(message.DeviceCategories.Where(x => x.IsSelected).Select(x => new Guid(x.Id)).ToArray())
                        .List();

            var role = this.persistence.Get<Role>(message.Id);
            role.ArticleCategories = categories;
            role.DeviceArticleCategories = deviceCategories;
            this.persistence.Update(role);

            return true;
        }

        #endregion
    }
}