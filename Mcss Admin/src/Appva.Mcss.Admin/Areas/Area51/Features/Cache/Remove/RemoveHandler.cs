// <copyright file="RemoveHandler.cs" company="Appva AB">
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
    public sealed class RemoveHandler : RequestHandler<RemoveCacheKey, bool>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IRuntimeMemoryCache"/>.
        /// </summary>
        private readonly IRuntimeMemoryCache cache;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveHandler"/> class.
        /// </summary>
        /// <param name="cache">The <see cref="IRuntimeMemoryCache"/> implementation</param>
        public RemoveHandler(IRuntimeMemoryCache cache)
        {
            this.cache = cache;
        }

        #endregion

        #region RequestHandler<ListAccountCommand,ListAccountModel> Overrides.

        /// <inheritdoc />
        public override bool Handle(RemoveCacheKey message)
        {
            return this.cache.Remove(message.CacheKey);
        }

        #endregion
    }
}