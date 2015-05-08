// <copyright file="AuditEventObjectLifecycle.cs" company="Appva AB">
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
    /// Identifier for the data life-cycle stage for the participant object.
    /// <externalLink>
    ///     <linkText>1.15.2.1.168.1 AuditEventObjectLifecycle</linkText>
    ///     <linkUri>
    ///         http://hl7-fhir.github.io/object-lifecycle.html
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    [FhirVersion(Fhir.V050)]
    [ProtoContract]
    public sealed class AuditEventObjectLifecycle : Code
    {
        #region Variables.

        /// <summary>
        /// Origination / Creation.
        /// </summary>
        public static readonly AuditEventObjectLifecycle Creation = new AuditEventObjectLifecycle("1");

        /// <summary>
        /// Import / Copy from original.
        /// </summary>
        public static readonly AuditEventObjectLifecycle Import = new AuditEventObjectLifecycle("2");

        /// <summary>
        /// Amendment.
        /// </summary>
        public static readonly AuditEventObjectLifecycle Amendment = new AuditEventObjectLifecycle("3");

        /// <summary>
        /// Verification.
        /// </summary>
        public static readonly AuditEventObjectLifecycle Verification = new AuditEventObjectLifecycle("4");

        /// <summary>
        /// Translation.
        /// </summary>
        public static readonly AuditEventObjectLifecycle Translation = new AuditEventObjectLifecycle("5");

        /// <summary>
        /// Access / Use.
        /// </summary>
        public static readonly AuditEventObjectLifecycle Access = new AuditEventObjectLifecycle("6");

        /// <summary>
        /// De-identification.
        /// </summary>
        public static readonly AuditEventObjectLifecycle DeIdentification = new AuditEventObjectLifecycle("7");

        /// <summary>
        /// Aggregation, summarization, derivation.
        /// </summary>
        public static readonly AuditEventObjectLifecycle Aggregation = new AuditEventObjectLifecycle("8");

        /// <summary>
        /// Report.
        /// </summary>
        public static readonly AuditEventObjectLifecycle Report = new AuditEventObjectLifecycle("9");

        /// <summary>
        /// Export / Copy to target.
        /// </summary>
        public static readonly AuditEventObjectLifecycle Export = new AuditEventObjectLifecycle("10");

        /// <summary>
        /// Disclosure.
        /// </summary>
        public static readonly AuditEventObjectLifecycle Disclosure = new AuditEventObjectLifecycle("11");

        /// <summary>
        /// Receipt of disclosure.
        /// </summary>
        public static readonly AuditEventObjectLifecycle ReceiptOfDisclosure = new AuditEventObjectLifecycle("12");

        /// <summary>
        /// Archiving.
        /// </summary>
        public static readonly AuditEventObjectLifecycle Archiving = new AuditEventObjectLifecycle("13");

        /// <summary>
        /// Logical deletion.
        /// </summary>
        public static readonly AuditEventObjectLifecycle LogicalDeletion = new AuditEventObjectLifecycle("14");

        /// <summary>
        /// Permanent erasure / Physical destruction.
        /// </summary>
        public static readonly AuditEventObjectLifecycle PermanentErasure = new AuditEventObjectLifecycle("15");

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditEventObjectLifecycle"/> class.
        /// </summary>
        /// <param name="value">The string code value</param>
        private AuditEventObjectLifecycle(string value) 
            : base(value)
        {
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="AuditEventObjectLifecycle" /> class from being created.
        /// </summary>
        /// <remarks>For Json deserialization</remarks>
        [JsonConstructor]
        private AuditEventObjectLifecycle()
            : base(null)
        {
        }

        #endregion
    }
}