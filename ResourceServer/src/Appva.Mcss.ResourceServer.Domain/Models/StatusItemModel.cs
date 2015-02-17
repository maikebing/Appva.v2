// <copyright file="StatusItemModel.cs" company="Appva AB">
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
    /// Status Item model.
    /// </summary>
    [JsonObject]
    public class StatusItemModel
    {
        /// <summary>
        /// The id of the status-item
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public Guid Id 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// The name of the status-item
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// The image of the status-item
        /// </summary>
        [JsonProperty(PropertyName = "image_url")]
        public string ImageUrl 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Reference to contact-object
        /// </summary>
        [JsonProperty(PropertyName = "contact_ref")]
        public string ContactRef 
        { 
            get; 
            set; 
        }
    }
}