﻿// <copyright file="CodeableConcept.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//    <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Fhir.Complex
{
    #region Imports.

    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Xml.Serialization;

    #endregion

    /// <summary>
    /// A <c>CodeableConcept</c> represents a value that is usually supplied by providing a 
    /// reference to one or more terminologies or ontologies, but may also be defined by 
    /// the provision of text. This is a common pattern in healthcare data.
    /// <para>
    /// Each coding is a representation of the concept as described above. The concept 
    /// may be coded multiple times in different code systems (or even multiple times in 
    /// the same code systems, where multiple forms are possible, such as with SNOMED CT). 
    /// The different codings may have slightly different granularity due to the 
    /// differences in the definitions of the underlying codes. There is no meaning 
    /// associated with the ordering of coding within a <c>CodeableConcept</c>. 
    /// A typical use of <c>CodeableConcept</c> is to send the local code that the concept was 
    /// coded with, and also one or more translations to publicly defined code systems 
    /// such as LOINC or SNOMED CT. Sending local codes is useful and important for the 
    /// purposes of debugging and integrity auditing.
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
    ///         http://hl7.org/implement/standards/FHIR-Develop/datatypes.html#CodeableConcept
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
    [FhirVersion(Fhir.V040)]
    [XmlTypeAttribute(Namespace = Fhir.Namespace)]
    public class CodeableConcept : Element
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeableConcept"/> class.
        /// </summary>
        /// <param name="coding">A reference to a code defined by a terminology system</param>
        public CodeableConcept(Collection<Coding> coding) : this(null, coding)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeableConcept"/> class.
        /// </summary>
        /// <param name="text">
        /// A human language representation of the concept as seen/selected/uttered by the 
        /// user who entered the data and/or which represents the intended meaning of the user
        /// </param>
        /// <param name="coding">A reference to a code defined by a terminology system</param>
        public CodeableConcept(string text, Collection<Coding> coding)
        {
            this.Text = text;
            this.Coding = coding;
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The name of the coding system.
        /// <externalLink>
        ///     <linkText>CodeableConcept.Coding</linkText>
        ///     <linkUri>
        ///         http://hl7.org/implement/standards/FHIR-Develop/datatypes-definitions.html#CodeableConcept.coding
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        [XmlElementAttribute("coding")]
        public IEnumerable<Coding> Coding
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
        ///         http://hl7.org/implement/standards/FHIR-Develop/datatypes-definitions.html#CodeableConcept.text
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        [XmlElementAttribute("text")]
        public string Text
        {
            get;
            private set;
        }

        #endregion
    }
}