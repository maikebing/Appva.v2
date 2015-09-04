// <copyright file="ResponseItem.cs" company="Appva AB">
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
    public sealed class ResponseItem<TItem> where TItem : class
    {
        /// <summary>
        /// Timestamp when object was last changed? NEEDS TO BE CHECKED UP
        /// </summary>
        [JsonProperty(PropertyName = "timestamp")]
        public DateTime TimeStamp
        {
            get;
            set;
        }

        /// <summary>
        /// The item
        /// </summary>
        [JsonProperty(PropertyName = "data")]
        public TItem Content
        {
            get;
            set;
        }
    }
}