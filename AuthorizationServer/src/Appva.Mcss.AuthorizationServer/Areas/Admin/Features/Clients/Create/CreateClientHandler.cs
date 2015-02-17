// <copyright file="CreateClientHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Models.Handlers
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Core.Extensions;
    using Appva.Cryptography;
    using Appva.Mcss.AuthorizationServer.Domain.Entities;
    using Appva.Mcss.AuthorizationServer.Models;
    using Appva.Mvc.Html.Models;
    using Appva.Mvc.Imaging;
    using Appva.Persistence;
    using NHibernate.Criterion;
    using NHibernate.Transform;
    using Validation;

    #endregion

    #region Get Request Handler.

    /// <summary>
    /// Handler for create client GET request.
    /// </summary>
    internal sealed class CreateClientGetHandler : PersistentRequestHandler<NoParameter<CreateClient>, CreateClient>
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateClientGetHandler"/> class.
        /// </summary>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public CreateClientGetHandler(IPersistenceContext persistenceContext)
            : base(persistenceContext)
        {
        }

        #endregion

        #region Overrides.

        /// <inheritdocs />
        public override CreateClient Handle(NoParameter<CreateClient> message)
        {
            Tickable tickable = null;
            var tenants = this.Persistence.QueryOver<Tenant>()
                .Select(Projections.ProjectionList()
                    .Add(Projections.Property<Tenant>(x => x.Id).WithAlias(() => tickable.Id))
                    .Add(Projections.Property<Tenant>(x => x.Name).WithAlias(() => tickable.Label))
                    .Add(Projections.Property<Tenant>(x => x.Description).WithAlias(() => tickable.HelpText))
                ).TransformUsing(Transformers.AliasToBean<Tickable>()).List<Tickable>();
            var authorizationGrants = this.Persistence.QueryOver<AuthorizationGrant>()
                .Select(Projections.ProjectionList()
                    .Add(Projections.Property<AuthorizationGrant>(x => x.Id).WithAlias(() => tickable.Id))
                    .Add(Projections.Property<AuthorizationGrant>(x => x.Key).WithAlias(() => tickable.Label))
                    .Add(Projections.Property<AuthorizationGrant>(x => x.Description).WithAlias(() => tickable.HelpText))
                ).TransformUsing(Transformers.AliasToBean<Tickable>()).List<Tickable>();
            var scopes = this.Persistence.QueryOver<Scope>()
                .Select(Projections.ProjectionList()
                    .Add(Projections.Property<Scope>(x => x.Id).WithAlias(() => tickable.Id))
                    .Add(Projections.Property<Scope>(x => x.Name).WithAlias(() => tickable.Label))
                    .Add(Projections.Property<Scope>(x => x.Description).WithAlias(() => tickable.HelpText))
                ).TransformUsing(Transformers.AliasToBean<Tickable>()).List<Tickable>();
            return new CreateClient
            {
                Tenants = tenants,
                Scopes = scopes,
                AuthorizationGrants = authorizationGrants,
                IsConfidential = new Tickable() { Id = Guid.NewGuid(), Label = string.Empty }
            };
        }

        #endregion
    }

    #endregion

    #region Post Request Handler.

    /// <summary>
    /// Handler for create client POST request.
    /// </summary>
    internal class CreateClientPostHandler : PersistentRequestHandler<CreateClient, Id<DetailsClient>>
    {
        #region Variables.

        /// <summary>
        /// Image processing.
        /// </summary>
        private readonly IImageProcessor imageProcessor;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateClientPostHandler"/> class.
        /// </summary>
        /// <param name="imageProcessor">The <see cref="IImageProcessor"/></param>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public CreateClientPostHandler(IImageProcessor imageProcessor, IPersistenceContext persistenceContext)
            : base(persistenceContext)
        {
            this.imageProcessor = imageProcessor;
        }

        #endregion

        #region Overrides.

        /// <inheritdocs />
        public override Id<DetailsClient> Handle(CreateClient message)
        {
            var authorizationGrants = this.ResolveAuthorizationGrants(message);
            var tenants = this.ResolveTenants(message);
            var scopes = this.ResolveScopes(message);
            string imageFileName = null;
            string imageMimeType = null;
            if (message.Logotype.IsNotNull())
            {
                imageMimeType = message.Logotype.ContentType;
                this.imageProcessor.Save(message.Logotype, out imageFileName);
            }
            var client = new Client(message.Name, message.Description, imageFileName, imageMimeType, message.AccessTokenLifetime, message.RefreshTokenLifetime, message.RedirectionEndpoint, ! message.IsConfidential.IsSelected, authorizationGrants, tenants, scopes);
            this.Persistence.Save(client.Activate());
            return new Id<DetailsClient>
            {
                Id = client.Id,
                Slug = client.Slug.Name
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private IList<Tenant> ResolveTenants(CreateClient message)
        {
            var ids = message.Tenants.Where(x => x.IsSelected.Equals(true)).Select(x => x.Id).ToArray();
            var tenants = this.Persistence.QueryOver<Tenant>().WhereRestrictionOn(x => x.Id).IsIn(ids).List();
            Requires.ValidState(tenants.Count > 0, "Zero tenants found while trying to create client");
            return tenants;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private IList<Scope> ResolveScopes(CreateClient message)
        {
            var ids = message.Scopes.Where(x => x.IsSelected.Equals(true)).Select(x => x.Id).ToArray();
            var scopes = this.Persistence.QueryOver<Scope>().WhereRestrictionOn(x => x.Id).IsIn(ids).List();
            Requires.ValidState(scopes.Count > 0, "Zero scopes found while trying to create client");
            return scopes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private IList<AuthorizationGrant> ResolveAuthorizationGrants(CreateClient message)
        {
            var ids = message.AuthorizationGrants.Where(x => x.IsSelected.Equals(true)).Select(x => x.Id).ToArray();
            var authorizationGrants = this.Persistence.QueryOver<AuthorizationGrant>().WhereRestrictionOn(x => x.Id).IsIn(ids).List();
            Requires.ValidState(authorizationGrants.Count > 0, "Zero authorization grants found while trying to create client");
            return authorizationGrants;
        }

        #endregion
    }

    #endregion
}