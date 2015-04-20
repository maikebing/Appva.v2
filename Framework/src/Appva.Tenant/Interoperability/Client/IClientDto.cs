// <copyright file="IClientDto.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Tenant.Interoperability.Client
{
    /// <summary>
    /// Representation of a client DTO.
    /// </summary>
    public interface IClientDto
    {
        /// <summary>
        /// The client identifier.
        /// </summary>
        string Identifier
        {
            get;
        }

        /// <summary>
        /// The client secret.
        /// </summary>
        string Secret
        {
            get;
        }
    }
}