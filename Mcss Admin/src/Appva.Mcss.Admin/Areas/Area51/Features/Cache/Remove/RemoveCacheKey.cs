// <copyright file="RemoveCacheKey.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Area51.Features.Cache.Remove
{
    #region Imports.

    using System;
    using System.Collections.Generic;
using Appva.Cqrs;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class RemoveCacheKey : IRequest<bool>
    {
        /// <summary>
        /// 
        /// </summary>
        public string CacheKey
        {
            get;
            set;
        }
    }
}