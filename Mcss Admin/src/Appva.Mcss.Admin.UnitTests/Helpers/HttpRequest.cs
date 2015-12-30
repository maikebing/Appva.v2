// <copyright file="HttpRequest.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.UnitTests.Helpers
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Net;
    using System.Security.Claims;
    using System.Web;
    using Appva.Mcss.Admin.UnitTests.Domain.Handlers;
    using Moq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal static class MockedHttpRequestBase
    {
        public static HttpContextBase CreateNew()
        {
            var server   = new Mock<HttpServerUtilityBase>(MockBehavior.Loose);
            var response = new Mock<HttpResponseBase>(MockBehavior.Loose);
            var request  = new Mock<HttpRequestBase>(MockBehavior.Loose);
            request.Setup(x => x.UserHostAddress).Returns("127.0.0.1");
            request.Setup(x => x.Url).Returns(new Uri("https://test.example.com/auth"));
            request.Setup(x => x.ServerVariables).Returns(new NameValueCollection
                {
                    { "REMOTE_ADDR",     "127.0.0.1" },
                    { "X-Forwarded-For", "127.0.0.1" }
                });
            request.Setup(x => x.Headers).Returns(new NameValueCollection
                {
                    { "X-Real-IP", "127.0.0.1" }
                });
            var session = new Mock<HttpSessionStateBase>();
            session.Setup(x => x.SessionID).Returns(Guid.NewGuid().ToString());
            var context = new Mock<HttpContextBase>();
            context.SetupGet(x => x.Request).Returns(request.Object);
            context.SetupGet(x => x.Response).Returns(response.Object);
            context.SetupGet(x => x.Server).Returns(server.Object);
            context.SetupGet(x => x.Session).Returns(session.Object);
            context.SetupGet(x => x.User).Returns(new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, CurrentPrincipal.Ids.First().ToString())
            }, "anonymous")));
            return context.Object;
        }
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal static class HttpRequest
    {
        /// <summary>
        /// The HTTP GET Method.
        /// </summary>
        public const string Get = "GET";

        /// <summary>
        /// The HTTP GET Method.
        /// </summary>
        public const string Post = "POST";

        /// <summary>
        /// The json content type.
        /// </summary>
        public const string Json = "application/json;charset=UTF-8";

        /// <summary>
        /// The form content type.
        /// </summary>
        public const string Form = "application/x-www-form-urlencoded;charset=UTF-8";

        /// <summary>
        /// Creates a new mocked HTTP GET <see cref="HttpRequestBase"/>.
        /// </summary>
        /// <returns>A new <see cref="HttpRequestBase"/> instance</returns>
        public static HttpRequestBase CreateNew()
        {
            var request = new Mock<HttpRequestBase>();
            request.Setup(x => x.Headers).Returns(
                new WebHeaderCollection
                {
                    { "Cache-Control", "no-cache" },
                });
            request.Setup(x => x.ContentType).Returns(Json);
            request.Setup(x => x.HttpMethod).Returns(Get);
            request.Setup(x => x.Url).Returns(new Uri("http://localhost/"));
            request.Setup(x => x.ServerVariables).Returns(new NameValueCollection());
            return request.Object;
        }

        /// <summary>
        /// Creates a new mocked HTTP POST <see cref="HttpRequestBase"/>.
        /// </summary>
        /// <param name="body">The post body</param>
        /// <returns>A new <see cref="HttpRequestBase"/> instance</returns>
        public static HttpRequestBase CreateNew(NameValueCollection body)
        {
            var request = new Mock<HttpRequestBase>();
            request.Setup(x => x.Headers).Returns(
                new WebHeaderCollection
                {
                    { "Cache-Control", "no-cache" },
                });
            request.Setup(x => x.Form).Returns(body);
            request.Setup(x => x.ContentType).Returns(Form);
            request.Setup(x => x.HttpMethod).Returns(Post);
            request.Setup(x => x.Url).Returns(new Uri("http://localhost/"));
            request.Setup(x => x.ServerVariables).Returns(new NameValueCollection());
            return request.Object;
        }
    }
}