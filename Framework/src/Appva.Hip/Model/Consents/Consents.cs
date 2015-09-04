﻿using Appva.Hip.Model;
// <copyright file="Consents.cs" company="Appva AB">
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
    public class Consents
    {
        /// <summary>
        /// Consents for the druglist
        /// </summary>
        [JsonProperty(PropertyName = "druglist")]
        public Consent Druglist { get; set; }

        /// <summary>
        /// Consents for the PDL operations
        /// </summary>
        [JsonProperty(PropertyName = "pdl")]
        public Consent Pdl { get; set; }
    }
}