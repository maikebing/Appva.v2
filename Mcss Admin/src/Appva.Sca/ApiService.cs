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
    using System.Threading.Tasks;
    using Appva.Sca.Models;

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
        Task<GetResidentModel> GetResidentAsync(string id);

        /// <summary>
        /// Posts the manual event asynchronous.
        /// </summary>
        /// <param name="manualEvents">The manual events.</param>
        /// <returns>Task&lt;List&lt;GetManualEventModel&gt;&gt;.</returns>
        Task<List<GetManualEventModel>> PostManualEventAsync(List<PostManualEventModel> manualEvents);

        /// <summary>
        /// Gets a value indicating whether this instance has credentials.
        /// </summary>
        /// <value><c>true</c> if this instance has credentials; otherwise, <c>false</c>.</value>
        bool HasCredentials { get; }

        /// <summary>
        /// Sets the credentials.
        /// </summary>
        /// <param name="credentials">The credentials.</param>
        void SetCredentials(string credentials);

        /// <summary>
        /// Sets the credentials.
        /// </summary>
        /// <param name="tenant">The tenant.</param>
        /// <param name="credentials">The credentials.</param>
        void SetCredentials(string tenant, string credentials);
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

        /// <summary>
        /// Gets a value indicating whether this instance has credentials.
        /// </summary>
        /// <value><c>true</c> if this instance has credentials; otherwise, <c>false</c>.</value>
        public bool HasCredentials => this.config.HasCredentials;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiService"/> class.
        /// </summary>
        /// <param name="baseAddress">The base address.</param>
        public ApiService(Uri baseAddress)
        {
            this.config = new Configuration(baseAddress);
            this.client = new ApiClient(this.config);
        }

        #endregion

        #region ApiService Members

        /// <inheritdoc />
        public async Task<GetResidentModel> GetResidentAsync(string id)
        {
            var response = await this.client.GetResidentAsync(id);

            if (response.Response.IsSuccessStatusCode)
            {
                return response.Result;
            }

            var result = new GetResidentModel();

            switch (response.Response.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    result.Message = "Användaren kan inte hittas";
                    break;
                default:
                    result.Message = "Ett fel inträffade. Var god försök igen.";
                    break;
            }
            return result;
        }

        /// <inheritdoc />
        public async Task<List<GetManualEventModel>> PostManualEventAsync(List<PostManualEventModel> manualEvents)
        {
            var response = await this.client.PostManualEventAsync(manualEvents);
            var result = response.Result;

            //// TODO: response handling?

            return result;
        }

        /// <inheritdoc />
        public void SetCredentials(string credentials)
        {
            this.config.SetCredentials(credentials);
        }

        /// <inheritdoc />
        public void SetCredentials(string tenant, string credentials)
        {
            this.config.SetCredentials(tenant, credentials);
        }
        
        #endregion
    }
}