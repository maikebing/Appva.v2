// <copyright file="DrugChoice.cs" company="Appva AB">
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
    public sealed class DrugChoice
    {
        [JsonProperty(PropertyName = "comment")]
        public string Comment
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "unstructuredDrugInformation")]
        public string UnstructuredDrugInformation
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "merchandise")]
        public Merchandise Merchandise
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "drug")]
        public Drug Drug
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

        [JsonProperty(PropertyName = "generics")]
        public Generics Generics
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "name")]
        public string Name
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "dosages")]
        public IEnumerable<Dosage> Dosages
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "type")]
        public string Type
        {
            get;
            set;
        }
    }
}