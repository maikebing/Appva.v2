// <copyright file="ClientScopesHandler.cs" company="Appva AB">
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
    internal class ClientScopesHandler : PersistentRequestHandler<ClientScopesId, IList<ClientScope>>
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientScopesHandler"/> class.
        /// </summary>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public ClientScopesHandler(IPersistenceContext persistenceContext)
            : base(persistenceContext)
        {
        }

        #endregion

        #region Overrides.

        /// <inheritdocs />
        public override IList<ClientScope> Handle(ClientScopesId message)
        {
            Client clientAlias = null;
            Scope scopeAlias = null;
            ClientScope model = null;
            return this.Persistence.QueryOver<Client>(() => clientAlias)
                .Where(x => x.Id == message.Id)
                .JoinQueryOver<Scope>(x => x.Scopes, () => scopeAlias)
                .Select(Projections.ProjectionList()
                    .Add(Projections.Property(() => scopeAlias.Name).WithAlias(() => model.Name))
                    .Add(Projections.Property(() => scopeAlias.Description).WithAlias(() => model.Description))
                )
                .TransformUsing(Transformers.AliasToBean<ClientScope>())
                .List<ClientScope>();
        }

        #endregion
    }
}