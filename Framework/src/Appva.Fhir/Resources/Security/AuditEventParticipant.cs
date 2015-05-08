// <copyright file="AuditEventParticipant.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Fhir.Resources.Security
{
    #region Imports.

    using System.Collections.Generic;
    using Complex;
    using Newtonsoft.Json;
    using ProtoBuf;

    #endregion

    /// <summary>
    /// A person, a hardware device or software process.
    /// <externalLink>
    ///     <linkText>Participant</linkText>
    ///     <linkUri>
    ///         http://hl7-fhir.github.io/securityevent-definitions.html#AuditEvent.participant
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    [FhirVersion(Fhir.V050)]
    [ProtoContract]
    public sealed class AuditEventParticipant : Element
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditEventParticipant"/> class.
        /// </summary>
        /// <param name="roles">
        /// Specification of the role(s) the user plays when performing the event
        /// </param>
        /// <param name="userId">
        /// Unique identifier for the user actively participating in the event
        /// </param>
        /// <param name="alternativeId">
        /// Alternative participant identifier
        /// </param>
        /// <param name="name">
        /// Human-meaningful name for the user
        /// </param>
        /// <param name="isRequestor">
        /// Indicator that the user is or is not the requestor, or initiator, for the event 
        /// being audited
        /// </param>
        /// <param name="media">
        /// Type of media involved. Used when the event is about exporting/importing onto 
        /// media
        /// </param>
        /// <param name="network">
        /// Logical network location for application activity, if the activity has a network 
        /// location
        /// </param>
        public AuditEventParticipant(IEnumerable<CodeableConcept> roles, string userId, string alternativeId, string name, bool isRequestor, Coding media, AuditEventParticipantNetwork network)
        {
            this.Roles = roles;
            this.UserId = userId;
            this.AlternativeId = alternativeId;
            this.Name = name;
            this.IsRequestor = isRequestor;
            this.Media = media;
            this.Network = network;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="AuditEventParticipant" /> class from being created.
        /// </summary>
        /// <remarks>For Json deserialization</remarks>
        [JsonConstructor]
        private AuditEventParticipant()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// Specification of the role(s) the user plays when performing the event. Usually 
        /// the codes used in this element are local codes defined by the role-based access 
        /// control security system used in the local context.
        /// <externalLink>
        ///     <linkText>Role</linkText>
        ///     <linkUri>
        ///         http://hl7-fhir.github.io/securityevent-definitions.html#AuditEvent.participant.role
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        /// <remarks>This value ties an audited event to a user's role(s). It is an 
        /// optional value that might be used to group events for analysis by user 
        /// functional role categories.
        /// </remarks>
        [ProtoMember(1), JsonProperty]
        public IEnumerable<CodeableConcept> Roles
        {
            get;
            private set;
        }

        /// <summary>
        /// Direct reference to a resource that identifies the participant.
        /// <externalLink>
        ///     <linkText>Reference</linkText>
        ///     <linkUri>
        ///         http://hl7-fhir.github.io/securityevent-definitions.html#AuditEvent.participant.reference
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        /// <remarks>
        /// Inv-3: Either a userId or a reference, but not both (xpath: exists(f:userId) != 
        /// exists(f:reference))
        /// </remarks>
        /*[XmlElementAttribute(Name = "reference")]
        public Reference Reference
        {
            get;
            set;
        }*/

        /// <summary>
        /// Unique identifier for the user actively participating in the event.
        /// <externalLink>
        ///     <linkText>UserId</linkText>
        ///     <linkUri>
        ///         http://hl7-fhir.github.io/securityevent-definitions.html#AuditEvent.participant.userId
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        [ProtoMember(2), JsonProperty]
        public string UserId
        {
            get;
            private set;
        }

        /// <summary>
        /// Alternative participant identifier. For a human, this should be a user 
        /// identifier text string from authentication system. This identifier would be one 
        /// known to a common authentication system (e.g., single sign-on), if available.
        /// <externalLink>
        ///     <linkText>AlternativeId</linkText>
        ///     <linkUri>
        ///         http://hl7-fhir.github.io/securityevent-definitions.html#AuditEvent.participant.altId
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        /// <remarks>
        /// In some situations a human user may authenticate with one identity but, to access 
        /// a specific application system, may use a synonymous identify. For example, some 
        /// "single sign on" implementations will do this. The alternative identifier would 
        /// then be the original identify used for authentication, and the User ID is the one 
        /// known to and used by the application.
        /// </remarks>
        [ProtoMember(3), JsonProperty("altId")]
        public string AlternativeId
        {
            get;
            private set;
        }

        /// <summary>
        /// Human-meaningful name for the user.
        /// <externalLink>
        ///     <linkText>Name</linkText>
        ///     <linkUri>
        ///         http://hl7-fhir.github.io/securityevent-definitions.html#AuditEvent.participant.name
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        /// <remarks>
        /// The User ID and Authorization User ID may be internal or otherwise obscure 
        /// values. This field assists the auditor in identifying the actual user.
        /// </remarks>
        [ProtoMember(4), JsonProperty]
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// Indicator that the user is or is not the requestor, or initiator, for the event 
        /// being audited.
        /// <externalLink>
        ///     <linkText>Requestor</linkText>
        ///     <linkUri>
        ///         http://hl7-fhir.github.io/securityevent-definitions.html#AuditEvent.participant.requestor
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        /// <remarks>
        /// There can only be one initiator. If the initiator is not clear, then do not 
        /// choose any one participant as the initiator.
        /// </remarks>
        [ProtoMember(5), JsonProperty("requestor")]
        public bool IsRequestor
        {
            get;
            private set;
        }

        /// <summary>
        /// Type of media involved. Used when the event is about exporting/importing onto 
        /// media.
        /// <externalLink>
        ///     <linkText>Media</linkText>
        ///     <linkUri>
        ///         http://hl7-fhir.github.io/securityevent-definitions.html#AuditEvent.participant.media
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        [ProtoMember(6), JsonProperty]
        public Coding Media
        {
            get;
            private set;
        }

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
        [ProtoMember(7), JsonProperty]
        public AuditEventParticipantNetwork Network
        {
            get;
            private set;
        }

        #endregion
    }
}