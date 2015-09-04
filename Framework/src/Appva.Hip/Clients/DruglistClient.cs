// <copyright file="DruglistClient.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Hip.Clients
{
    #region Imports.

    using Appva.Apis.Http;
    using Appva.Core.Logging;
    using Appva.Hip.Exceptions;
    using Appva.Hip.Identity;
    using Appva.Hip.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class DruglistClient : BaseClient
    {
        #region Private fields

        /// <summary>
        /// The <see cref="IHipIdentity"/>
        /// </summary>
        private readonly IHipIdentity identity;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="DruglistClient"/> class.
        /// </summary>
        public DruglistClient(IHttpRequestClient httpClient, IHipIdentity identity) : base(httpClient, "data/{0}/?infoType=druglist")
        {
            this.identity = identity;
        }

        #endregion


        #region Members

        public async Task<HipResponse<DruglistItem>> GetAsync(string patientId)
        {
            var response = await this.HttpClient.Get(string.Format(this.UrlFormat, patientId)).WithHeaders(this.GetHeaders(patientId)).GetAsync().ConfigureAwait(false);
            if (response.GetStatusCode().Equals(HttpStatusCode.Forbidden))
            {
                Log.Debug("No Consent found");
                throw new MissingDruglistConsentException();
            }
            return await response.ToResultAsync<HipResponse<DruglistItem>>();
        }

        protected override IDictionary<string, string> GetHeaders(string patientId)
        {
            var retval = this.identity.GetDefaultHeaders();
            retval.Add("X-IPV-Resource-PatientId", patientId);
            return retval;
        }

        #endregion
    }
}