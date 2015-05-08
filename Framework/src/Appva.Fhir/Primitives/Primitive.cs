// <copyright file="Primitive.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//    <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Fhir.Primitives
{
    #region Imports.

    using Newtonsoft.Json;
    using ProtoBuf;

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
    ///         http://hl7-fhir.github.io/datatypes.html#primitive
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    [FhirVersion(Fhir.V050)]
    [ProtoContract]
    [ProtoInclude(1000, typeof(Code))]
    [ProtoInclude(1001, typeof(Oid))]
    [ProtoInclude(1002, typeof(Uri))]
    [ProtoInclude(1003, typeof(Uuid))]
    [ProtoInclude(1004, typeof(Instant))]
    [ProtoInclude(1005, typeof(Base64Binary))]
    public abstract class Primitive : Element, IValidate
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Primitive"/> class.
        /// </summary>
        /// <param name="value">The struct value</param>
        protected Primitive(string value)
        {
            this.Value = value;
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The string value.
        /// </summary>
        [ProtoMember(1), JsonProperty]
        public string Value
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