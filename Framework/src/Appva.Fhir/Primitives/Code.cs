// <copyright file="Code.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//    <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Fhir.Primitives
{
    #region Imports.

    using System.Text.RegularExpressions;
    using System.Xml.Serialization;

    #endregion

    /// <summary>
    /// Indicates that the value is taken from a set of controlled strings defined 
    /// elsewhere (see Using codes for further discussion). Technically, a code is 
    /// restricted to string which has at least one character and no leading or trailing 
    /// whitespace, and where there is no whitespace other than single spaces in the 
    /// contents regex: <c>[^\s]+([\s]+[^\s]+)*</c>
    /// <externalLink>
    ///     <linkText>FHIR Code</linkText>
    ///     <linkUri>
    ///         http://hl7.org/implement/standards/FHIR-Develop/datatypes.html#code
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    [FhirVersion(Fhir.V040)]
    [XmlTypeAttribute(Namespace = Fhir.Namespace)]
    public sealed class Code : Primitive<string>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Code"/> class.
        /// </summary>
        /// <param name="value">The string code value</param>
        public Code(string value)
            : base(value)
        {
        }

        #endregion

        #region Primitive<string> Overrides.

        /// <inheritdoc />
        public override bool IsValid()
        {
            return Regex.IsMatch(this.Value, @"^[^\s]+([\s]+[^\s]+)*$", RegexOptions.Singleline);
        }

        #endregion
    }
}