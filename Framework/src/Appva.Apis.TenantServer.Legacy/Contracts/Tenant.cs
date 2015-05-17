// <copyright file="Tenant.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Apis.TenantServer.Legacy.Contracts
{
    #region Imports.

    using System;
    using Appva.Tenant.Interoperability.Client;

    #endregion

    /// <summary>
    /// The tenant model contract.
    /// </summary>
    internal sealed class Tenant : ITenantDto
    {
        #region ITenantDto Members.

        /// <summary>
        /// The tenant id.
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// The tenant identifier.
        /// </summary>
        public string Identifier
        {
            get;
            set;
        }

        /// <summary>
        /// The tenant database connection.
        /// </summary>
        public string ConnectionString
        {
            get;
            set;
        }

        /// <summary>
        /// The tenant name.
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// The tenant host.
        /// </summary>
        public string HostName
        {
            get;
            set;
        }

        #endregion
    }
}
