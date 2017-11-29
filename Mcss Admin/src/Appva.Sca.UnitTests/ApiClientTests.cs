/*using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Appva.Sca.UnitTests.Handlers;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;

namespace Appva.Sca.UnitTests
{
    [TestClass]
    public class ApiClientTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var fakeHandler = new MockedHttpMessageHandler();
            fakeHandler.AddMockedResponse(new Uri("http://example.org/test"), new HttpResponseMessage(HttpStatusCode.OK));

            var httpClient = new HttpClient(fakeHandler);

            var response1 = Task.Run(() => httpClient.GetAsync("http://example.org/notthere")).Result;
            var response2 = Task.Run(() => httpClient.GetAsync("http://example.org/test")).Result;

            Assert.AreEqual(response1.StatusCode, HttpStatusCode.NotFound);
            Assert.AreEqual(response2.StatusCode, HttpStatusCode.OK);
        }

        [TestMethod]
        public void ApiClientTest1()
        {

            Assert.Fail();
        }
    }
}
*/