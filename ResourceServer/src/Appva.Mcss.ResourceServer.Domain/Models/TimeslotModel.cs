// <copyright file="TimeslotModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.ResourceServer.Models
{
    #region Imports

    using System.Collections.Generic;
    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// Timeslot model.
    /// </summary>
    [JsonObject]
    public class TimeslotModel
    {
        /// <summary>
        /// TODO: Timeslot.
        /// </summary>
        [JsonProperty(PropertyName = "timeslot")]
        public string Timeslot 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// TODO: Timeslot.
        /// </summary>
        [JsonProperty(PropertyName = "category")]
        public string Category 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// TODO: Timeslot.
        /// </summary>
        [JsonProperty(PropertyName = "tasks")]
        public IList<HydratedTaskModel> Tasks 
        { 
            get; 
            set; 
        }
    }
}
