// <copyright file="LeastFrequentlyUsedPolicy.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Caching.Policies
{
    /// <summary>
    /// Least-Frequently Used (LFU)
    /// Counts how often an item is needed. Those that are used least often are 
    /// discarded first.
    /// </summary>
    public sealed class LeastFrequentlyUsedPolicy : CachePolicy
    {
        #region Implementation.

        /// <inheritdoc />
        public override bool Compare(ICacheItem element1, ICacheItem element2)
        {
            return element2.Hits < element1.Hits;
        }

        #endregion
    }
}