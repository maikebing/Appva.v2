// <copyright file="CacheEntry.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Caching
{
    /// <summary>
    /// Representation of a cache entry.
    /// </summary>
    public struct CacheEntry
    {
        #region Variables.

        /// <summary>
        /// The cache key.
        /// </summary>
        private readonly object key;

        /// <summary>
        /// The cache value.
        /// </summary>
        private readonly object value;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheEntry"/> struct.
        /// </summary>
        /// <param name="key">The cache key</param>
        /// <param name="value">The cache value</param>
        private CacheEntry(object key, object value)
        {
            this.key = key;
            this.value = value;
        }

        #endregion

        #region Public Properties.

        /// <summary>
        /// Returns the cache key.
        /// </summary>
        public object Key
        {
            get
            {
                return this.key;
            }
        }

        /// <summary>
        /// Returns the cache value.
        /// </summary>
        public object Value
        {
            get
            {
                return this.value;
            }
        }

        #endregion

        #region Public Static Functions.

        /// <summary>
        /// Creates a new instance of the <see cref="CacheEntry"/> class.
        /// </summary>
        /// <param name="key">The cache key</param>
        /// <param name="value">The cache value</param>
        /// <returns>A new <see cref="CacheEntry"/> instance</returns>
        internal static CacheEntry CreateNew(object key, object value)
        {
            return new CacheEntry(key, value);
        }

        #endregion
    }
}