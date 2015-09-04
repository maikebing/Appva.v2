// <copyright file="AuthifyClient.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Siths
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Appva.Core.Logging;
    using Appva.Siths.Configuration;
    using Appva.Siths.Http;
    using Appva.Siths.Identity;
    using Appva.Siths.Security;
    using JetBrains.Annotations;

    #endregion

    /// <summary>
    /// Abstract base class for explicit Authify clients.
    /// <example>
    /// The preferred usage is to use the client as a singleton and not dispose after 
    /// each call. 
    /// <code language="cs" title="Not Preferred Example">
    ///     using (var client = new SomeAuthifyClient())
    ///     {
    ///         client.DoSomthing();
    ///     }
    /// </code>
    /// <code language="cs" title="Preferred Example with IoC">
    ///     var builder = new ContainerBuilder();
    ///     builder.RegisterType{SomeAuthifyClient}().As{ISomeAuthifyClient}().SingleInstance();
    /// </code>
    /// </example>
    /// <externalLink>
    ///     <linkText>Documentation</linkText>
    ///     <linkUri>
    ///         http://www.authify.com/developer/authify-client/api.html
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    public abstract class AuthifyClient : IDisposable
    {
        #region Variables.

        /// <summary>
        /// The Authify API version.
        /// </summary>
        private const string Version = "8.5";

        /// <summary>
        /// The <see cref="ILog"/> for <see cref="TenantClient"/>.
        /// </summary>
        private static readonly ILog Log = LogProvider.For<AuthifyClient>();

        /// <summary>
        /// The re-usable <see cref="HttpClient"/> instance.
        /// </summary>
        private readonly HttpClient httpClient;

         /// <summary>
        /// The token generator to use.
        /// </summary>
        private readonly ITokenizer tokenizer;

        /// <summary>
        /// The Authify configuration.
        /// </summary>
        private readonly IAuthifyConfiguration configuration;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthifyClient"/> class.
        /// </summary>
        /// <param name="tokenizer">The token provider to use</param>
        /// <param name="configuration">The siths configuration</param>
        protected AuthifyClient([NotNull] ITokenizer tokenizer, [NotNull] IAuthifyConfiguration configuration = null)
        {
            this.tokenizer = tokenizer;
            this.configuration = configuration;
            this.httpClient = new HttpClient
            {
                BaseAddress = this.configuration.ServerAddress
            };
        }

        #endregion

        #region IDisposable Members.

        /// <inheritdoc />
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the class.
        /// </summary>
        /// <param name="disposing">Whether or not to dispose</param>
        protected virtual void Dispose(bool disposing)
        {
            if (! disposing)
            {
                return;
            }
            if (this.httpClient != null)
            {
                this.httpClient.Dispose();
            }
        }

        #endregion

        #region Protected Members.

        /// <summary>
        /// Makes all the necessary requests to authenticate the current user to the server 
        /// using the chosen identity provider (IdP).
        /// </summary>
        /// <returns>The identity provider (IdP) authentication <c>Uri</c></returns>
        protected virtual async Task<Uri> RequireLogin()
        {
            var token = this.tokenizer.Generate();
            var request = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("api_key", this.configuration.Key),
                    new KeyValuePair<string, string>("uri", HttpUtils.RedirectPath(this.configuration.RedirectPath).ToString()),
                    new KeyValuePair<string, string>("secret_key", this.configuration.Secret),
                    new KeyValuePair<string, string>("authify_request_token", token),
                    new KeyValuePair<string, string>("idp", this.configuration.IdentityProvider),
                    new KeyValuePair<string, string>("luid", string.Empty),
                    new KeyValuePair<string, string>("loginparameters", string.Empty),
                    new KeyValuePair<string, string>("function", "require_login"),
                    new KeyValuePair<string, string>("reseller_id", this.configuration.ResellerId),
                    new KeyValuePair<string, string>("v", Version)
                };
            await this.httpClient.PostAsFormUrlEncodedAsync<string>("request/", request);
            return new Uri(this.httpClient.BaseAddress, string.Format("tokenidx.php?authify_request_token={0}", token));
        }

        /// <summary>
        /// This function returns the user profile query response.
        /// </summary>
        /// <typeparam name="T">The identity type</typeparam>
        /// <param name="token">The authify response token</param>
        /// <returns>A collection of {T} identity</returns>
        protected virtual async Task<IdentityCollection<T>> GetResponse<T>([NotNull] string token) where T : class
        {
            var request = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("api_key", this.configuration.Key),
                    new KeyValuePair<string, string>("uri", HttpUtils.RedirectPath(this.configuration.RedirectPath).ToString()),
                    new KeyValuePair<string, string>("secret_key", this.configuration.Secret),
                    new KeyValuePair<string, string>("authify_checksum", token),
                    new KeyValuePair<string, string>("protocol", "json"),
                    new KeyValuePair<string, string>("v", Version)
                };
            return await this.httpClient.PostAsFormUrlEncodedAsync<IdentityCollection<T>>("json/", request);
        }

        /// <summary>
        /// Terminates the current user's session, debugs the request and reset's the 
        /// internal debugger.
        /// </summary>
        /// <param name="token">The authify response token</param>
        /// <returns>
        /// True if the session was terminated successfully, false otherwise
        /// </returns>
        protected virtual async Task<string> RequireLogout([NotNull] string token)
        {
            var request = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("authify_checksum", token),
                    new KeyValuePair<string, string>("v", Version)
                };
            return await this.httpClient.PostAsFormUrlEncodedAsync<string>("out/", request);
        }

        #endregion
    }
}