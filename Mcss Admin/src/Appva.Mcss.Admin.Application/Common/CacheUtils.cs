// <copyright file="CacheUtils.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Common
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Caching;
    using Appva.Caching.Policies;
    using Appva.Caching.Providers;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal static class CacheUtils
    {
        public static void CopyAndCacheList<TEntity, TCacheEntry>(
            IRuntimeMemoryCache cache, string cacheKey, IList<TEntity> items, Func<TEntity, TCacheEntry> map)
        {
            if (cacheKey == null || items == null || items.Count == 0)
            {
                return;
            }
            var copy = items.Select(map).ToList();
            cache.Upsert(
                cacheKey, 
                copy, 
                new RuntimeEvictionPolicy
                {
                    Priority = CacheItemPriority.Default
                });
        }
    }
}