// <copyright file="ListCache.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Features.Area51.Cache
{
    #region Imports.

    using System.Collections.Generic;
    using Appva.Caching;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ListCache
    {
        /// <summary>
        /// The cache entries.
        /// </summary>
        public IEnumerable<CacheEntry> Items
        {
            get;
            set;
        }
    }
}