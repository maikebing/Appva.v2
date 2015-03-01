// <copyright file="InMemoryCacheProvider.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Caching.Providers
{
    #region Imports.

    using System.Collections.Generic;
    using Logging;
    using Policies;
    using Validation;

    #endregion

    /// <summary>
    /// In memory cache implementation of <see cref="CacheProvider"/>.
    /// </summary>
    /// <example>
    /// <code language="cs" title="Example">
    ///     var caching = new InMemoryCacheProvider(new LeastFrequentlyUsedPolicy());
    ///     caching.Add("foo", "bar");
    ///     caching.Get{string}("foo");
    /// </code>
    /// <code language="cs" title="Example with IoC">
    ///     var builder = new ContainerBuilder();
    ///     builder.Register{LeastFrequentlyUsedPolicy}().As{IEvicationPolicy}().SingleInstance();
    ///     builder.Register{InMemoryCacheProvider}().As{ICacheProvider}().SingleInstance();
    /// </code>
    /// </example>
    public sealed class InMemoryCacheProvider : CacheProvider
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ILog"/> for <see cref="InMemoryCacheProvider"/>.
        /// </summary>
        private static readonly ILog Log = LogProvider.For<InMemoryCacheProvider>();

        /// <summary>
        /// The sync lock.
        /// </summary>
        private readonly object syncLock = new object();

        /// <summary>
        /// The hashtable cache store.
        /// </summary>
        private readonly IDictionary<object, ICacheItem> store = new Dictionary<object, ICacheItem>();

        /// <summary>
        /// The cache eviction policy.
        /// </summary>
        private readonly IEvicationPolicy policy;

        /// <summary>
        /// The maximum capacity of cacheable items before the eviction policy is executed.
        /// </summary>
        private readonly int capacity;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="InMemoryCacheProvider"/> class.
        /// </summary>
        /// <param name="policy">The <see cref="IEvicationPolicy"/></param>
        /// <param name="capacity">
        /// The maximum capacity of cacheable items before the eviction policy is executed, 
        /// defaults to 1000
        /// </param>
        /// <exception cref="System.ArgumentNullException">If policy is null</exception>
        public InMemoryCacheProvider(IEvicationPolicy policy, int capacity = 1000)
        {
            Requires.NotNull(policy, "policy");
            this.policy = policy;
            this.capacity = capacity;
        }

        #endregion

        #region CacheProvider Implementation.

        /// <inheritdoc />
        public override T Get<T>(object key)
        {
            var item = this.Get(key);
            return item != null ? (T) item : default(T);
        }

        /// <inheritdoc />
        public override object Get(object key)
        {
            Requires.NotNull(key, "key");
            lock (this.syncLock)
            {
                ICacheItem item;
                if (! this.store.ContainsKey(key) || ! this.store.TryGetValue(key, out item))
                {
                    return null;
                }
                item.UpdateHit();
                return item.Value;
            }
        }

        /// <inheritdoc />
        public override bool Add(object key, object value)
        {
            Requires.NotNull(key, "key");
            Requires.NotNull(value, "value");
            lock (this.syncLock)
            {
                if (this.store.ContainsKey(key))
                {
                    return false;
                }
                if (this.store.Count >= this.capacity)
                {
                    this.Evict();
                }
                this.store.Add(key, CacheItem.CreateNew(key, value));
                return true;
            }
        }

        /// <inheritdoc />
        public override bool AddOrUpdate(object key, object value)
        {
            if (this.Add(key, value) || this.Update(key, value))
            {
                return true;
            }
            return false;
        }

        /// <inheritdoc />
        public override bool Update(object key, object value)
        {
            Requires.NotNull(key, "key");
            Requires.NotNull(value, "value");
            lock (this.syncLock)
            {
                if (! this.store.ContainsKey(key))
                {
                    return false;
                }
                ICacheItem item;
                if (this.store.TryGetValue(key, out item))
                {
                    item.UpdateValue(value);
                }
                return item != null;
            }
        }

        /// <inheritdoc />
        public override bool Remove(object key)
        {
            Requires.NotNull(key, "key");
            lock (this.syncLock)
            {
                if (! this.store.ContainsKey(key))
                {
                    return false;
                }
                if (this.store.Remove(key))
                {
                    Log.DebugFormat(Debug.Messages.InMemoryCacheProviderCacheItemRemoved, key);
                    return true;
                }
                return false;
            }
        }

        /// <inheritdoc />
        public override void RemoveAll()
        {
            lock (this.syncLock)
            {
                this.store.Clear();
                Log.Debug(Debug.Messages.InMemoryCacheProviderClearStore);
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
            var candidate = this.policy.FindCandidate(samplePopulation);
            if (candidate != null)
            {
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
            var randomIds = EvictionPolicy.GenerateRandomSample(storeSize);
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
