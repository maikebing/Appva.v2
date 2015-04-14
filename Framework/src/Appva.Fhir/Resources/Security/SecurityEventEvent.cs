// <copyright file="SecurityEventEvent.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Fhir.Resources.Security
{
    #region Imports.

    using System;
    using System.Collections.ObjectModel;
    using System.Xml.Serialization;
    using Appva.Fhir.Resources.Security.ValueSets;
    using Complex;
    using Newtonsoft.Json;
    using Primitives;
    using Validation;

    #endregion

    /// <summary>
    /// Identifies the name, action type, time, and disposition of the audited event.
    /// <externalLink>
    ///     <linkText>Event</linkText>
    ///     <linkUri>
    ///         http://hl7.org/implement/standards/FHIR-Develop/securityevent-definitions.html#SecurityEvent.event
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    [FhirVersion(Fhir.V040)]
    [XmlTypeAttribute(Namespace = Fhir.Namespace)]
    public sealed class SecurityEventEvent : Element
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityEventEvent"/> class.
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
        public SecurityEventEvent(SecurityEventType type, SecurityEventAction action, SecurityEventOutcome outcome)
            : this(type, null, action, DateTime.Now, outcome, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityEventEvent"/> class.
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
        public SecurityEventEvent(SecurityEventType type, Collection<SecurityEventSubType> subtypes, SecurityEventAction action, DateTime dateTime, SecurityEventOutcome outcome, string outcomeDescription)
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
        /// Initializes a new instance of the <see cref="SecurityEventEvent"/> class.
        /// </summary>
        /// <remarks>For Json deserialization</remarks>
        [JsonConstructor]
        protected SecurityEventEvent()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// Identifier for a family of the event, see <c>SecurityEventType</c>.
        /// <externalLink>
        ///     <linkText>Type</linkText>
        ///     <linkUri>
        ///         http://hl7.org/implement/standards/FHIR-Develop/securityevent-definitions.html#SecurityEvent.event.type
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        /// <remarks>Required</remarks>
        [JsonProperty, XmlElementAttribute("type")]
        public SecurityEventType Type
        {
            get;
            private set;
        }

        /// <summary>
        /// Identifier for the category of event, see <c>SecurityEventSubType</c>.
        /// <externalLink>
        ///     <linkText>Subtype</linkText>
        ///     <linkUri>
        ///         http://hl7.org/implement/standards/FHIR-Develop/securityevent-definitions.html#SecurityEvent.event.subtype
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        [JsonProperty, XmlElementAttribute("subtype")]
        public Collection<SecurityEventSubType> Subtypes
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
        ///         http://hl7.org/implement/standards/FHIR-Develop/securityevent-definitions.html#SecurityEvent.event.action
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        /// <remarks>Required</remarks>
        [JsonProperty, XmlElementAttribute("action")]
        public SecurityEventAction Action
        {
            get;
            private set;
        }

        /// <summary>
        /// The time when the event occurred on the source.
        /// <externalLink>
        ///     <linkText>DateTime</linkText>
        ///     <linkUri>
        ///         http://hl7.org/implement/standards/FHIR-Develop/securityevent-definitions.html#SecurityEvent.event.dateTime
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        /// <remarks>Required</remarks>
        [JsonProperty, XmlElementAttribute("dateTime")]
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
        ///         http://hl7.org/implement/standards/FHIR-Develop/securityevent-definitions.html#SecurityEvent.event.outcome
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        /// <remarks>
        /// Required.
        /// In some cases a "success" may be partial, for example, an incomplete or 
        /// interrupted transfer of a radiological study. For the purpose of establishing 
        /// accountability, these distinctions are not relevant.
        /// </remarks>
        [JsonProperty, XmlElementAttribute("outcome")]
        public SecurityEventOutcome Outcome
        {
            get;
            private set;
        }

        /// <summary>
        /// A free text description of the outcome of the event.
        /// <externalLink>
        ///     <linkText>OutcomeDesc</linkText>
        ///     <linkUri>
        ///         http://hl7.org/implement/standards/FHIR-Develop/securityevent-definitions.html#SecurityEvent.event.outcomeDesc
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        [JsonProperty, XmlElementAttribute("outcomeDesc")]
        public string OutcomeDescription
        {
            get;
            private set;
        }

        #endregion
    }
}
