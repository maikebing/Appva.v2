// <copyright file="SecurityEventParticipantNetwork.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//    <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Fhir.Resources.Security
{
    #region Imports.

    using System.Xml.Serialization;
    using Appva.Fhir.Primitives;

    #endregion

    /// <summary>
    /// Logical network location for application activity, if the activity has a network 
    /// location.
    /// <externalLink>
    ///     <linkText>Network</linkText>
    ///     <linkUri>
    ///         http://hl7.org/implement/standards/FHIR-Develop/securityevent-definitions.html#SecurityEvent.participant.network
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    [FhirVersion(Fhir.V040)]
    [XmlTypeAttribute(Namespace = Fhir.Namespace)]
    public sealed class SecurityEventParticipantNetwork : Element
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityEventParticipantNetwork"/> class.
        /// </summary>
        /// <param name="identifier">
        /// An identifier for the network access point of the user device for the audit 
        /// event
        /// </param>
        /// <param name="type">
        /// An identifier for the type of network access point that originated the audit 
        /// event, see <see cref="SecurityEventParticipantNetworkType"/>.
        /// </param>
        public SecurityEventParticipantNetwork(string identifier, Code type)
        {
            this.Identifier = identifier;
            this.Type = type;
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
        ///         http://hl7.org/implement/standards/FHIR-Develop/resource-definitions.html#SecurityEvent.participant.network.identifier
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        [XmlElementAttribute("identifier")]
        public string Identifier
        {
            get;
            private set;
        }

        /// <summary>
        /// An identifier for the type of network access point that originated the audit 
        /// event, see <see cref="SecurityEventParticipantNetworkType"/>.
        /// <externalLink>
        ///     <linkText>Network.Type</linkText>
        ///     <linkUri>
        ///         http://hl7.org/implement/standards/FHIR-Develop/resource-definitions.html#SecurityEvent.participant.network.type
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        [XmlElementAttribute("type")]
        public Code Type
        {
            get;
            private set;
        }

        #endregion
    }
}