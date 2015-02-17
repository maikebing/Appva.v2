// <copyright file="ListPermissionsHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Models.Handlers
{
    #region Imports.

    using System.Collections.Generic;
    using Appva.Mcss.AuthorizationServer.Domain.Entities;
    using Appva.Persistence;
    using NHibernate.Criterion;
    using NHibernate.Transform;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class ListPermissionsHandler : PersistentRequestHandler<ListPermission, IList<ListPermission>>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ListPermissionsHandler"/> class.
        /// </summary>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public ListPermissionsHandler(IPersistenceContext persistenceContext) 
            : base(persistenceContext)
        {
        }

        #endregion

        #region Overrides.

        /// <inheritdocs />
        public override IList<ListPermission> Handle(ListPermission message)
        {
            ListPermission list = null;
            var permissions = this.Persistence.QueryOver<Permission>()
                .OrderBy(x => x.Context).Asc
                .ThenBy(x => x.Name).Asc
                .Select(Projections.ProjectionList()
                    .Add(Projections.Property<Permission>(x => x.Id).WithAlias(() => list.Id))
                    .Add(Projections.Property<Permission>(x => x.Name).WithAlias(() => list.Name))
                    .Add(Projections.Property<Permission>(x => x.Description).WithAlias(() => list.Description))
                    //.Add(Projections.Property<Permission>(x => x.Context.Name).WithAlias(() => list.Context))
                )
                .TransformUsing(Transformers.AliasToBean<ListPermission>())
                .List<ListPermission>();
            return permissions;
        }

        #endregion
    }
}