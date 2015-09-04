// <copyright file="DemoHipClient.cs" company="Appva AB">
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
    using System.Web;
    using Appva.Apis.Http;
    using Appva.Hip.Model;
    using System.Net;
    using Appva.Core.Logging;
    using Appva.Hip.Identity;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class DemoHipClient : BaseHipClient<DemoHipClient>
    {
        #region Static strings

        private static readonly string BaseUrl = "https://appva-dev-hip.cloudapp.net/ws/";

        private static readonly string TrustCertificatePath = HttpContext.Current.Server.MapPath("~/App_Data/Certificates/appva.p12"); 
        private static readonly string TrustCertificatePassword = "password";

        private static readonly string ClientCertificatePath = HttpContext.Current.Server.MapPath("~/App_Data/Certificates/ipv.p12"); 
        private static readonly string ClientCertificatePassword = "eM0rtALh";




        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="DemoHipClient"/> class.
        /// </summary>
        public DemoHipClient()
            : base(new DemoHipIdentity(), new HttpRequestClient(BaseUrl, ClientCertificatePath, ClientCertificatePassword, TrustCertificatePath, TrustCertificatePassword))
        {
        }

        #endregion
    }
}