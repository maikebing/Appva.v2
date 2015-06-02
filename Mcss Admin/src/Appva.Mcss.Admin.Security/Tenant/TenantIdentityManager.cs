// <copyright file="TenantIdentityManager.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Security
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Caching;
    using Appva.Caching.Policies;
    using Appva.Caching.Providers;
    using Appva.Core.Resources;
    using Appva.Tenant.Identity;
    using Appva.Tenant.Interoperability.Client;
    using JetBrains.Annotations;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface ITenantIdentityManager
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
        /// <param name="identifier">The unique identifier</param>
        ITenantIdentity Find(ITenantIdentifier identifier);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class TenantIdentityManager : ITenantIdentityManager
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ITenantClient"/>.
        /// </summary>
        private readonly ITenantClient client;

        /// <summary>
        /// The <see cref="ITenantIdentificationStrategy"/>.
        /// </summary>
        private readonly ITenantIdentificationStrategy strategy;

        /// <summary>
        /// The <see cref="IRuntimeMemoryCache"/>.
        /// </summary>
        private readonly IRuntimeMemoryCache cache;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="TenantIdentityManager"/> class.
        /// </summary>
        public TenantIdentityManager([NotNull] ITenantClient client, [NotNull] ITenantIdentificationStrategy strategy, [NotNull] IRuntimeMemoryCache cache)
        {
            this.client = client;
            this.strategy = strategy;
            this.cache = cache;
        }

        #endregion

        #region ITenantIdentityManager Members.

        /// <inheritdoc />
        public bool TryIdentifyTenant(out ITenantIdentity identity)
        {
            ITenantIdentifier identifier = null;
            if (! this.strategy.TryIdentifyTenant(out identifier))
            {
                identity = null;
            }
            else
            {
                identity = this.Find(identifier);
            }
            return identity != null;
        }

        /// <inheritdoc />
        public ITenantIdentity Find(ITenantIdentifier identifier)
        {
            var cacheKey = string.Format(CacheTypes.Tenant, identifier.Value);
            if (this.cache.Find<ITenantIdentity>(cacheKey) == null)
            {
                var tenant = this.client.FindByIdentifier(identifier.Value);
                this.cache.Upsert<ITenantIdentity>(
                    cacheKey, 
                    new TenantIdentity(identifier, tenant.Name, tenant.HostName), 
                    new RuntimeEvictionPolicy
                    {
                        Priority = CacheItemPriority.Default
                    });
            }
            return this.cache.Find<ITenantIdentity>(cacheKey);
        }

        #endregion
    }
}