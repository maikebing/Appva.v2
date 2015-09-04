// <copyright file="PQ.cs" company="Appva AB">
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
    public sealed class PQ
    {
        [JsonProperty(PropertyName = "value")]
        public Double Value
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "unit")]
        public string Unit
        {
            get;
            set;
        }
    }
}