// <copyright file="ITenantClient.cs" company="Appva AB">
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
    /// Represents the tenant server client interface.
    /// </summary>
    public interface ITenantClient : ITenantClientAsync, IDisposable
    {
        /// <summary>
        /// Returns a tenant by internal id.
        /// </summary>
        /// <param name="id">The tenant internal id</param>
        /// <returns>A <see cref="ITenantDto{TId, TIdentifier}"/></returns>
        ITenantDto Find(Guid id);

        /// <summary>
        /// Returns a tenant by identifier.
        /// </summary>
        /// <param name="id">The tenant identifier</param>
        /// <returns>A <see cref="ITenantDto{TId, TIdentifier}"/></returns>
        ITenantDto FindByIdentifier(string id);

        /// <summary>
        /// Returns a collection of <see cref="ITenantDto{TId, TIdentifier}"/>.
        /// </summary>
        /// <returns>A collection of <see cref="ITenantDto{TId, TIdentifier}"/></returns>
        IList<ITenantDto> List();

        /// <summary>
        /// Returns a client by tenant internal id asyncronous.
        /// </summary>
        /// <param name="id">The tenant internal id</param>
        /// <returns>A <see cref="IClient"/></returns>
        IClientDto FindClientByTenantId(Guid id);
    }
}