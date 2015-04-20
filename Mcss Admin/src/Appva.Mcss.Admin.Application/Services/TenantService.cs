// <copyright file="TenantService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Services
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Caching;
    using Appva.Apis.TenantServer;
    using Appva.Apis.TenantServer.Contracts;
    using Appva.Caching.Policies;
    using Appva.Caching.Providers;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Tenant.Identity;
    using Appva.Tenant.Interoperability.Client;
    using Validation;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface ITenantService : IService
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
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class TenantService : ITenantService
    {
        #region Variables.

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
            var cacheKey = CacheKey + id.Value;
            if (this.cache.Find<ITenantIdentity>(cacheKey) == null)
            {
                var tenant = this.client.FindByIdentifier(id.Value);
                this.cache.Upsert<ITenantIdentity>(cacheKey, new TenantIdentity(id, tenant.Name, tenant.HostName), new RuntimeEvictionPolicy
                {
                    Priority = CacheItemPriority.Default
                });
            }
            return this.cache.Find<ITenantIdentity>(cacheKey);
        }

        #endregion
    }
}