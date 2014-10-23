// <copyright file="ListTenantsHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Models.Handlers
{
    #region Imports.

    using Appva.Mcss.AuthorizationServer.Domain.Entities;
    using Appva.Mcss.AuthorizationServer.Infrastructure;
    using Appva.Mcss.AuthorizationServer.Models;
    using Appva.Persistence;
    using NHibernate.Criterion;
    using NHibernate.Transform;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class ListTenantsHandler : PersistentRequestHandler<PageableQueryParams<Pageable<ListTenants>>, Pageable<ListTenants>>
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="ListTenantsHandler"/> class.
        /// </summary>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public ListTenantsHandler(IPersistenceContext persistenceContext)
            : base(persistenceContext)
        {
        }

        #endregion

        #region Overrides.

        /// <inheritdocs />
        public override Pageable<ListTenants> Handle(PageableQueryParams<Pageable<ListTenants>> message)
        {
            ListTenants tenants = null;
            var query = this.Persistence.QueryOver<Tenant>()
                .Select(Projections.ProjectionList()
                    .Add(Projections.Property<Tenant>(x => x.Id).WithAlias(() => tenants.Id))
                    .Add(Projections.Property<Tenant>(x => x.Slug.Name).WithAlias(() => tenants.Slug))
                    .Add(Projections.Property<Tenant>(x => x.Name).WithAlias(() => tenants.Name))
                    .Add(Projections.Property<Tenant>(x => x.Description).WithAlias(() => tenants.Description))
                    .Add(Projections.Property<Tenant>(x => x.Image).WithAlias(() => tenants.Logotype))
                )
                .TransformUsing(Transformers.AliasToBean<ListTenants>());
            return PageableHelper.ToPageable<Tenant, ListTenants>(message, query);
        }

        #endregion
    }
}