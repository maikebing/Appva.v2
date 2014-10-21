// <copyright file="BeaconModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a></author>
namespace Appva.Mcss.ResourceServer.Models
{
    #region Imports

    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// Beacon model.
    /// </summary>
    [JsonObject]
    public class BeaconModel
    {
        /// <summary>
        /// Major.
        /// </summary>
        [JsonProperty(PropertyName = "major")]
        public string Major 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Minor.
        /// </summary>
        [JsonProperty(PropertyName = "minor")]
        public string Minor 
        { 
            get; 
            set; 
        }
    }
}