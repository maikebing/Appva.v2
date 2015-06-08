// <copyright file="AuditEventObject.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Fhir.Resources.Security
{
    #region Imports.

    using Newtonsoft.Json;
    using Primitives;
    using ProtoBuf;
    using Validation;
    using ValueSets;

    #endregion

    /// <summary>
    /// Specific instances of data or objects that have been accessed.
    /// Required unless the values for Event Identification, Active Participant 
    /// Identification, and Audit Source Identification are sufficient to document the 
    /// entire auditable event. Because events may have more than one participant 
    /// object, this group can be a repeating set of values.
    /// <externalLink>
    ///     <linkText>AuditEventObject</linkText>
    ///     <linkUri>
    ///         http://hl7-fhir.github.io/auditevent-definitions.html#AuditEvent.object
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    [FhirVersion(Fhir.V050)]
    [ProtoContract]
    public sealed class AuditEventObject : Element
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditEventObject"/> class.
        /// </summary>
        /// <param name="identifier">
        /// Identifies a specific instance of the participant object
        /// </param>
        /// <param name="type">
        /// Object type being audited, see <c>SecurityEventObjectType</c>
        /// </param>
        /// <param name="role">
        /// Code representing the functional application role of Participant Object being 
        /// audited, see <c>SecurityEventObjectRole"/></c>
        /// </param>
        /// <param name="lifeCycle">
        /// Identifier for the data life-cycle stage for the participant object, see
        /// <c>SecurityEventObjectLifecycle</c>.
        /// </param>
        /// <param name="sensitivity">
        /// Denotes policy-defined sensitivity for the Participant Object ID such as VIP, 
        /// HIV status, mental health status or similar topics, see 
        /// <c>SecurityEventObjectSensitivity</c>
        /// </param>
        /// <param name="name">
        /// An instance-specific descriptor of the Participant Object ID audited, such as a 
        /// person's name
        /// </param>
        /// <param name="description">
        /// Text that describes the object in more detail
        /// </param>
        /// <param name="query">
        /// The actual query for a query-type participant object
        /// </param>
        public AuditEventObject(
            string identifier,
            AuditEventObjectType type,
            AuditEventObjectRole role,
            AuditEventObjectLifecycle lifeCycle,
            AuditEventObjectSensitivity sensitivity, 
            string name,
            string description,
            Base64Binary query)
        {
            Requires.NotNull(identifier, "identifier");
            this.Identifier = identifier;
            this.Type = type;
            this.Role = role;
            this.Lifecycle = lifeCycle;
            this.Sensitivity = sensitivity;
            this.Name = name;
            this.Description = description;
            this.Query = query;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="AuditEventObject" /> class from being created.
        /// </summary>
        /// <remarks>For Json deserialization</remarks>
        [JsonConstructor]
        private AuditEventObject()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// Identifies a specific instance of the participant object. The reference should 
        /// always be version specific.
        /// <externalLink>
        ///     <linkText>Identifier</linkText>
        ///     <linkUri>
        ///         http://hl7-fhir.github.io/auditevent-definitions.html#AuditEvent.object.identifier
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        [ProtoMember(1), JsonProperty]
        public string Identifier
        {
            get;
            set;
        }

        /// <summary>
        /// Object type being audited, see <c>SecurityEventObjectType</c>.
        /// To describe the object being acted upon. In addition to queries on the subject 
        /// of the action in an auditable event, it is also important to be able to query on 
        /// the object type for the action.
        /// This value is distinct from the user's role or any user relationship to the 
        /// participant object.
        /// <externalLink>
        ///     <linkText>Type</linkText>
        ///     <linkUri>
        ///         http://hl7-fhir.github.io/auditevent-definitions.html#AuditEvent.object.type
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        [ProtoMember(2), JsonProperty]
        public AuditEventObjectType Type
        {
            get;
            set;
        }

        /// <summary>
        /// Code representing the functional application role of Participant Object being 
        /// audited, see <c>AuditEventObjectRole</c>.
        /// For some detailed audit analysis it may be necessary to indicate a more granular 
        /// type of participant, based on the application role it serves.
        /// See RFC 3881 for rules concerning matches between role and type.
        /// <externalLink>
        ///     <linkText>Role</linkText>
        ///     <linkUri>
        ///         http://hl7-fhir.github.io/auditevent-definitions.html#AuditEvent.object.role
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        [ProtoMember(3), JsonProperty]
        public AuditEventObjectRole Role
        {
            get;
            set;
        }

        /// <summary>
        /// Identifier for the data life-cycle stage for the participant object, see
        /// <c>AuditEventObjectLifecycle</c>.
        /// Institutional policies for privacy and security may optionally fall under 
        /// different accountability rules based on data life cycle. This provides a 
        /// differentiating value for those cases.
        /// This can be used to provide an audit trail for data, over time, as it passes 
        /// through the system.
        /// <externalLink>
        ///     <linkText>Lifecycle</linkText>
        ///     <linkUri>
        ///         http://hl7-fhir.github.io/auditevent-definitions.html#AuditEvent.object.lifecycle
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        [ProtoMember(4), JsonProperty]
        public AuditEventObjectLifecycle Lifecycle
        {
            get;
            set;
        }

        /// <summary>
        /// Denotes policy-defined sensitivity for the Participant Object ID such as VIP, 
        /// HIV status, mental health status or similar topics, see 
        /// <c>AuditEventObjectSensitivity</c>.
        /// This field identifies a specific instance of an object, such as a patient, to 
        /// detect/track privacy and security issues.
        /// Values from ATNA are institution- and implementation-defined text strings (in 
        /// sensitivity.text). HL7 defines confidentiality codes for records, documents etc. 
        /// that can also be used here.
        /// <externalLink>
        ///     <linkText>Sensitivity</linkText>
        ///     <linkUri>
        ///         http://hl7-fhir.github.io/auditevent-definitions.html#AuditEvent.object.sensitivity
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        [ProtoMember(5), JsonProperty]
        public AuditEventObjectSensitivity Sensitivity
        {
            get;
            set;
        }

        /// <summary>
        /// An instance-specific descriptor of the Participant Object ID audited, such as a 
        /// person's name.
        /// This field may be used in a query/report to identify audit events for a specific 
        /// person, e.g., where multiple synonymous Participant Object IDs (patient number, 
        /// medical record number, encounter number, etc.) have been used.
        /// Inv-1: Either a name or a query (or both) (xpath: not(exists(f:name)) or 
        /// not(exists(f:query)))
        /// <externalLink>
        ///     <linkText>Name</linkText>
        ///     <linkUri>
        ///         http://hl7-fhir.github.io/auditevent-definitions.html#AuditEvent.object.name
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        [ProtoMember(6), JsonProperty]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Text that describes the object in more detail.
        /// <externalLink>
        ///     <linkText>Description</linkText>
        ///     <linkUri>
        ///         http://hl7-fhir.github.io/auditevent-definitions.html#AuditEvent.object.description
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        [ProtoMember(7), JsonProperty]
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// The actual query for a query-type participant object.
        /// For query events it may be necessary to capture the actual query input to the 
        /// query process in order to identify the specific event. Because of differences 
        /// among query implementations and data encoding for them, this is a base 64 
        /// encoded data blob. It may be subsequently decoded or interpreted by downstream 
        /// audit analysis processing.
        /// <externalLink>
        ///     <linkText>Query</linkText>
        ///     <linkUri>
        ///         http://hl7-fhir.github.io/auditevent-definitions.html#AuditEvent.object.query
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        [ProtoMember(8), JsonProperty]
        public Base64Binary Query
        {
            get;
            set;
        }

        #endregion
    }
}
