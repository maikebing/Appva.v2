// <copyright file="ApiService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>


using System.Threading.Tasks;
using Appva.Http;

namespace Appva.Sca
{
    #region Imports

    using Appva.Sca.Models;
    using System;
    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// The <see cref="IApiService"/>.
    /// </summary>
    public interface IApiService
    {
        /// <summary>
        /// GetResident
        /// </summary>
        /// <param name="id">Identifi ID</param>
        /// <returns>Returns a <see cref="GetResidentModel"/>.</returns>
        GetResidentModel GetResident(string id);

        /// <summary>
        /// Get the request URI.
        /// </summary>
        /// <param name="id">Identifi ID</param>
        /// <param name="manualeventList">List of <see cref="PostManualEventModel"/></param>
        /// <returns>Returns a <see cref="GetManualEventModel"/>.</returns>
        List<GetManualEventModel> PostManualEvent(List<PostManualEventModel> manualEventList);

        Task<IHttpResponseMessage<GetResidentModel>> GetResidentAsync(string id);
    }

    /// <summary>
    /// The <see cref="ApiService"/> service.
    /// </summary>
    public class ApiService : IApiService
    {
        #region Variables.

        /// <summary>
        /// The <see cref="Configuration"/> configuration.
        /// </summary>
        private Configuration config;

        /// <summary>
        /// The <see cref="ApiClient"/> client.
        /// </summary>
        private ApiClient client;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiService"/> class.
        /// </summary>
        /// <param name="baseAddress">The <see cref="Uri"/>.</param>
        /// <param name="clientId">The <see cref="String"/> Client Id.</param>
        /// <param name="clientSecret">The <see cref="String"/> Client Secret.</param>
        public ApiService(Uri baseAddress, string clientId, string clientSecret)
        {
            this.config = new Configuration(baseAddress, clientId, clientSecret);
            this.client = new ApiClient(this.config);
            
        }

        #endregion

        #region ApiService Members

        /// <inheritdoc />
        public GetResidentModel GetResident(string id)
        {
            // TODO: Exception Handling.
            return this.client.GetResident(id);
        }

        /// <inheritdoc />
        public List<GetManualEventModel> PostManualEvent(List<PostManualEventModel> manualEventList)
        {
            // TODO: Exception Handling.
            return this.client.PostManualEvent(manualEventList);
        }

        public async Task<IHttpResponseMessage<GetResidentModel>> GetResidentAsync(string id)
        {
            return await this.client.GetResidentAsync(id);
        }
        #endregion
    }
}
