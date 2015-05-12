// <copyright file="RuntimeEvictionPolicy.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Caching.Policies
{
    #region Imports.

    using System;
    using System.Runtime.Caching;

    #endregion

    public interface IRuntimeEvictionPolicy
    {
        /// <summary>
        /// Returns one of the enumeration values that indicates the priority for eviction. 
        /// The default priority value is 
        /// <c>System.Runtime.Caching.CacheItemPriority.Default</c>, which means no 
        /// priority.
        /// </summary>
        CacheItemPriority Priority
        {
            get;
            set;
        }

        /// <summary>
        /// Returns a span of time within which a cache entry must be accessed before the 
        /// cache entry is evicted from the cache. The default is 
        /// <c>System.Runtime.Caching.ObjectCache.NoSlidingExpiration</c>, meaning that the 
        /// item should not be expired based on a time span.
        /// </summary>
        TimeSpan SlidingExpiration
        {
            get;
            set;
        }
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class RuntimeEvictionPolicy : CacheItemPolicy, IRuntimeEvictionPolicy
    {
        /// <summary>
        /// Non removable policy.
        /// </summary>
        public static readonly RuntimeEvictionPolicy NonRemovable = new RuntimeEvictionPolicy
            {
                Priority = CacheItemPriority.NotRemovable
            };

        /// <summary>
        /// Default policy.
        /// </summary>
        public static readonly RuntimeEvictionPolicy Default = new RuntimeEvictionPolicy
            {
                Priority = CacheItemPriority.Default
            };
    }
}