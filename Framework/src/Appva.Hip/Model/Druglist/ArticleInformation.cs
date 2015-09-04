// <copyright file="ArticleInformation.cs" company="Appva AB">
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
    public sealed class ArticleInformation
    {
        [JsonProperty(PropertyName="unit")]
        public string Unit
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "count")]
        public Double Count
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "atcCode")]
        public string Atc
        { 
            get;
            set;
        }

        [JsonProperty(PropertyName = "activeSubstance")]
        public string ActiveSubstance
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "packageSize")]
        public string PackageSize
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "PackageType")]
        public string PackageType
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "drugForm")]
        public string DrugForm
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "productName")]
        public string ProductName
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "strength")]
        public string Strength
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "strengthUnit")]
        public string StrengthUnit
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "strengthDisplayName")]
        public string StrengthDisplayName
        {
            get;
            set;
        }
    }
}