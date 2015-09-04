// <copyright file="ConsentHandler.cs" company="Appva AB">
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
    using Appva.Hip.Identity;
    using Appva.Hip.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ConsentsClient : BaseClient
    {
        #region Private fields

        /// <summary>
        /// The <see cref="IHipIdentity"/>
        /// </summary>
        private readonly IHipIdentity identity;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsentsClient"/> class.
        /// </summary>
        public ConsentsClient(IHttpRequestClient httpClient, IHipIdentity identity)
            : base(httpClient, "consents/{0}")
        {
            this.identity = identity;
        }

        #endregion

        #region Members

        public async Task<Consents> GetAsync(string patientId)
        {
            var response = await this.HttpClient.Get(string.Format(this.UrlFormat, patientId)).WithHeaders(this.GetHeaders(patientId)).GetAsync().ConfigureAwait(false);
            return await response.ToResultAsync<Consents>().ConfigureAwait(false);
        }

        public async Task<bool> SetDruglistConsentAsync(string patientId, bool ongoing)
        {
            var consents = new PostConsents { Druglist = new PostDruglistConsent { Ongoing = ongoing } };
            return await this.SetConsentsAsync(patientId, consents).ConfigureAwait(false);
        }

        public async Task<bool> SetPdlConsentAsync(string patientId, bool onlyme, DateTime endDate)
        {
            var consents = new PostConsents 
            { 
                Pdl = new PostPdlConsent 
                { 
                    Emergency = false,
                    OnlyMe = onlyme,
                    StartDate = DateTime.Now.Date,
                    EndDate = endDate.Date
                } 
            };
            return await this.SetConsentsAsync(patientId, consents).ConfigureAwait(false);
        }

        public async Task<bool> SetConsentsAsync(string patientId, bool druglistOngoing, bool onlyme, DateTime endDate)
        {
            var consents = new PostConsents 
            {
                Druglist = new PostDruglistConsent 
                { 
                    Ongoing = druglistOngoing 
                }, 
                Pdl = new PostPdlConsent 
                { 
                    Emergency = false,
                    OnlyMe = onlyme,
                    StartDate = DateTime.Now.Date,
                    EndDate = endDate
                }
            };
            return await this.SetConsentsAsync(patientId, consents).ConfigureAwait(false);
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

        #region Private members

        private async Task<bool> SetConsentsAsync(string patientId, PostConsents model)
        {
            var response = await this.HttpClient.Post(string.Format(this.UrlFormat, patientId)).WithHeaders(this.GetHeaders(patientId)).WithJsonEncodedBody(model).GetAsync().ConfigureAwait(false);
            return response.IsSuccessStatusCode();
        }

        #endregion
    }
}