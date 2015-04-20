// <copyright file="ITenantClientAsync.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Tenant.Interoperability.Client
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    #endregion

    /// <summary>
    /// Represents the tenant server client asynchronous interface.
    /// </summary>
    public interface ITenantClientAsync
    {
        /// <summary>
        /// Returns a tenant by internal id asyncronous.
        /// </summary>
        /// <param name="id">The tenant internal id</param>
        /// <returns>A <see cref="ITenantDto"/></returns>
        Task<ITenantDto> FindAsync(Guid id);

        /// <summary>
        /// Returns a tenant by identifier asyncronous.
        /// </summary>
        /// <param name="id">The tenant identifier</param>
        /// <returns>A <see cref="ITenantDto"/></returns>
        Task<ITenantDto> FindByIdentifierAsync(string id);

        /// <summary>
        /// Returns a collection of <see cref="ITenantDto"/> asyncronous.
        /// </summary>
        /// <returns>A collection of <see cref="ITenantDto"/></returns>
        Task<IList<ITenantDto>> ListAsync();

        /// <summary>
        /// Returns a client by tenant internal id asyncronous.
        /// </summary>
        /// <param name="id">The tenant internal id</param>
        /// <returns>A <see cref="IClient"/></returns>
        Task<IClientDto> FindClientByTenantIdAsync(Guid id);
    }
}