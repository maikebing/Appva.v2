// <copyright file="Instant.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Fhir.Primitives
{
    #region Imports.

    using System;
    using Newtonsoft.Json;
    using ProtoBuf;

    #endregion

    /// <summary>
    /// An instant in time - known at least to the second and always includes a time 
    /// zone.
    /// Note: This type is for system times, not human times.
    /// <externalLink>
    ///     <linkText>FHIR Instant</linkText>
    ///     <linkUri>
    ///        http://hl7-fhir.github.io/datatypes.html#instant
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    [FhirVersion(Fhir.V050)]
    [ProtoContract]
    public sealed class Instant : Primitive
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Instant"/> class.
        /// </summary>
        /// <param name="value">The string OID value</param>
        public Instant(DateTimeOffset value)
            : base(value.ToString("o"))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Instant"/> class.
        /// </summary>
        /// <param name="value">The string OID value</param>
        public Instant(string value) : base(value)
        {
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="Instant" /> class from being created.
        /// </summary>
        /// <remarks>For Json deserialization</remarks>
        [JsonConstructor]
        private Instant()
            : base(null)
        {
        }

        #endregion

        #region Primitive Overrides.

        /// <inheritdoc />
        public override bool IsValid()
        {
            return true;
        }

        #endregion
    }
}