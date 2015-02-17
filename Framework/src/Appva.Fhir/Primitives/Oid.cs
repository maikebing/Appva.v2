// <copyright file="Oid.cs" company="Appva AB">
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

    #endregion

    /// <summary>
    /// An OID represented as a URI (RFC 3001): urn:oid:1.2.3.4.5.
    /// <externalLink>
    ///     <linkText>FHIR Oid</linkText>
    ///     <linkUri>
    ///        http://hl7.org/implement/standards/FHIR-Develop/datatypes.html#oid
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    [FhirVersion(Fhir.V040)]
    [XmlTypeAttribute(Namespace = Fhir.Namespace)]
    public sealed class Oid : Primitive<string>
    {
        #region constants.

        /// <summary>
        /// The OID valid pattern.
        /// </summary>
        private const string Pattern = @"^urn:oid:(0|[1-9][0-9]*)(\.(0|[1-9][0-9]*))*$";

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Oid"/> class.
        /// </summary>
        /// <param name="value">The string OID value</param>
        public Oid(string value) : base(value)
        {
        }

        #endregion

        #region Public static Functions.

        /// <summary>
        /// Returns whether or not the value is a valid OID.
        /// </summary>
        /// <param name="value">The string representation of an OID</param>
        /// <returns>True if valid</returns>
        public static bool IsValid(string value)
        {
            return Regex.IsMatch(value, Pattern, RegexOptions.Singleline);
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