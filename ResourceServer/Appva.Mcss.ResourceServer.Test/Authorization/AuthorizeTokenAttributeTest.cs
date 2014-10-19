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
        private static readonly string ClientCredentialsBearerToken = "gAAAAGvPdD6GtuuGISPA8CxRc3ckiBAFt_MLmFHxJS3OwJ36hW3wWX0paMkxNMu6M2_4C8M34dT2hGLycTF25CslIUHpCfJVLkLIOEDK0L7evgDJTTMtJReztAP6cJC1uq1yEOxeYLNNgh8VrCbzuNq-RbeOgmOmpdkU7JPn6n6XqCIohAIAAIAAAAAPWfZFeghjHCw9LqSQTj_eJMsaCMf9WtOGSqBFiQsLeUqm-0JFMB_s1ty4icfGeTjg3k3sUxFnnbau6nF26WlOWr18A9K-91t5YSZPvJg18SHjK-7FDZ67gRW6NxXDsBnBD2ZgbrR6DcthpCC5inBDTKbqZE5baKAPZvGLY1aMpoS0Y071hcJCfocOHK7GaikEAkt_ZdHqcIEE6DNW3rSo5j2REOKD5zds-g84_evu-SAOfoTbhzqf3h01nsG9cPb7NNCYw3q4Kd0MkJPBouZLsCnluNAsfAbowqVUzPkEoqIL2vnk_EkLJO--JwdIw7ZAaGMl4RqBTLohpcre2hI3j1MzvEriZjXgX4ZmIojtUR6q42u9_Uq4Jl4e5tik5lu9FdKU0e4ufbxWwhkOkOrAmaCUHJEAbroJC8oHDAjNOgqMMPCCBiKbORO46HnEq7r1-R4oJMmzFIWdd5rmMOrNlLWEGbwHfLlb_j5fDD8a_ljktQ4ANgBtsBzI7OBLMBkevGQRbzCbnI7TohmQFPDSm81ndiNPV8S95zfrKCo038eFF3uRgi0gwxLV5vZ6BPbGBI0Rwh0FeN5WcYG83Bao6a2SVtWNlnjPIFkxyvqM51A2eSdhgEbX9QEZgOKVJtkeVEflJzLVyELKsp6CFxU_5WJjxwvbCnwDiyZ1PdYk3VHylnz7yYDgyVvvTkD5BT0rvy4sygGLakUcCQb6DnK0cZ_qpni-f93S8bA3VSoLShAzX-kIwjtwQIfYyx7qeA9Krw1IpHZqw2q1SpeXpKbPYoqoJnXIln5ZNe6JQleXAI0-w0GrJnDaap2tZiLFg9BMeRsFGMQL3XWoJdrAACkc";
        
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