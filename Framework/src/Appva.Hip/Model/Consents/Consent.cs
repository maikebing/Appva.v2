// <copyright file="ConsentsDruglist.cs" company="Appva AB">
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
    public class Consent
    {
        /// <summary>
        /// If the current consent is valid
        /// </summary>
        [JsonProperty(PropertyName = "valid")]
        public bool Valid { get; set; }

        /// <summary>
        /// If the current consent is allowed
        /// </summary>
        [JsonProperty(PropertyName = "allowed")]
        public bool Allowed { get; set; }

        /// <summary>
        /// If its allowed to add an ongoning consent
        /// </summary>
        [JsonProperty(PropertyName = "ongoingAllowed")]
        public bool OngoingAllowed { get; set; }
    }
}