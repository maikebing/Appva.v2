// <copyright file="MedicationHistoryClient.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Hip.Clients
{
    #region Imports.

    using Appva.Apis.Http;
    using Appva.Hip.Exceptions;
    using Appva.Hip.Identity;
    using Appva.Hip.Model;
    using Appva.Core.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class MedicationHistoryClient : BaseClient
    {
        #region Private fields

        /// <summary>
        /// The <see cref="IHipIdentity"/>
        /// </summary>
        private readonly IHipIdentity identity;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="MedicationHistoryClient"/> class.
        /// </summary>
        public MedicationHistoryClient(IHttpRequestClient httpClient, IHipIdentity identity)
            : base(httpClient, "data/{0}/?infoType=medicationhistory{1}")
        {
            this.identity = identity;
        }

        #endregion

        #region Members

        public async Task<HipResponse<MedicationItem>> ListAsync(string patientId, int page = 1, int pageSize = 10)
        {
            var urlParams = string.Format("&start={0}&limit={1}", (page - 1) * pageSize, pageSize);
            var url = string.Format(this.UrlFormat, patientId, urlParams);

            return await this.GetDataFromUrlAsync(url, this.GetHeaders(patientId)).ConfigureAwait(false);
        }

        public async Task<ResponseItem<MedicationItem>> GetAsync(string patientId, string id)
        {
            var urlParams = string.Format("&filters=id${0};", id);
            var url = string.Format(this.UrlFormat, patientId, urlParams);

            var result = await this.GetDataFromUrlAsync(url, this.GetHeaders(patientId)).ConfigureAwait(false);

            return result.Content.FirstOrDefault();
        }

        #endregion

        #region Overrides

        protected override IDictionary<string, string> GetHeaders(string patientId)
        {
            var retval = this.identity.GetDefaultHeaders();
            retval.Add("X-IPV-Resource-PatientId", patientId);
            return retval;
        }

        #endregion

        #region Helpers

        private async Task<HipResponse<MedicationItem>> GetDataFromUrlAsync(string url, IDictionary<string, string> headers)
        {
            var response = await this.HttpClient.Get(url).WithHeaders(headers).GetAsync().ConfigureAwait(false);

            if (response.GetStatusCode().Equals(HttpStatusCode.Forbidden))
            {
                Log.Debug("No Consent found");
                throw new MissingPdlConsentException();
            }

            return await response.ToResultAsync<HipResponse<MedicationItem>>().ConfigureAwait(false);
        }

        #endregion
    }
}