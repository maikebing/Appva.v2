// <copyright file="SecurityEvent.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Fhir.Resources.Security
{
    #region Imports.

    using System.Xml.Serialization;
    using Validation;

    #endregion

    /// <summary>
    /// Event record kept for security purposes - a record of an event made for purposes 
    /// of maintaining a security log. Typical uses include detection of intrusion 
    /// attempts and monitoring for inappropriate usage.
    /// <externalLink>
    ///     <linkText>SecurityEvent</linkText>
    ///     <linkUri>http://hl7.org/implement/standards/FHIR-Develop/securityevent.html</linkUri>
    /// </externalLink>
    /// </summary>
    [FhirVersion(Fhir.V040)]
    [XmlRoot(Namespace = Fhir.Namespace)]
    public sealed class SecurityEvent : DomainResource
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityEvent"/> class.
        /// </summary>
        /// <param name="event">
        /// Identifies the name, action type, time, and disposition of the audited event
        /// </param>
        /// <param name="participant">
        /// A person, a hardware device or software process
        /// </param>
        /// <param name="source">
        /// Application systems and processes
        /// </param>
        /// <param name="object">
        /// Specific instances of data or objects that have been accessed
        /// </param>
        public SecurityEvent(SecurityEventEvent @event, SecurityEventParticipant participant, SecurityEventSource source, SecurityEventObject @object)
        {
            Requires.NotNull(@event, "event");
            Requires.NotNull(participant, "participant");
            Requires.NotNull(source, "source");
            Requires.NotNull(@object, "object");
            this.Event = @event;
            this.Participant = participant;
            this.Source = source;
            this.Object = @object;
        }

        #endregion

        #region Properties.

        /// <summary>
        /// Identifies the name, action type, time, and disposition of the audited event.
        /// </summary>
        [XmlElementAttribute("event")]
        public SecurityEventEvent Event
        {
            get;
            private set;
        }

        /// <summary>
        /// A person, a hardware device or software process.
        /// </summary>
        [XmlElementAttribute("participant")]
        public SecurityEventParticipant Participant
        {
            get;
            private set;
        }

        /// <summary>
        /// Application systems and processes.
        /// </summary>
        [XmlElementAttribute("source")]
        public SecurityEventSource Source
        {
            get;
            private set;
        }

        /// <summary>
        /// Specific instances of data or objects that have been accessed.
        /// </summary>
        [XmlElementAttribute("object")]
        public SecurityEventObject Object
        {
            get;
            private set;
        }

        #endregion
    }
}