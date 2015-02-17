// <copyright file="DeviceModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a></author>
namespace Appva.Mcss.ResourceServer.Models
{
    #region Imports

    using System;
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// Device model.
    /// </summary>
    /// <example>
    /// Example response:
    /// {
    ///    "remote_messaging_id": "b4e58ca3-4533-4722-b1c2-b27635302de6",
    ///    "name": "Kitchen device",
    ///    "description": "Kitchen device on fourth floor",
    ///    "taxon_id": "a9169409-d6ba-4b2d-a6e5-975792a614c2"
    /// }
    /// </example>
    [JsonObject]
    public class UpdateDeviceModel
    {
        /// <summary>
        /// Remote messaging ID, e.g. for IOS Push messages.
        /// </summary>
        [JsonProperty(PropertyName = "remote_messaging_id")]
        public string RemoteMessagingId 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// The name of the device.
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// The description of the device, e.g. where it is located etc.
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Taxon ID.
        /// </summary>
        [JsonProperty(PropertyName = "taxon_id")]
        public Guid? TaxonId 
        { 
            get; 
            set; 
        }
    }
}