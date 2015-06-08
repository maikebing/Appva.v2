// <copyright file="AuditEventSource.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Fhir.Resources.Security
{
    #region Imports.

    using Newtonsoft.Json;
    using ProtoBuf;
    using Validation;
    using ValueSets;

    #endregion

    /// <summary>
    /// Application systems and processes. 
    /// Since multi-tier, distributed, or composite applications make source 
    /// identification ambiguous, this collection of fields may repeat for each 
    /// application or process actively involved in the event. For example, multiple 
    /// value-sets can identify participating web servers, application processes, and 
    /// database server threads in an n-tier distributed application. Passive event 
    /// participants, e.g., low-level network transports, need not be identified.
    /// <externalLink>
    ///     <linkText>Source</linkText>
    ///     <linkUri>
    ///         http://hl7-fhir.github.io/securityevent-definitions.html#AuditEvent.source
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    [FhirVersion(Fhir.V050)]
    [ProtoContract]
    public sealed class AuditEventSource : Element
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditEventSource"/> class.
        /// </summary>
        /// <param name="site">
        /// Logical source location within the healthcare enterprise network
        /// </param>
        /// <param name="identifier">
        /// Identifier of the source where the event originated
        /// </param>
        /// <param name="type">
        /// Code specifying the type of source where event originated, see 
        /// <c>AuditEventSourceType</c>.
        /// </param>
        public AuditEventSource(string site, string identifier, AuditEventSourceType type)
        {
            Requires.NotNull(identifier, "identifier");
            this.Site = site;
            this.Identifier = identifier;
            this.Type = type;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="AuditEventSource" /> class from being created.
        /// </summary>
        /// <remarks>For Json deserialization</remarks>
        [JsonConstructor]
        private AuditEventSource()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// Logical source location within the healthcare enterprise network. A hospital or 
        /// other provider location within a multi-entity provider group.
        /// <externalLink>
        ///     <linkText>Site</linkText>
        ///     <linkUri>
        ///         http://hl7-fhir.github.io/securityevent-definitions.html#AuditEvent.source.site
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        [ProtoMember(1), JsonProperty]
        public string Site
        {
            get;
            set;
        }

        /// <summary>
        /// Identifier of the source where the event originated. This field ties the event 
        /// to a specific source system. It may be used to group events for analysis 
        /// according to where the event occurred.
        /// <externalLink>
        ///     <linkText>Identifier</linkText>
        ///     <linkUri>
        ///         http://hl7-fhir.github.io/securityevent-definitions.html#AuditEvent.source.identifier
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        [ProtoMember(2), JsonProperty]
        public string Identifier
        {
            get;
            set;
        }

        /// <summary>
        /// Code specifying the type of source where event originated, see 
        /// <c>AuditEventSourceType</c>.
        /// <externalLink>
        ///     <linkText>Type</linkText>
        ///     <linkUri>
        ///         http://hl7-fhir.github.io/securityevent-definitions.html#AuditEvent.source.type
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        [ProtoMember(3), JsonProperty]
        public AuditEventSourceType Type
        {
            get;
            set;
        }

        #endregion
    }
}