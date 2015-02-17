// <copyright file="ReadUpdateClientFormHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.AuthorizationServer.Models.Handlers
{
    #region Imports.

    using System;
    using System.Linq;
    using Appva.Core.Extensions;
    using Appva.Mcss.AuthorizationServer.Common;
    using Appva.Mcss.AuthorizationServer.Domain.Entities;
    using Appva.Mcss.AuthorizationServer.Models;
    using Appva.Mvc.Html.Models;
    using Appva.Persistence;
    using Appva.Persistence.Transformers;
    using NHibernate.Criterion;
    using NHibernate.Transform;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    internal class ReadUpdateClientFormHandler 
        : PersistentRequestHandler<Id<ReadUpdateClientForm>, ReadUpdateClientForm>
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadUpdateClientFormHandler"/> class.
        /// </summary>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public ReadUpdateClientFormHandler(IPersistenceContext persistenceContext) 
            : base(persistenceContext)
        {
        }

        #endregion

        #region Overrides.

        /// <inheritdocs />
        public override ReadUpdateClientForm Handle(Id<ReadUpdateClientForm> message)
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
            var client = this.Persistence.Get<Client>(message.Id);
            client.Tenants.ForEach(t =>
            {
                var tenant = tenants.Where(x => x.Id.Equals(t.Id)).SingleOrDefault();
                if (tenant.IsNotNull())
                {
                    tenant.IsSelected = true;
                }
            });
            client.AuthorizationGrants.ForEach(ag =>
            {
                var authorizationGrant = authorizationGrants.Where(x => x.Id.Equals(ag.Id)).SingleOrDefault();
                if (authorizationGrant.IsNotNull())
                {
                    authorizationGrant.IsSelected = true;
                }
            });
            client.Scopes.ForEach(s =>
            {
                var scope = scopes.Where(x => x.Id.Equals(s.Id)).SingleOrDefault();
                if (scope.IsNotNull())
                {
                    scope.IsSelected = true;
                }
            });
            return new ReadUpdateClientForm
            {
                Id = client.Id,
                Slug = client.Slug.Name,
                Name = client.Name,
                Description = client.Description,
                Logotype = new Logotype(client.Image),
                Secret = client.Secret,
                Password = client.Password,
                AccessTokenLifetime = client.AccessTokenLifetime,
                RefreshTokenLifetime = client.RefreshTokenLifetime,
                RedirectionEndpoint = client.RedirectionEndpoint,
                IsConfidential = new Tickable
                {
                    IsSelected = client.Type.Equals(ClientType.Public)
                },
                Tenants = tenants,
                AuthorizationGrants = authorizationGrants,
                Scopes = scopes
            };
        }

        #endregion
    }
}