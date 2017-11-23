// <copyright file="GetResidentModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
namespace Appva.Sca.Models
{
    #region Imports.
    using Appva.Http;
    using Newtonsoft.Json;
    #endregion

    /// <summary>
    /// GetResidentModel Http model
    /// </summary>
    [JsonObject]
    public class Resident
    {
        #region Properties.

        /// <summary>
        /// RoomNumber.
        /// </summary>
        [JsonProperty("roomNumber")]
        public string RoomNumber
        {
            get;
            set;
        }

        /// <summary>
        /// FacilityName.
        /// </summary>
        [JsonProperty("facilityName")]
        public string FacilityName
        {
            get;
            set;
        }

        /// <summary>
        /// External ID.
        /// </summary>
        [JsonProperty("externalId")]
        public string ExternalId
        {
            get;
            set;
        }

        /// <summary>
        /// Message.
        /// </summary>
        public string Message
        {
            get;
            set;
        }
        #endregion
    }
}
