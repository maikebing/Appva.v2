// <copyright file="SecurityEventType.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//    <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Fhir.Resources.Security.ValueSets
{
    #region Imports.

    using System.Collections.ObjectModel;
    using Complex;
    using Primitives;

    #endregion

    /// <summary>
    /// A <c>SecurityEventType</c>. 
    /// <externalLink>
    ///     <linkText>1.15.2.1.220 SecurityEventType</linkText>
    ///     <linkUri>
    ///         http://hl7.org/implement/standards/FHIR-Develop/valueset-security-event-type.html
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    public sealed class SecurityEventType : CodeableConcept
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
        public static readonly SecurityEventType ApplicationActivity = new SecurityEventType(new Collection<Coding>
            {
                new Coding(System, new Code("110100"), "Application Activity")
            });

        /// <summary>
        /// Audit event: Audit Log has been used.
        /// </summary>
        public static readonly SecurityEventType AuditLogUsed = new SecurityEventType(new Collection<Coding>
            {
                new Coding(System, new Code("110101"), "Audit Log Used")
            });

        /// <summary>
        /// Audit event: Storage of DICOM Instances has begun.
        /// </summary>
        public static readonly SecurityEventType BeginTransferringDicomInstances = new SecurityEventType(new Collection<Coding>
            {
                new Coding(System, new Code("110102"), "Begin Transferring DICOM Instances")
            });

        /// <summary>
        /// Audit event: DICOM Instances have been created, read, updated, or deleted -audit 
        /// event.
        /// </summary>
        public static readonly SecurityEventType DicomInstancesAccessed = new SecurityEventType(new Collection<Coding>
            {
                new Coding(System, new Code("110103"), "DICOM Instances Accessed")
            });

        /// <summary>
        /// Audit event: Storage of DICOM Instances has been completed.
        /// </summary>
        public static readonly SecurityEventType DicomInstancesTransferred = new SecurityEventType(new Collection<Coding>
            {
                new Coding(System, new Code("110104"), "DICOM Instances Transferred")
            });

        /// <summary>
        /// Audit event: Entire Study has been deleted.
        /// </summary>
        public static readonly SecurityEventType DicomStudyDeleted = new SecurityEventType(new Collection<Coding>
            {
                new Coding(System, new Code("110105"), "DICOM Study Deleted")
            });

        /// <summary>
        /// Audit event: Data has been exported out of the system.
        /// </summary>
        public static readonly SecurityEventType Export = new SecurityEventType(new Collection<Coding>
            {
                new Coding(System, new Code("110106"), "Export")
            });

        /// <summary>
        /// Audit event: Data has been imported into the system.
        /// </summary>
        public static readonly SecurityEventType Import = new SecurityEventType(new Collection<Coding>
            {
                new Coding(System, new Code("110107"), "Import")
            });

        /// <summary>
        /// Audit event: System has joined or left network.
        /// </summary>
        public static readonly SecurityEventType NetworkEntry = new SecurityEventType(new Collection<Coding>
            {
                new Coding(System, new Code("110108"), "Network Entry")
            });

        /// <summary>
        /// Audit event: Query has been made.
        /// </summary>
        public static readonly SecurityEventType Query = new SecurityEventType(new Collection<Coding>
            {
                new Coding(System, new Code("110112"), "Query")
            });

        /// <summary>
        /// Audit event: Security Alert has been raised.
        /// </summary>
        public static readonly SecurityEventType SecurityAlert = new SecurityEventType(new Collection<Coding>
            {
                new Coding(System, new Code("110113"), "Security Alert")
            });

        /// <summary>
        /// Audit event: User Authentication has been attempted.
        /// </summary>
        public static readonly SecurityEventType UserAuthentication = new SecurityEventType(new Collection<Coding>
            {
                new Coding(System, new Code("110114"), "User Authentication")
            });

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityEventType"/> class.
        /// </summary>
        /// <param name="coding">
        /// A reference to a code defined by a terminology system
        /// </param>
        private SecurityEventType(Collection<Coding> coding) 
            : base(null, coding)
        {
        }

        #endregion
    }
}