// <copyright file="OAuth2Tests.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Test.OAuth2
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    using Appva.OAuth;
    using Appva.Test.OAuth2.Impl;
    //using Appva.WebApi.OAuth;
    using DotNetOpenAuth.OAuth2;
    using Moq;
    using Newtonsoft.Json;
    using Xunit;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    /*public class OAuth2Tests
    {
        #region Variables.

        /// <summary>
        /// The authorization server token signing key.
        /// </summary>
        private const string AuthorizationServerTokenSigningKey = "PFJTQUtleVZhbHVlPjxNb2R1bHVzPjkwNU9zRjVnYXNIOUVFY0VYV2RaSXNpNlozbWxKRjhlMFlPancrVmY0M0lYTnhmc3ZzOUxvdTR6dVpUOHV5dndpT25jaDUrSXBIOHZTZ2ZzaUZLbFZuQXRzcXhUcU5HVXFBWk5HWG9rZ3FiS0d6WTFoajZLVWxHUlErcThJMHdFbzBrWFh3cjQ3bWFIN01pRVYvaXBiSjZvVmtkbC9XVHJybXMyb2JFR09CRT08L01vZHVsdXM+PEV4cG9uZW50PkFRQUI8L0V4cG9uZW50PjxQPi9DSTYxZ1ZhZzlUMTRkMmdLb0hJSUc2NU5rQ0FzQlVzTlEzMGtRK2l2UEFIWTV5b2JpSXdxTDVxSk54cjhsVUhGMDJxQVR2TUxOYnNaT3J2a3V1bjF3PT08L1A+PFE+K3hrZ0pSQXJhRFRiem9VeElEbVZ4UVZtUVhia1NFS244bnpTYkZEN2ptSzRKd3h5NmlNR2ZTakljdFliNmhxQTc3dFlrTzFpSHVHTnZtS0FHMU9DVnc9PTwvUT48RFA+R1MwUjB1MFY3TFFIR1ZhWDk2YWQ1UjhwUDFHUmlBT1ZObmIrUkwzYThpTEZtaHk2ZE1UVk53Uk1kUUhOaFpVWDhDdkJIZjVxbE0raEt6S0tXWkZPWVE9PTwvRFA+PERRPjRQMnpldUpSTXE5aVlWdWhHREhoREVmNVJ5RmtEWWVFZTFmektGRXNCbnBZYmN6T3p4TVJSbWFicmFKQ0l2TWFvelNvZUR2c1ZxVmVYOEJjNzU5VlF3PT08L0RRPjxJbnZlcnNlUT5vVm5hNG1HQkx5SzN3OHdOQzhGVVBlVHlISzN5SkFSTXdDU0ZTLytTajI5eUdEbCtPeE5CRlNvUW9uWmwwdWFFeFdBN0VJTjJVZUxSZzhicWFELzUyQT09PC9JbnZlcnNlUT48RD5ZTkJQRGN4a2dtYWU0eGhxSlFhb1ptMmVTNVBiaW5tU1h3TGh3WGF5S3lBbTVuSi9ROU56RUwyZmtpODVJU3o2WlI3b0xrL045bGV6ODQ5V2thZUpBYUMzZm96c2Zrek9KVXBOQlNWS1RCRkR6K0dyRHV1a0tLL2JDbDBCVHZnT3E5R0k2UWUwUXpFUnV0SVIwUjY3cXptUUxmenRhVVc4UGVOSTcwTVhHZUU9PC9EPjwvUlNBS2V5VmFsdWU+";

        /// <summary>
        /// The base64 encoded identifier:secret.
        /// </summary>
        private const string BasicAuthentication = "Basic TUlJQjBUQ0NBVHFnQXdJQkFnSVFGTURUL2pKRlVaSkxrdDJWNDR2dVVEQU5CZ2txaGtpRzl3MEJBUVVGQURBVTpzZWNyZXQ=";

        /// <summary>
        /// The <see cref="AuthorizationServer"/>
        /// </summary>
        private readonly AuthorizationServer authorizationServer;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="OAuth2Tests"/> class.
        /// </summary>
        public OAuth2Tests()
        {
            var oAuth2Service = new OAuth2Service(new OAuth2AuthorizationServerSigningKeyHandler(AuthorizationServerTokenSigningKey));
            this.authorizationServer = new AuthorizationServer(new OAuth2AuthorizationServer(oAuth2Service, new OAuth2CryptoStore(), new OAuth2NonceStore()));
        }

        #endregion

        #region Tests.

        [Fact]
        public void OAuth2_AuthorizeResourceOwnerCredentialGrant_IsAuthenticated()
        {
            var request = this.HttpPost(new NameValueCollection()
                    { 
                        { "scope", "https://test.api.resource" },
                        { "grant_type", "password" },
                        { "username", "admin" },
                        { "password", "password" }
                    });
            var response = this.authorizationServer.HandleTokenRequest(request);
            //var authorize = new OAuthAuthorizeAttribute("https://test.api.resource");
            //new JsonSerializer().Deserialize()
            var obj = JsonConvert.DeserializeObject<Boll>(response.Body);
            var token = obj.access_token as string;
            var resourceServer = new ResourceServer(new StandardAccessTokenAnalyzer(new AuthorizationServerSigningKeyHandler().PublicKey, new ResourceServerSigningKeyHandler().PrivateKey));
            var httpGet = this.HttpGet(token);
            var principal = resourceServer.GetPrincipal(httpGet, "https://test.api.resource");
            var access = resourceServer.GetAccessToken(httpGet, "https://test.api.resource");
            var etra = access.ExtraData.ContainsKey("test") || access.ExtraData.ContainsKey("lastname");
            Assert.True(etra);
            Assert.Equal(HttpStatusCode.OK, response.Status);
        }

       
        private HttpRequestBase HttpGet(string bearerToken)
        {
            var request = new Mock<HttpRequestBase>();
            request.Setup(x => x.Headers).Returns(
                new System.Net.WebHeaderCollection
                {
                    { "Cache-Control", "no-cache" },
                    { "Authorization", "Bearer " + bearerToken }
                });
            request.Setup(x => x.ContentType).Returns("application/json;charset=UTF-8");
            request.Setup(x => x.HttpMethod).Returns("GET");
            request.Setup(x => x.Url).Returns(new Uri("http://localhost/tokens"));
            request.Setup(x => x.ServerVariables).Returns(new System.Collections.Specialized.NameValueCollection());
            return request.Object;
        }

        private HttpRequestBase HttpPost(NameValueCollection formCollection)
        {
            var request = new Mock<HttpRequestBase>();
            request.Setup(x => x.Headers).Returns(
                new System.Net.WebHeaderCollection
                {
                    { "Cache-Control", "no-cache" },
                    { "Authorization", BasicAuthentication }
                });
            request.Setup(x => x.Form).Returns(formCollection);
            request.Setup(x => x.ContentType).Returns("application/x-www-form-urlencoded;charset=UTF-8");
            request.Setup(x => x.HttpMethod).Returns("POST");
            request.Setup(x => x.Url).Returns(new Uri("http://localhost/tokens"));
            request.Setup(x => x.ServerVariables).Returns(new System.Collections.Specialized.NameValueCollection());
            return request.Object;
        }

        public class Boll
        {
            public string access_token { get; set; }
            public string scopes { get; set; }
        }

        #endregion
    }*/
}