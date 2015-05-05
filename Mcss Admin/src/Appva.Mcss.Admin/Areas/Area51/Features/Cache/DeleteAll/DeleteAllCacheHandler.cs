// <copyright file="DeleteAllCacheHandler.cs" company="Appva AB">
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
    using Appva.Caching.Providers;
    using Appva.Cqrs;
using Appva.Mcss.Admin.Areas.Area51.Features.Cache.DeleteAll;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class DeleteAllCacheHandler : RequestHandler<DeleteAllCache, bool>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IRuntimeMemoryCache"/>.
        /// </summary>
        private readonly IRuntimeMemoryCache cache;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteAllCacheHandler"/> class.
        /// </summary>
        /// <param name="cache">The <see cref="IRuntimeMemoryCache"/> implementation</param>
        public DeleteAllCacheHandler(IRuntimeMemoryCache cache)
        {
            this.cache = cache;
        }

        #endregion

        #region RequestHandler<DeleteAllCache, bool> Overrides.

        /// <inheritdoc />
        public override bool Handle(DeleteAllCache message)
        {
            this.cache.RemoveAll();
            return true;
        }

        #endregion
    }
}