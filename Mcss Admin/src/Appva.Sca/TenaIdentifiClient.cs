// <copyright file="ApiService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
namespace Appva.Sca
{
    #region Imports

    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Linq;
    using System.Threading.Tasks;
    using Appva.Sca.Models;
    using Appva.Http;
    using Appva.Core.Extensions;
using System.Net.Http;

    #endregion

    /// <summary>
    /// The <see cref="TenaIdentifiClient"/> service.
    /// </summary>
    public class TenaIdentifiClient : RestClient, ITenaIdentifiClient
    {
        #region Variables.

        /// <summary>
        /// The token
        /// </summary>
        private Dictionary<TenaIdentifiCredentials, Token> tokens;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="TenaIdentifiClient"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="baseAddress">The base address.</param>
        /// <param name="handler">The handler.</param>
        /// <param name="disposeHandler">if set to <c>true</c> [dispose handler].</param>
        public TenaIdentifiClient(
            IRestOptions options,
            string baseAddress,
            HttpMessageHandler handler = null,
            bool disposeHandler = true)
            : this(options, new Uri(baseAddress), handler, disposeHandler)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TenaIdentifiClient"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="baseAddress">The base address.</param>
        /// <param name="handler">The handler.</param>
        /// <param name="disposeHandler">if set to <c>true</c> [dispose handler].</param>
        public TenaIdentifiClient( 
            IRestOptions options,
            Uri baseAddress,
            HttpMessageHandler handler = null, 
            bool disposeHandler = true) 
            : base(options, handler, disposeHandler)
        {
            this.BaseAddress = baseAddress;
            this.tokens      = new Dictionary<TenaIdentifiCredentials, Token>();
        }

        #endregion

        #region ITenaIdentifiClient Members

        /// <inheritdoc />
        public async Task<Resident> GetResidentAsync(string id, TenaIdentifiCredentials credentials)
        {
            var response = await this.Get(UriHelper.GetResidentUrl(id))
                                    .WithBearerToken((await this.GetTokenFor(credentials)).ToString())
                                    .ToResultAsync<Resident>();

            if (response.Response.IsSuccessStatusCode)
            {
                return response.Result;
            }

            var result = new Resident();

            switch (response.Response.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    result.Message = "Ingen boende kunde hittas för givet ID";
                    break;
                default:
                    result.Message = "Ett fel inträffade. Var god försök igen.";
                    break;
            }
            return result;
        }

        /// <inheritdoc />
        public async Task<List<ManualEventResult>> PostManualEventsAsync(List<ManualEvent> manualEvents, TenaIdentifiCredentials credentials)
        {
            var response = await this.Post(UriHelper.ManualEventUrl, manualEvents)
                                .AsJson()
                                .WithBearerToken((await this.GetTokenFor(credentials)).ToString())
                                .ToResultAsync<List<ManualEventResult>>();

            return response.Result;
        }
        
        #endregion

        #region Private members

        /// <summary>
        /// GetTokenAsync
        /// </summary>
        /// <returns>Token value as string<see cref="String"/>.</returns>
        private async Task<Token> GetTokenFor(TenaIdentifiCredentials credentials)
        {
            if (!this.tokens.ContainsKey(credentials) || !this.tokens[credentials].IsValid)
            {
                await RequestNewTokenFor(credentials);
            }

            return this.tokens[credentials];
        }

        /// <summary>
        /// Requests the new token for.
        /// </summary>
        /// <param name="credentials">The credentials.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">
        /// Could not get a authorization-token for TENA Identifi
        /// or
        /// Could not get response-deaders for authorization token in TENA Identifi
        /// </exception>
        private async Task RequestNewTokenFor(TenaIdentifiCredentials credentials)
        {
            //// If no valid token, request a new token
            var response = await this.Get(UriHelper.TokenUrl)
                .WithBasicAuthorization(credentials.BasicAuthorizationHeader)
                .ToResultAsync<dynamic>();
            var result = response.Response;

            if (result.IsSuccessStatusCode == false)
            {
                //// If credentials and/or Token Endpoint is invalid.
                throw new Exception("Could not get a authorization-token for TENA Identifi");
            }

            IEnumerable<string> authvalues;
            result.Headers.TryGetValues("Authorization", out authvalues);

            var expires = result.Content.Headers.Expires;

            if (authvalues.IsNull() || expires == null)
            {
                throw new Exception("Could not get response-deaders for authorization token in TENA Identifi");
            }

            var token = authvalues.FirstOrDefault().Replace("Bearer", string.Empty).TrimStart();
            this.tokens.Add(credentials, Token.Create(token, (DateTimeOffset)expires));
        }

   

        #endregion
    }
}