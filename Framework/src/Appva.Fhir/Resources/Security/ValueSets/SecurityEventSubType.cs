// <copyright file="SecurityEventSubType.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Fhir.Resources.Security.ValueSets
{
    #region Imports.

    using System.Collections.ObjectModel;
    using Complex;
    using Primitives;

    #endregion

    /// <summary>
    /// A <c>SecurityEventSubType</c>. 
    /// <externalLink>
    ///     <linkText>1.15.2.1.221.1 Security Event Sub-Type</linkText>
    ///     <linkUri>
    ///         http://hl7.org/implement/standards/FHIR-Develop/valueset-security-event-sub-type.html
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    public sealed class SecurityEventSubType : CodeableConcept
    {
        #region Variables.

        /// <summary>
        /// The identification of the code system that defines the meaning of the symbol in 
        /// the code. 
        /// </summary>
        public static readonly Uri System = Fhir.CreateNewUri("restful-interaction");

        /// <summary>
        /// Audit event: Application Entity has started.
        /// </summary>
        public static readonly SecurityEventSubType ApplicationStart = new SecurityEventSubType(new Collection<Coding>
            {
                new Coding(Dicom.System, new Code("110120"), "Application Start")
            });

        /// <summary>
        /// Audit event: Application Entity has stopped.
        /// </summary>
        public static readonly SecurityEventSubType ApplicationStop = new SecurityEventSubType(new Collection<Coding>
            {
                new Coding(Dicom.System, new Code("110121"), "Application Stop")
            });

        /// <summary>
        /// Audit event: User login has been attempted.
        /// </summary>
        public static readonly SecurityEventSubType Login = new SecurityEventSubType(new Collection<Coding>
            {
                new Coding(Dicom.System, new Code("110122"), "Login")
            });

        /// <summary>
        /// Audit event: User logout has been attempted.
        /// </summary>
        public static readonly SecurityEventSubType Logout = new SecurityEventSubType(new Collection<Coding>
            {
                new Coding(Dicom.System, new Code("110123"), "Logout")
            });

        /// <summary>
        /// Audit event: Node has been attached.
        /// </summary>
        public static readonly SecurityEventSubType Attach = new SecurityEventSubType(new Collection<Coding>
            {
                new Coding(Dicom.System, new Code("110124"), "Attach")
            });

        /// <summary>
        /// Audit event: Node has been detached.
        /// </summary>
        public static readonly SecurityEventSubType Detach = new SecurityEventSubType(new Collection<Coding>
            {
                new Coding(Dicom.System, new Code("110125"), "Detach")
            });

        /// <summary>
        /// Audit event: Node Authentication has been attempted.
        /// </summary>
        public static readonly SecurityEventSubType NodeAuthentication = new SecurityEventSubType(new Collection<Coding>
            {
                new Coding(Dicom.System, new Code("110126"), "Node Authentication")
            });

        /// <summary>
        /// Audit event: Emergency Override has started.
        /// </summary>
        public static readonly SecurityEventSubType EmergencyOverrideStarted = new SecurityEventSubType(new Collection<Coding>
            {
                new Coding(Dicom.System, new Code("110127"), "Emergency Override Started")
            });

        /// <summary>
        /// Audit event: Network configuration has been changed.
        /// </summary>
        public static readonly SecurityEventSubType NetworkConfiguration = new SecurityEventSubType(new Collection<Coding>
            {
                new Coding(Dicom.System, new Code("110128"), "Network Configuration")
            });

        /// <summary>
        /// Audit event: Security configuration has been changed.
        /// </summary>
        public static readonly SecurityEventSubType SecurityConfiguration = new SecurityEventSubType(new Collection<Coding>
            {
                new Coding(Dicom.System, new Code("110129"), "Security Configuration")
            });

        /// <summary>
        /// Audit event: Hardware configuration has been changed.
        /// </summary>
        public static readonly SecurityEventSubType HardwareConfiguration = new SecurityEventSubType(new Collection<Coding>
            {
                new Coding(Dicom.System, new Code("110130"), "Hardware Configuration")
            });

        /// <summary>
        /// Audit event: Software configuration has been changed.
        /// </summary>
        public static readonly SecurityEventSubType SoftwareConfiguration = new SecurityEventSubType(new Collection<Coding>
            {
                new Coding(Dicom.System, new Code("110131"), "Software Configuration")
            });

        /// <summary>
        /// Audit event: A use of a restricted function has been attempted.
        /// </summary>
        public static readonly SecurityEventSubType UseOfRestrictedFunction = new SecurityEventSubType(new Collection<Coding>
            {
                new Coding(Dicom.System, new Code("110132"), "Use of Restricted Function")
            });

        /// <summary>
        /// Audit event: Audit recording has been stopped.
        /// </summary>
        public static readonly SecurityEventSubType AuditRecordingStopped = new SecurityEventSubType(new Collection<Coding>
            {
                new Coding(Dicom.System, new Code("110133"), "Audit Recording Stopped")
            });

        /// <summary>
        /// Audit event: Audit recording has been started.
        /// </summary>
        public static readonly SecurityEventSubType AuditRecordingStarted = new SecurityEventSubType(new Collection<Coding>
            {
                new Coding(Dicom.System, new Code("110134"), "Audit Recording Started")
            });

        /// <summary>
        /// Audit event: Security attributes of an object have been changed.
        /// </summary>
        public static readonly SecurityEventSubType ObjectSecurityAttributesChanged = new SecurityEventSubType(new Collection<Coding>
            {
                new Coding(Dicom.System, new Code("110135"), "Object Security Attributes Changed")
            });

        /// <summary>
        /// Audit event: Security roles have been changed.
        /// </summary>
        public static readonly SecurityEventSubType SecurityRolesChanged = new SecurityEventSubType(new Collection<Coding>
            {
                new Coding(Dicom.System, new Code("110136"), "Security Roles Changed")
            });

        /// <summary>
        /// Audit event: Security attributes of a user have been changed.
        /// </summary>
        public static readonly SecurityEventSubType UserSecurityAttributesChanged = new SecurityEventSubType(new Collection<Coding>
            {
                new Coding(Dicom.System, new Code("110137"), "User security Attributes Changed")
            });

        /// <summary>
        /// Audit event: Emergency Override has Stopped.
        /// </summary>
        public static readonly SecurityEventSubType EmergencyOverrideStopped = new SecurityEventSubType(new Collection<Coding>
            {
                new Coding(Dicom.System, new Code("110138"), "Emergency Override Stopped")
            });

        /// <summary>
        /// Audit event: Remote Service Operation has Begun.
        /// </summary>
        public static readonly SecurityEventSubType RemoteServiceOperationStarted = new SecurityEventSubType(new Collection<Coding>
            {
                new Coding(Dicom.System, new Code("110139"), "Remote Service Operation Started")
            });

        /// <summary>
        /// Audit event: Remote Service Operation Stopped.
        /// </summary>
        public static readonly SecurityEventSubType RemoteServiceOperationStopped = new SecurityEventSubType(new Collection<Coding>
            {
                new Coding(Dicom.System, new Code("110140"), "Remote Service Operation Stopped")
            });

        /// <summary>
        /// Audit event: Local Service Operation has Begun.
        /// </summary>
        public static readonly SecurityEventSubType LocalServiceOperationStarted = new SecurityEventSubType(new Collection<Coding>
            {
                new Coding(Dicom.System, new Code("110141"), "Local Service Operation Started")
            });

        /// <summary>
        /// Audit event: Local Service Operation Stopped.
        /// </summary>
        public static readonly SecurityEventSubType LocalServiceOperationStopped = new SecurityEventSubType(new Collection<Coding>
            {
                new Coding(Dicom.System, new Code("110142"), "Local Service Operation Stopped")
            });

        /// <summary>
        /// Read the current state of the resource.
        /// </summary>
        public static readonly SecurityEventSubType Read = new SecurityEventSubType(new Collection<Coding>
            {
                new Coding(System, new Code("read"), "read")
            });

        /// <summary>
        /// Read the state of a specific version of the resource.
        /// </summary>
        public static readonly SecurityEventSubType VRead = new SecurityEventSubType(new Collection<Coding>
            {
                new Coding(System, new Code("vread"), "vread")
            });

        /// <summary>
        /// Update an existing resource by its id (or create it if it is new).
        /// </summary>
        public static readonly SecurityEventSubType Update = new SecurityEventSubType(new Collection<Coding>
            {
                new Coding(System, new Code("update"), "update")
            });

        /// <summary>
        /// Delete a resource.
        /// </summary>
        public static readonly SecurityEventSubType Delete = new SecurityEventSubType(new Collection<Coding>
            {
                new Coding(System, new Code("delete"), "delete")
            });

        /// <summary>
        /// Check that the content would be acceptable as an update.
        /// </summary>
        public static readonly SecurityEventSubType Validate = new SecurityEventSubType(new Collection<Coding>
            {
                new Coding(System, new Code("validate"), "validate")
            });

        /// <summary>
        /// Create a new resource with a server assigned id.
        /// </summary>
        public static readonly SecurityEventSubType Create = new SecurityEventSubType(new Collection<Coding>
            {
                new Coding(System, new Code("create"), "create")
            });

        /// <summary>
        /// Retrieve the update history for a particular resource.
        /// </summary>
        public static readonly SecurityEventSubType HistoryInstance = new SecurityEventSubType(new Collection<Coding>
            {
                new Coding(System, new Code("history-instance"), "history-instance")
            });

        /// <summary>
        /// Retrieve the update history for a all resources of a particular type.
        /// </summary>
        public static readonly SecurityEventSubType HistoryType = new SecurityEventSubType(new Collection<Coding>
            {
                new Coding(System, new Code("history-type"), "history-type")
            });

        /// <summary>
        /// Retrieve the update history for all resources on a system.
        /// </summary>
        public static readonly SecurityEventSubType HistorySystem = new SecurityEventSubType(new Collection<Coding>
            {
                new Coding(System, new Code("history-system"), "history-system")
            });

        /// <summary>
        /// Search all resources of the specified type based on some filter criteria.
        /// </summary>
        public static readonly SecurityEventSubType SearchType = new SecurityEventSubType(new Collection<Coding>
            {
                new Coding(System, new Code("search-type"), "search-type")
            });

        /// <summary>
        /// Search all resources based on some filter criteria.
        /// </summary>
        public static readonly SecurityEventSubType SearchSystem = new SecurityEventSubType(new Collection<Coding>
            {
                new Coding(System, new Code("search-system"), "search-system")
            });

        /// <summary>
        /// Update, create or delete a set of resources as a single transaction.
        /// </summary>
        public static readonly SecurityEventSubType Transaction = new SecurityEventSubType(new Collection<Coding>
            {
                new Coding(System, new Code("transaction"), "transaction")
            });

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityEventSubType"/> class.
        /// </summary>
        /// <param name="coding">
        /// A reference to a code defined by a terminology system
        /// </param>
        private SecurityEventSubType(Collection<Coding> coding) 
            : base(null, coding)
        {
        }

        #endregion
    }
}
