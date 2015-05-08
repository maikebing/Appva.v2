// <copyright file="AuditEventSourceType.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Fhir.Resources.Security.ValueSets
{
    #region Imports.

    using System.Collections.Generic;
    using Complex;
    using Newtonsoft.Json;
    using Primitives;
    using ProtoBuf;

    #endregion

    /// <summary>
    /// A <c>AuditEventSourceType</c>. 
    /// <externalLink>
    ///     <linkText>1.15.2.1.165.1 AuditEventSourceType</linkText>
    ///     <linkUri>
    ///         http://hl7-fhir.github.io/valueset-audit-source-type.html
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    [FhirVersion(Fhir.V050)]
    [ProtoContract]
    public sealed class AuditEventSourceType : CodeableConcept
    {
        #region Variables.

        /// <summary>
        /// The identification of the code system that defines the meaning of the symbol in 
        /// the code. 
        /// </summary>
        public static readonly Uri System = Fhir.CreateNewUri("security-source-type");

        /// <summary>
        /// End-user display device, diagnostic device.
        /// </summary>
        public static readonly AuditEventSourceType UserDevice = new AuditEventSourceType(new Coding(System, new Code("1"), "User Device"));

        /// <summary>
        /// Data acquisition device or instrument.
        /// </summary>
        public static readonly AuditEventSourceType DataInterface = new AuditEventSourceType(new Coding(System, new Code("2"), "Data Interface"));

        /// <summary>
        /// Web Server process or thread.
        /// </summary>
        public static readonly AuditEventSourceType WebServer = new AuditEventSourceType(new Coding(System, new Code("3"), "Web Server"));

        /// <summary>
        /// Application Server process or thread.
        /// </summary>
        public static readonly AuditEventSourceType ApplicationServer = new AuditEventSourceType(new Coding(System, new Code("4"), "Application Server"));

        /// <summary>
        /// Database Server process or thread.
        /// </summary>
        public static readonly AuditEventSourceType DatabaseServer = new AuditEventSourceType(new Coding(System, new Code("5"), "Database Server"));

        /// <summary>
        /// Security server, e.g., a domain controller.
        /// </summary>
        public static readonly AuditEventSourceType SecurityServer = new AuditEventSourceType(new Coding(System, new Code("6"), "Security Server"));

        /// <summary>
        /// ISO level 1-3 network component.
        /// </summary>
        public static readonly AuditEventSourceType NetworkDevice = new AuditEventSourceType(new Coding(System, new Code("7"), "Network Device"));

        /// <summary>
        /// ISO level 4-6 operating software.
        /// </summary>
        public static readonly AuditEventSourceType NetworkRouter = new AuditEventSourceType(new Coding(System, new Code("8"), "Network Router"));

        /// <summary>
        /// Other kind of device (defined by DICOM, but some other code/system can be used).
        /// </summary>
        public static readonly AuditEventSourceType Other = new AuditEventSourceType(new Coding(System, new Code("9"), "Other"));

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditEventSourceType"/> class.
        /// </summary>
        /// <param name="coding">
        /// A reference to a code defined by a terminology system
        /// </param>
        public AuditEventSourceType(IList<Coding> coding)
            : base(null, coding)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditEventSourceType"/> class.
        /// </summary>
        /// <param name="coding">
        /// A reference to a code defined by a terminology system
        /// </param>
        public AuditEventSourceType(params Coding[] coding)
            : base(null, coding)
        {
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="AuditEventSourceType" /> class from being created.
        /// </summary>
        /// <remarks>For Json deserialization</remarks>
        [JsonConstructor]
        private AuditEventSourceType() 
            : base()
        {
        }

        #endregion
    }
}