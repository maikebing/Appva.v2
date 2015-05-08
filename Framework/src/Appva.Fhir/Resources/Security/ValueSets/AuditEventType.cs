// <copyright file="AuditEventType.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//    <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
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
    /// A <c>AuditEventType</c>. 
    /// <externalLink>
    ///     <linkText>1.15.2.1.220 AuditEventType</linkText>
    ///     <linkUri>
    ///         http://hl7-fhir.github.io/valueset-audit-event-type.html
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    [FhirVersion(Fhir.V050)]
    [ProtoContract]
    public sealed class AuditEventType : CodeableConcept
    {
        #region Variables.

        /// <summary>
        /// The identification of the code system that defines the meaning of the symbol in 
        /// the code. 
        /// </summary>
        public static readonly Uri System = Fhir.CreateNewUri("security-event-type");

        /// <summary>
        /// Audit event: Application Activity has taken place.
        /// </summary>
        public static readonly AuditEventType ApplicationActivity = new AuditEventType(new Coding(System, new Code("110100"), "Application Activity"));

        /// <summary>
        /// Audit event: Audit Log has been used.
        /// </summary>
        public static readonly AuditEventType AuditLogUsed = new AuditEventType(new Coding(System, new Code("110101"), "Audit Log Used"));

        /// <summary>
        /// Audit event: Storage of DICOM Instances has begun.
        /// </summary>
        public static readonly AuditEventType BeginTransferringDicomInstances = new AuditEventType(new Coding(System, new Code("110102"), "Begin Transferring DICOM Instances"));

        /// <summary>
        /// Audit event: DICOM Instances have been created, read, updated, or deleted -audit 
        /// event.
        /// </summary>
        public static readonly AuditEventType DicomInstancesAccessed = new AuditEventType(new Coding(System, new Code("110103"), "DICOM Instances Accessed"));

        /// <summary>
        /// Audit event: Storage of DICOM Instances has been completed.
        /// </summary>
        public static readonly AuditEventType DicomInstancesTransferred = new AuditEventType(new Coding(System, new Code("110104"), "DICOM Instances Transferred"));

        /// <summary>
        /// Audit event: Entire Study has been deleted.
        /// </summary>
        public static readonly AuditEventType DicomStudyDeleted = new AuditEventType(new Coding(System, new Code("110105"), "DICOM Study Deleted"));

        /// <summary>
        /// Audit event: Data has been exported out of the system.
        /// </summary>
        public static readonly AuditEventType Export = new AuditEventType(new Coding(System, new Code("110106"), "Export"));

        /// <summary>
        /// Audit event: Data has been imported into the system.
        /// </summary>
        public static readonly AuditEventType Import = new AuditEventType(new Coding(System, new Code("110107"), "Import"));

        /// <summary>
        /// Audit event: System has joined or left network.
        /// </summary>
        public static readonly AuditEventType NetworkEntry = new AuditEventType(new Coding(System, new Code("110108"), "Network Entry"));

        /// <summary>
        /// Audit event: Query has been made.
        /// </summary>
        public static readonly AuditEventType Query = new AuditEventType(new Coding(System, new Code("110112"), "Query"));

        /// <summary>
        /// Audit event: Security Alert has been raised.
        /// </summary>
        public static readonly AuditEventType SecurityAlert = new AuditEventType(new Coding(System, new Code("110113"), "Security Alert"));

        /// <summary>
        /// Audit event: User Authentication has been attempted.
        /// </summary>
        public static readonly AuditEventType UserAuthentication = new AuditEventType(new Coding(System, new Code("110114"), "User Authentication"));

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditEventType"/> class.
        /// </summary>
        /// <param name="coding">
        /// A reference to a code defined by a terminology system
        /// </param>
        public AuditEventType(IList<Coding> coding) 
            : base(null, coding)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditEventType"/> class.
        /// </summary>
        /// <param name="coding">
        /// A reference to a code defined by a terminology system
        /// </param>
        public AuditEventType(params Coding[] coding)
            : base(null, coding)
        {
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="AuditEventType" /> class from being created.
        /// </summary>
        /// <remarks>For Json deserialization</remarks>
        [JsonConstructor]
        private AuditEventType() 
            : base()
        {
        }

        #endregion
    }
}