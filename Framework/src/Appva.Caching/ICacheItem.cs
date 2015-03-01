// <copyright file="ICacheItem.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Caching
{
    /// <summary>
    /// Representation of a cached item.
    /// </summary>
    public interface ICacheItem
    {
        #region Public Properties.

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
        /// The amount of times the cache item has been accessed since creation.
        /// </summary>
        long Hits
        {
            get;
        }

        /// <summary>
        /// The timestamp of the cache item first creation date time.
        /// </summary>
        long CreatedAt
        {
            get;
        }

        /// <summary>
        /// The timestamp of the cache item last modification date time.
        /// </summary>
        long ModifiedAt
        {
            get;
        }

        /// <summary>
        /// The timestamp of the cached item last accessed date time.
        /// </summary>
        long AccessedAt
        {
            get;
        }

        #endregion

        #region Public Methods.

        /// <summary>
        /// Updates the value of the cached item.
        /// </summary>
        /// <param name="value">The object to be updated</param>
        void UpdateValue(object value);

        /// <summary>
        /// Updates the cached item hit count.
        /// </summary>
        void UpdateHit();

        #endregion
    }
}
