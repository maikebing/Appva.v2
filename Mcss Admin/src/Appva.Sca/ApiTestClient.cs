// <copyright file="ApiClient.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
using Appva.Core.Extensions;
using Appva.Http;
using Appva.Sca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace Appva.Sca
{
    #region Imports.


    #endregion

    /// <summary>
    /// Class ApiClient. This class cannot be inherited.
    /// </summary>
    internal sealed class ApiTestClient : RestClient
    {
        #region Variables.

        /// <summary>
        /// The configuration
        /// </summary>
        private readonly Configuration config;

        /// <summary>
        /// The token
        /// </summary>
        private Token token;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiClient"/> class.
        /// </summary>
        /// <param name="config">Configuration <see cref="Configuration"/>.</param>
        /// <param name="options">Options <see cref="IRestOptions"/>.</param>
        /// <param name="handler">Handler <see cref="System.Net.Http.HttpMessageHandler"/>.</param>
        /// <param name="disposeHandler">DisposeHandler <see cref="bool"/>.</param>
        internal ApiTestClient(Configuration config, IRestOptions options = null, System.Net.Http.HttpMessageHandler handler = null, bool disposeHandler = true)
            : base(options, handler, disposeHandler)
        {
            this.config = config;
            this.BaseAddress = new Uri("https://tenaidentifistage.sca.com/");
        }

        #endregion

        #region Members.

        public async Task<string> GetMeTheBloodyToken(string url)
        {
            var tokenvalue = string.Empty;
            var httpMessage = new HttpResponseMessage();
            try
            {
                var getmeabloodytoken = await this.Get("https://tenaidentifistage.sca.com/api/token/")
                    .WithBasicAuthorization(
                        "RUFCRTY3NTEtMkFCRC00MzExLUE3OTQtNzBBODMzRDMxQzMxOkM1QzhEQUVCLTZDMDctNDIzRC04MkNGLTgxNzdDOENCOTYwNA==")
                    .ToResultAsync<string>();

                tokenvalue = "this is a fake token value";
            }
            catch (HttpResponseException exp)
            {
                tokenvalue = exp.Message;
                httpMessage = exp.Response;

            }

            //var x = getmeabloodytoken.Response.IsSuccessStatusCode;

            return tokenvalue;
        }

        /// <summary>
        /// Get Resident
        /// </summary>
        /// <param name="url">External Id.</param>
        /// <returns>IHttpResponseMessage</returns>
        public GetResidentModel GiveMeAResident(string url)
        {
            var model = new GetResidentModel();
            model.ExternalId = url;

            try
            {
                var abloodytokenvalue = this.GetMeTheBloodyToken(string.Empty).Result;
                model.FacilityName = abloodytokenvalue;
                model.RoomNumber = "123";
            }
            catch (Exception ex)
            {
                model.RoomNumber = ex.StackTrace;
                model.FacilityName = ex.Message;
            }
            return model;
        }

        /// <summary>
        /// Get Token
        /// </summary>
        /// <returns>String</returns>
        private string GetToken()
        {
            if (this.token.IsNotNull() && this.token.IsValid)
            {
                return this.token.Value;
            }
            else
            {
                /* Fake response generated to simulate a real environment */
                var FakeResponse = new HttpResponseMessage();
                FakeResponse.Headers.Add("Authorization", "Bearer FAKETOKENVALUE");
                FakeResponse.Content.Headers.Expires = DateTimeOffset.UtcNow.AddMinutes(30);
                FakeResponse.StatusCode = HttpStatusCode.OK;

                //var response = this.Get(UriHelper.TokenUrl).WithBasicAuthorization(this.config.Credentials).ToResultAsync<dynamic>().Result;
                //var result = response.Response;

                var response = FakeResponse;
                var result = response;

                if (result.IsSuccessStatusCode)
                {
                    IEnumerable<string> authvalues = null;
                    string tokenValue = string.Empty;
                    this.token = new Token();

                    result.Headers.TryGetValues("Authorization", out authvalues);
                    var expires = result.Content.Headers.Expires;

                    if (authvalues.IsNull() || expires == null)
                    {
                        ////If Bearer value is missing or invalid format and/or Expires value are missing.
                        this.token.SetValues("NOT_VALID", DateTimeOffset.UtcNow);
                    }
                    else
                    {
                        tokenValue = authvalues.FirstOrDefault().Replace("Bearer", string.Empty).TrimStart();
                        this.token.SetValues(tokenValue, (DateTimeOffset)expires);
                    }

                    return this.token.Value;
                }

                // kommer hit om credentials eller token enpoint är felinställda. Generar 500 fel i slutändan.
                this.token = new Token("NOT_VALID");
            }

            return this.token.Value;
        }

        #endregion
    }
}
