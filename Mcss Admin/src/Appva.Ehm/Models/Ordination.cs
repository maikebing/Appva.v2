// <copyright file="Ordination.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.Ehm.Models
{
    #region Imports.

    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [JsonObject]
    public class Ordination
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Ordination"/> class.
        /// </summary>
        public Ordination()
        {
        }

        #endregion

        #region Properties.

        [JsonProperty("ordinationsstatus")]
        public int? Status
        {
            get;
            set;
        }

        [JsonProperty("ordinationsId")]
        public long Id
        {
            get;
            set;
        }

        [JsonProperty("historisktOrdinationsId")]
        public long? HistoricalOrdinationId
        {
            get;
            set;
        }

        [JsonProperty("ordinationstyp")]
        public string OrdinationType
        {
            get;
            set;
        }

        [JsonProperty("framtidaOrdination")]
        public bool? IsCommingOrdination
        {
            get;
            set;
        }

        [JsonProperty("ordinationInsattningstidpunkt")]
        public DateTime? OrdinationStartsAt
        {
            get;
            set;
        }

        [JsonProperty("ordinationstidpunkt")]
        public DateTime OrdinationCreatedAt
        {
            get;
            set;
        }

        [JsonProperty("ordinationSistaGiltighetsdag")]
        public DateTime? OrdinationValidUntil
        {
            get;
            set;
        }

        [JsonProperty("behandlingsstart")]
        public DateTime? TreatmentStartsAt
        {
            get;
            set;
        }

        [JsonProperty("behandlingsslut")]
        public DateTime? TreatmentEndsAt
        {
            get;
            set;
        }

        [JsonProperty("behandlingAndamalKlartext")]
        public string TreatmentPurpose
        {
            get;
            set;
        }

        [JsonProperty("behandlingDoseringstext1")]
        public string TreatmentDosageText1
        {
            get;
            set;
        }

        [JsonProperty("behandlingDoseringstext2")]
        public string TreatmentDosageText2
        {
            get;
            set;
        }

        [JsonProperty("doseringsschema")]
        public DosageScheme Dosing
        {
            get;
            set;
        }

        [JsonProperty("utsattningstidpunkt")]
        public DateTime? DiscontinuedAt
        {
            get;
            set;
        }

        [JsonProperty("utsattningstyp")]
        public string DiscontinuedType
        {
            get;
            set;
        }

        [JsonProperty("utsattningKommentar")]
        public string DiscontinuedComment
        {
            get;
            set;
        }

        #region Expidition.

        [JsonProperty("senasteUttagsdatum")]
        public DateTime? LastExpiditedAt
        {
            get;
            set;
        }

        [JsonProperty("senastExpedieradMangd")]
        public double? LastExpiditedAmount
        {
            get;
            set;
        }

        [JsonProperty("senastExpedieratNplPackId")]
        public string LastExpiditedNplPackId
        {
            get;
            set;
        }

        [JsonProperty("ordinationForman")]
        public bool OridnationSubsidize
        {
            get;
            set;
        }

        [JsonProperty("helforpackningForeskrivetAntalUttag")]
        public int? NumbersOfExpiditions
        {
            get;
            set;
        }

        [JsonProperty("helforpackningResterandeAntalUttag")]
        public int? RemainingExpiditions
        {
            get;
            set;
        }

        [JsonProperty("helforpackningForstaUttagFore")]
        public DateTime? FirstExpiditionBefore
        {
            get;
            set;
        }

        [JsonProperty("helforpackningStartforpackning")]
        public bool? ExpiditionStartPackage
        {
            get;
            set;
        }

        #endregion

        #region Article

        [JsonProperty("artikelNamn")]
        public string ArticleName
        {
            get;
            set;
        }

        /// <summary>
        /// FIXME: NAME!!
        /// </summary>
        [JsonProperty("artikelbenamning")]
        public string ArticleNaming
        {
            get;
            set;
        }

        [JsonProperty("artikelForm")]
        public string ArticleForm
        {
            get;
            set;
        }

        [JsonProperty("artikelStyrka")]
        public string ArticleStrength
        {
            get;
            set;
        }

        [JsonProperty("nplId")]
        public string NplId
        {
            get;
            set;
        }

        [JsonProperty("nplPackId")]
        public string NplPackId
        {
            get;
            set;
        }

        [JsonProperty("forpackningsmangd")]
        public double ArticlePackageSize
        {
            get;
            set;
        }

        [JsonProperty("forpackningsenhet")]
        public string ArticlePackageUnit
        {
            get;
            set;
        }

        [JsonProperty("forpackingstyp")]
        public string ArticlePackageType
        {
            get;
            set;
        }

        [JsonProperty("varunummer")]
        public string ArticleNumber
        {
            get;
            set;
        }

        [JsonProperty("atcKlartext")]
        public string ArticleAtc
        {
            get;
            set;
        }

        [JsonProperty("atcKod")]
        public string ArticleAtcCode
        {
            get;
            set;
        }

        [JsonProperty("artikelIntressent")]
        public string ArticleStakeholder
        {
            get;
            set;
        }

        [JsonProperty("artikelTillhandahalls")]
        public bool ArticleProvided
        {
            get;
            set;
        }

        [JsonProperty("artikelRekommendation")]
        public string ArticleRecomendation
        {
            get;
            set;
        }

        #endregion

        #region Previous ordinations

        [JsonProperty("tidigareOrdinationer")]
        public IList<Ordination> PreviousOrdinations
        {
            get;
            set;
        }


        [JsonProperty("tidigareOrdinationsId")]
        public long? PreviousOrdinationId
        {
            get;
            set;
        }

        #endregion

        #region Prescriber.

        [JsonProperty("ordinatorFornamn")]
        public string PrescriberGivenName
        {
            get;
            set;
        }

        [JsonProperty("ordinatorEfternamn")]
        public string PrescriberFamilyName
        {
            get;
            set;
        }

        [JsonProperty("ordinatorBefattningskod")]
        public string PrescriberCode
        {
            get;
            set;
        }

        [JsonProperty("ordinatorArbetsplatskod")]
        public string PrescriberWorkPlaceCode
        {
            get;
            set;
        }


        #endregion

        #region Cancellation

        [JsonProperty("makuleringTidpunkt")]
        public DateTime? CanceledAt
        {
            get;
            set;
        }

        [JsonProperty("makuleringOrsakskod")]
        public int? CancellationReasonCode
        {
            get;
            set;
        }

        [JsonProperty("makuleringOrsakskodKlartext")]
        public string CancellationReason
        {
            get;
            set;
        }

        [JsonProperty("makuleringKommentar")]
        public string CancellationComment
        {
            get;
            set;
        }

        #endregion

        #endregion
    }
}

