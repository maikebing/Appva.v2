// <copyright file="TenantService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Services
{
    #region Imports.

    using System.Runtime.Caching;
    using Appva.Caching.Policies;
    using Appva.Caching.Providers;
    using Appva.Core.Extensions;
    using Appva.Core.Resources;
    using Appva.Tenant.Identity;
    using Appva.Tenant.Interoperability.Client;
    using Microsoft.Owin;
    using Validation;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface ITenantService
    {
        /// <summary>
        /// Attempts to identify the tenant from the current execution context.
        /// </summary>
        /// <param name="identity">The current tenant identity</param>
        /// <returns>True if the tenant could be identified; false if not</returns>
        bool TryIdentifyTenant(out ITenantIdentity identity);

        /// <summary>
        /// Locates the tenant by unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier</param>
        ITenantIdentity Find(ITenantIdentifier id);

        /// <summary>
        /// Validates the current request and and tenant identifier.
        /// </summary>
        /// <param name="context">An owin context</param>
        /// <returns>A <see cref="IValidateTenantIdentificationResult"/></returns>
        IValidateTenantIdentificationResult Validate(IOwinContext context);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class TenantService : ITenantService
    {
        #region Variables.

        /// <summary>
        /// The tenant header key.
        /// </summary>
        private const string TenantKey = "Tenant-Name";

        /// <summary>
        /// The cache key.
        /// </summary>
        private const string CacheKey = "https://schemas.appva.se/2015/04/cache/tenants";

        /// <summary>
        /// The <see cref="ITenantClient"/> implementation.
        /// </summary>
        private readonly ITenantClient client;

        /// <summary>
        /// The implemented <see cref="ITenantIdentificationStrategy"/> instance.
        /// </summary>
        private readonly ITenantIdentificationStrategy strategy;

        /// <summary>
        /// The implemented <see cref="IRuntimeMemoryCache"/> instance.
        /// </summary>
        private readonly IRuntimeMemoryCache cache;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="TenantService"/> class.
        /// </summary>
        public TenantService(ITenantClient client, ITenantIdentificationStrategy strategy, IRuntimeMemoryCache cache)
        {
            Requires.NotNull(client, "client");
            Requires.NotNull(strategy, "strategy");
            Requires.NotNull(cache, "cache");
            this.client = client;
            this.strategy = strategy;
            this.cache = cache;
        }

        #endregion

        #region ITenantService Members.

        /// <inheritdoc />
        public bool TryIdentifyTenant(out ITenantIdentity identity)
        {
            ITenantIdentifier identifier = null;
            if (! this.strategy.TryIdentifyTenant(out identifier))
            {
                identity = null;
                return false;
            }
            identity = this.Find(identifier);
            return identity != null;
        }

        /// <inheritdoc />
        public ITenantIdentity Find(ITenantIdentifier id)
        {
            Requires.NotNull(id, "id");
            var cacheKey = CacheTypes.Tenant.FormatWith(id.Value);
            if (this.cache.Find<ITenantIdentity>(cacheKey) == null)
            {
                var tenant = this.client.FindByIdentifier(id);
                if (tenant != null)
                {
                    this.cache.Upsert<ITenantIdentity>(cacheKey, new TenantIdentity(id, tenant.Name, tenant.HostName), new RuntimeEvictionPolicy
                    {
                        Priority = CacheItemPriority.Default
                    });
                }
            }
            return this.cache.Find<ITenantIdentity>(cacheKey);
        }

        /// <inheritdoc />
        public IValidateTenantIdentificationResult Validate(IOwinContext context)
        {
            ITenantIdentity identity;
            if (this.TryIdentifyTenant(out identity))
            {
                context.Request.Headers.Add(TenantKey, new[]
                {
                    identity.Name
                });
            }
            return this.strategy.Validate(identity, context.Request.Uri);
        }

        #endregion
    }
}