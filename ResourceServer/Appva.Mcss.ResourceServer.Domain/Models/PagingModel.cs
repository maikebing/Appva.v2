// <copyright file="PagingModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.ResourceServer.Models
{
    #region Imports

    using System.Collections.Generic;
    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// Paging model
    /// </summary>
    /// <example>
    /// Example response:
    /// {
    ///    "page": "1",
    ///    "next_page": "2",
    ///    "max_results": "100",
    ///    "entities": [{
    ///         ...
    ///     }]
    /// }
    /// </example>
    /// <typeparam name="T">The Type</typeparam>
    [JsonObject]
    public class PagingModel<T> where T : class
    {
        /// <summary>
        /// Current page number.
        /// </summary>
        [JsonProperty(PropertyName = "page")]
        public long PageNumber 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Next page number.
        /// </summary>
        [JsonProperty(PropertyName = "next_page")]
        public long NextPageNumber 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Max results.
        /// </summary>
        [JsonProperty(PropertyName = "max_result")]
        public long MaxResults 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Entities by count.
        /// </summary>
        [JsonProperty(PropertyName = "entities")]
        public IList<T> Entities 
        { 
            get; 
            set; 
        }
    }
}