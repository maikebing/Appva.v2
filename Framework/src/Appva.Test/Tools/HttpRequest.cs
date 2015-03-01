// <copyright file="HttpRequest.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Test.Tools
{
    #region Imports.

    using System;
    using System.Collections.Specialized;
    using System.Net;
    using System.Web;
    using Moq;

    #endregion

    /// <summary>
    /// Http request helper.
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
        /// <param name="bearer">The OAuth 2 bearer token</param>
        /// <returns>A new <see cref="HttpRequestBase"/> instance</returns>
        public static HttpRequestBase CreateNew(string bearer)
        {
            bearer = bearer.Contains("Bearer") ? bearer : "Bearer " + bearer;
            var request = new Mock<HttpRequestBase>();
            request.Setup(x => x.Headers).Returns(
                new WebHeaderCollection
                {
                    { "Cache-Control", "no-cache" },
                    { "Authorization", bearer }
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
        /// <param name="authorization">The OAuth 2 basic authorization</param>
        /// <param name="body">The post body</param>
        /// <returns>A new <see cref="HttpRequestBase"/> instance</returns>
        public static HttpRequestBase CreateNew(string authorization, NameValueCollection body)
        {
            authorization = authorization.Contains("Basic") ? authorization : "Basic " + authorization;
            var request = new Mock<HttpRequestBase>();
            request.Setup(x => x.Headers).Returns(
                new WebHeaderCollection
                {
                    { "Cache-Control", "no-cache" },
                    { "Authorization", authorization }
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