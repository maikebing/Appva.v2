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
    public sealed class SecurityEventParticipantNetworkType : Code
    {
        #region Variables.

        /// <summary>
        /// Machine Name, including DNS name.
        /// </summary>
        public static readonly SecurityEventParticipantNetworkType MachineName = new SecurityEventParticipantNetworkType("1");

        /// <summary>
        /// IP Address.
        /// </summary>
        public static readonly SecurityEventParticipantNetworkType IpAddress = new SecurityEventParticipantNetworkType("2");

        /// <summary>
        /// Telephone Number.
        /// </summary>
        public static readonly SecurityEventParticipantNetworkType TelephoneNumber = new SecurityEventParticipantNetworkType("3");

        /// <summary>
        /// Email address.
        /// </summary>
        public static readonly SecurityEventParticipantNetworkType EmailAddress = new SecurityEventParticipantNetworkType("4");

        /// <summary>
        /// URI (User directory, HTTP-PUT, ftp etc).
        /// </summary>
        public static readonly SecurityEventParticipantNetworkType Uri = new SecurityEventParticipantNetworkType("5");

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityEventParticipantNetworkType"/> class.
        /// </summary>
        /// <param name="value">The string code value</param>
        private SecurityEventParticipantNetworkType(string value) 
            : base(value)
        {
        }

        #endregion
    }
}