using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace Appva.Sca.Models
{
    public sealed class GetManualEventModel
    {
        public string Id { get; set; }
        public string ImportResult { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
