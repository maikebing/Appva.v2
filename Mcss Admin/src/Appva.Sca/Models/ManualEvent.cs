// <copyright file="PostManualEventModel.cs" company="Appva AB">
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
    using System;
    #endregion

    /// <summary>
    /// PostManualEventModel Http model
    /// </summary>
    [JsonObject]
    public class ManualEvent
    {
        #region Properties.

        /// <summary>
        /// ID.
        /// </summary>
        [JsonProperty("id")]
        public string Id
        {
            get;
            set;
        }

        /// <summary>
        /// EventType.
        /// </summary>
        [JsonProperty("eventType")]
        public string EventType
        {
            get;
            set;
        }

        /// <summary>
        /// Resident ID.
        /// </summary>
        [JsonProperty("residentId")]
        public string ResidentId
        {
            get;
            set;
        }

        /// <summary>
        /// TimeStamp.
        /// Must be in UTC
        /// </summary>
        [JsonProperty("timeStamp")]
        public DateTime Timestamp
        {
            get;
            set;
        }

        /// <summary>
        /// Active.
        /// </summary>
        [JsonProperty("active")]
        public bool Active
        {
            get;
            set;
        }
        #endregion
    }
}
