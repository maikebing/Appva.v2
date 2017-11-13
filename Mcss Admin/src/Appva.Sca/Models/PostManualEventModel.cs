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
    using System;
    #endregion

    /// <summary>
    /// PostManualEventModel Http model
    /// </summary>
    [HttpRequest]
    public class PostManualEventModel
    {
        #region Fields.

        /// <summary>
        /// ID.
        /// </summary>
        [HttpRequestProperty("id")]
        public string Id
        {
            get;
            set;
        }

        /// <summary>
        /// EventType.
        /// </summary>
        [HttpRequestProperty("eventType")]
        public string EventType
        {
            get;
            set;
        }

        /// <summary>
        /// Resident ID.
        /// </summary>
        [HttpRequestProperty("residentId")]
        public string ResidentId
        {
            get;
            set;
        }

        /// <summary>
        /// TimeStamp.
        /// Must be in UTC
        /// </summary>
        [HttpRequestProperty("timeStamp")]
        public DateTime Timestamp
        {
            get;
            set;
        }

        /// <summary>
        /// Active.
        /// </summary>
        [HttpRequestProperty("active")]
        public bool Active
        {
            get;
            set;
        }
        #endregion
    }
}
