// <copyright file="UpdateRolesCategoriesPermissionsPublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Roles.Handlers
{
    #region Imports.

    using System.Collections.Generic;
    using System.Linq;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Areas.Roles.Models;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class UpdateRolesCategoriesPermissionsPublisher : RequestHandler<UpdateRolesCategoriesPermissions, bool>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistence;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateRolesCategoriesPermissionsPublisher"/> class.
        /// </summary>
        public UpdateRolesCategoriesPermissionsPublisher(IPersistenceContext persistence)
        {
            this.persistence = persistence;
        }

        #endregion

        #region RequestHandler overrides

        /// <inheritdoc />
        public override bool Handle(UpdateRolesCategoriesPermissions message)
        {
            var categories = this.persistence.QueryOver<ArticleCategory>()
                .AndRestrictionOn(x => x.Id)
                    .IsIn(message.Categories.Where(x => x.IsSelected).Select(x => x.Id).ToArray())
                        .List();

            var role = this.persistence.Get<Role>(message.Id);
            role.ArticleCategories = categories;
            this.persistence.Update(role);

            return true;
        }

        #endregion
    }
}