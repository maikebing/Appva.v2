// <copyright file="CacheItem.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Caching
{
    /// <summary>
    /// Implementation of <see cref="ICacheItem"/>.
    /// </summary>
    public sealed class CacheItem : ICacheItem
    {
        /// <inheritdoc />
        public object Key
        {
            get;
            set;
        }

        /// <inheritdoc />
        public object Value
        {
            get;
            set;
        }

        /// <inheritdoc />
        public long Hits
        {
            get;
            set;
        }

        /// <inheritdoc />
        public long Created
        {
            get;
            set;
        }

        /// <inheritdoc />
        public long LastModified
        {
            get;
            set;
        }

        /// <inheritdoc />
        public long LastAccessed
        {
            get;
            set;
        }
    }
}