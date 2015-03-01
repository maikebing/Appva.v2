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
    public static class SecurityEventOutcome
    {
        /// <summary>
        /// The operation completed successfully (whether with warnings or not).
        /// </summary>
        public static readonly Code Success = new Code("0");

        /// <summary>
        /// The action was not successful due to some kind of catered for error (often 
        /// equivalent to an HTTP 400 response).
        /// </summary>
        public static readonly Code MinorFailure = new Code("4");

        /// <summary>
        /// The action was not successful due to some kind of unexpected error (often 
        /// equivalent to an HTTP 500 response).
        /// </summary>
        public static readonly Code SeriousFailure = new Code("8");

        /// <summary>
        /// An error of such magnitude occurred that the system is not longer available for 
        /// use (i.e. the system died).
        /// </summary>
        public static readonly Code MajorFailure = new Code("12");
    }
}