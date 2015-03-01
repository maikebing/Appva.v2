// <copyright file="AuthorizeTokenAttributeTest.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.ResourceServer.Test.Authorization
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading;
    using System.Web.Http.Controllers;
    using Application;
    using Application.Authorization;
    using Moq;
    using Xunit;
    using Xunit.Extensions;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public class AuthorizeTokenAttributeTest
    {
        #region Variabels.

        /// <summary>
        /// Password (authorization grant) bearer token without expiration. 
        /// Granted scopes: 
        /// https://appvaapis.se/auth/resource.readonly 
        /// https://appvaapis.se/auth/resource.adminonly
        /// </summary>
        private const string PasswordBearerToken = "gAAAAFCkdatHHiRQ_BQ6ibdE7Nr0PXQJUUA7VB803dMD4UatrJpUvppHKYxfI0FkiE7TA2lBTyMzSpGTOwPGySqaygfzqv4Se6_mRDdEliwUgUkLJc4pS_n5a_iWpocAGLhI5ZcS8EF-CjF2UPQ_iqfZoCTNN2kQRZatZkzbgT6QVc591AIAAIAAAABIfx4Uf98uVxdrjQF0ub7QOhdU9IAYpFrxlyhKOSPXQtGjlla1NJwAZ5ESi6RaFoXU9btWiaARoTN8K5pDFbCoGMmpRDdB0de1bxjbyGrb0nx9O4lraMqJKGq3QmnWzi3orDBHXptyolNxpYNTtxbLRd-sgAbFke3xS12QHhLaPilUKyQBj57lCD0RjULR2LTB-GwZ8lgHAbiBAOt_oRp41bW3tRmoJlztz_yDlMhlZnvfLHfm7s9xNu59ZQ2CoWtMBz4nUZyQHQL6l5oP355bpc3jTMXBl2r0U-jynb-wh2PiEq8Xa4_dwq9muZam79ch_bFSgM_JFgIJsYvD0u_0KP94ERgsaZ9gKZiMsgh_2FZY9IueL3tFARzHnKbUOstzTHG3diRiCcguEMViiEuExr1i-eJ9u9KBrWmWW3ddc2maIOb2GPbKkenmHr83WP2yXCQhM5b9pjRkdyNPZZ1t2QySZcUORUzjT41PpQdYZsxcOIS1NeZQZuVagchRPJ68XtAe7AjxwS_DlWVOKsSZ7ngu98RdsJb8jxzMFLaHj_Rkifmn7FkAcm6-xdCPI4uftjJXedy7B39jcZDLq939jmmFVqJSmd4iD1NGhZZKMw5eTfs_kSr9Ez7qaa-NJc2gAoFY5b9cUPw-jee6Ouo0vUZbPm2A0hwWjVuiPAHJisZrBpPFQqYJrNrE1_dLNXaQrj63T-2ntrEfSdCE9LiW6NmKUNJnZsj7MbWta5m9ERoQufKwP7H42opKl40edoCG0CG40vlV6mIXO1VgQYYOkIWFY5CUZVlfZN9s0QjMhLuBmZXVpl_unpDktjkM5MZ7gX9ilFRbdTtCSdWL5qu3YYiUIwC5EYif5j7Ug4hQC6BwBpi7D0qVCqvVoiFuriQ50l5sYmbEzLYs7tYpPxvZ3-GYJ3hkNZxyU6ahj5MpyniKf72HKtkqhCOoci63ufQ";

        /// <summary>
        /// Client Credentials (authorization grant) bearer token without expiration. 
        /// Granted scopes: 
        /// https://appvaapis.se/auth/resource
        /// </summary>
        private const string ClientCredentialsBearerToken = "gAAAAFWO9bMlcK6Qxe38XgsPmHmNE2b2ltN4Pl6LpLQ81qiw02ieqxo-ezEA-PTvchqCHbJsvGnKVlWuajaG2U9-wSvmkH6NQ9r2gZZXR5Ya4RpASmuRYKr5CxQdBXXkPFCYh_zhZ_fGZEPmm-CjP8PGnLoP-IIXfqiZsXv7DrVPAGy7dAIAAIAAAABxlXZqPU0W5o9fyAdDORXnvK9lNhi6Ef2wLKtMPr2PMZ8PNlanwXUFrCx7acoi8-OQ9INEtp9zWojsBKOafLPM4EF9Jx-ffYdepnT8XFmQQEgGuJUCztFGv3sAaIWn4rj2o5UMwMVZ4w-RrLg2q2R3ipjw_LWqOy4TYh0AWFAYg6WqnnZL4W-N2aoCI969WG_Mz-YG36L6zZPmZK3r3t6RQwHQZ-iIPmbyiDqQZw678PwQ8vz41Uomg2YAmkRCJQqSV7bNaQW2jo01Dgcb2TdraTnY-_kwQcgEcWvGnyKwelff781FmSCnKVNp87uK8iwtUIeM66yo7EXEUi_9T6_btvryzXOZQHg6jGYo8fKf1GJq4MzTHDlngKGVhYgeY9nxAYxSd7F_B2F2zZj9CC_tGchT10JyUMB-68BGyJYNznePOvmswlgK_O905KsaNMLhiZH5Em1dqRg7pr4_cqVGxnDmfRiPLyJY6uEpNwh3cW-S9GaLI5l80MVzCEJ7uRd--OGTHkbe6LJwQnERPwEa77ogvrv4H_E4_wnVeQhJr3eUjSaOSz4828j30CovoGs6nvS1jfivDlMwQf5vzEdSVGRWH63AISsWrFZzlaqUY_VSjarCF7CkPgqXRPBXVOik_FnUJFRn15f9d1W5rQz-7QWgwSqjVMkba4Tx8EKMWnKJDuOE_3gXxpgsG1YXtDz_EpeBFRxr0bzItI7xwbfH-k-Sn4KTsq0T-7Bpnttg0ta10R_4IPe7IcnzAlUO7-kzkEp25kDCXHLmQXpkO8atV4lX2V2vuqsRXxqmLbLihnQQN6Zbuk7-R0K6gnBXdMQ";

        #endregion

        #region Test Data.

        /// <summary>
        /// Authorized test data.
        /// </summary>
        public static IEnumerable<object[]> AuthorizedTokenTestData
        {
            get
            {
                var data = new[]
                {
                    new[] { new[] { PasswordBearerToken, Scope.ReadOnly } },
                    new[] { new[] { PasswordBearerToken, Scope.AdminOnly } },
                    new[] { new[] { PasswordBearerToken, Scope.ReadOnly, Scope.AdminOnly } },
                    new[] { new[] { ClientCredentialsBearerToken, Scope.ReadWrite } },
                    new[] { new[] { ClientCredentialsBearerToken, Scope.ReadWrite, Scope.ReadOnly } }
                };
                return data;
            }
        }

        /// <summary>
        /// Unauthorized test data.
        /// </summary>
        public static IEnumerable<object[]> UnauthorizedTokenTestData
        {
            get
            {
                var data = new[]
                {
                    new[] { new[] { PasswordBearerToken, Scope.ReadWrite } },
                    new[] { new[] { ClientCredentialsBearerToken, Scope.AdminOnly } },
                    new[] { new[] { ClientCredentialsBearerToken, Scope.ReadOnly } }
                };
                return data;
            }
        }

        #endregion

        #region Tests.

        /// <summary>
        /// Verifies that the bearer tokens are valid and has the required scopes.
        /// </summary>
        /// <param name="testData">The data to test</param>
        [Theory, PropertyData("AuthorizedTokenTestData")]
        public void AuthorizeToken_AuthorizeTokenWithGrantedScopes_IsAuthorized(string[] testData)
        {
            var scopes = new string[testData.Length - 1];
            Array.Copy(testData, 1, scopes, 0, testData.Length - 1);
            var context = MockAuthorizationAttributeTest(testData.First());
            new AuthorizeTokenAttribute(scopes).OnAuthorization(context);
            Thread.CurrentPrincipal = null;
            Assert.True(context.RequestContext.Principal.Identity.IsAuthenticated);
            Assert.Null(context.Response);
        }

        /// <summary>
        /// Verifies that the bearer tokens are valid but the required scopes
        /// are not granted.
        /// </summary>
        /// <param name="testData">The data to test</param>
        [Theory, PropertyData("UnauthorizedTokenTestData")]
        public void AuthorizeToken_AuthorizeTokenWithNonGrantedScopes_IsUnauthorized(string[] testData)
        {
            var scopes = new string[testData.Length - 1];
            Array.Copy(testData, 1, scopes, 0, testData.Length - 1);
            var context = MockAuthorizationAttributeTest(testData.First());
            new AuthorizeTokenAttribute(scopes).OnAuthorization(context);
            Thread.CurrentPrincipal = null;
            Assert.Null(context.RequestContext.Principal);
            Assert.Equal(HttpStatusCode.Unauthorized, context.Response.StatusCode);
        }

        #endregion

        #region Private Functions.

        /// <summary>
        /// Mocks a GET request with OAuth 2 credentials.
        /// </summary>
        /// <param name="bearerToken">The bearer token to use</param>
        /// <returns><see cref="HttpActionContext"/></returns>
        private static HttpActionContext MockAuthorizationAttributeTest(string bearerToken)
        {
            var controllerDescriptor = new Mock<HttpControllerDescriptor>().Object;
            var actionDescriptor = new Mock<HttpActionDescriptor>().Object;
            var request = new HttpRequestMessage(HttpMethod.Get, "https://test.resource.server.se/");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
            request.Content = new StringContent("dummy", Encoding.UTF8, "application/x-www-form-urlencoded");
            var controllerContext = new HttpControllerContext
            {
                Request = request,
                ControllerDescriptor = controllerDescriptor
            };
            return new HttpActionContext(controllerContext, actionDescriptor);
        }

        #endregion
    }
}