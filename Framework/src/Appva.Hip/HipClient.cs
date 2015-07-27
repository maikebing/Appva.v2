// <copyright file="HipClient.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Hip
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class HipClient
    {
        #region Fields 

        private readonly string baseUrl = "https://appva-dev-hip.cloudapp.net/ws/";

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="HipClient"/> class.
        /// </summary>
        public HipClient()
        {
        }

        #endregion

        public bool GetConsents(string personalId)
        {
            var client = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Get, baseUrl);

        }
    }
}