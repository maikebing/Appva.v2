﻿// <copyright file="CacheItem.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Caching
{
    #region Imports.

    using System;
    using Logging;

    #endregion

    /// <summary>
    /// Implementation of <see cref="ICacheItem"/>.
    /// </summary>
    internal sealed class CacheItem : ICacheItem
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ILog"/> for <see cref="CacheItem"/>.
        /// </summary>
        private static readonly ILog Log = LogProvider.For<CacheItem>();

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheItem"/> class.
        /// </summary>
        /// <param name="key">The unique key which is used for key value pair lookup</param>
        /// <param name="value">The value to be cached</param>
        private CacheItem(object key, object value)
        {
            var utcNow = DateTime.UtcNow.Ticks;
            this.Key = key;
            this.Value = value;
            this.Hits = 0L;
            this.CreatedAt = utcNow;
            this.ModifiedAt = utcNow;
            this.AccessedAt = utcNow;
        }

        #endregion

        #region ICacheItem Members.

        /// <inheritdoc />
        public object Key
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public object Value
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public long Hits
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public long CreatedAt
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public long ModifiedAt
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public long AccessedAt
        {
            get;
            private set;
        }

        #endregion

        #region Public static Functions.

        /// <summary>
        /// Creates a new <see cref="CacheItem"/> instance.
        /// </summary>
        /// <param name="key">The unique key which is used for key value pair lookup</param>
        /// <param name="value">The value to be cached</param>
        /// <returns>A new <see cref="ICacheItem"/> instance</returns>
        public static ICacheItem CreateNew(object key, object value)
        {
            Log.DebugFormat(Debug.Messages.CacheItemConstructorInitialization, key, value);
            return new CacheItem(key, value);
        }

        #endregion

        #region ICacheItem Members.

        /// <inheritdoc />
        public void UpdateValue(object value)
        {
            this.Value = value;
            this.ModifiedAt = DateTime.UtcNow.Ticks;
            Log.DebugFormat(Debug.Messages.CacheItemUpdateValue, value);
        }

        /// <inheritdoc />
        public void UpdateHit()
        {
            this.Hits += 1;
            this.AccessedAt = DateTime.UtcNow.Ticks;
            Log.DebugFormat(Debug.Messages.CacheItemUpdateHit, this.Hits);
        }

        #endregion
    }
}
