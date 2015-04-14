// <copyright file="SecurityEventObjectLifecycle.cs" company="Appva AB">
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
    /// Identifier for the data life-cycle stage for the participant object.
    /// <externalLink>
    ///     <linkText>1.15.2.1.168.1 SecurityEventObjectLifecycle</linkText>
    ///     <linkUri>
    ///         http://hl7.org/implement/standards/FHIR-Develop/object-lifecycle.html
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    public sealed class SecurityEventObjectLifecycle : Code
    {
        #region Variables.

        /// <summary>
        /// Origination / Creation.
        /// </summary>
        public static readonly SecurityEventObjectLifecycle Creation = new SecurityEventObjectLifecycle("1");

        /// <summary>
        /// Import / Copy from original.
        /// </summary>
        public static readonly SecurityEventObjectLifecycle Import = new SecurityEventObjectLifecycle("2");

        /// <summary>
        /// Amendment.
        /// </summary>
        public static readonly SecurityEventObjectLifecycle Amendment = new SecurityEventObjectLifecycle("3");

        /// <summary>
        /// Verification.
        /// </summary>
        public static readonly SecurityEventObjectLifecycle Verification = new SecurityEventObjectLifecycle("4");

        /// <summary>
        /// Translation.
        /// </summary>
        public static readonly SecurityEventObjectLifecycle Translation = new SecurityEventObjectLifecycle("5");

        /// <summary>
        /// Access / Use.
        /// </summary>
        public static readonly SecurityEventObjectLifecycle Access = new SecurityEventObjectLifecycle("6");

        /// <summary>
        /// De-identification.
        /// </summary>
        public static readonly SecurityEventObjectLifecycle DeIdentification = new SecurityEventObjectLifecycle("7");

        /// <summary>
        /// Aggregation, summarization, derivation.
        /// </summary>
        public static readonly SecurityEventObjectLifecycle Aggregation = new SecurityEventObjectLifecycle("8");

        /// <summary>
        /// Report.
        /// </summary>
        public static readonly SecurityEventObjectLifecycle Report = new SecurityEventObjectLifecycle("9");

        /// <summary>
        /// Export / Copy to target.
        /// </summary>
        public static readonly SecurityEventObjectLifecycle Export = new SecurityEventObjectLifecycle("10");

        /// <summary>
        /// Disclosure.
        /// </summary>
        public static readonly SecurityEventObjectLifecycle Disclosure = new SecurityEventObjectLifecycle("11");

        /// <summary>
        /// Receipt of disclosure.
        /// </summary>
        public static readonly SecurityEventObjectLifecycle ReceiptOfDisclosure = new SecurityEventObjectLifecycle("12");

        /// <summary>
        /// Archiving.
        /// </summary>
        public static readonly SecurityEventObjectLifecycle Archiving = new SecurityEventObjectLifecycle("13");

        /// <summary>
        /// Logical deletion.
        /// </summary>
        public static readonly SecurityEventObjectLifecycle LogicalDeletion = new SecurityEventObjectLifecycle("14");

        /// <summary>
        /// Permanent erasure / Physical destruction.
        /// </summary>
        public static readonly SecurityEventObjectLifecycle PermanentErasure = new SecurityEventObjectLifecycle("15");

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityEventObjectLifecycle"/> class.
        /// </summary>
        /// <param name="value">The string code value</param>
        private SecurityEventObjectLifecycle(string value) 
            : base(value)
        {
        }

        #endregion
    }
}