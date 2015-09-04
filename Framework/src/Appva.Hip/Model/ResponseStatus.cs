// <copyright file="ResponseStatus.cs" company="Appva AB">
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
    public sealed class ResponseStatus
    {
        [JsonProperty(PropertyName="filterCount")]
        public int FilterCount
        {
            get;
            set;
        }

        [JsonProperty(PropertyName="totalCount")]
        public int TotalCount
        {
            get;
            set;
        }

        /// <summary>
        /// TODO:
        /// "timespan":[
        ///     {
        ///         "year":2014,
        ///         "occasions":[
        ///             {
        ///                 "11":1
        ///             },
        ///             ...
        ///         ]
        ///     },
        ///     ...
        /// ]
        /// </summary>
        /*public object timespan
        {
            get;
            set;
        }*/
    }
}