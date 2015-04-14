// <copyright file="Uuid.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Fhir.Primitives
{
    #region Imports.

    using System.Text.RegularExpressions;
    using System.Xml.Serialization;
    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// A <c>UUID</c>, represented as a URI (RFC 4122): 
    /// urn:uuid:a5afddf4-e880-459b-876e-e4591b0acc11. Note the RFC comments about case: 
    /// <c>UUID</c> values SHALL be represented in lower case, but systems SHOULD 
    /// interpret them case insensitively.
    /// <externalLink>
    ///     <linkText>FHIR Uuid</linkText>
    ///     <linkUri>
    ///        http://hl7.org/implement/standards/FHIR-Develop/datatypes.html#uuid
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    [FhirVersion(Fhir.V040)]
    [XmlTypeAttribute(Namespace = Fhir.Namespace)]
    public sealed class Uuid : Primitive<string>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Uuid"/> class.
        /// </summary>
        /// <param name="value">The string UUID value</param>
        public Uuid(string value) : base(value)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Uuid"/> class.
        /// </summary>
        /// <remarks>For Json deserialization</remarks>
        [JsonConstructor]
        public Uuid()
            : base(null)
        {
        }

        #endregion

        #region Public static Functions.

        /// <summary>
        /// Returns whether or not the value is a valid UUID.
        /// </summary>
        /// <param name="value">The string representation of a UUID</param>
        /// <returns>True if valid</returns>
        public static bool IsValid(string value)
        {
            return Regex.IsMatch(
                value, 
                "^urn:uuid:[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$", 
                RegexOptions.Singleline);
        }

        #endregion

        #region Primitive<string> Overrides.

        /// <inheritdoc />
        public override bool IsValid()
        {
            return IsValid(this.Value);
        }

        #endregion
    }
}