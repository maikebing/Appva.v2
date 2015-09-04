// <copyright file="PostConsents.cs" company="Appva AB">
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
    public class PostConsents
    {
        /// <summary>
        /// Druglist consent to set
        /// </summary>
        [JsonProperty(PropertyName = "druglist")]
        public PostDruglistConsent Druglist { get; set; }

        /// <summary>
        /// PDL consent to set
        /// </summary>
        [JsonProperty(PropertyName = "pdl")]
        public PostPdlConsent Pdl { get; set; }
    }
}