// <copyright file="SecurityEventAction.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Fhir.Resources.Security.ValueSets
{
    #region Imports.

    using Primitives;

    #endregion

    /// <summary>
    /// Indicator for type of action performed during the event that generated the audit.
    /// <externalLink>
    ///     <linkText>1.15.2.1.210.1 SecurityEventAction</linkText>
    ///     <linkUri>
    ///         http://hl7.org/implement/standards/FHIR-Develop/security-event-action.html
    ///     </linkUri>
    /// </externalLink>
    /// This is the equivalent of RIVTA EHR log <c>ActivityTypeType</c>, although the 
    /// activities "Signera", "Vidimera", "Utskrift", "Nödöppning" are considered a sub 
    /// type, see <c>SecutiryEventSubType</c>, to an event.
    /// <externalLink>
    ///     <linkText>EHR Log 1.2 RC2</linkText>
    ///     <linkUri>
    ///         http://rivta.se/downloads/ServiceContracts_ehr_log_1.2_RC2.zip
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    public sealed class SecurityEventAction : Code
    {
        #region Variables.

        /// <summary>
        /// Create a new database object, such as Placing an Order.
        /// </summary>
        public static readonly SecurityEventAction Create = new SecurityEventAction("C");

        /// <summary>
        /// Display or print data, such as a Doctor Census.
        /// </summary>
        public static readonly SecurityEventAction Read = new SecurityEventAction("R");

        /// <summary>
        /// Update data, such as Revise Patient Information.
        /// </summary>
        public static readonly SecurityEventAction Update = new SecurityEventAction("U");

        /// <summary>
        /// Delete items, such as a doctor master file record.
        /// </summary>
        public static readonly SecurityEventAction Delete = new SecurityEventAction("D");

        /// <summary>
        /// Perform a system or application function such as log-on, program execution or 
        /// use of an object's method, or perform a query/search operation.
        /// </summary>
        public static readonly SecurityEventAction Execute = new SecurityEventAction("E");

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityEventAction"/> class.
        /// </summary>
        /// <param name="value">The string code value</param>
        private SecurityEventAction(string value) 
            : base(value)
        {
        }

        #endregion
    }
}