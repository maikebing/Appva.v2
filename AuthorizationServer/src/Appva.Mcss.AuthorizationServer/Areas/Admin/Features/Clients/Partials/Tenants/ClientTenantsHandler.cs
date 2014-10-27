// <copyright file="ClientTenantsHandler.cs" company="Appva AB">
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
    internal class ClientTenantsHandler : PersistentRequestHandler<ClientTenantsId, IList<ClientTenant>>
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientTenantsHandler"/> class.
        /// </summary>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public ClientTenantsHandler(IPersistenceContext persistenceContext)
            : base(persistenceContext)
        {
        }

        #endregion

        #region Overrides.

        /// <inheritdocs />
        public override IList<ClientTenant> Handle(ClientTenantsId message)
        {
            Client clientAlias = null;
            Tenant tenantAlias = null;
            ClientTenant model = null;
            return this.Persistence.QueryOver<Client>(() => clientAlias)
                .Where(x => x.Id == message.Id)
                .JoinQueryOver<Tenant>(x => x.Tenants, () => tenantAlias)
                .Select(Projections.ProjectionList()
                    .Add(Projections.Property(() => tenantAlias.Name).WithAlias(() => model.Name))
                    .Add(Projections.Property(() => tenantAlias.Description).WithAlias(() => model.Description))
                    .Add(Projections.Property(() => tenantAlias.Image).WithAlias(() => model.Logotype))
                )
                .TransformUsing(Transformers.AliasToBean<ClientTenant>())
                .List<ClientTenant>();
        }

        #endregion
    }
}