// <copyright file="SecurityEventOutcome.cs" company="Appva AB">
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
    /// Indicates whether the event succeeded or failed.
    /// <externalLink>
    ///     <linkText>1.15.2.1.211.1 SecurityEventOutcome</linkText>
    ///     <linkUri>
    ///         http://hl7.org/implement/standards/FHIR-Develop/security-event-outcome.html
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    public sealed class SecurityEventOutcome : Code
    {
        #region Variables.

        /// <summary>
        /// The operation completed successfully (whether with warnings or not).
        /// </summary>
        public static readonly SecurityEventOutcome Success = new SecurityEventOutcome("0");

        /// <summary>
        /// The action was not successful due to some kind of catered for error (often 
        /// equivalent to an HTTP 400 response).
        /// </summary>
        public static readonly SecurityEventOutcome MinorFailure = new SecurityEventOutcome("4");

        /// <summary>
        /// The action was not successful due to some kind of unexpected error (often 
        /// equivalent to an HTTP 500 response).
        /// </summary>
        public static readonly SecurityEventOutcome SeriousFailure = new SecurityEventOutcome("8");

        /// <summary>
        /// An error of such magnitude occurred that the system is not longer available for 
        /// use (i.e. the system died).
        /// </summary>
        public static readonly SecurityEventOutcome MajorFailure = new SecurityEventOutcome("12");

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityEventOutcome"/> class.
        /// </summary>
        /// <param name="value">The string code value</param>
        private SecurityEventOutcome(string value) 
            : base(value)
        {
        }

        #endregion
    }
}