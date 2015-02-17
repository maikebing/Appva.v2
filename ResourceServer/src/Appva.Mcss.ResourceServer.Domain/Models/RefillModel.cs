// <copyright file="RefillModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.ResourceServer.Models
{
    #region Imports

    using System;
    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// Refill Model.
    /// </summary>
    [JsonObject]
    public class RefillModel
    {
        /// <summary>
        /// TODO: Id
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public Guid Id 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// TODO: Ordered
        /// </summary>
        [JsonProperty(PropertyName="ordered")]
        public bool Ordered 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// TODO: RefillOrderedBy
        /// </summary>
        [JsonProperty(PropertyName = "ordered_by")]
        public string RefillOrderedBy 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// TODO: RefillOrderedTime
        /// </summary>
        [JsonProperty(PropertyName = "ordered_time")]
        public string RefillOrderedTime 
        { 
            get; 
            set; 
        }
    }
}
