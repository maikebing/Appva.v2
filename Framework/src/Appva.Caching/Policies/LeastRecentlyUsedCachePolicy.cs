// <copyright file="LeastRecentlyUsedCachePolicy.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Caching.Policies
{
    /// <summary>
    /// Least Recently Used (LRU)
    /// Discards the least recently used items first. This algorithm requires keeping track 
    /// of what was used when, which is expensive if one wants to make sure the algorithm 
    /// always discards the least recently used item. General implementations of this technique 
    /// require keeping "age bits" for cache-lines and track the "Least Recently Used" 
    /// cache-line based on age-bits. In such an implementation, every time a cache-line is used, 
    /// the age of all other cache-lines changes.
    /// </summary>
    public sealed class LeastRecentlyUsedCachePolicy : CachePolicy
    {
        #region Implementation.

        /// <inheritdoc />
        public override bool Compare(ICacheItem element1, ICacheItem element2)
        {
            return element2.LastAccessed < element1.LastAccessed;
        }

        #endregion
    }
}