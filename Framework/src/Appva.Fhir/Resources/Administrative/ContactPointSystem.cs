// <copyright file="ContactPointSystem.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Fhir.Resources.Administrative
{
    #region Imports.

    using Primitives;
    using Newtonsoft.Json;
    using ProtoBuf;

    #endregion

    /// <summary>
    /// Telecommunications form for contact point
    /// <externalLink>
    ///     <linkText>1.15.2.1.425.1 ContactPointSystem</linkText>
    ///     <linkUri>
    ///         http://hl7-fhir.github.io/contact-point-system.html
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    [FhirVersion(Fhir.V050)]
    [ProtoContract]
    public sealed class ContactPointSystem : Code
    {
        #region Variables.

        /// <summary>
        /// The value is a telephone number used for voice calls. Use of full international 
        /// numbers starting with + is recommended to enable automatic dialing support but 
        /// not required.
        /// </summary>
        public static readonly ContactPointSystem Phone = new ContactPointSystem("phone");

        /// <summary>
        /// The value is a fax machine. Use of full international numbers starting with + is 
        /// recommended to enable automatic dialing support but not required.
        /// </summary>
        public static readonly ContactPointSystem Fax = new ContactPointSystem("fax");

        /// <summary>
        /// The value is an email address.
        /// </summary>
        public static readonly ContactPointSystem Email = new ContactPointSystem("email");

        /// <summary>
        /// The value is an email address.The value is a url. This is intended for various 
        /// personal contacts including blogs, Twitter, Facebook, etc. Do not use for email 
        /// addresses.
        /// </summary>
        public static readonly ContactPointSystem Url = new ContactPointSystem("url");

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactPointSystem"/> class.
        /// </summary>
        /// <param name="value">The string code value</param>
        private ContactPointSystem(string value) 
            : base(value)
        {
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="ContactPointSystem" /> class from being created.
        /// </summary>
        /// <remarks>For Json deserialization</remarks>
        [JsonConstructor]
        private ContactPointSystem()
            : base(null)
        {
        }

        #endregion
    }
}