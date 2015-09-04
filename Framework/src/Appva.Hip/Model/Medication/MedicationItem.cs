// <copyright file="MedicationItem.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Hip.Model
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
    public sealed class MedicationItem
    {
        [JsonProperty(PropertyName = "id")]
        public string Id
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "time")]
        public long? Time
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "signed")]
        public string Signed
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "accountableProfessional")]
        public HealthcareProfessional AccountableProfessional
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "approvedForPatient")]
        public bool? ApprovedForPatient
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "informationType")]
        public string InformationType
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "nullified")]
        public string Nullified
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "nullifiedReason")]
        public string NullifiedReason
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "careUnitHsaId")]
        public string CareUnitHsaId
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "careProviderHsaId")]
        public string CareProviderHsaId
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "prescription")]
        public Prescription Prescription
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "drugArticle")]
        public string DrugArticle
        {
            get;
            set;
        }
    }
}