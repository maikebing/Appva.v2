using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Appva.Sca.Models
{
    public class MockedTokenResponse
    {
        public string Value { get; set; }
        public DateTimeOffset? Expires { get; set; }
        public HttpResponseMessage Message { get; set; }
        public bool IsSuccessStatusCode { get; set; }
    }
}
