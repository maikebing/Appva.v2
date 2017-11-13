// <copyright file="FindTenaId.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    #endregion

    /// <summary>
    /// 
    /// </summary>
    [JsonObject]
    public class GetResidentModel
    {
        #region Properties.

        /// <summary>
        /// Gets or sets the tena identifier.
        /// </summary>
        /// <value>
        /// The tena identifier.
        /// </value>
        [JsonProperty("external_id")]
        public string TenaId 
        { 
            get;
            set; 
        }

        /// <summary>
        /// Gets or sets the room number.
        /// </summary>
        /// <value>
        /// The room number.
        /// </value>
        [JsonProperty("room_number")]
        public string RoomNumber 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Gets or sets the name of the facility.
        /// </summary>
        /// <value>
        /// The name of the facility.
        /// </value>
        [JsonProperty("facility_name")]
        public string FacilityName 
        { 
            get;
            set; 
        }

        /// <summary>
        /// Gets or sets the status message.
        /// </summary>
        /// <value>
        /// The status message.
        /// </value>
        [JsonProperty("error_message")]
        public string Error 
        { 
            get; 
            set; 
        }

        #endregion
    }
}