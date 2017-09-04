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
    #endregion

    /// <summary>
    /// GetResidentModel Http model
    /// </summary>
    [HttpRequest]
    public class GetResidentModel
    {
        #region Fields

        /// <summary>
        /// RoomNumber.
        /// </summary>
        [HttpRequestProperty("roomNumber")]
        public string RoomNumber
        {
            get;
            set;
        }

        /// <summary>
        /// FacilityName.
        /// </summary>
        [HttpRequestProperty("facilityName")]
        public string FacilityName
        {
            get;
            set;
        }

        /// <summary>
        /// External ID.
        /// </summary>
        [HttpRequestProperty("externalId")]
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
