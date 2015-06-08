// <copyright file="CodeableConcept.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//    <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Fhir.Complex
{
    #region Imports.

    using System.Collections.Generic;
    using Newtonsoft.Json;
    using ProtoBuf;
    using Resources.Administrative;
    using Resources.Security.ValueSets;

    #endregion

    /// <summary>
    /// A <c>CodeableConcept</c> represents a value that is usually supplied by 
    /// providing a reference to one or more terminologies or ontologies, but may also 
    /// be defined by the provision of text. This is a common pattern in healthcare 
    /// data.
    /// <para>
    /// Each coding is a representation of the concept as described above. The concept 
    /// may be coded multiple times in different code systems (or even multiple times in 
    /// the same code systems, where multiple forms are possible, such as with SNOMED 
    /// CT). 
    /// The different codings may have slightly different granularity due to the 
    /// differences in the definitions of the underlying codes. There is no meaning 
    /// associated with the ordering of coding within a <c>CodeableConcept</c>. 
    /// A typical use of <c>CodeableConcept</c> is to send the local code that the 
    /// concept was coded with, and also one or more translations to publicly defined 
    /// code systems such as LOINC or SNOMED CT. Sending local codes is useful and 
    /// important for the purposes of debugging and integrity auditing.
    /// </para>
    /// <para>
    /// Whether or not coding elements are present, the text is the representation of 
    /// the concept as entered or chosen by the user, and  which most closely represents 
    /// the intended meaning of the user or concept. Very often the text is the same as 
    /// a display of one of the codings. One of the codings may be flagged as the 
    /// primary -  the code or concept that the user actually selected directly. When 
    /// none of the coding elements is marked as primary, the text (if present) is the 
    /// preferred source of meaning.
    /// </para>
    /// <externalLink>
    ///     <linkText>1.13.0.5 CodeableConcept</linkText>
    ///     <linkUri>
    ///         http://hl7-fhir.github.io/datatypes.html#CodeableConcept
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    /// <example>
    /// <code language="xml" title="CodeableConcept Example">
    /// <![CDATA[
    /// <[name] xmlns="http://hl7.org/fhir">
    ///     <!-- from Element: extension -->
    ///     <coding>
    ///         <!-- 0..* Coding Code defined by a terminology system -->
    ///     </coding>
    ///     <text value="[string]"/>
    ///         <!-- 0..1 Plain text representation of the concept -->
    /// </[name]>
    /// ]]>
    /// </code>
    /// </example>
    [FhirVersion(Fhir.V050)]
    [ProtoContract]
    [ProtoInclude(1, typeof(AuditEventType))]
    [ProtoInclude(2, typeof(AuditEventSubType))]
    [ProtoInclude(3, typeof(AuditEventObjectSensitivity))]
    [ProtoInclude(4, typeof(AuditEventSourceType))]
    [ProtoInclude(5, typeof(IdentifierType))]
    [ProtoInclude(6, typeof(MaritalStatus))]
    public class CodeableConcept : Element
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeableConcept"/> class.
        /// </summary>
        /// <param name="coding">
        /// A reference to a code defined by a terminology system
        /// </param>
        public CodeableConcept(params Coding[] coding)
            : this(null, new List<Coding>(coding))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeableConcept"/> class.
        /// </summary>
        /// <param name="coding">
        /// A reference to a code defined by a terminology system
        /// </param>
        public CodeableConcept(IList<Coding> coding)
            : this(null, coding)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeableConcept"/> class.
        /// </summary>
        /// <param name="text">
        /// A human language representation of the concept as seen/selected/uttered by the 
        /// user who entered the data and/or which represents the intended meaning of the 
        /// user
        /// </param>
        /// <param name="coding">
        /// A reference to a code defined by a terminology system
        /// </param>
        public CodeableConcept(string text, params Coding[] coding)
            : this(text, new List<Coding>(coding))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeableConcept"/> class.
        /// </summary>
        /// <param name="text">
        /// A human language representation of the concept as seen/selected/uttered by the 
        /// user who entered the data and/or which represents the intended meaning of the 
        /// user
        /// </param>
        /// <param name="coding">
        /// A reference to a code defined by a terminology system
        /// </param>
        public CodeableConcept(string text, IList<Coding> coding)
        {
            this.Text = text;
            this.Coding = coding;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="CodeableConcept" /> class from being created.
        /// </summary>
        /// <remarks>For Json deserialization</remarks>
        [JsonConstructor]
        private CodeableConcept()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The name of the coding system.
        /// <externalLink>
        ///     <linkText>CodeableConcept.Coding</linkText>
        ///     <linkUri>
        ///         http://hl7-fhir.github.io/datatypes-definitions.html#CodeableConcept.coding
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        [ProtoMember(1000), JsonProperty]
        public IList<Coding> Coding
        {
            get;
            private set;
        }

        /// <summary>
        /// A human language representation of the concept as seen/selected/uttered by the 
        /// user who entered the data and/or which represents the intended meaning of the
        /// user.
        /// <externalLink>
        ///     <linkText>CodeableConcept.Text</linkText>
        ///     <linkUri>
        ///         http://hl7-fhir.github.io/datatypes-definitions.html#CodeableConcept.text
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        [ProtoMember(1001), JsonProperty]
        public string Text
        {
            get;
            private set;
        }

        #endregion
    }
}