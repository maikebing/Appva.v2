// <copyright file="TenantAwareMemoryCache.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Caching
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security;
    using Appva.Caching;
    using Appva.Caching.Policies;
    using Appva.Caching.Providers;
    using Appva.Tenant.Identity;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface ITenantAwareMemoryCache : IRuntimeMemoryCache
    {
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class TenantAwareMemoryCache : ITenantAwareMemoryCache
    {
        #region Variables.

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
        /// Initializes a new instance of the <see cref="TenantAwareMemoryCache"/> class.
        /// </summary>
        public TenantAwareMemoryCache(ITenantIdentificationStrategy strategy, IRuntimeMemoryCache cache)
        {
            this.cache = cache;
            this.strategy = strategy;
        }

        #endregion

        #region IRuntimeMemoryCache Members.

        /// <inheritdoc />
        public object Find(object cacheKey)
        {
            return this.cache.Find(this.CreateKey(cacheKey));
        }

        /// <inheritdoc />
        public T Find<T>(object cacheKey)
        {
            return this.cache.Find<T>(this.CreateKey(cacheKey));
        }

        /// <inheritdoc />
        public T Find<T>(object cacheKey, T defaultValue)
        {
            return this.cache.Find<T>(this.CreateKey(cacheKey), defaultValue);
        }

        /// <inheritdoc />
        public IEnumerable<CacheEntry> List()
        {
            return this.cache.List()
                .Where(x => x.Key.ToString().StartsWith(this.CreateKey(null))).ToList();
        }

        /// <inheritdoc />
        public bool Add<T>(object cacheKey, T value, RuntimeEvictionPolicy policy)
        {
            return this.cache.Add<T>(this.CreateKey(cacheKey), value, policy);
        }

        /// <inheritdoc />
        public void Upsert<T>(object cacheKey, T value, RuntimeEvictionPolicy policy)
        {
            this.cache.Upsert<T>(this.CreateKey(cacheKey), value, policy);
        }

        /// <inheritdoc />
        public bool Remove(object cacheKey)
        {
            return this.cache.Remove(this.CreateKey(cacheKey));
        }

        /// <inheritdoc />
        public void RemoveAll()
        {
            foreach (var entry in this.List())
            {
                this.cache.Remove(entry.Key);
            }
        }

        /// <inheritdoc />
        public bool Contains(object cacheKey)
        {
            return this.cache.Contains(this.CreateKey(cacheKey));
        }

        /// <inheritdoc />
        public long Count()
        {
            return this.List().Count();
        }

        #endregion

        #region Private Methods.

        /// <summary>
        /// Creates a tenant specific cache key.
        /// </summary>
        /// <param name="cacheKey">The cache key</param>
        /// <returns>A tenant specific cache key</returns>
        private string CreateKey(object cacheKey)
        {
            ITenantIdentifier identifier = null;
            if (this.strategy.TryIdentifyTenant(out identifier))
            {
                if (cacheKey == null)
                {
                    return identifier.Value;
                }
                return string.Format("{0}.{1}", identifier.Value, cacheKey);
            }
            throw new SecurityException("Unable to identify tenant!");
        }

        #endregion

        #region IDisposable Members.

        /// <inheritdoc />
        public void Dispose()
        {
            if (this.cache != null)
            {
                this.cache.Dispose();
            }
        }

        #endregion
    }
}