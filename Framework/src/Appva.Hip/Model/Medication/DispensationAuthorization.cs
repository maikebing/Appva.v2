// <copyright file="DispensationAuthorization.cs" company="Appva AB">
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
    public sealed class DispensationAuthorization
    {
        [JsonProperty(PropertyName = "validUntil")]
        public long? ValidUntil
        { 
            get;
            set;
        }

        [JsonProperty(PropertyName = "receivingPharmacy")]
        public Pharmacy ReceivingPharmacy
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "minimumDispensationInterval")]
        public PQ MinimumDispensationInterval
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "totalAmount")]
        public string TotalAmount
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "packageUnit")]
        public string PackageUnit
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "distributionMethod")]
        public string DistributionMethod
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "comment")]
        public string Comment
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "firstDispensationBefore")]
        public long? FirstDispensationBefore
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "prescriptionSignature")]
        public string PrescriptionSignature
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "nonReplacable")]
        public string NonReplacable
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "authorizer")]
        public HealthcareProfessional Authorizer
        {
            get;
            set;
        }
            
    }
}