// <copyright file="ETagResultTests.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Test.WebApi
{
    #region Imports.

    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Appva.WebApi;
    using Xunit;

    #endregion

    /// <summary>
    /// TODO: Add a summary here for readability.
    /// </summary>
    public class ETagResultTests : InMemoryHttpServer
    {
        #region Tests.

        /// <summary>
        /// TODO: Add a summary here for readability.
        /// </summary>
        [Fact]
        public void ETagResult_SendNoIfNoneMatchForGetMethod_ExpectStatusIsOk()
        {
            using (var invoker = new HttpMessageInvoker(this.HttpServer))
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/v0/etag/af505077-c1a0-4a72-abc0-e7d3677cfba9"))
                {
                    using (var response = invoker.SendAsync(request, CancellationToken.None).Result)
                    {
                        var tag = response.Headers.ETag.Tag.Replace("\"", string.Empty);
                        Assert.Equal("61663530353037372d633161302d346137322d616263302d6537643336373763666261392e31", tag);
                        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                    }
                }
            }
        }

        /// <summary>
        /// TODO: Add a summary here for readability.
        /// </summary>
        [Fact]
        public void ETagResult_SendIfNoneMatchForGetMethod_ExpectStatusIsNotModified()
        {
            using (var invoker = new HttpMessageInvoker(this.HttpServer))
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/v0/etag/af505077-c1a0-4a72-abc0-e7d3677cfba9"))
                {
                    request.Headers.IfNoneMatch.Add(new EntityTagHeaderValue("\"61663530353037372d633161302d346137322d616263302d6537643336373763666261392e31\""));
                    using (var response = invoker.SendAsync(request, CancellationToken.None).Result)
                    {
                        Assert.Equal(HttpStatusCode.NotModified, response.StatusCode);
                    }
                }
            }
        }

        #endregion
    }
}
