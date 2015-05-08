// <copyright file="AuditEvent.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Fhir.Resources.Security
{
    #region Imports.

    using System.Collections.Generic;
    using Newtonsoft.Json;
    using ProtoBuf;
    using Validation;

    #endregion

    /// <summary>
    /// Event record kept for security purposes - a record of an event made for purposes 
    /// of maintaining a security log. Typical uses include detection of intrusion 
    /// attempts and monitoring for inappropriate usage.
    /// <externalLink>
    ///     <linkText>SecurityEvent</linkText>
    ///     <linkUri>http://hl7-fhir.github.io/securityevent.html</linkUri>
    /// </externalLink>
    /// </summary>
    [FhirVersion(Fhir.V050)]
    [ProtoContract]
    public sealed class AuditEvent : DomainResource
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditEvent"/> class.
        /// </summary>
        /// <param name="evt">
        /// Identifies the name, action type, time, and disposition of the audited event
        /// </param>
        /// <param name="participants">
        /// A person, a hardware device or software process
        /// </param>
        /// <param name="source">
        /// Application systems and processes
        /// </param>
        /// <param name="objects">
        /// Specific instances of data or objects that have been accessed
        /// </param>
        public AuditEvent(AuditEventEvent evt, IList<AuditEventParticipant> participants, AuditEventSource source, IList<AuditEventObject> objects)
        {
            Requires.NotNull(evt, "evt");
            Requires.NotNull(participants, "participants");
            Requires.NotNull(source, "source");
            this.Event = evt;
            this.Participant = participants;
            this.Source = source;
            this.Object = objects;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="AuditEvent" /> class from being created.
        /// </summary>
        /// <remarks>For Json deserialization</remarks>
        [JsonConstructor]
        private AuditEvent()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// Identifies the name, action type, time, and disposition of the audited event.
        /// </summary>
        [ProtoMember(1), JsonProperty]
        public AuditEventEvent Event
        {
            get;
            private set;
        }

        /// <summary>
        /// A person, a hardware device or software process.
        /// </summary>
        [ProtoMember(2), JsonProperty]
        public IList<AuditEventParticipant> Participant
        {
            get;
            private set;
        }

        /// <summary>
        /// Application systems and processes.
        /// </summary>
        [ProtoMember(3), JsonProperty]
        public AuditEventSource Source
        {
            get;
            private set;
        }

        /// <summary>
        /// Specific instances of data or objects that have been accessed.
        /// </summary>
        [ProtoMember(4), JsonProperty]
        public IList<AuditEventObject> Object
        {
            get;
            private set;
        }

        #endregion
    }
}