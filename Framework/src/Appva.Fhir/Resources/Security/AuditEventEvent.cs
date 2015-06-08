// <copyright file="AuditEventEvent.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Fhir.Resources.Security
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Newtonsoft.Json;
    using ProtoBuf;
    using Validation;
    using ValueSets;

    #endregion

    /// <summary>
    /// Identifies the name, action type, time, and disposition of the audited event.
    /// <externalLink>
    ///     <linkText>Event</linkText>
    ///     <linkUri>
    ///         http://hl7-fhir.github.io/securityevent-definitions.html#AuditEvent.event
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    [FhirVersion(Fhir.V050)]
    [ProtoContract]
    public sealed class AuditEventEvent : Element
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditEventEvent"/> class.
        /// </summary>
        /// <param name="type">
        /// Identifier for a family of the event, see <c>SecurityEventType</c>
        /// </param>
        /// <param name="action">
        /// Indicator for type of action performed during the event that generated the 
        /// audit, see <c>SecurityEventAction</c>
        /// </param>
        /// <param name="outcome">
        /// Indicates whether the event succeeded or failed, see 
        /// <c>SecurityEventOutcome</c>
        /// </param>
        public AuditEventEvent(AuditEventType type, AuditEventAction action, AuditEventOutcome outcome)
            : this(type, null, action, DateTime.Now, outcome, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditEventEvent"/> class.
        /// </summary>
        /// <param name="type">
        /// Identifier for a family of the event, see <c>SecurityEventType</c>
        /// </param>
        /// <param name="subtypes">
        /// Identifier for the category of event, see <c>SecurityEventSubType</c>
        /// </param>
        /// <param name="action">
        /// Indicator for type of action performed during the event that generated the 
        /// audit, see <c>SecurityEventAction</c>
        /// </param>
        /// <param name="dateTime">
        /// The time when the event occurred on the source.
        /// </param>
        /// <param name="outcome">
        /// Indicates whether the event succeeded or failed, see 
        /// <c>SecurityEventOutcome</c>
        /// </param>
        /// <param name="outcomeDescription">
        /// A free text description of the outcome of the event
        /// </param>
        public AuditEventEvent(AuditEventType type, Collection<AuditEventSubType> subtypes, AuditEventAction action, DateTime dateTime, AuditEventOutcome outcome, string outcomeDescription)
        {
            Requires.NotNull(type, "type");
            Requires.NotNull(action, "action");
            Requires.NotNull(outcome, "outcome");
            this.Type = type;
            this.Subtypes = subtypes;
            this.Action = action;
            this.DateTime = dateTime;
            this.Outcome = outcome;
            this.OutcomeDescription = outcomeDescription;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="AuditEventEvent" /> class from being created.
        /// </summary>
        /// <remarks>For Json deserialization</remarks>
        [JsonConstructor]
        private AuditEventEvent()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// Identifier for a family of the event, see <c>SecurityEventType</c>.
        /// <externalLink>
        ///     <linkText>Type</linkText>
        ///     <linkUri>
        ///         http://hl7-fhir.github.io/securityevent-definitions.html#AuditEvent.event.type
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        /// <remarks>Required</remarks>
        [ProtoMember(1), JsonProperty]
        public AuditEventType Type
        {
            get;
            private set;
        }

        /// <summary>
        /// Identifier for the category of event, see <c>SecurityEventSubType</c>.
        /// <externalLink>
        ///     <linkText>Subtype</linkText>
        ///     <linkUri>
        ///         http://hl7-fhir.github.io/securityevent-definitions.html#AuditEvent.event.subtype
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        [ProtoMember(2), JsonProperty]
        public IList<AuditEventSubType> Subtypes
        {
            get;
            private set;
        }

        /// <summary>
        /// Indicator for type of action performed during the event that generated the 
        /// audit, see <c>SecurityEventAction</c>.
        /// <externalLink>
        ///     <linkText>Subtype</linkText>
        ///     <linkUri>
        ///         http://hl7-fhir.github.io/securityevent-definitions.html#AuditEvent.event.action
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        /// <remarks>Required</remarks>
        [ProtoMember(3), JsonProperty]
        public AuditEventAction Action
        {
            get;
            private set;
        }

        /// <summary>
        /// The time when the event occurred on the source.
        /// <externalLink>
        ///     <linkText>DateTime</linkText>
        ///     <linkUri>
        ///         http://hl7-fhir.github.io/securityevent-definitions.html#AuditEvent.event.dateTime
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        /// <remarks>Required</remarks>
        [ProtoMember(4), JsonProperty]
        public DateTime DateTime
        {
            get;
            private set;
        }

        /// <summary>
        /// Indicates whether the event succeeded or failed, see <c>SecurityEventOutcome</c>.
        /// <externalLink>
        ///     <linkText>Outcome</linkText>
        ///     <linkUri>
        ///         http://hl7-fhir.github.io/securityevent-definitions.html#AuditEvent.event.outcome
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        /// <remarks>
        /// Required.
        /// In some cases a "success" may be partial, for example, an incomplete or 
        /// interrupted transfer of a radiological study. For the purpose of establishing 
        /// accountability, these distinctions are not relevant.
        /// </remarks>
        [ProtoMember(5), JsonProperty]
        public AuditEventOutcome Outcome
        {
            get;
            private set;
        }

        /// <summary>
        /// A free text description of the outcome of the event.
        /// <externalLink>
        ///     <linkText>OutcomeDesc</linkText>
        ///     <linkUri>
        ///         http://hl7-fhir.github.io/securityevent-definitions.html#AuditEvent.event.outcomeDesc
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        [ProtoMember(6), JsonProperty("outcomeDesc")]
        public string OutcomeDescription
        {
            get;
            private set;
        }

        /// <summary>
        /// The purposeOfUse (reason) that was used during the event being recorded.
        /// <externalLink>
        ///     <linkText>PurposeOfEvent</linkText>
        ///     <linkUri>
        ///         http://hl7-fhir.github.io/securityevent-definitions.html#AuditEvent.event.purposeOfEvent
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        /// <remarks>
        /// Use participant.purposeOfUse when you know that is specific to the participant, 
        /// otherwise use event.purposeOfEvent. (e.g. during a machine-to-machine transfer 
        /// it might not be obvious to the audit system who caused the event, but it does 
        /// know why)
        /// </remarks>
        [ProtoMember(7), JsonProperty]
        public string PurposeOfEvent
        {
            get;
            private set;
        }

        #endregion
    }
}
