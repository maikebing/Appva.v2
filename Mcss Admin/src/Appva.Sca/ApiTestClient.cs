// <copyright file="ApiClient.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
using Appva.Core.Extensions;
using Appva.Http;
using Appva.Mcss.Admin.Domain.Entities;
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
            this.BaseAddress = config.BaseAddress;
        }

        #endregion

        #region Members.

        /// <summary>
        /// Get Resident
        /// </summary>
        /// <param name="id">A List of <see cref="string"/>.</param>
        /// <returns>GetResidentModel<see cref="GetResidentModel"/>.</returns>
        public GetResidentModel GetResident(string id)
        {
            var myToken = this.GetToken();
            var mockedModel = new GetResidentModel
            {
                ExternalId = id,
                RoomNumber = "1",
                FacilityName = myToken
            };
            var result = mockedModel;
            return result;
        }

        /// <summary>
        /// Post ManualEvent
        /// </summary>
        /// <param name="events">A List of <see cref="PostManualEventModel"/>.</param>
        /// <returns>IHttpResponseMessage</returns>
        public List<GetManualEventModel> PostManualEvent(IList<PostManualEventModel> events)
        {
            var tokenvalue = this.GetToken();

            var FakeResponseData = new List<GetManualEventModel>();
            foreach (var item in events)
            {
                FakeResponseData.Add(new GetManualEventModel()
                {
                    Id = item.Id,
                    ImportResult = "Created"
                });
            }

            //var result = this.Post(UriHelper.ManualEventUrl, events)
            //    .WithBearerToken(tokenvalue)
            //    .ToResultAsync<dynamic>().Result;
            //if (result.Response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            //{
            //    this.token = null;
            //}
            return FakeResponseData;
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
                var response = this.Get(UriHelper.TokenUrl).WithBasicAuthorization(this.config.Credentials).ToResultAsync<dynamic>().Result;
                var result = response.Response;


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

        public async Task<string> GetTokenAsync()
        {
            if (this.token.IsNotNull() && this.token.IsValid)
            {
                return this.token.Value;
            }
            else
            {
                var response = await this.Get("api/token/").WithBasicAuthorization(this.config.Credentials).ToResultAsync<dynamic>();
                var result = response.Response;

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

        #region Testmembers.
        // TEST Async
        public async Task<IHttpResponseMessage<GetResidentModel>> GetResidentAsync(string id)
        {
            // snygga till denna kod och lägg in den i riktiga klienten.
            var myToken = await this.GetTokenAsync();
            var response = await this.Get(UriHelper.ResidentUrl(id)).WithBearerToken(myToken)
                .ToResultAsync<GetResidentModel>();
            return response;
        }

        public async Task<IHttpResponseMessage<List<GetManualEventModel>>> PostManualEventModelAsync(List<PostManualEventModel> manualEvents)
        {
            //TODO: Logic in here.
            var myToken = await this.GetTokenAsync();
            var response = await this.Post(UriHelper.ManualEventUrl, manualEvents).WithBearerToken(myToken).AsJson().ToResultAsync<List<GetManualEventModel>>();
            return response;
        }
        #endregion
    }
}
