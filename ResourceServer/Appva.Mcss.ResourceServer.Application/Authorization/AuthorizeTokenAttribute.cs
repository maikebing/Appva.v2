// <copyright file="AuthorizeTokenAttribute.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.ResourceServer.Application.Authorization
{
    #region Imports.

    using System;
    using System.IdentityModel.Tokens;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Security.Cryptography;
    using System.Threading;
    using System.Web;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using Appva.Core.Configuration;
    using Appva.Core.Extensions;
    using Appva.Mcss.ResourceServer.Application.Configuration;
    using Common.Logging;
    using DotNetOpenAuth.Messaging;
    using DotNetOpenAuth.OAuth2;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class AuthorizeTokenAttribute : AuthorizeAttribute
    {
        #region Variabels.

        /// <summary>
        /// The logger for <see cref="AuthorizeTokenAttribute"/>.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger<AuthorizeTokenAttribute>();

        /// <summary>
        /// Responsible for providing the key to verify the token is intended for this resource.
        /// </summary>
        private readonly ResourceServerSigningKeyHandler decrypter;

        /// <summary>
        /// Responsible for providing the key to verify the token came from the authorization server.
        /// </summary>
        private readonly AuthorizationServerSigningKeyHandler signatureVerifier;

        /// <summary>
        /// The required scopes.
        /// </summary>
        private readonly string[] scopes;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizeTokenAttribute"/> class.
        /// </summary>
        /// <param name="scopes">The required scopes (OR) - one must match</param>
        public AuthorizeTokenAttribute(params string[] scopes)
        {
            this.decrypter = new ResourceServerSigningKeyHandler();
            this.signatureVerifier = new AuthorizationServerSigningKeyHandler();
            this.scopes = scopes;
        }

        #endregion

        #region Overrides.

        /// <inheritdoc />
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (ConfigurableApplicationContext.Get<ResourceServerConfiguration>().SkipTokenAndScopeAuthorization)
            {
                return;
            }
            var request = actionContext.Request;
            try
            {
                if (request.RequestUri.Scheme != Uri.UriSchemeHttps)
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden)
                    {
                        ReasonPhrase = "HTTPS Required"
                    };
                }
                var authHeader = request.Headers.FirstOrDefault(x => x.Key.Equals("Authorization"));
                if (authHeader.Value.IsNull() || !authHeader.Value.Any())
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                    Log.Warn("Missing Authorization header!");
                    return;
                }
                var authHeaderValue = authHeader.Value.FirstOrDefault(x => x.StartsWith("Bearer "));
                if (authHeaderValue.IsNull())
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                    Log.Warn("Missing Bearer token!");
                    return;
                }
                var resourceServer = new ResourceServer(new StandardAccessTokenAnalyzer(this.signatureVerifier.Provider, this.decrypter.Provider));
                var access = resourceServer.GetAccessToken(request);
                if (! access.Scope.Overlaps(this.scopes))
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                    Log.Error(x => x("Missing Scopes! Required: {0}, Granted: {1}", this.scopes, access.Scope));
                    return;
                }
                var principal = resourceServer.GetPrincipal(request);
                if (principal.IsNotNull())
                {
                    Thread.CurrentPrincipal = principal;
                    HttpContext.Current.User = principal;
                    actionContext.Response = null;
                }
                else
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                    return;
                }
            }
            catch (Exception ex)
            {
                Log.Info("Bad request or unauthorized", ex);
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                return;
            }
            base.OnAuthorization(actionContext);
        }

        #endregion
    }
}