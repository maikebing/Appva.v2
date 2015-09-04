// <copyright file="Interval.cs" company="Appva AB">
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
    /// 
    [JsonObject]
    public sealed class Interval
    {
        [JsonProperty(PropertyName = "low")]
        public double? Low
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "high")]
        private double? High
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "unit")]
        private string Unit
        {
            get;
            set;
        }
    }
}