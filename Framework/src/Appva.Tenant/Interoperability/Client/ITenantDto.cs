// <copyright file="ITenantDto.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Tenant.Interoperability.Client
{
    #region Imports.

    using System;

    #endregion

    /// <summary>
    /// Representation of a tenant DTO.
    /// </summary>
    public interface ITenantDto
    {
        /// <summary>
        /// The ID.
        /// </summary>
        Guid Id
        {
            get;
        }

        /// <summary>
        /// The unique identifier.
        /// </summary>
        string Identifier
        {
            get;
        }

        /// <summary>
        /// The user-friendly name.
        /// </summary>
        string Name
        {
            get;
        }

        /// <summary>
        /// The host name.
        /// </summary>
        string HostName
        {
            get;
        }

        /// <summary>
        /// The connection string.
        /// </summary>
        string ConnectionString
        {
            get;
        }
    }
}