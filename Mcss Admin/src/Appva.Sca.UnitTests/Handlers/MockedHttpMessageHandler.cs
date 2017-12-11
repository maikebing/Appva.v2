using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Appva.Sca.UnitTests.Handlers
{
    public class MockedHttpMessageHandler : DelegatingHandler
    {
        private readonly Dictionary<Uri, HttpResponseMessage> _MockedResponse = new Dictionary<Uri, HttpResponseMessage>();


        public void AddMockedResponse(Uri uri, HttpResponseMessage responseMessage)
        {
            _MockedResponse.Add(uri, responseMessage);
        }


        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            if(_MockedResponse.ContainsKey(request.RequestUri))
            {
                return Task.FromResult(_MockedResponse[request.RequestUri]);
            }
            else
            {
                return Task.FromResult(new HttpResponseMessage(System.Net.HttpStatusCode.NotFound) { RequestMessage = request });
            }
        }
    }
}
