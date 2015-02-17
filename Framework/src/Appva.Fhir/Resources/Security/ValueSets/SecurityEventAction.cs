// <copyright file="SecurityEventAction.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Fhir.Resources.Security.ValueSets
{
    #region Imports.

    using Appva.Fhir.Primitives;

    #endregion

    /// <summary>
    /// Indicator for type of action performed during the event that generated the audit.
    /// <externalLink>
    ///     <linkText>1.15.2.1.210.1 SecurityEventAction</linkText>
    ///     <linkUri>
    ///         http://hl7.org/implement/standards/FHIR-Develop/security-event-action.html
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    public static class SecurityEventAction
    {
        /// <summary>
        /// Create a new database object, such as Placing an Order.
        /// </summary>
        public static readonly Code Create = new Code("C");

        /// <summary>
        /// Display or print data, such as a Doctor Census.
        /// </summary>
        public static readonly Code Read = new Code("R");

        /// <summary>
        /// Update data, such as Revise Patient Information.
        /// </summary>
        public static readonly Code Update = new Code("U");

        /// <summary>
        /// Delete items, such as a doctor master file record.
        /// </summary>
        public static readonly Code Delete = new Code("D");

        /// <summary>
        /// Perform a system or application function such as log-on, program execution or 
        /// use of an object's method, or perform a query/search operation.
        /// </summary>
        public static readonly Code Execute = new Code("E");
    }
}