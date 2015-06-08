// <copyright file="AuditEventParticipantNetwork.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//    <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Fhir.Resources.Security
{
    #region Imports.

    using Newtonsoft.Json;
    using ProtoBuf;
    using ValueSets;

    #endregion

    /// <summary>
    /// Logical network location for application activity, if the activity has a network 
    /// location.
    /// <externalLink>
    ///     <linkText>Network</linkText>
    ///     <linkUri>
    ///         http://hl7-fhir.github.io/securityevent-definitions.html#AuditEvent.participant.network
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    [FhirVersion(Fhir.V050)]
    [ProtoContract]
    public sealed class AuditEventParticipantNetwork : Element
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditEventParticipantNetwork"/> class.
        /// </summary>
        /// <param name="identifier">
        /// An identifier for the network access point of the user device for the audit 
        /// event
        /// </param>
        /// <param name="type">
        /// An identifier for the type of network access point that originated the audit 
        /// event, see <c>AuditEventParticipantNetworkType</c>.
        /// </param>
        public AuditEventParticipantNetwork(string identifier, AuditEventParticipantNetworkType type)
        {
            this.Identifier = identifier;
            this.Type = type;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="AuditEventParticipantNetwork" /> class from being created.
        /// </summary>
        /// <remarks>For Json deserialization</remarks>
        [JsonConstructor]
        private AuditEventParticipantNetwork()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// An identifier for the network access point of the user device for the audit 
        /// event. This could be a device id, IP address or some other identifier associated 
        /// with a device.
        /// <externalLink>
        ///     <linkText>Network.Identifier</linkText>
        ///     <linkUri>
        ///         http://hl7-fhir.github.io/resource-definitions.html#AuditEvent.participant.network.identifier
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        [ProtoMember(1), JsonProperty]
        public string Identifier
        {
            get;
            private set;
        }

        /// <summary>
        /// An identifier for the type of network access point that originated the audit 
        /// event, see <c>AuditEventParticipantNetworkType</c>.
        /// <externalLink>
        ///     <linkText>Network.Type</linkText>
        ///     <linkUri>
        ///         http://hl7-fhir.github.io/resource-definitions.html#AuditEvent.participant.network.type
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        [ProtoMember(2), JsonProperty]
        public AuditEventParticipantNetworkType Type
        {
            get;
            private set;
        }

        #endregion
    }
}