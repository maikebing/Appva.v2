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
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
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
            var request = this.Get(string.Format("{0}{1}?personnummer={2}", config.baseUri, EhmConfiguration.Endpoints.List, forPatientUniqueId.Replace("-","")));
            var response = await request.ToResultAsync<ListOrdinationsResponse>();
            
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

        
    }
}