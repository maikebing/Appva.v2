// <copyright file="PostPdlConsent.cs" company="Appva AB">
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
    public class PostPdlConsent
    {
        [JsonProperty(PropertyName = "emergency")]
        public bool Emergency
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "onlyMe")]
        public bool OnlyMe
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "startDate")]
        public DateTime StartDate
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "endDate")]
        public DateTime EndDate
        {
            get;
            set;
        }
    }
}