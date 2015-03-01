// <copyright file="SecurityEventParticipantNetworkType.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Fhir.Resources.Security.ValueSets
{
    #region Imports.

    using Primitives;

    #endregion

    /// <summary>
    ///  The type of network access point that originated the audit event.
    /// <externalLink>
    ///     <linkText>1.15.2.1.164.1 SecurityEventParticipantNetworkType</linkText>
    ///     <linkUri>
    ///         http://hl7.org/implement/standards/FHIR-Develop/network-type.html
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    public static class SecurityEventParticipantNetworkType
    {
        /// <summary>
        /// Machine Name, including DNS name.
        /// </summary>
        public static readonly Code MachineName = new Code("1");

        /// <summary>
        /// IP Address.
        /// </summary>
        public static readonly Code IpAddress = new Code("2");

        /// <summary>
        /// Telephone Number.
        /// </summary>
        public static readonly Code TelephoneNumber = new Code("3");

        /// <summary>
        /// Email address.
        /// </summary>
        public static readonly Code EmailAddress = new Code("4");

        /// <summary>
        /// URI (User directory, HTTP-PUT, ftp etc).
        /// </summary>
        public static readonly Code Uri = new Code("5");
    }
}