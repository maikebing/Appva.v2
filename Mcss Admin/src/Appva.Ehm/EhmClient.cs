// <copyright file="EhmClient.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.Ehm
{
    #region Imports.

    using Appva.Ehm.Exceptions;
    using Appva.Ehm.Models;
    using Appva.Http;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class EhmClient : RestClient, IEhmClient
    {
        #region Fields.
        
        /// <summary>
        /// The eHM-api configuration
        /// </summary>
        private readonly EhmConfiguration config;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="EhmClient"/> class.
        /// </summary>
        public EhmClient(IRestOptions options, EhmConfiguration config, HttpMessageHandler handler = null) 
            : base(options, handler)
        {
            this.config      = config;
            this.BaseAddress = config.baseUri;
        }

        #endregion

        #region IEhmClient members

        /// <inheritdoc />
        public async Task<IList<Ordination>> ListOrdinations(string forPatientUniqueId, User byUser)
        {
            var response = await this.Get(string.Format("{0}{1}?personnummer={2}", config.baseUri, EhmConfiguration.Endpoints.List, forPatientUniqueId.Replace("-", "")))
                .WithBasicAuthorization(this.GetAuthorizationToken(byUser))
                .ToResultAsync<ListOrdinationsResponse>();
            
            if (response.Response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new EhmUnauthorizedException();
            }
            if(response.Response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new EhmPatientNotFoundException();
            }
            if (!response.Response.IsSuccessStatusCode)
            {
                throw new EhmBadRequestException();
            }

            return response.Result.Ordinations;
        }

        #endregion

        #region Private members.

        /// <summary>
        /// Gets the authorization token.
        /// </summary>
        /// <param name="forUser">For user.</param>
        /// <returns></returns>
        private string GetAuthorizationToken(User forUser)
        {
            var json         = JsonConvert.SerializeObject(forUser);
            var bytes        = Encoding.UTF8.GetBytes(json);
            var base64String = Convert.ToBase64String(bytes);

            return base64String;
        }

        #endregion
    }
}