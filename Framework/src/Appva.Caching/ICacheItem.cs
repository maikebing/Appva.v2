// <copyright file="ICacheItem.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Caching
{
    /// <summary>
    /// Representation of a cached item.
    /// </summary>
    public interface ICacheItem
    {
        /// <summary>
        /// The cache key.
        /// </summary>
        object Key
        {
            get;
        }

        /// <summary>
        /// The cached value.
        /// </summary>
        object Value
        {
            get;
        }

        /// <summary>
        /// Hits as amount of requests.
        /// </summary>
        long Hits
        {
            get;
        }

        /// <summary>
        /// The cached item created timestamp.
        /// </summary>
        long Created
        {
            get;
        }

        /// <summary>
        /// The cached item last modified timestamp.
        /// </summary>
        long LastModified
        {
            get;
        }

        /// <summary>
        /// The cached item last accessed timestamp.
        /// </summary>
        long LastAccessed
        {
            get;
        }
    }
}