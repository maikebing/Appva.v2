// <copyright file="IdentifierUse.cs" company="Appva AB">
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
    /// Identifies the purpose for this identifier, if known.
    /// <externalLink>
    ///     <linkText>1.23.2.1.13.1 IdentifierUse</linkText>
    ///     <linkUri>
    ///         http://hl7-fhir.github.io/identifier-use.html
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    [FhirVersion(Fhir.V050)]
    [ProtoContract]
    public sealed class IdentifierUse : Code
    {
        #region Variables.

        /// <summary>
        /// The identifier recommended for display and use in real-world interactions.
        /// </summary>
        public static readonly IdentifierUse Usual = new IdentifierUse("usual");

        /// <summary>
        /// The identifier considered to be most trusted for the identification of this 
        /// item.
        /// </summary>
        public static readonly IdentifierUse Official = new IdentifierUse("official");

        /// <summary>
        /// A temporary identifier.
        /// </summary>
        public static readonly IdentifierUse Temp = new IdentifierUse("temp");

        /// <summary>
        /// An identifier that was assigned in secondary use - it serves to identify the 
        /// object in a relative context, but cannot be consistently assigned to the same 
        /// object again in a different context.
        /// </summary>
        public static readonly IdentifierUse Secondary = new IdentifierUse("secondary");

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentifierUse"/> class.
        /// </summary>
        /// <param name="value">The string code value</param>
        private IdentifierUse(string value) 
            : base(value)
        {
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="IdentifierUse" /> class from being created.
        /// </summary>
        /// <remarks>For Json deserialization</remarks>
        [JsonConstructor]
        private IdentifierUse()
            : base(null)
        {
        }

        #endregion
    }
}