// <copyright file="Prescription.cs" company="Appva AB">
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
    public sealed class Prescription
    {
        [JsonProperty(PropertyName = "type")]
        public string Type
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "status")]
        public string Status
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "note")]
        public string Note
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

        [JsonProperty(PropertyName = "treatmentPurpose")]
        public string TreatmentPurpose
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "principalReasons")]
        public IEnumerable<string> PrincipalReasons
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "additionalReasons")]
        public IEnumerable<string> AdditionalReasons
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "prescriber")]
        public HealthcareProfessional Prescriber
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "evaluator")]
        public HealthcareProfessional Evaluator
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "drug")]
        public DrugChoice Drug
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "startOfFirstTreatment")]
        public long? StartOfFirstTreatment
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "startOfTreatment")]
        public long? StartOfTreatment
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "endOfTreatment")]
        public long? EndOfTreatment
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "endOfTreatmentReason")]
        public string EndOfTreatmentReason
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "selfMedication")]
        public bool? SelfMedication
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "administrations")]
        public IEnumerable<Administration> Administrations
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "dispensationAuthorization")]
        public DispensationAuthorization DispensationAuthorization
        {
            get;
            set;
        }
    }
}