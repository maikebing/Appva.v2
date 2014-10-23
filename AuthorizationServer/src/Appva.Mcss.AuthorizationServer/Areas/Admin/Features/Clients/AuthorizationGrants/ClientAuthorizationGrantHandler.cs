// <copyright file="ClientAuthorizationGrantHandler.cs" company="Appva AB">
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
    internal class ClientAuthorizationGrantHandler : PersistentRequestHandler<ClientAuthorizationGrantsId, IList<ClientAuthorizationGrant>>
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientAuthorizationGrantHandler"/> class.
        /// </summary>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public ClientAuthorizationGrantHandler(IPersistenceContext persistenceContext)
            : base(persistenceContext)
        {
        }

        #endregion

        #region Overrides.

        /// <inheritdocs />
        public override IList<ClientAuthorizationGrant> Handle(ClientAuthorizationGrantsId message)
        {
            Client clientAlias = null;
            AuthorizationGrant authorizationGrantAlias = null;
            ClientAuthorizationGrant model = null;
            return this.Persistence.QueryOver<Client>(() => clientAlias)
                .Where(x => x.Id == message.Id)
                .JoinQueryOver<AuthorizationGrant>(x => x.AuthorizationGrants, () => authorizationGrantAlias)
                .Select(Projections.ProjectionList()
                    .Add(Projections.Property(() => authorizationGrantAlias.Key).WithAlias(() => model.Name))
                    .Add(Projections.Property(() => authorizationGrantAlias.Description).WithAlias(() => model.Description))
                )
                .TransformUsing(Transformers.AliasToBean<ClientAuthorizationGrant>())
                .List<ClientAuthorizationGrant>();
        }

        #endregion
    }
}