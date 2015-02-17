// <copyright file="SecurityEventObjectLifecycle.cs" company="Appva AB">
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
    /// Identifier for the data life-cycle stage for the participant object.
    /// <externalLink>
    ///     <linkText>1.15.2.1.168.1 SecurityEventObjectLifecycle</linkText>
    ///     <linkUri>
    ///         http://hl7.org/implement/standards/FHIR-Develop/object-lifecycle.html
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    public static class SecurityEventObjectLifecycle
    {
        /// <summary>
        /// Origination / Creation.
        /// </summary>
        public static readonly Code Creation = new Code("1");

        /// <summary>
        /// Import / Copy from original.
        /// </summary>
        public static readonly Code Import = new Code("2");

        /// <summary>
        /// Amendment.
        /// </summary>
        public static readonly Code Amendment = new Code("3");

        /// <summary>
        /// Verification.
        /// </summary>
        public static readonly Code Verification = new Code("4");

        /// <summary>
        /// Translation.
        /// </summary>
        public static readonly Code Translation = new Code("5");

        /// <summary>
        /// Access / Use.
        /// </summary>
        public static readonly Code Access = new Code("6");

        /// <summary>
        /// De-identification.
        /// </summary>
        public static readonly Code DeIdentification = new Code("7");

        /// <summary>
        /// Aggregation, summarization, derivation.
        /// </summary>
        public static readonly Code Aggregation = new Code("8");

        /// <summary>
        /// Report.
        /// </summary>
        public static readonly Code Report = new Code("9");

        /// <summary>
        /// Export / Copy to target.
        /// </summary>
        public static readonly Code Export = new Code("10");

        /// <summary>
        /// Disclosure.
        /// </summary>
        public static readonly Code Disclosure = new Code("11");

        /// <summary>
        /// Receipt of disclosure.
        /// </summary>
        public static readonly Code ReceiptOfDisclosure = new Code("12");

        /// <summary>
        /// Archiving.
        /// </summary>
        public static readonly Code Archiving = new Code("13");

        /// <summary>
        /// Logical deletion.
        /// </summary>
        public static readonly Code LogicalDeletion = new Code("14");

        /// <summary>
        /// Permanent erasure / Physical destruction.
        /// </summary>
        public static readonly Code PermanentErasure = new Code("15");
    }
}