// <copyright file="Identifier.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Fhir.Resources.Administrative
{
    #region Imports.

    using Appva.Fhir.Primitives;

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
    ///         http://hl7.org/implement/standards/FHIR-Develop/datatypes-definitions.html#identifier
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    public sealed class Identifier : Element
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Identifier"/> class.
        /// </summary>
        /// <param name="use">The purpose of this identifier</param>
        /// <param name="label">
        /// A text string for the identifier that can be displayed to a human so they can 
        /// recognize the identifier
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
            Code use, 
            string label, 
            Uri system, 
            string value, 
            Period period,
            Organization assigner)
        {
            this.Use = use;
            this.Label = label;
            this.System = system;
            this.Value = value;
            this.Period = period;
            this.Assigner = assigner;
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The purpose of this identifier.
        /// <externalLink>
        ///     <linkText>Use</linkText>
        ///     <linkUri>
        ///         http://hl7.org/implement/standards/FHIR-Develop/datatypes-definitions.html#identifier.use
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        public Code Use
        {
            get;
            private set;
        }

        /// <summary>
        /// A text string for the identifier that can be displayed to a human so they can 
        /// recognize the identifier.
        /// <externalLink>
        ///     <linkText>Label</linkText>
        ///     <linkUri>
        ///         http://hl7.org/implement/standards/FHIR-Develop/datatypes-definitions.html#identifier.label
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        public string Label
        {
            get;
            private set;
        }

        /// <summary>
        /// Establishes the namespace in which set of possible id values is unique.
        /// <externalLink>
        ///     <linkText>System</linkText>
        ///     <linkUri>
        ///         http://hl7.org/implement/standards/FHIR-Develop/datatypes-definitions.html#identifier.system
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        /// <remarks>
        /// There are many sequences of identifiers. To perform matching, we need to know 
        /// what sequence we're dealing with. The system identifies a particular sequence or 
        /// set of unique identifiers
        /// </remarks>
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
        ///         http://hl7.org/implement/standards/FHIR-Develop/datatypes-definitions.html#identifier.value
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        /// <remarks>
        /// If the value is a full URI, then the system SHALL be urn:ietf:rfc:3986.
        /// </remarks>
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
        ///         http://hl7.org/implement/standards/FHIR-Develop/datatypes-definitions.html#identifier.period
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
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
        ///         http://hl7.org/implement/standards/FHIR-Develop/datatypes-definitions.html#identifier.assigner
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        public Organization Assigner
        {
            get;
            private set;
        }

        #endregion
    }
}