// <copyright file="Drug.cs" company="Appva AB">
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
    public sealed class Drug
    {
        [JsonProperty(PropertyName = "nplId")]
        public string NplId
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "atcCode")]
        public string AtcCode
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "routeOfAdministration")]
        public string RouteOfAdministration
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "pharmaceuticalForm")]
        public string PharmaceuticalForm
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "strength")]
        public double? Strength
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
    }
}