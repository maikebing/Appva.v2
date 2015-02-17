// <copyright file="PostUpdateClientFormHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.AuthorizationServer.Models.Handlers
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Core.Extensions;
    using Appva.Mcss.AuthorizationServer.Common;
    using Appva.Mcss.AuthorizationServer.Domain.Entities;
    using Appva.Mcss.AuthorizationServer.Models;
    using Appva.Mvc.Imaging;
    using Appva.Persistence;
    using Appva.Persistence.Transformers;
    using NHibernate.Criterion;
    using NHibernate.Transform;
    using Validation;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    internal class PostUpdateClientFormHandler
        : PersistentRequestHandler<PostUpdateClientForm, Id<DetailsClient>>
    {
        #region Variables.

        /// <summary>
        /// Image processing.
        /// </summary>
        private readonly IImageProcessor imageProcessor;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="PostUpdateClientFormHandler"/> class.
        /// </summary>
        /// <param name="imageProcessor">The <see cref="IImageProcessor"/></param>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public PostUpdateClientFormHandler(IImageProcessor imageProcessor, IPersistenceContext persistenceContext)
            : base(persistenceContext)
        {
            this.imageProcessor = imageProcessor;
        }

        #endregion

        #region Overrides.

        /// <inheritdocs />
        public override Id<DetailsClient> Handle(PostUpdateClientForm message)
        {
            var client = this.Persistence.Get<Client>(message.Id);
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
            client.Update(
                message.Name,
                message.Description,
                imageFileName,
                imageMimeType,
                message.Secret,
                message.Password,
                message.AccessTokenLifetime,
                message.RefreshTokenLifetime,
                message.RedirectionEndpoint,
                ! message.IsConfidential.IsSelected, 
                authorizationGrants, 
                tenants,
                scopes);
            this.Persistence.Update(client);
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
        private IList<Tenant> ResolveTenants(PostUpdateClientForm message)
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
        private IList<Scope> ResolveScopes(PostUpdateClientForm message)
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
        private IList<AuthorizationGrant> ResolveAuthorizationGrants(PostUpdateClientForm message)
        {
            var ids = message.AuthorizationGrants.Where(x => x.IsSelected.Equals(true)).Select(x => x.Id).ToArray();
            var authorizationGrants = this.Persistence.QueryOver<AuthorizationGrant>().WhereRestrictionOn(x => x.Id).IsIn(ids).List();
            Requires.ValidState(authorizationGrants.Count > 0, "Zero authorization grants found while trying to create client");
            return authorizationGrants;
        }

        #endregion
    }
}