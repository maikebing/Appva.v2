// <copyright file="DetailsClientHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.AuthorizationServer.Models.Handlers
{
    #region Imports.

    using Appva.Mcss.AuthorizationServer.Domain.Entities;
    using Appva.Mcss.AuthorizationServer.Models;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class DetailsClientGetHandler 
        : PersistentRequestHandler<Id<DetailsClient>, DetailsClient>
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
        public override DetailsClient Handle(Id<DetailsClient> message)
        {
            var client = this.Persistence.Get<Client>(message.Id);
            return new DetailsClient
            {
                Id = client.Id,
                Slug = client.Slug.Name,
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