// <copyright file="ICachePolicy.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Caching.Policies
{
    /// <summary>
    /// An eviction policy.
    /// </summary>
    public interface ICachePolicy
    {
        /// <summary>
        /// Finds the best eviction candidate based on the sampled elements.
        /// </summary>
        /// <param name="sampledElements">A random subset of the population</param>
        /// <returns>The candidate <see cref="ICacheItem"/></returns>
        ICacheItem FindEvictionCandidate(ICacheItem[] sampledElements);

        /// <summary>
        /// Compares the desirableness for eviction of two elements.
        /// </summary>
        /// <param name="element1">The element to compare against</param>
        /// <param name="element2">The element to compare</param>
        /// <returns><code>true</code> if the second element is preferable for eviction to 
        /// the first element under the policy</returns>
        bool Compare(ICacheItem element1, ICacheItem element2);
    }
}