// <copyright file="Extension.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Fhir
{
    #region Imports.

    using System.Xml.Serialization;
    using Primitives;

    #endregion

    /// <summary>
    /// Every element in a resource or data type includes an optional "extension" child 
    /// element that may be present any number of times. This is the content model of 
    /// the extension as it appears in each resource:
    /// <code>
    /// <![CDATA[
    /// <[name] xmlns="http://hl7.org/fhir" 
    ///     url="identifies the meaning of the extension (uri)">
    ///     <!-- from Element: extension -->
    ///     <value[x]><!-- 0..1 * Value of extension --></value[x]>
    /// </[name]>
    /// ]]>
    /// </code>
    /// <externalLink>
    ///     <linkText>Extension Element</linkText>
    ///     <linkUri>
    ///         http://hl7.org/implement/standards/FHIR-Develop/extensibility.html#extension
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    [FhirVersion(Fhir.V040)]
    [XmlTypeAttribute(Namespace = Fhir.Namespace)]
    public sealed class Extension : Element
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Extension"/> class.
        /// </summary>
        /// <param name="url">
        ///     Source of the definition for the extension code - a logical name or a URL
        /// </param>
        /// <param name="value">
        ///     Value of extension - may be a resource or one of a constrained set of the 
        ///     data types
        /// </param>
        public Extension(Uri url, Element value)
        {
            this.Url = url;
            this.Value = value;
        }

        #endregion

        #region Properties.

        /// <summary>
        /// Source of the definition for the extension code - a logical name or a URL.
        /// <externalLink>
        ///     <linkText>Url</linkText>
        ///     <linkUri>
        ///         http://hl7.org/implement/standards/FHIR-Develop/extensibility-definitions.html#Extension.url
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        [XmlAttributeAttribute("url", DataType = "anyURI")]
        public Uri Url
        {
            get;
            private set;
        }

        /// <summary>
        /// Value of extension - may be a resource or one of a constrained set of the data 
        /// types.
        /// <externalLink>
        ///     <linkText>Value</linkText>
        ///     <linkUri>
        ///         http://hl7.org/implement/standards/FHIR-Develop/extensibility-definitions.html#Extension.value_x_
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        [XmlAttributeAttribute("value")]
        public Element Value
        {
            get;
            private set;
        }

        #endregion
    }
}