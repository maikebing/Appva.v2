// <copyright file="RuntimeMemoryCacheProvider.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Caching.Providers
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Runtime.Caching;
    using System.Threading.Tasks;
    using Logging;
    using Policies;

    #endregion

    /// <summary>
    /// Marker interface for run time cache provider.
    /// </summary>
    public interface IRuntimeMemoryCache
    {
        /// <summary>
        /// Returns a cached entry by its identifier.
        /// </summary>
        /// <param name="cacheKey">
        /// A unique identifier for the cache entry to retrieve
        /// </param>
        /// <returns>
        /// A reference to the cache entry that is identified by key, if the entry exists; 
        /// otherwise, null
        /// </returns>
        object Find(object cacheKey);

        /// <summary>
        /// Returns a cached entry by its identifier and region. 
        /// </summary>
        /// <typeparam name="T">The cached entry type</typeparam>
        /// <param name="cacheKey">
        /// A unique identifier for the cache entry to retrieve
        /// </param>
        /// <returns>
        /// A reference to the cache entry that is identified by key, if the entry exists; 
        /// otherwise, the default value explicitly set
        /// </returns>
        T Find<T>(object cacheKey);

        /// <summary>
        /// Returns a cached entry by its identifier. 
        /// </summary>
        /// <typeparam name="T">The cached entry type</typeparam>
        /// <param name="cacheKey">
        /// A unique identifier for the cache entry to retrieve
        /// </param>
        /// <param name="defaultValue">
        /// A default return value if the cache entry does not exist
        /// </param>
        /// <returns>
        /// A reference to the cache entry that is identified by key, if the entry exists; 
        /// otherwise, the default value explicitly set
        /// </returns>
        T Find<T>(object cacheKey, T defaultValue);

        /// <summary>
        /// Returns an immutable collection of cached entries.
        /// </summary>
        /// <returns>
        /// A collection of all references to the cache entries avaiblable in the current 
        /// cache
        /// </returns>
        IEnumerable<CacheEntry> List();

        /// <summary>
        /// Adds a cached entry {T} by its identifier.
        /// </summary>
        /// <typeparam name="T">The cache entry type</typeparam>
        /// <param name="cacheKey">
        /// A unique identifier for the cache entry to insert
        /// </param>
        /// <param name="value">The object to add</param>
        /// <param name="policy">
        /// An object that contains eviction details for the cache entry. This object 
        /// provides more options for eviction than a simple absolute expiration
        /// </param>
        /// <returns>
        /// True if insertion succeeded, or false if there is an already an entry in the 
        /// cache that has the same key as item
        /// </returns>
        /// <exception cref="System.ArgumentNullException">If cacheKey is null</exception>
        /// <exception cref="System.ArgumentNullException">If value is null</exception>
        bool Add<T>(object cacheKey, T value, RuntimeEvictionPolicy policy = null);

        /// <summary>
        /// Adds a cache value in the cache, regardless whether a matching entry already 
        /// exists. If the specified entry does not exist in the cache, a new cache entry is 
        /// inserted. If the specified entry exists, it is updated.
        /// </summary>
        /// <typeparam name="T">The cache entry type</typeparam>
        /// <param name="cacheKey">
        /// A unique identifier for the cache entry to update or add
        /// </param>
        /// <param name="value">The object to update or add</param>
        /// <param name="policy">
        /// An object that contains eviction details for the cache entry. This object 
        /// provides more options for eviction than a simple absolute expiration
        /// </param>
        /// <exception cref="System.ArgumentNullException">If cacheKey is null</exception>
        /// <exception cref="System.ArgumentNullException">If value is null</exception>
        void Upsert<T>(object cacheKey, T value, RuntimeEvictionPolicy policy = null);

        /// <summary>
        /// Removes a cache entry from the cache.
        /// </summary>
        /// <param name="cacheKey">
        /// A unique identifier for the cache entry to remove
        /// </param>
        /// <returns>
        /// True if the entry is found and and removed from the cache; otherwise, false 
        /// </returns>
        /// <exception cref="System.ArgumentNullException">If cacheKey is null</exception>
        bool Remove(object cacheKey);

        /// <summary>
        /// Removes all cache entries from the cache.
        /// </summary>
        void RemoveAll();

        /// <summary>
        /// Determines whether a cache entry exists in the cache.
        /// </summary>
        /// <param name="cacheKey">
        /// A unique identifier for the cache entry to search for
        /// </param>
        /// <returns>
        /// True if the cache contains a cache entry whose key matches key; otherwise, false
        /// </returns>
        bool Contains(object cacheKey);

        /// <summary>
        /// Returns the total number of cache entries in the cache.
        /// </summary>
        /// <returns>The number of entries in the cache</returns>
        long Count();
    }

    /// <summary>
    /// A runtime memory cache implementation of <see cref="CacheProvider"/> using the
    /// .NET 4.0 <c>System.Runtime.Caching.MemoryCache</c>.
    /// <externalLink>
    ///     <linkText>MemoryCache Class</linkText>
    ///     <linkUri>
    ///         https://msdn.microsoft.com/en-us/library/system.runtime.caching.memorycache(v=vs.110).aspx
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    /// <example>
    /// <code language="cs" title="Example">
    ///     var cache = new RuntimeMemoryCacheProvider("MyCache");
    ///     cache.Add("foo", "bar");
    ///     cache.Get{string}("foo");
    /// </code>
    /// <code language="cs" title="Example with IoC">
    ///     var builder = new ContainerBuilder();
    ///     var cache = new RuntimeMemoryCacheProvider("MyCache");
    ///     builder.Register{RuntimeMemoryCacheProvider}(x => cache).As{ICacheProvider}().SingleInstance();
    /// </code>
    /// </example>
    public sealed class RuntimeMemoryCache : IRuntimeMemoryCache
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ILog"/> for <see cref="RuntimeMemoryCache"/>.
        /// </summary>
        private static readonly ILog Log = LogProvider.For<RuntimeMemoryCache>();

        /// <summary>
        /// The underlying cache implementation.
        /// </summary>
        private readonly MemoryCache cache;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="RuntimeMemoryCache"/> 
        /// class.
        /// </summary>
        /// <param name="name">The name to use to look up configuration information</param>
        /// <param name="configuration">
        /// An optional collection of name/value pairs of configuration information to use 
        /// for configuring the cache
        /// </param>
        /// <exception cref="ArgumentNullException">The name is null</exception>
        /// <exception cref="ArgumentNullException">
        /// The string value "default" (case insensitive) is assigned to name. The value 
        /// "default" cannot be assigned to a new MemoryCache instance, because the value is 
        /// reserved for use by the Default property
        /// </exception>
        /// <exception cref="System.Configuration.ConfigurationException">
        /// A value in the configuration collection is invalid
        /// </exception>
        /// <exception cref="ArgumentException">
        /// A name or value in the configuration parameter could not be parsed
        /// </exception>
        public RuntimeMemoryCache(string name, NameValueCollection configuration = null)
        {
            this.cache = new MemoryCache(name, configuration);
        }

        #endregion

        #region Static Functions.

        /// <summary>
        /// Creates a new <see cref="RuntimeMemoryCache"/> instance.
        /// </summary>
        /// <param name="name">The name to use to look up configuration information</param>
        /// <param name="configuration">
        /// An optional collection of name/value pairs of configuration information to use 
        /// for configuring the cache
        /// </param>
        /// <returns>A new <see cref="RuntimeMemoryCache"/> instance</returns>
        /// <exception cref="ArgumentNullException">The name is null</exception>
        /// <exception cref="ArgumentNullException">
        /// The string value "default" (case insensitive) is assigned to name. The value 
        /// "default" cannot be assigned to a new MemoryCache instance, because the value is 
        /// reserved for use by the Default property
        /// </exception>
        /// <exception cref="System.Configuration.ConfigurationException">
        /// A value in the configuration collection is invalid
        /// </exception>
        /// <exception cref="ArgumentException">
        /// A name or value in the configuration parameter could not be parsed
        /// </exception>
        public static IRuntimeMemoryCache CreateNew(string name, NameValueCollection configuration = null)
        {
            return new RuntimeMemoryCache(name, configuration);
        }

        #endregion

        #region IRuntimeMemoryCacheProvider Implementation.

        /// <inheritdoc />
        public object Find(object cacheKey)
        {
            var key = cacheKey as string;
            return key == null ? null : this.cache.Get(key);
        }

        /// <inheritdoc />
        public T Find<T>(object cacheKey)
        {
            var entry = this.Find(cacheKey);
            return entry == null ? default(T) : (T) entry;
        }

        /// <inheritdoc />
        public T Find<T>(object cacheKey, T defaultValue)
        {
            var entry = this.Find(cacheKey);
            return entry == null ? defaultValue == null ? default(T) : defaultValue : (T) entry;
        }

        /// <inheritdoc />
        public IEnumerable<CacheEntry> List()
        {
            return this.cache.Select(x => CacheEntry.CreateNew(x.Key, x.Value)).ToList();
        }

        /// <inheritdoc />
        public bool Add<T>(object cacheKey, T value, RuntimeEvictionPolicy policy = null)
        {
            var key = cacheKey as string;
            if (key == null)
            {
                return false;
            }
            return this.cache.Add(new CacheItem(key, value), policy);
        }

        /// <inheritdoc />
        public void Upsert<T>(object cacheKey, T value, RuntimeEvictionPolicy policy = null)
        {
            var key = cacheKey as string;
            if (key == null)
            {
                return;
            }
            this.cache.Set(new CacheItem(key, value), policy);
        }

        /// <inheritdoc />
        public bool Remove(object cacheKey)
        {
            var key = cacheKey as string;
            if (key == null)
            {
                return false;
            }
            return this.cache.Remove(key) != null;
        }

        /// <inheritdoc />
        public void RemoveAll()
        {
            var keys = this.cache.Select(x => x.Key);
            Parallel.ForEach(keys, x => this.cache.Remove(x));
        }

        /// <inheritdoc />
        public bool Contains(object cacheKey)
        {
            var key = cacheKey as string;
            if (key == null)
            {
                return false;
            }
            return this.cache.Contains(key);
        }

        /// <inheritdoc />
        public long Count()
        {
            return this.cache.GetCount();
        }

        #endregion
    }
}