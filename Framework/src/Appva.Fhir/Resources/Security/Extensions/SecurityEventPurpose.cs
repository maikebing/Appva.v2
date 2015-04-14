// <copyright file="SecurityEventPurpose.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Fhir.Resources.Security.Extensions
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Fhir.Primitives;

    #endregion

    /// <summary>
    /// xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
    /// Enumerationsvärde som anger syfte till aktivitet. 
    /// Kan vara Vård och behandling, Kvalitetssäkring, Annan dokumentation enligt lag, Statistik, Administration och Kvalitetsregister
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
    public sealed class SecurityEventPurpose : Code
    {
        #region Variables.

        /// <summary>
        /// xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        /// Vård och behandling.
        /// Innefattar följande ändamål enligt Patientdatalag 2 kap 4§: Att fullgöra de 
        /// skyldigheter som anges i 3 kap. (Skyldigheten att föra patientjournal) och 
        /// upprätta annan dokumentation som behövs i och för vården av patienter. 
        /// Administration som rör patienter och som syftar till att ge vård i enskilda fall 
        /// eller som annars föranleds av vård i enskilda fall.
        /// </summary>
        public static SecurityEventPurpose Treatment = new SecurityEventPurpose("Vård och behandling");

        /// <summary>
        /// xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        /// Kvalitetssäkring.
        /// Innefattar följande ändamål enligt Patientdatalag 2 kap 4§: Systematiskt och 
        /// fortlöpande utveckla och säkra kvaliteten i verksamheten.
        /// </summary>
        public static SecurityEventPurpose QualityAssurance = new SecurityEventPurpose("Kvalitetssäkring");

        /// <summary>
        /// xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        /// Annan dokumentation enligt lag.
        /// Innefattar följande ändamål enligt Patientdatalag 2 kap 4§: Upprätta annan 
        /// dokumentation som följer av lag, förordning eller annan författning.
        /// </summary>
        public static SecurityEventPurpose OtherDocumentationByLaw = new SecurityEventPurpose("Annan dokumentation enligt lag");

        /// <summary>
        /// xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        /// Statistik.
        /// Innefattar följande ändamål enligt Patientdatalag 2 kap 4§: Framställa statistik 
        /// om hälso- och sjukvården.
        /// </summary>
        public static SecurityEventPurpose Statistics = new SecurityEventPurpose("Statistik");

        /// <summary>
        /// xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        /// Administration.
        /// Innefattar följande ändamål enligt Patientdatalag 2 kap 4§: Administration, 
        /// planering, uppföljning, utvärdering och tillsyn av verksamheten.
        /// </summary>
        public static SecurityEventPurpose Administration = new SecurityEventPurpose("Administration");

        /// <summary>
        /// xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        /// Kvalitetsregister.
        /// Beskrivs i Patientdatalagen 7 kap 4§: Att systematiskt och fortlöpande utveckla 
        /// och säkra vårdens kvalitet. Ändamålet ska användas vid kvalitetsuppföljning i av 
        /// SKL godkända kvalitetsregister.
        /// </summary>
        public static SecurityEventPurpose QualityIndex = new SecurityEventPurpose("Kvalitetsregister");

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityEventPurpose"/> class.
        /// </summary>
        /// <param name="value">The string code value</param>
        private SecurityEventPurpose(string value)
            : base(value)
        {
        }

        #endregion
    }
}