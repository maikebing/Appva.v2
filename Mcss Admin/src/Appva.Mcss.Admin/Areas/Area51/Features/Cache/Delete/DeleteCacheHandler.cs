// <copyright file="DeleteCacheHandler.cs" company="Appva AB">
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

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class DeleteCacheHandler : RequestHandler<DeleteCache, bool>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IRuntimeMemoryCache"/>.
        /// </summary>
        private readonly IRuntimeMemoryCache cache;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteCacheHandler"/> class.
        /// </summary>
        /// <param name="cache">The <see cref="IRuntimeMemoryCache"/> implementation</param>
        public DeleteCacheHandler(IRuntimeMemoryCache cache)
        {
            this.cache = cache;
        }

        #endregion

        #region RequestHandler<ListAccountCommand,ListAccountModel> Overrides.

        /// <inheritdoc />
        public override bool Handle(DeleteCache message)
        {
            return this.cache.Remove(message.CacheKey);
        }

        #endregion
    }
}