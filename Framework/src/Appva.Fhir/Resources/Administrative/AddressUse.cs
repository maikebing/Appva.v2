// <copyright file="AddressUse.cs" company="Appva AB">
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
    /// The use of an address.
    /// <externalLink>
    ///     <linkText>1.23.2.1.16.1 AddressUse</linkText>
    ///     <linkUri>
    ///         http://hl7-fhir.github.io/address-use.html
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    [FhirVersion(Fhir.V050)]
    [ProtoContract]
    public sealed class AddressUse : Code
    {
        #region Variables.

        /// <summary>
        /// A communication address at a home.
        /// </summary>
        public static readonly AddressUse Home = new AddressUse("home");

        /// <summary>
        /// An office address. First choice for business related contacts during business 
        /// hours.
        /// </summary>
        public static readonly AddressUse Work = new AddressUse("work");

        /// <summary>
        /// A temporary address. The period can provide more detailed information.
        /// </summary>
        public static readonly AddressUse Temp = new AddressUse("temp");

        /// <summary>
        /// This address is no longer in use (or was never correct, but retained for 
        /// records).
        /// </summary>
        public static readonly AddressUse Old = new AddressUse("old");

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AddressUse"/> class.
        /// </summary>
        /// <param name="value">The string code value</param>
        private AddressUse(string value) 
            : base(value)
        {
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="AddressUse" /> class from being created.
        /// </summary>
        /// <remarks>For Json deserialization</remarks>
        [JsonConstructor]
        private AddressUse() 
            : base(null)
        {
        }

        #endregion
    }
}