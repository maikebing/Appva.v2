// <copyright file="ContactPointUse.cs" company="Appva AB">
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
    /// Use of contact point.
    /// <externalLink>
    ///     <linkText>1.15.2.1.426.1 ContactPointUse</linkText>
    ///     <linkUri>
    ///         http://hl7-fhir.github.io/contact-point-use.html
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    [FhirVersion(Fhir.V050)]
    [ProtoContract]
    public sealed class ContactPointUse : Code
    {
        #region Variables.

        /// <summary>
        /// A communication contact point at a home; attempted contacts for business 
        /// purposes might intrude privacy and chances are one will contact family or other 
        /// household members instead of the person one wishes to call. Typically used with 
        /// urgent cases, or if no other contacts are available.
        /// </summary>
        public static readonly ContactPointUse Home = new ContactPointUse("home");

        /// <summary>
        /// An office contact point. First choice for business related contacts during 
        /// business hours.
        /// </summary>
        public static readonly ContactPointUse Work = new ContactPointUse("work");

        /// <summary>
        /// A temporary contact point. The period can provide more detailed information.
        /// </summary>
        public static readonly ContactPointUse Temp = new ContactPointUse("temp");

        /// <summary>
        /// This contact point is no longer in use (or was never correct, but retained for 
        /// records).
        /// </summary>
        public static readonly ContactPointUse Old = new ContactPointUse("old");

        /// <summary>
        /// A telecommunication device that moves and stays with its owner. May have 
        /// characteristics of all other use codes, suitable for urgent matters, not the 
        /// first choice for routine business.
        /// </summary>
        public static readonly ContactPointUse Mobile = new ContactPointUse("mobile");

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactPointUse"/> class.
        /// </summary>
        /// <param name="value">The string code value</param>
        private ContactPointUse(string value) 
            : base(value)
        {
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="ContactPointUse" /> class from being created.
        /// </summary>
        /// <remarks>For Json deserialization</remarks>
        [JsonConstructor]
        private ContactPointUse()
            : base(null)
        {
        }

        #endregion
    }
}