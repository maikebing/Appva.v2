// <copyright file="BaseClient.cs" company="Appva AB">
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
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public abstract class BaseClient
    {
        #region Private fields

        /// <summary>
        /// The <see cref="ILog"/> for <see cref="IHipClient"/>.
        /// </summary>
        protected static readonly ILog Log = LogProvider.For<IHipClient>();

        /// <summary>
        /// The <see cref="IHttpRequestClient"/>
        /// </summary>
        private readonly IHttpRequestClient httpClient;

        protected readonly string UrlFormat;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseClient"/> class.
        /// </summary>
        public BaseClient(IHttpRequestClient httpClient, string urlFormat)
        {
            this.httpClient = httpClient;
            this.UrlFormat = urlFormat;
        }

        #endregion

        #region Properties

        protected IHttpRequestClient HttpClient
        {
            get { return this.httpClient; }
        }

        #endregion

        #region Members

        //public abstract Task<object> GetAsync(string patientId);

        //public abstract async Task<bool> Create(string patientId, object model) { return null; }

        #endregion

        #region Abstract members

        protected abstract IDictionary<string, string> GetHeaders(string patientId);

        #endregion

    }
}