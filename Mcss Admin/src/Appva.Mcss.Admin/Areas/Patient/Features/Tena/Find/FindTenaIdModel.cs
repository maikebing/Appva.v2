using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appva.Mcss.Admin.Models
{
    public class FindTenaIdModel
    {
        public string TenaId { get; set; }
        public string RoomNumber { get; set; }
        public string FacilityName { get; set; }
        public string StatusMessage { get; set; }
        public int StatusCode { get; set; }
    }
}