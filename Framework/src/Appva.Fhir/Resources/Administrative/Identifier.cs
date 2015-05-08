// <copyright file="Identifier.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Fhir.Resources.Administrative
{
    #region Imports.

    using Primitives;
    using Newtonsoft.Json;
    using ProtoBuf;

    #endregion

    /// <summary>
    /// A numeric or alphanumeric string that is associated with a single object or 
    /// entity within a given system. Typically, identifiers are used to connect content 
    /// in resources to external content available in other frameworks or protocols. 
    /// Typically, identifiers are used to connect content in resources to external 
    /// content available in other frameworks or protocols. Identifiers are associated 
    /// with objects, and may be changed or retired due to human or system process and 
    /// errors.
    /// <externalLink>
    ///     <linkText>1.14.2.3 Identifier</linkText>
    ///     <linkUri>
    ///         http://hl7-fhir.github.io/datatypes-definitions.html#identifier
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    [FhirVersion(Fhir.V050)]
    [ProtoContract]
    public sealed class Identifier : Element
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Identifier"/> class.
        /// </summary>
        /// <param name="use">The purpose of this identifier</param>
        /// <param name="type">
        /// A coded type for the identifier that can be used to determine which identifier 
        /// to use for a specific purpose
        /// </param>
        /// <param name="system">
        /// Establishes the namespace in which set of possible id values is unique
        /// </param>
        /// <param name="value">
        /// The portion of the identifier typically displayed to the user and which is 
        /// unique within the context of the system
        /// </param>
        /// <param name="period">
        /// Time period during which identifier is/was valid for use
        /// </param>
        /// <param name="assigner">
        /// Organization that issued/manages the identifier
        /// </param>
        public Identifier(
            IdentifierUse use,
            IdentifierType type, 
            Uri system, 
            string value, 
            Period period,
            Organization assigner)
        {
            this.Use = use;
            this.Type = type;
            this.System = system;
            this.Value = value;
            this.Period = period;
            this.Assigner = assigner;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="Identifier" /> class from being created.
        /// </summary>
        /// <remarks>For Json deserialization</remarks>
        [JsonConstructor]
        private Identifier()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The purpose of this identifier.
        /// <externalLink>
        ///     <linkText>Use</linkText>
        ///     <linkUri>
        ///         http://hl7-fhir.github.io/datatypes-definitions.html#identifier.use
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        [ProtoMember(1), JsonProperty]
        public IdentifierUse Use
        {
            get;
            private set;
        }

        /// <summary>
        /// A coded type for the identifier that can be used to determine which identifier 
        /// to use for a specific purpose.
        /// <externalLink>
        ///     <linkText>Type</linkText>
        ///     <linkUri>
        ///         http://hl7-fhir.github.io/datatypes-definitions.html#Identifier.type
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        /// <remarks>
        /// Where the system is known, a type is unnecessary because the type is always part 
        /// of the system definition. However systems often need to handle identifiers where 
        /// the system is not known. There is not a 1:1 relationship between type and 
        /// system, since many different systems have the same type
        /// </remarks>
        [ProtoMember(2), JsonProperty]
        public IdentifierType Type
        {
            get;
            private set;
        }

        /// <summary>
        /// Establishes the namespace in which set of possible id values is unique.
        /// <externalLink>
        ///     <linkText>System</linkText>
        ///     <linkUri>
        ///         http://hl7-fhir.github.io/datatypes-definitions.html#identifier.system
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        /// <remarks>
        /// There are many sequences of identifiers. To perform matching, we need to know 
        /// what sequence we're dealing with. The system identifies a particular sequence or 
        /// set of unique identifiers
        /// </remarks>
        [ProtoMember(3), JsonProperty]
        public Uri System
        {
            get;
            private set;
        }

        /// <summary>
        /// The portion of the identifier typically displayed to the user and which is 
        /// unique within the context of the system.
        /// <externalLink>
        ///     <linkText>Value</linkText>
        ///     <linkUri>
        ///         http://hl7-fhir.github.io/datatypes-definitions.html#identifier.value
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        /// <remarks>
        /// If the value is a full URI, then the system SHALL be urn:ietf:rfc:3986.
        /// </remarks>
        [ProtoMember(4), JsonProperty]
        public string Value
        {
            get;
            private set;
        }

        /// <summary>
        /// Time period during which identifier is/was valid for use.
        /// <externalLink>
        ///     <linkText>Period</linkText>
        ///     <linkUri>
        ///         http://hl7-fhir.github.io/datatypes-definitions.html#identifier.period
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        [ProtoMember(5), JsonProperty]
        public Period Period
        {
            get;
            private set;
        }

        /// <summary>
        /// Organization that issued/manages the identifier.
        /// <externalLink>
        ///     <linkText>Assigner</linkText>
        ///     <linkUri>
        ///         http://hl7-fhir.github.io/datatypes-definitions.html#identifier.assigner
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        [ProtoMember(6), JsonProperty]
        public Organization Assigner
        {
            get;
            private set;
        }

        #endregion
    }
}