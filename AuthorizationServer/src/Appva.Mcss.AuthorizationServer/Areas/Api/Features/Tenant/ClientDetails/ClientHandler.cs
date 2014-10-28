// <copyright file="ClientHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Api.Models.Handlers
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Appva.Cqrs;
    using Appva.Mcss.AuthorizationServer.Domain.Entities;
    using Appva.Mcss.AuthorizationServer.Models.Handlers;
    using Appva.Persistence;
    using NHibernate.Transform;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class ClientHandler : PersistentRequestHandler<TenantIdForClient, ClientModel>
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientHandler"/> class.
        /// </summary>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public ClientHandler(IPersistenceContext persistenceContext)
            : base(persistenceContext)
        {
        }

        #endregion

        #region Overrides.

        /// <inheritdocs />
        public override ClientModel Handle(TenantIdForClient message)
        {
            var clients = this.Persistence.QueryOver<Client>()
                .Where(x => x.IsActive == true)
                .Where(x => x.IsGlobal != true)
                .JoinQueryOver<Tenant>(x => x.Tenants)
                    .Where(x => x.Id == message.Id)
                .TransformUsing(Transformers.DistinctRootEntity)
                .List();
            var client = clients.FirstOrDefault();
            return new ClientModel
            {
                Id = client.Id,
                Slug = client.Slug.Name,
                Identifier = client.Identifier,
                Secret = client.Secret,
                Password = client.Password,
                Name = client.Name,
                Description = client.Description,
                RedirectionEndpoint = client.RedirectionEndpoint,
                Logotype = new Appva.Mcss.AuthorizationServer.Models.Logotype(client.Image)            
            };
        }

        #endregion
    }
}