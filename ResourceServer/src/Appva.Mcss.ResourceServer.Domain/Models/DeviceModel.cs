// <copyright file="DeviceModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
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
    ///    "uuid": "24e00ef8-b5e4-45ac-b50a-7bbea3ec2d20",
    ///    "remote_messaging_id": "b4e58ca3-4533-4722-b1c2-b27635302de6",
    ///    "name": "Kitchen device",
    ///    "description": "Kitchen device on fourth floor",
    ///    "taxon_id": "a9169409-d6ba-4b2d-a6e5-975792a614c2"
    /// }
    /// </example>
    [JsonObject]
    public class DeviceModel
    {
        /// <summary>
        /// The self created device ID.
        /// </summary>
        [Required, JsonProperty(PropertyName = "uuid", Required = Required.Always)]
        public string Uuid 
        { 
            get; 
            set; 
        }

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
        [Required, JsonProperty(PropertyName = "name", Required = Required.Always)]
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
        [Required, JsonProperty(PropertyName = "taxon_id", Required = Required.Always)]
        public Guid TaxonId 
        { 
            get; 
            set; 
        }
    }
}