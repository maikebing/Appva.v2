// <copyright file="AuditEventPurpose.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Fhir.Resources.Security.Extensions
{
    #region Imports.

    using Primitives;
    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// Enumerationsvärde som anger syfte till aktivitet. 
    /// Kan vara Vård och behandling, Kvalitetssäkring, Annan dokumentation enligt lag, 
    /// Statistik, Administration och Kvalitetsregister
    /// <para>
    /// This is the equivalent of RIVTA EHR log <c>PurposeTypeType</c>.
    /// </para>
    /// <externalLink>
    ///     <linkText>EHR Log 1.2 RC2</linkText>
    ///     <linkUri>
    ///         http://rivta.se/downloads/ServiceContracts_ehr_log_1.2_RC2.zip
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    public sealed class AuditEventPurpose : Code
    {
        #region Variables.

        /// <summary>
        /// Vård och behandling.
        /// Innefattar följande ändamål enligt Patientdatalag 2 kap 4§: Att fullgöra de 
        /// skyldigheter som anges i 3 kap. (Skyldigheten att föra patientjournal) och 
        /// upprätta annan dokumentation som behövs i och för vården av patienter. 
        /// Administration som rör patienter och som syftar till att ge vård i enskilda fall 
        /// eller som annars föranleds av vård i enskilda fall.
        /// </summary>
        public static readonly AuditEventPurpose Treatment = new AuditEventPurpose("Vård och behandling");

        /// <summary>
        /// Kvalitetssäkring.
        /// Innefattar följande ändamål enligt Patientdatalag 2 kap 4§: Systematiskt och 
        /// fortlöpande utveckla och säkra kvaliteten i verksamheten.
        /// </summary>
        public static readonly AuditEventPurpose QualityAssurance = new AuditEventPurpose("Kvalitetssäkring");

        /// <summary>
        /// Annan dokumentation enligt lag.
        /// Innefattar följande ändamål enligt Patientdatalag 2 kap 4§: Upprätta annan 
        /// dokumentation som följer av lag, förordning eller annan författning.
        /// </summary>
        public static readonly AuditEventPurpose OtherDocumentationByLaw = new AuditEventPurpose("Annan dokumentation enligt lag");

        /// <summary>
        /// Statistik.
        /// Innefattar följande ändamål enligt Patientdatalag 2 kap 4§: Framställa statistik 
        /// om hälso- och sjukvården.
        /// </summary>
        public static readonly AuditEventPurpose Statistics = new AuditEventPurpose("Statistik");

        /// <summary>
        /// Administration.
        /// Innefattar följande ändamål enligt Patientdatalag 2 kap 4§: Administration, 
        /// planering, uppföljning, utvärdering och tillsyn av verksamheten.
        /// </summary>
        public static readonly AuditEventPurpose Administration = new AuditEventPurpose("Administration");

        /// <summary>
        /// Kvalitetsregister.
        /// Beskrivs i Patientdatalagen 7 kap 4§: Att systematiskt och fortlöpande utveckla 
        /// och säkra vårdens kvalitet. Ändamålet ska användas vid kvalitetsuppföljning i av 
        /// SKL godkända kvalitetsregister.
        /// </summary>
        public static readonly AuditEventPurpose QualityIndex = new AuditEventPurpose("Kvalitetsregister");

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditEventPurpose"/> class.
        /// </summary>
        /// <param name="value">The string code value</param>
        private AuditEventPurpose(string value)
            : base(value)
        {
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="AuditEventPurpose" /> class from being created.
        /// </summary>
        /// <remarks>For Json deserialization</remarks>
        [JsonConstructor]
        private AuditEventPurpose()
            : base(null)
        {
        }

        #endregion
    }
}