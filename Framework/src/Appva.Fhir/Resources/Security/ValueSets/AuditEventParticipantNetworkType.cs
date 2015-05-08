// <copyright file="AuditEventParticipantNetworkType.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Fhir.Resources.Security.ValueSets
{
    #region Imports.

    using Newtonsoft.Json;
    using Primitives;
    using ProtoBuf;

    #endregion

    /// <summary>
    ///  The type of network access point that originated the audit event.
    /// <externalLink>
    ///     <linkText>1.15.2.1.164.1 AuditEventParticipantNetworkType</linkText>
    ///     <linkUri>
    ///         http://hl7-fhir.github.io/network-type.html
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    [FhirVersion(Fhir.V050)]
    [ProtoContract]
    public sealed class AuditEventParticipantNetworkType : Code
    {
        #region Variables.

        /// <summary>
        /// Machine Name, including DNS name.
        /// </summary>
        public static readonly AuditEventParticipantNetworkType MachineName = new AuditEventParticipantNetworkType("1");

        /// <summary>
        /// IP Address.
        /// </summary>
        public static readonly AuditEventParticipantNetworkType IpAddress = new AuditEventParticipantNetworkType("2");

        /// <summary>
        /// Telephone Number.
        /// </summary>
        public static readonly AuditEventParticipantNetworkType TelephoneNumber = new AuditEventParticipantNetworkType("3");

        /// <summary>
        /// Email address.
        /// </summary>
        public static readonly AuditEventParticipantNetworkType EmailAddress = new AuditEventParticipantNetworkType("4");

        /// <summary>
        /// URI (User directory, HTTP-PUT, ftp etc).
        /// </summary>
        public static readonly AuditEventParticipantNetworkType Uri = new AuditEventParticipantNetworkType("5");

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditEventParticipantNetworkType"/> class.
        /// </summary>
        /// <param name="value">The string code value</param>
        private AuditEventParticipantNetworkType(string value) 
            : base(value)
        {
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="AuditEventParticipantNetworkType" /> class from being created.
        /// </summary>
        /// <remarks>For Json deserialization</remarks>
        [JsonConstructor]
        private AuditEventParticipantNetworkType() 
            : base(null)
        {
        }

        #endregion
    }
}