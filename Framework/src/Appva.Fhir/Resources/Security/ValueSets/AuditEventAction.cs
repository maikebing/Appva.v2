// <copyright file="AuditEventAction.cs" company="Appva AB">
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
    /// Indicator for type of action performed during the event that generated the audit.
    /// <externalLink>
    ///     <linkText>1.15.2.1.210.1 AuditEventAction</linkText>
    ///     <linkUri>
    ///         http://hl7-fhir.github.io/audit-event-action.html
    ///     </linkUri>
    /// </externalLink>
    /// This is the equivalent of RIVTA EHR log <c>ActivityTypeType</c>, although the 
    /// activities "Signera", "Vidimera", "Utskrift", "Nödöppning" are considered a sub 
    /// type, see <c>AuditEventSubType</c>, to an event.
    /// <externalLink>
    ///     <linkText>EHR Log 1.2 RC2</linkText>
    ///     <linkUri>
    ///         http://rivta.se/downloads/ServiceContracts_ehr_log_1.2_RC2.zip
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    [FhirVersion(Fhir.V050)]
    [ProtoContract]
    public sealed class AuditEventAction : Code
    {
        #region Variables.

        /// <summary>
        /// Create a new database object, such as Placing an Order.
        /// </summary>
        public static readonly AuditEventAction Create = new AuditEventAction("C");

        /// <summary>
        /// Display or print data, such as a Doctor Census.
        /// </summary>
        public static readonly AuditEventAction Read = new AuditEventAction("R");

        /// <summary>
        /// Update data, such as Revise Patient Information.
        /// </summary>
        public static readonly AuditEventAction Update = new AuditEventAction("U");

        /// <summary>
        /// Delete items, such as a doctor master file record.
        /// </summary>
        public static readonly AuditEventAction Delete = new AuditEventAction("D");

        /// <summary>
        /// Perform a system or application function such as log-on, program execution or 
        /// use of an object's method, or perform a query/search operation.
        /// </summary>
        public static readonly AuditEventAction Execute = new AuditEventAction("E");

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditEventAction"/> class.
        /// </summary>
        /// <param name="value">The string code value</param>
        private AuditEventAction(string value) 
            : base(value)
        {
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="AuditEventAction" /> class from being created.
        /// </summary>
        /// <remarks>For Json deserialization</remarks>
        [JsonConstructor]
        private AuditEventAction() 
            : base(null)
        {
        }

        #endregion
    }
}