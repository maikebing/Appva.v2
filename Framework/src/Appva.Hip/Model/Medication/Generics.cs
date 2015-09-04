// <copyright file="Generics.cs" company="Appva AB">
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
    public sealed class Generics
    {
        [JsonProperty(PropertyName="substance")]
        public string Substance
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "strength")]
        public PQ Strength
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "form")]
        public string Form
        {
            get;
            set;
        }

    }
}