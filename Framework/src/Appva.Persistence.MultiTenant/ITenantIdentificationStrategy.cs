// <copyright file="ITenantIdentificationStrategy.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Persistence.MultiTenant
{
    #region Imports.

    using System;
    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// The tenant identification strategy handler.
    /// </summary>
    public interface ITenantIdentificationStrategy
    {
        /// <summary>
        /// Attempts to identify a tenant.
        /// </summary>
        /// <param name="tenant">The tenant instance or null if not found</param>
        /// <returns>True if identification was successful</returns>
        bool TryIdentifyTenant(out ITenant tenant);
    }
}