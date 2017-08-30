using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using Appva.Http;

namespace Appva.Sca.Models
{
    [HttpRequest]
    public class GetResidentModel
    {
        [HttpRequestProperty("roomNumber")]
        public string RoomNumber { get; set; }
        [HttpRequestProperty("facilityName")]
        public string FacilityName { get; set; }
        [HttpRequestProperty("externalId")]
        public string ExternalId { get; set; }
        //public HttpStatusCode StatusCode { get; set; }
    }
}
