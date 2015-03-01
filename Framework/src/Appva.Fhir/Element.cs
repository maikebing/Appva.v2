// <copyright file="Element.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//    <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Fhir
{
    #region Imports.

    using System.Collections.ObjectModel;
    using System.Xml.Serialization;

    #endregion

    /// <summary>
    /// The base definition for all elements contained inside a resource. All elements, 
    /// whether defined as a Data Type or as part of a resource structure, have this 
    /// base content.
    /// <code language="xml" title="Element Example">
    /// <![CDATA[
    /// <?xml version="1.0" encoding="utf-8"?>
    /// <Element xmlns="http://hl7.org/fhir" id="xml:id (or equivalent in JSON) (id)">
    ///     <extension>
    ///         <!-- 0..* Extension Additional Content defined by implementations -->
    ///     </extension>
    /// </Element>
    /// ]]>
    /// </code>
    /// <externalLink>
    ///     <linkText>FHIR Element</linkText>
    ///     <linkUri>
    ///         http://hl7.org/implement/standards/FHIR-Develop/element.html
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    [FhirVersion(Fhir.V040)]
    [XmlTypeAttribute(Namespace = Fhir.Namespace)]
    public abstract class Element : IExtendable
    {
        /// <summary>
        /// Unique id for the element within a resource (for internal references).
        /// <externalLink>
        ///     <linkText>FHIR Element.Id</linkText>
        ///     <linkUri>
        ///         http://hl7.org/implement/standards/FHIR-Develop/element-definitions.html#Element.id
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        [XmlAttributeAttribute("id")]
        public string Id
        {
            get;
            set;
        }

        /// <summary>
        /// May be used to represent additional information that is not part of the basic 
        /// definition of the element. In order to make the use of extensions safe and 
        /// manageable, there is a strict set of governance applied to the definition and 
        /// use of extensions. Though any implementer is allowed to define an extension, 
        /// there is a set of requirements that SHALL be met as part of the definition of 
        /// the extension.
        /// <externalLink>
        ///     <linkText>FHIR Element.Extensions</linkText>
        ///     <linkUri>
        ///         http://hl7.org/implement/standards/FHIR-Develop/element-definitions.html#Element.extension
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        [XmlElementAttribute("extension")]
        public Collection<Extension> Extension
        {
            get;
            set;
        }
    }
}