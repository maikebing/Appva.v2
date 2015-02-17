// <copyright file="ITenantClient.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Apis.TenantServer
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using Appva.Apis.TenantServer.Contracts;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface ITenantClient
    {
        /// <summary>
        /// Returns a tenant by id.
        /// </summary>
        /// <param name="id">The tenant id</param>
        /// <returns>A <see cref="Tenant"/></returns>
        Tenant Get(object id);

        /// <summary>
        /// Returns a collection of <see cref="Tenant"/>.
        /// </summary>
        /// <returns>A collection of <see cref="Tenant"/></returns>
        IList<Tenant> ListAll();

        /// <summary>
        /// Returns a client by tenant id.
        /// </summary>
        /// <param name="id">The tenant id</param>
        /// <returns>A <see cref="Client"/></returns>
        Client GetClientByTenantId(object id);
    }
}