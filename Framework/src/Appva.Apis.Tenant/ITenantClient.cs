// <copyright file="ITenantClient.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Apis.TenantServer
{
    #region Imports.

    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Contracts;

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
        /// Returns a tenant by id asyncronous.
        /// </summary>
        /// <param name="id">The tenant id</param>
        /// <returns>A <see cref="Tenant"/></returns>
        Task<Tenant> GetAsync(object id);

        /// <summary>
        /// Returns a collection of <see cref="Tenant"/>.
        /// </summary>
        /// <returns>A collection of <see cref="Tenant"/></returns>
        IList<Tenant> ListAll();

        /// <summary>
        /// Returns a collection of <see cref="Tenant"/> asyncronous.
        /// </summary>
        /// <returns>A collection of <see cref="Tenant"/></returns>
        Task<IList<Tenant>> ListAllAsync();

        /// <summary>
        /// Returns a client by tenant id.
        /// </summary>
        /// <param name="id">The tenant id</param>
        /// <returns>A <see cref="Client"/></returns>
        Client GetClientByTenantId(object id);

        /// <summary>
        /// Returns a client by tenant id asyncronous.
        /// </summary>
        /// <param name="id">The tenant id</param>
        /// <returns>A <see cref="Client"/></returns>
        Task<Client> GetClientByTenantIdAsync(object id);
    }
}