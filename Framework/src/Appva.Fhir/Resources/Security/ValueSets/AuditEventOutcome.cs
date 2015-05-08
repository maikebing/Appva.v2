// <copyright file="AuditEventOutcome.cs" company="Appva AB">
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
    /// Indicates whether the event succeeded or failed.
    /// <externalLink>
    ///     <linkText>1.15.2.1.211.1 AuditEventOutcome</linkText>
    ///     <linkUri>
    ///         http://hl7-fhir.github.io/audit-event-outcome.html
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    [FhirVersion(Fhir.V050)]
    [ProtoContract]
    public sealed class AuditEventOutcome : Code
    {
        #region Variables.

        /// <summary>
        /// The operation completed successfully (whether with warnings or not).
        /// </summary>
        public static readonly AuditEventOutcome Success = new AuditEventOutcome("0");

        /// <summary>
        /// The action was not successful due to some kind of catered for error (often 
        /// equivalent to an HTTP 400 response).
        /// </summary>
        public static readonly AuditEventOutcome MinorFailure = new AuditEventOutcome("4");

        /// <summary>
        /// The action was not successful due to some kind of unexpected error (often 
        /// equivalent to an HTTP 500 response).
        /// </summary>
        public static readonly AuditEventOutcome SeriousFailure = new AuditEventOutcome("8");

        /// <summary>
        /// An error of such magnitude occurred that the system is not longer available for 
        /// use (i.e. the system died).
        /// </summary>
        public static readonly AuditEventOutcome MajorFailure = new AuditEventOutcome("12");

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditEventOutcome"/> class.
        /// </summary>
        /// <param name="value">The string code value</param>
        private AuditEventOutcome(string value) 
            : base(value)
        {
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="AuditEventOutcome" /> class from being created.
        /// </summary>
        /// <remarks>For Json deserialization</remarks>
        [JsonConstructor]
        private AuditEventOutcome() 
            : base(null)
        {
        }

        #endregion
    }
}