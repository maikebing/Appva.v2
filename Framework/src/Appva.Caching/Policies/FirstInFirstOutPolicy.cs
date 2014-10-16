// <copyright file="FirstInFirstOutPolicy.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Caching.Policies
{
    /// <summary>
    /// First In First Out (FIFO)
    /// Elements are evicted in the same order as they come in. When a put call is made for 
    /// a new element (and assuming that the max limit is reached for the memory store) the 
    /// element that was placed first (First-In) in the store is the candidate for eviction (First-Out).
    /// </summary>
    public sealed class FirstInFirstOutPolicy : CachePolicy
    {
        #region Implementation.

        /// <inheritdoc />
        public override bool Compare(ICacheItem element1, ICacheItem element2)
        {
            return element2.Created < element1.Created;
        }

        #endregion
    }
}