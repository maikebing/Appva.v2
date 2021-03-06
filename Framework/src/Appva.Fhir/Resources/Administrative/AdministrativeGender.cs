﻿// <copyright file="AdministrativeGender.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Fhir.Resources.Administrative
{
    #region Imports.

    using Newtonsoft.Json;
    using Primitives;
    using ProtoBuf;

    #endregion

    /// <summary>
    /// The gender of a person used for administrative purposes.
    /// <externalLink>
    ///     <linkText>1.15.2.1.24.1 AdministrativeGender</linkText>
    ///     <linkUri>
    ///         http://hl7-fhir.github.io/administrative-gender.html
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    [FhirVersion(Fhir.V050)]
    [ProtoContract]
    public sealed class AdministrativeGender : Code
    {
        #region Variables.

        /// <summary>
        /// Male concept.
        /// </summary>
        public static readonly AdministrativeGender Male = new AdministrativeGender("male");

        /// <summary>
        /// Female concept.
        /// </summary>
        public static readonly AdministrativeGender Female = new AdministrativeGender("female");

        /// <summary>
        /// Other concept.
        /// </summary>
        public static readonly AdministrativeGender Other = new AdministrativeGender("other");

        /// <summary>
        /// Unknown concept.
        /// </summary>
        public static readonly AdministrativeGender Unknown = new AdministrativeGender("unknown");

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AdministrativeGender"/> class.
        /// </summary>
        /// <param name="value">The string code value</param>
        private AdministrativeGender(string value)
            : base(value)
        {
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="AdministrativeGender" /> class from being created.
        /// </summary>
        /// <remarks>For Json deserialization</remarks>
        [JsonConstructor]
        private AdministrativeGender() 
            : base(null)
        {
        }

        #endregion
    }
}