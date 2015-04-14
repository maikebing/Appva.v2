// <copyright file="Tenant.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Apis.TenantServer.Contracts
{
    #region Imports.

    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// The tenant model contract.
    /// </summary>
    public sealed class Tenant
    {
        #region Public Properties.

        /// <summary>
        /// The tenant id.
        /// </summary>
        [JsonProperty("id")]
        public string Id
        {
            get;
            set;
        }

        /// <summary>
        /// The tenant identifier.
        /// </summary>
        [JsonProperty("identifier")]
        public string Identifier
        {
            get;
            set;
        }

        /// <summary>
        /// The tenant database connection.
        /// </summary>
        [JsonProperty("connection_string")]
        public string ConnectionString
        {
            get;
            set;
        }

        /// <summary>
        /// The tenant name.
        /// </summary>
        [JsonProperty("name")]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// The tenant host.
        /// </summary>
        [JsonProperty("host_name")]
        public string HostName
        {
            get;
            set;
        }

        #endregion
    }
}
