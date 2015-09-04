// <copyright file="Administration.cs" company="Appva AB">
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
    public sealed class Administration
    {
        [JsonProperty(PropertyName = "startTime")]
        public long? StartTime
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "endTime")]
        public long? EndTime
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

        [JsonProperty(PropertyName = "drugChoice")]
        public DrugChoice Drug
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

        [JsonProperty(PropertyName = "professional")]
        public HealthcareProfessional Professional
        {
            get;
            set;
        }
    }
}