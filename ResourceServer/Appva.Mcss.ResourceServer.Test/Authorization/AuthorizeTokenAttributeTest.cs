// <copyright file="AuthorizeTokenAttributeTest.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
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
    using Appva.Mcss.ResourceServer.Application;
    using Appva.Mcss.ResourceServer.Application.Authorization;
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
        private static readonly string PasswordBearerToken = "gAAAAFCkdatHHiRQ_BQ6ibdE7Nr0PXQJUUA7VB803dMD4UatrJpUvppHKYxfI0FkiE7TA2lBTyMzSpGTOwPGySqaygfzqv4Se6_mRDdEliwUgUkLJc4pS_n5a_iWpocAGLhI5ZcS8EF-CjF2UPQ_iqfZoCTNN2kQRZatZkzbgT6QVc591AIAAIAAAABIfx4Uf98uVxdrjQF0ub7QOhdU9IAYpFrxlyhKOSPXQtGjlla1NJwAZ5ESi6RaFoXU9btWiaARoTN8K5pDFbCoGMmpRDdB0de1bxjbyGrb0nx9O4lraMqJKGq3QmnWzi3orDBHXptyolNxpYNTtxbLRd-sgAbFke3xS12QHhLaPilUKyQBj57lCD0RjULR2LTB-GwZ8lgHAbiBAOt_oRp41bW3tRmoJlztz_yDlMhlZnvfLHfm7s9xNu59ZQ2CoWtMBz4nUZyQHQL6l5oP355bpc3jTMXBl2r0U-jynb-wh2PiEq8Xa4_dwq9muZam79ch_bFSgM_JFgIJsYvD0u_0KP94ERgsaZ9gKZiMsgh_2FZY9IueL3tFARzHnKbUOstzTHG3diRiCcguEMViiEuExr1i-eJ9u9KBrWmWW3ddc2maIOb2GPbKkenmHr83WP2yXCQhM5b9pjRkdyNPZZ1t2QySZcUORUzjT41PpQdYZsxcOIS1NeZQZuVagchRPJ68XtAe7AjxwS_DlWVOKsSZ7ngu98RdsJb8jxzMFLaHj_Rkifmn7FkAcm6-xdCPI4uftjJXedy7B39jcZDLq939jmmFVqJSmd4iD1NGhZZKMw5eTfs_kSr9Ez7qaa-NJc2gAoFY5b9cUPw-jee6Ouo0vUZbPm2A0hwWjVuiPAHJisZrBpPFQqYJrNrE1_dLNXaQrj63T-2ntrEfSdCE9LiW6NmKUNJnZsj7MbWta5m9ERoQufKwP7H42opKl40edoCG0CG40vlV6mIXO1VgQYYOkIWFY5CUZVlfZN9s0QjMhLuBmZXVpl_unpDktjkM5MZ7gX9ilFRbdTtCSdWL5qu3YYiUIwC5EYif5j7Ug4hQC6BwBpi7D0qVCqvVoiFuriQ50l5sYmbEzLYs7tYpPxvZ3-GYJ3hkNZxyU6ahj5MpyniKf72HKtkqhCOoci63ufQ";

        /// <summary>
        /// Client Credentials (authorization grant) bearer token without expiration. 
        /// Granted scopes: 
        /// https://appvaapis.se/auth/resource
        /// </summary>
        private static readonly string ClientCredentialsBearerToken = "gAAAABW7k6nU9GoC5mUAJASxfNTbykoMb7ecVwyCPE3HCluRFft5jshBSiwyDccVSXZV5loshfvCy3sbjRjO1x8Qt8nzayT58Z48xfIAfrhCXWKhB9NiBn9IHwtgiQCfZGetG3N3TUwin-qNrOmFaco5FQi_JVX4Ds73pSFnTwh7jLxIhAIAAIAAAAAAGtNFJHkCvpIAV9gI1Iu3nnc3skaQoAt9F5CDhmA0iR9USF-BxOTyj3K6LY48Tr-87pMxuv1Uf15uFFHjQ7B16LW_PfUaGu0CVeB2BdOGDuoC-r2L_jx3qjOI4u0hSHc9lH4RkMs7KjKrBtrhSp9smqoQ5HwAIaRYhwSpBKaOWDQsMIYxjLbj6p_AcCo1DQe5QeNt8P7kNxJ2i-8Lkef44IAaf2bhfeqM8Vt82PjbIiOPaKJTYaqb3v-mbm9v7qpryOsH5G2gzMxTk6msEo_o-YUGm63E5qqMjqbb7vYO-Xi2rNOmrY5aZVRHZGSxnFv3I6w7j9f2AoQLfZ7tGnP9Jwcy5G3ksy1Z44xGw00lJLSNmNdk9S6IbTCSH4RITR1o4BoLJpnSeWsS6EfjrMRR6f2GvDWW5zm706MVy-KS8wMumUOhJfqcO1FggcopfbzSkV3zjQcEyPqlMdGEizmkCdFBQ19_IFblC9FIIncmeZqM7gDUQK_CPXymmuRdjoxawvm32RVlGX5AfR-7zZlxz39NBuCSe8RBKEZmoB9wpyn0-xhMrxFmeg84UIgtVHBnB_f9kwWju61BucP1Nu15Mm-90muPxlOqwQmle10UN_j-b6ZkH2V3sUOyIPyIL03WpG5IuBTf6s3xY_cuBOhRuyGs7qyCnhsVvgFkxA2QYxkrAYrQBKZFRJOjuyv_G0wsbyxLRVR34PDkVBhiAhytW0ZRLr04g98fmBVD07rYncOro7RcK8LmKuoEs3SUagKybcUUy7kt_AdEGPvLGKDHsCLExiEY7dJqw7EwhnUpdnvXiMUGm5CWD1pHXvECPIJY9ehUjbZqmfEIkiQMs0HR";
        
        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizeTokenAttributeTest"/> class.
        /// </summary>
        public AuthorizeTokenAttributeTest()
        {
        }

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
            var context = this.MockAuthorizationAttributeTest(testData.First());
            new AuthorizeTokenAttribute(scopes).OnAuthorization(context);
            Thread.CurrentPrincipal = null;
            Assert.True(context.RequestContext.Principal.Identity.IsAuthenticated);
            Assert.Null(context.Response);
        }

        /// <summary>
        /// Verifies that the bearer tokens are valid but the required scopes
        /// are not granted
        /// </summary>
        /// <param name="testData">The data to test</param>
        [Theory, PropertyData("UnauthorizedTokenTestData")]
        public void AuthorizeToken_AuthorizeTokenWithNonGrantedScopes_IsUnauthorized(string[] testData)
        {
            var scopes = new string[testData.Length - 1];
            Array.Copy(testData, 1, scopes, 0, testData.Length - 1);
            var context = this.MockAuthorizationAttributeTest(testData.First());
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
        private HttpActionContext MockAuthorizationAttributeTest(string bearerToken)
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