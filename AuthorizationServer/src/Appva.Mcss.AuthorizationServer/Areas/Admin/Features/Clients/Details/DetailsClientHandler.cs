// <copyright file="DetailsClientHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Models.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.AuthorizationServer.Domain.Entities;
    using Appva.Mcss.AuthorizationServer.Models;
    using Appva.Persistence;
    using NHibernate.Criterion;
    using NHibernate.Transform;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class DetailsClientGetHandler : PersistentRequestHandler<DetailsClientId, DetailsClient>
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="DetailsClientGetHandler"/> class.
        /// </summary>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public DetailsClientGetHandler(IPersistenceContext persistenceContext) 
            : base(persistenceContext)
        {
        }

        #endregion

        #region Overrides.

        /// <inheritdocs />
        public override DetailsClient Handle(DetailsClientId message)
        {
            /*DetailsClient detailsClient = null;
            TenantListModel tenantList = null;
            Client client = null;
            Tenant tenant = null;
            var result = this.Persistence.QueryOver<Client>()
                .Where(x => x.Id == message.Id)
                .Select(Projections.ProjectionList()
                    .Add(Projections.Property<Client>(x => x.IsActive).WithAlias(() => detailsClient.IsActive))
                    .Add(Projections.Property<Client>(x => x.Id).WithAlias(() => detailsClient.Id))
                    .Add(Projections.Property<Client>(x => x.Name).WithAlias(() => detailsClient.Name))
                    .Add(Projections.Property<Client>(x => x.Description).WithAlias(() => detailsClient.Description))
                    .Add(Projections.Property<Client>(x => x.Identifier).WithAlias(() => detailsClient.Identifier))
                    .Add(Projections.Property<Client>(x => x.Secret).WithAlias(() => detailsClient.Secret))
                    .Add(Projections.Property<Client>(x => x.Password).WithAlias(() => detailsClient.Password))
                    //.Add(Projections.Property<Client>(x => x.Grant).WithAlias(() => detailsClient.Grant))
                ).TransformUsing(Transformers.AliasToBean<DetailsClient>()).SingleOrDefault<DetailsClient>();
            /*var tenants = this.Persistence.QueryOver<Client>(() => client)
                .Where(x => x.Id == message.Id)
                .JoinQueryOver<Tenant>(x => x.Tenants, () => tenant)
                .Select(Projections.ProjectionList()
                    .Add(Projections.Property(() => tenant.Name).WithAlias(() => tenantList.Name))
                    .Add(Projections.Property(() => tenant.Description).WithAlias(() => tenantList.AvatarPath))
                ).TransformUsing(Transformers.AliasToBean<TenantListModel>()).List<TenantListModel>();
            result.Tenants = tenants;*/
            var client = this.Persistence.Get<Client>(message.Id);
            return new DetailsClient
            {
                Id = client.Id,
                IsActive = client.IsActive,
                Resource = new MetaData(client),
                Name = client.Name,
                Description = client.Description,
                Logotype = new Logotype(client.Image),
                Identifier = client.Identifier,
                Secret = client.Secret,
                Password = client.Password,
                AccessTokenLifetime = client.AccessTokenLifetime,
                RefreshTokenLifetime = client.RefreshTokenLifetime,
                RedirectionEndpoint = client.RedirectionEndpoint,
                IsPublic = client.Type.Equals(ClientType.Public)
            };
        }

        #endregion
    }
}