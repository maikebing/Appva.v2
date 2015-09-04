// <copyright file="BaseHipClient.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Hip
{
    #region Imports.

    using Appva.Apis.Http;
    using Appva.Core.Logging;
    using Appva.Hip.Clients;
    using Appva.Hip.Identity;
    using Appva.Hip.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public abstract class BaseHipClient<T> : IHipClient
    {
        #region Fields

        /// <summary>
        /// The <see cref="IHttpClient"/>
        /// </summary>
        private readonly IHttpRequestClient httpClient;

        /// <summary>
        /// The <see cref="ConsentsClient"/>
        /// </summary>
        private readonly ConsentsClient consents;

        /// <summary>
        /// The <see cref="DruglistClient"/>
        /// </summary>
        private readonly DruglistClient druglist;

        /// <summary>
        /// The <see cref="DruglistClient"/>
        /// </summary>
        private readonly MedicationHistoryClient medication;

        /// <summary>
        /// The <see cref="ILog"/> for <see cref="DemoHipClient"/>.
        /// </summary>
        private static readonly ILog Log = LogProvider.For<T>();

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseHipClient"/> class.
        /// </summary>
        public BaseHipClient(IHipIdentity identity, IHttpRequestClient httpClient)
        {
            this.httpClient = httpClient;
            this.consents = new ConsentsClient(httpClient, identity);
            this.druglist = new DruglistClient(httpClient, identity);
            this.medication = new MedicationHistoryClient(httpClient, identity);
        }

        #endregion

        #region Public Properties

        public ConsentsClient Consents
        {
            get { return this.consents; } 
        }

        public DruglistClient Druglist
        {
            get { return this.druglist; }
        }

        public MedicationHistoryClient Medication
        {
            get { return this.medication; } 
        }

        #endregion

        #region IHipClient members

        /// <inheritdoc />
        public async Task<bool> CheckDruglistConsent(string patientId)
        {
            var consent = await this.consents.GetAsync(patientId).ConfigureAwait(false);
            return consent.Druglist.Allowed && consent.Druglist.Valid;
        }

        #endregion
    }
}