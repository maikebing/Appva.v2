// <copyright file="Uri.cs" company="Appva AB">
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
    /// A Uniform Resource Identifier Reference. It can be absolute or relative, and may 
    /// have an optional fragment identifier
    /// <externalLink>
    ///     <linkText>FHIR Uri</linkText>
    ///     <linkUri>
    ///         http://hl7.org/implement/standards/FHIR-Develop/datatypes.html#uri
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    [FhirVersion(Fhir.V040)]
    [XmlTypeAttribute(Namespace = Fhir.Namespace)]
    public sealed class Uri : Primitive<string>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Uri"/> class.
        /// </summary>
        /// <param name="value">The string uri value</param>
        public Uri(string value)
            : base(value)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Uri"/> class.
        /// </summary>
        /// <remarks>For Json deserialization</remarks>
        [JsonConstructor]
        public Uri()
            : base(null)
        {
        }

        #endregion
        
        #region Public static Functions.

        /// <summary>
        /// Returns whether or not the value is a valid URI.
        /// </summary>
        /// <param name="value">The string representation of a URI</param>
        /// <returns>True if valid</returns>
        public static bool IsValid(string value)
        {
            System.Uri uri;
            try
            {
                uri = new System.Uri(value, System.UriKind.RelativeOrAbsolute);
            }
            catch
            {
                return false;
            }
            if (uri.IsAbsoluteUri && (value.StartsWith("urn:oid:") || value.StartsWith("urn:uuid:")))
            {
                if (!Oid.IsValid(value) || !Uuid.IsValid(value))
                {
                    return false;
                }
            }
            return true;
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