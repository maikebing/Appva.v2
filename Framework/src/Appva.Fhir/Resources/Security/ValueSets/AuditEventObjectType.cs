// <copyright file="AuditEventObjectType.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Fhir.Resources.Security.ValueSets
{
    #region Imports.

    using Newtonsoft.Json;
    using Primitives;
    using ProtoBuf;

    #endregion

    /// <summary>
    /// Code for the participant object type being audited.
    /// <externalLink>
    ///     <linkText>1.15.2.1.166.1 AuditEventObjectType</linkText>
    ///     <linkUri>
    ///         http://hl7-fhir.github.io/object-type.html
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    [FhirVersion(Fhir.V050)]
    [ProtoContract]
    public sealed class AuditEventObjectType : Code
    {
        #region Variables.

        /// <summary>
        /// Person.
        /// </summary>
        public static readonly AuditEventObjectType Person = new AuditEventObjectType("1");

        /// <summary>
        /// System Object.
        /// </summary>
        public static readonly AuditEventObjectType SystemObject = new AuditEventObjectType("2");

        /// <summary>
        /// Organization.
        /// </summary>
        public static readonly AuditEventObjectType Organization = new AuditEventObjectType("3");

        /// <summary>
        /// Other.
        /// </summary>
        public static readonly AuditEventObjectType Other = new AuditEventObjectType("4");

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditEventObjectType"/> class.
        /// </summary>
        /// <param name="value">The string code value</param>
        private AuditEventObjectType(string value) 
            : base(value)
        {
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="AuditEventObjectType" /> class from being created.
        /// </summary>
        /// <remarks>For Json deserialization</remarks>
        [JsonConstructor]
        private AuditEventObjectType() 
            : base(null)
        {
        }

        #endregion
    }
}