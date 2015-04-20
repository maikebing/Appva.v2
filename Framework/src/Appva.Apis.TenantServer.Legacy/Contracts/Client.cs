// <copyright file="Client.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Apis.TenantServer.Contracts
{
    #region Imports.

    using Appva.Tenant.Interoperability.Client;

    #endregion

    /// <summary>
    /// The client model contract.
    /// </summary>
    internal sealed class Client : IClientDto
    {
        #region IClientDto Members.

        /// <summary>
        /// The client identifier.
        /// </summary>
        public string Identifier
        {
            get;
            set;
        }

        /// <summary>
        /// The client secret.
        /// </summary>
        public string Secret
        {
            get;
            set;
        }

        #endregion
    }
}
