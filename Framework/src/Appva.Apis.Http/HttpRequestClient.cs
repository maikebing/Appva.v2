// <copyright file="HttpRequestClient.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Apis.Http
{
    #region Imports.

    using Appva.Core.Logging;
    using Appva.Cryptography.X509;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Security;
    using System.Security.Cryptography.X509Certificates;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class HttpRequestClient : IHttpRequestClient
    {
        #region Private fields

        /// <summary>
        /// The <see cref="HttpClient"/>
        /// </summary>
        private HttpClient httpClient;

        /// <summary>
        /// The certificate
        /// </summary>
        private X509Certificate2 clientCertificate;

        private static X509Certificate2 TrustCertificate;

        /// <summary>
        /// The <see cref="ILog"/> for <see cref="DemoHipClient"/>.
        /// </summary>
        private static readonly ILog Log = LogProvider.For<HttpRequest>();

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpRequestClient"/> class.
        /// </summary>
        public HttpRequestClient()
        {
            this.httpClient = new HttpClient();

        }

        public HttpRequestClient(string url)
        {
            this.httpClient = new HttpClient();
            this.httpClient.BaseAddress = new Uri(url);
        }

        public HttpRequestClient(string url, string clientCertificatePath, string clientCertificatePassword)
        {
            this.clientCertificate = CertificateUtils.LoadCertificateFromDisk(clientCertificatePath, clientCertificatePassword);

            this.httpClient = new HttpClient();
            this.httpClient.BaseAddress = new Uri(url);
        }

        public HttpRequestClient(string url, string clientCertificatePath, string clientCertificatePassword, string trustCertificatePath, string trustCertificatePassword)
        {
            this.clientCertificate = CertificateUtils.LoadCertificateFromDisk(clientCertificatePath, clientCertificatePassword);
            TrustCertificate = CertificateUtils.LoadCertificateFromDisk(trustCertificatePath, trustCertificatePassword);

            var handler = new WebRequestHandler();
            handler.ServerCertificateValidationCallback = ValidateServerCertficate;
            handler.ClientCertificates.Add(this.clientCertificate);
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            

            this.httpClient = new HttpClient(handler);
            this.httpClient.BaseAddress = new Uri(url);
        }

        #endregion

        #region IHttpRequestClient Members

        public IHttpRequest Get(string url)
        {
            return new HttpRequest(HttpMethod.Get, new Uri(url, UriKind.RelativeOrAbsolute), this.httpClient, this.clientCertificate);
        }

        public IHttpRequest Put(string url)
        {
            return new HttpRequest(HttpMethod.Put, new Uri(url, UriKind.RelativeOrAbsolute), this.httpClient, this.clientCertificate);
        }

        public IHttpRequest Post(string url)
        {
            return new HttpRequest(HttpMethod.Post, new Uri(url, UriKind.RelativeOrAbsolute), this.httpClient, this.clientCertificate);
        }

        #endregion

        #region Static helpers

        private static bool ValidateServerCertficate(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
            {
                return true;
            }
            Log.Debug("Ssl certificate error: {0}", sslPolicyErrors);
            Log.Debug("Ssl certificate remote SN: {0}", cert.GetSerialNumber());
            Log.Debug("Ssl certificate trust SN: {0}", TrustCertificate.GetSerialNumber());
            if(cert.GetSerialNumber() == TrustCertificate.GetSerialNumber())
            {
                return true;
            }
            return true;
        }

        #endregion
    }
}