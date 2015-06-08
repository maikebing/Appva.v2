// <copyright file="Language.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Fhir.Resources
{
    #region Imports.

    using Newtonsoft.Json;
    using Primitives;
    using ProtoBuf;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [FhirVersion(Fhir.V050)]
    [ProtoContract]
    public sealed class Language : Code
    {
        #region Variables.

        /// <summary>
        /// Swedish language.
        /// </summary>
        public static readonly Language Swedish = new Language("sv");
        
        /// <summary>
        /// English language.
        /// </summary>
        public static readonly Language English = new Language("en");

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Language"/> class.
        /// </summary>
        /// <param name="value">The string code value</param>
        public Language(string value) 
            : base(value)
        {
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="Language" /> class from being created.
        /// </summary>
        /// <remarks>For Json deserialization</remarks>
        [JsonConstructor]
        private Language()
            : base(null)
        {
        }

        #endregion
    }
}