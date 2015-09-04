// <copyright file="DruglistItem.cs" company="Appva AB">
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
    public sealed class DruglistItem
    {
        [JsonProperty(PropertyName = "id")]
        public string Id
        { 
            get; 
            set;
        }

        [JsonProperty(PropertyName = "time")]
        public long Time
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "signed")]
        public object Signed
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "approvedForPatient")]
        public bool ApprovedForPatient
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
        public object Nullified
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "nullifiedReason")]
        public object NullifiedReason
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

        [JsonProperty(PropertyName = "numberOfPackages")]
        public int NumberOfPackages
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "numberOfPills")]
        public string NumberOfPills
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "articleInformation")]
        public ArticleInformation Article
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "deleted")]
        public bool Deleted
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "deletionDate")]
        public string DeletedDate
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "deletionReason")]
        public string DeletedReason
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "removalType")]
        public string RemovalType
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "dosageText")]
        public string DosageText
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "amount")]
        public string Amount
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "prescriber")]
        public Prescriber Prescriber
        {
            get;
            set;
        }
    }
}