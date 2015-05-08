// <copyright file="Base64Binary.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Fhir.Primitives
{
    #region Imports.

    using System;
    using System.Linq;
    using Newtonsoft.Json;
    using ProtoBuf;

    #endregion

    /// <summary>
    /// A stream of bytes, base64 encoded (RFC 4648).
    /// <externalLink>
    ///     <linkText>FHIR Base64Binary</linkText>
    ///     <linkUri>
    ///         http://hl7-fhir.github.io/datatypes.html#base64Binary
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    [FhirVersion(Fhir.V050)]
    [ProtoContract]
    public sealed class Base64Binary : Primitive
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Base64Binary"/> class.
        /// </summary>
        /// <param name="value">The byte code value</param>
        public Base64Binary(byte[] value)
            : base(Convert.ToBase64String(value))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Base64Binary"/> class.
        /// </summary>
        /// <param name="value">The string code value</param>
        public Base64Binary(string value) 
            : base(value)
        {
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="Base64Binary" /> class from being created.
        /// </summary>
        /// <remarks>For Json deserialization</remarks>
        [JsonConstructor]
        private Base64Binary() 
            : base(null)
        {
        }

        #endregion

        #region Primitive Overrides.

        /// <inheritdoc />
        public override bool IsValid()
        {
            return IsBase64String(this.Value);
        }

        /// <summary>
        /// Verifies whether or not it is a base64 string.
        /// </summary>
        /// <param name="value">The value to be checked</param>
        /// <returns>True if the string is a valid base64 string</returns>
        private static bool IsBase64String(string value)
        {
            if (string.IsNullOrEmpty(value) || value.Length % 4 != 0
                || value.Contains(' ') || value.Contains('\t') || 
                value.Contains('\r') || value.Contains('\n'))
            {
                return false;
            }
            var index = value.Length - 1;
            if (value[index] == '=')
            {
                index--;
            }
            if (value[index] == '=')
            {
                index--;
            }
            for (var i = 0; i <= index; i++)
            {
                if (IsInvalidBase64Character(value[i]))
                {
                    return false;
                }
            }
            return true;
        }
        
        /// <summary>
        /// Verifies whether or not the character is a valid base64 character.
        /// </summary>
        /// <param name="value">The character to be checked</param>
        /// <returns>True if invalid</returns>
        private static bool IsInvalidBase64Character(char value)
        {
            var intValue = (int) value;
            if (intValue >= 48 && intValue <= 57)
            {
                return false;
            }
            if (intValue >= 65 && intValue <= 90)
            {
                return false;
            }
            if (intValue >= 97 && intValue <= 122)
            {
                return false;
            }
            return intValue != 43 && intValue != 47;
        }

        #endregion
    }
}