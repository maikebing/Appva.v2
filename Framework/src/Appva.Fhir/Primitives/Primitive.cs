// <copyright file="Primitive.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//    <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Fhir.Primitives
{
    #region Imports.

    using System.Xml.Serialization;
    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// Primitive types are those without sub-properties, though primitives, like all 
    /// types, have extensions. Primitive types have a value which has the same value 
    /// domain as defined in the W3C Schema (1.0) specification part 2, though in most 
    /// cases, this specification makes additional constraints.
    /// <externalLink>
    ///     <linkText>W3C Schema (1.0) specification part 2</linkText>
    ///     <linkUri>
    ///         http://www.w3.org/TR/xmlschema-2/
    ///     </linkUri>
    /// </externalLink>
    /// <externalLink>
    ///     <linkText>FHIR Primitive</linkText>
    ///     <linkUri>
    ///         http://hl7.org/implement/standards/FHIR-Develop/datatypes.html#primitive
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    /// <typeparam name="T">The type of primitive</typeparam>
    [FhirVersion(Fhir.V040)]
    public abstract class Primitive<T> : Element, IValidate
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Primitive{T}"/> class.
        /// </summary>
        /// <param name="value">The struct value</param>
        protected Primitive(T value)
        {
            this.Value = value;
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The string value.
        /// </summary>
        [JsonProperty]
        [XmlAttributeAttribute("value")]
        public T Value
        {
            get;
            private set;
        }

        #endregion

        #region Public Methods.

        /// <inheritdoc />
        public abstract bool IsValid();

        #endregion
    }
}