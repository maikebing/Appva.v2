// <copyright file="Druglist.cs" company="Appva AB">
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
    public sealed class HipResponse<TContent> where TContent : class
    {
        /// <summary>
        /// The InfoType of the response
        /// </summary>
        [JsonProperty(PropertyName = "infoType")]
        public string InfoType
        {
            get;
            set;
        }

        /// <summary>
        /// The type
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type
        {
            get;
            set;
        }

        /// <summary>
        /// The response status, e.g count, total-count etc.
        /// </summary>
        [JsonProperty(PropertyName = "status")]
        public ResponseStatus Status
        {
            get;
            set;
        }

        /// <summary>
        /// The response data.
        /// </summary>
        [JsonProperty(PropertyName = "data")]
        public IList<ResponseItem<TContent>> Content
        {
            get;
            set;
        }
    }
}