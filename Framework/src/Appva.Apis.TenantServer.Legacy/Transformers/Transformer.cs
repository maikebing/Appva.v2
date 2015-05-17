// <copyright file="Transformer.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>

using Appva.Apis.TenantServer.Legacy.Service_References.Wcf;

namespace Appva.Apis.TenantServer.Legacy.Transformers
{
    #region Imports.

    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Logging;
    using Tenant.Interoperability.Client;
    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// Internal DTO transformer.
    /// </summary>
    internal static class Transformer
    {
        /// <summary>
        /// The <see cref="ILog"/>.
        /// </summary>
        private static readonly ILog Log = LogProvider.For<TenantWcfClient>();

        /// <summary>
        /// Maps a collection of <see cref="TenantDto"/> to its equivalent part.
        /// </summary>
        /// <param name="dtos">The collection to be mapped</param>
        /// <returns>A collection of <see cref="Tenant"/></returns>
        public static IList<ITenantDto> Transform(IList<TenantDto> dtos)
        {
            return dtos == null ? new List<ITenantDto>() : dtos.Select(Transform).ToList();
        }

        /// <summary>
        /// Maps a single <see cref="TenantDto"/> to its equivalent part.
        /// </summary>
        /// <param name="dto">The entry to be mapped</param>
        /// <returns>A <see cref="Tenant"/> or null</returns>
        public static ITenantDto Transform(TenantDto dto)
        {
            if (Log.IsDebugEnabled())
            {
                Log.Debug(JsonConvert.SerializeObject(dto));
            }
            return dto == null ? null : new Tenant
            {
                Id = dto.Id,
                Identifier = dto.Identifier,
                ConnectionString = FromDtoConnectionToConnectionString(dto),
                Name = dto.Name,
                HostName = dto.Hostname
            };
        }

        /// <summary>
        /// Creates a connection string from the <see cref="TenantDto"/>.
        /// </summary>
        /// <param name="dto">The entry to extract the connection string from</param>
        /// <returns>A connection string</returns>
        public static string FromDtoConnectionToConnectionString(TenantDto dto)
        {
            if (dto.Connection == null)
            {
                return string.Empty;
            }
            if (! string.IsNullOrEmpty(dto.Connection.FailOverPartner))
            {
                dto.Connection.FailOverPartner = string.Format(
                    "Failover Partner={0};",
                    dto.Connection.FailOverPartner);
            }
            return string.Format(
                    @"Server={0};{1}Database={2};Trusted_Connection={3};User ID={4};Password={5}",
                    dto.Connection.Server,
                    dto.Connection.FailOverPartner,
                    dto.Connection.Database,
                    dto.Connection.TrustedConnection,
                    dto.Connection.User,
                    dto.Connection.Password);
        }
    }
}