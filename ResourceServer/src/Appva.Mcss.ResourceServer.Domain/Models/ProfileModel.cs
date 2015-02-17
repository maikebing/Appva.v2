// <copyright file="ProfileModel.cs" company="Appva AB">
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
    /// Profile model
    /// </summary>
    /// <example>
    /// Example response:
    /// {
    ///    "id": "b4e58ca3-4533-4722-b1c2-b27635302de6",
    ///    "name": "Waran",
    ///    "description": "Observera Waran",
    ///    "image": "waran.png"  //should be url
    /// }
    /// </example>
    [JsonObject]
    public class ProfileModel 
    {
        /// <summary>
        /// <code>Account</code> ID.
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public Guid Id 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// The name of the <code>Profile</code>.
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Description
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// The image
        /// </summary>
        [JsonProperty(PropertyName = "image")]
        public string Image 
        { 
            get; 
            set; 
        }
    }
}