// <copyright file="ApiClient.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
namespace Appva.Sca
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Appva.Core.Extensions;
    using Appva.Http;
    using Appva.Sca.Models;

    #endregion

    /// <summary>
    /// Class ApiClient. This class cannot be inherited.
    /// </summary>
    internal sealed class ApiClient : RestClient
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
        internal ApiClient(Configuration config, IRestOptions options = null, System.Net.Http.HttpMessageHandler handler = null, bool disposeHandler = true) 
            : base(options, handler, disposeHandler)
        {
            this.config = config;
            this.BaseAddress = config.BaseAddress;
        }

        #endregion

        #region Members.

        /// <summary>
        /// GetResidentAsync
        /// </summary>
        /// <param name="id">A List of <see cref="string"/>.</param>
        /// <returns>GetResidentModel<see cref="GetResidentModel"/>.</returns>
        internal async Task<IHttpResponseMessage<GetResidentModel>> GetResidentAsync(string id)
        {
            var myToken = await this.GetTokenAsync();
            return await this.Get(UriHelper.GetResidentUrl(id)).WithBearerToken(myToken).ToResultAsync<GetResidentModel>();
        }

        /// <summary>
        /// Post ManualEvent as an asynchronous operation.
        /// </summary>
        /// <param name="manualEvents">The manual events.</param>
        /// <returns>Task&lt;IHttpResponseMessage&lt;List&lt;GetManualEventModel&gt;&gt;&gt;.</returns>
        internal async Task<IHttpResponseMessage<List<GetManualEventModel>>> PostManualEventAsync(List<PostManualEventModel> manualEvents)
        {
            var myToken = await this.GetTokenAsync();
            return await this.Post(UriHelper.ManualEventUrl, manualEvents).WithBearerToken(myToken).AsJson().ToResultAsync<List<GetManualEventModel>>();
        }

        /// <summary>
        /// GetTokenAsync
        /// </summary>
        /// <returns>Token value as string<see cref="String"/>.</returns>
        private async Task<string> GetTokenAsync()
        {
            if (this.token.IsNotNull() && this.token.IsValid)
            {
                return this.token.Value;
            }
            else
            {
                var response = await this.Get(UriHelper.TokenUrl).WithBasicAuthorization(this.config.Credentials).ToResultAsync<dynamic>();
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
                        ////If Bearer and/or Expires are missing or invalid.
                        this.token.SetValues("NOT_VALID", DateTimeOffset.UtcNow);
                    }
                    else
                    {
                        tokenValue = authvalues.FirstOrDefault().Replace("Bearer", string.Empty).TrimStart();
                        this.token.SetValues(tokenValue, (DateTimeOffset)expires);
                    }

                    return this.token.Value;
                }
                //// If credentials and/or Token Endpoint is invalid.
                this.token = new Token("NOT_VALID");
            }
            return this.token.Value;
        }

        #endregion
    }
}
