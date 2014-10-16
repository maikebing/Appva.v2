// <copyright file="InMemoryCacheProvider.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Caching.Providers
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using Appva.Caching.Policies;
    using Common.Logging;
    using Validation;

    #endregion

    /// <summary>
    /// In memory cache implementation of <see cref="CacheProvider"/>.
    /// </summary>
    public sealed class InMemoryCacheProvider : CacheProvider
    {
        #region Private Variables.

        /// <summary>
        /// Sync lock.
        /// </summary>
        private readonly object syncLock = new object();

        /// <summary>
        /// The hashtable cache store.
        /// </summary>
        private readonly IDictionary<object, ICacheItem> store = new Dictionary<object, ICacheItem>();

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="InMemoryCacheProvider"/> class.
        /// </summary>
        /// <param name="policy">A <see cref="ICachePolicy"/></param>
        /// <param name="capacity">The cache capacity, defaults to 1000</param>
        public InMemoryCacheProvider(ICachePolicy policy, int capacity = 1000)
        {
            this.EvictionPolicy = policy;
            this.Capacity = capacity;
        }

        #endregion

        #region Public Properties.

        /// <summary>
        /// Maximum capacity of cacheable items before eviction
        /// policy is implemented.
        /// </summary>
        public ICachePolicy EvictionPolicy
        {
            get;
            private set;
        }

        /// <summary>
        /// Maximum capacity of cacheable items before eviction
        /// policy is implemented.
        /// </summary>
        public int Capacity
        {
            get;
            private set;
        }

        #endregion

        #region Implementation.

        /// <inheritdoc />
        public override T Get<T>(object key)
        {
            var item = this.Get(key);
            return (item != null) ? (T) item : default(T);
        }

        /// <inheritdoc />
        public override object Get(object key)
        {
            Requires.Argument(key == null, "key", "Key may not be null");
            lock (this.syncLock)
            {
                if (this.store.ContainsKey(key))
                {
                    var item = this.store[key];
                    if (item != null)
                    {
                        var cacheItem = (CacheItem) item;
                        cacheItem.Hits += 1;
                        cacheItem.LastAccessed = DateTime.UtcNow.Ticks;
                        this.store[key] = cacheItem;
                        return item.Value;
                    }
                }
                Log.Trace(x => x("Failed to retrieve cache by key: {0}.", key));
                return null;
            }
        }

        /// <inheritdoc />
        public override void Add(object key, object value)
        {
            Requires.Argument(key == null, "key", "Key may not be null");
            Requires.Argument(value == null, "value", "Value may not be null");
            lock (this.syncLock)
            {
                var now = DateTime.UtcNow.Ticks;
                if (! this.store.ContainsKey(key))
                {
                    if (this.store.Count >= this.Capacity)
                    {
                        this.Evict();
                    }
                    this.store.Add(
                        key, 
                        new CacheItem()
                    {
                        Key = key,
                        Value = value,
                        Hits = 0,
                        Created = now,
                        LastAccessed = now,
                        LastModified = now
                    });
                } 
                else 
                {
                    var ce = this.store[key] as CacheItem;
                    ce.Value = value;
                    ce.LastModified = now;
                    this.store[key] = ce;
                }
                Log.Debug(x => x("Cached value: {0} to key: {1}.", value, key));
            }
        }

        /// <inheritdoc />
        public override void Remove(object key)
        {
            lock (this.syncLock)
            {
                if (this.store.ContainsKey(key))
                {
                    this.store.Remove(key);
                    Log.Debug(x => x("Removed key {0} from cache!", key));
                }
            }
        }

        /// <inheritdoc />
        public override void RemoveAll()
        {
            lock (this.syncLock)
            {
                this.store.Clear();
                Log.Debug(x => x("Cleared all cache"));
            }
        }

        /// <inheritdoc />
        public override int Count()
        {
            return this.store.Count;
        }

        #endregion

        #region Private Helpers.

        /// <summary>
        /// Finds a suitable evictional candidate and removes it from the cache.
        /// </summary>
        private void Evict()
        {
            var samplePopulation = this.GetSamplePopulation();
            var candidate = this.EvictionPolicy.FindEvictionCandidate(samplePopulation);
            if (candidate != null)
            {
                Log.Trace(x => x("Found eviction candidate to remove"));
                this.Remove(candidate.Key);
            } 
            else
            {
                this.RemoveAll();
            }
        }

        /// <summary>
        /// Returns a random population sample.
        /// </summary>
        /// <returns>An array of <see cref="ICacheItem"/></returns>
        private ICacheItem[] GetSamplePopulation()
        {
            var storeSize = this.store.Count;
            var randomIds = CachePolicy.GenerateRandomSample(storeSize);
            var sampleSize = randomIds.Length;
            var retval = new ICacheItem[sampleSize];
            var copy = new ICacheItem[storeSize];
            this.store.Values.CopyTo(copy, 0);
            for (var i = 0; i < sampleSize; i++)
            {
                retval[i] = copy[randomIds[i]];
            }
            return retval;
        }

        #endregion
    }
}