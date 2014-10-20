// <copyright file="AuthorizeTokenAttribute.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.ResourceServer.Application.Authorization
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Security.Claims;
    using System.Security.Principal;
    using System.Threading;
    using System.Web;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using Appva.Core.Configuration;
    using Appva.Core.Extensions;
    using Appva.Mcss.ResourceServer.Application.Configuration;
    using Common.Logging;
    using DotNetOpenAuth.OAuth2;
    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class AuthorizeTokenAttribute : AuthorizeAttribute
    {
        #region Variabels.

        /// <summary>
        /// The user authenticate header.
        /// </summary>
        private const string UserAuthHeader = "X-Authenticated-User";

        /// <summary>
        /// The access token extra claims key.
        /// </summary>
        private const string AccessTokenClaimsKey = "claims";

        /// <summary>
        /// The logger for <see cref="AuthorizeTokenAttribute"/>.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger<AuthorizeTokenAttribute>();

        /// <summary>
        /// The <see cref="ResourceServerConfiguration"/>.
        /// </summary>
        private static readonly ResourceServerConfiguration Configuration;

        /// <summary>
        /// Responsible for providing the key to verify the token is intended for this resource.
        /// </summary>
        private static readonly ResourceServerSigningKeyHandler ResourceServerSigningKey;

        /// <summary>
        /// Responsible for providing the key to verify the token came from the authorization server.
        /// </summary>
        private static readonly AuthorizationServerSigningKeyHandler AuthorizationServerSigningKey;

        /// <summary>
        /// The required scopes.
        /// </summary>
        private readonly string[] scopes;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizeTokenAttribute"/> class.
        /// </summary>
        static AuthorizeTokenAttribute()
        {
            Configuration = ConfigurableApplicationContext.Get<ResourceServerConfiguration>();
            ResourceServerSigningKey = new ResourceServerSigningKeyHandler();
            AuthorizationServerSigningKey = new AuthorizationServerSigningKeyHandler();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizeTokenAttribute"/> class.
        /// </summary>
        /// <param name="scopes">The required scopes (OR) - one must match</param>
        public AuthorizeTokenAttribute(params string[] scopes)
        {
            this.scopes = scopes;
        }

        #endregion

        #region Overrides.

        /// <inheritdoc />
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (Configuration.IsNotNull() && Configuration.SkipTokenAndScopeAuthorization)
            {
                return;
            }
            var request = actionContext.Request;
            try
            {
                if (request.RequestUri.Scheme != Uri.UriSchemeHttps)
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                    return;
                }
                var authHeader = request.Headers.FirstOrDefault(x => x.Key.Equals("Authorization"));
                if (authHeader.Value.IsNull() || ! authHeader.Value.Any())
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
                var resourceServer = new ResourceServer(new StandardAccessTokenAnalyzer(AuthorizationServerSigningKey.Provider, ResourceServerSigningKey.Provider));
                var accessToken = resourceServer.GetAccessToken(request);
                if (! accessToken.Scope.Overlaps(this.scopes))
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                    Log.Error(x => x("Missing Scopes! Required: {0}, Granted: {1}", this.scopes, accessToken.Scope));
                    return;
                }
                var principal = resourceServer.GetPrincipal(request);
                if (principal.IsNotNull())
                {
                    this.MintPrincipal(actionContext, accessToken, principal);
                    actionContext.Response = null;
                    return;
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
        }

        #endregion

        #region Private Functions.

        /// <summary>
        /// Mints a <see cref="ClaimsPrincipal"/>.
        /// </summary>
        /// <param name="context">The <see cref="HttpActionContext"/></param>
        /// <param name="accessToken">The <see cref="AccessToken"/></param>
        /// <param name="principal">The <see cref="IPrincipal"/></param>
        private void MintPrincipal(HttpActionContext context, AccessToken accessToken, IPrincipal principal)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, principal.Identity.Name),
                new Claim(AppvaClaimTypes.Client, accessToken.ClientIdentifier)
            };
            if (accessToken.ExtraData.ContainsKey(AccessTokenClaimsKey))
            {
                var accessTokenClaims = JsonConvert.DeserializeObject<AccessTokenClaims>(accessToken.ExtraData[AccessTokenClaimsKey]);
                claims.AddRange(accessTokenClaims.Claims.Select(x => new Claim(x.Key, x.Value)));
            }
            if (context.Request.Headers.Contains(UserAuthHeader))
            {
                claims.Add(new Claim(ClaimTypes.NameIdentifier, context.Request.Headers.GetValues(UserAuthHeader).First()));
            }
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, principal.Identity.AuthenticationType));
            Thread.CurrentPrincipal = claimsPrincipal;
            context.RequestContext.Principal = claimsPrincipal;
            if (HttpContext.Current.IsNotNull())
            {
                HttpContext.Current.User = claimsPrincipal;
            }
        }

        #endregion

        #region Private Classes.

        /// <summary>
        /// The serialized access token claim.
        /// </summary>
        public class AccessTokenClaims
        {
            /// <summary>
            /// The extra claims.
            /// </summary>
            public IDictionary<string, string> Claims
            {
                get;
                set;
            }
        }

        #endregion
    }
}