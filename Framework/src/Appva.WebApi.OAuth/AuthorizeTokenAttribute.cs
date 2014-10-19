// <copyright file="AuthorizeTokenAttribute.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.WebApi.OAuth
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
    using DotNetOpenAuth.Messaging;
    using DotNetOpenAuth.OAuth2;
    using Appva.Core.Extensions;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class AuthorizeTokenAttribute : AuthorizeAttribute
    {
        #region Variabels.

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
        /// <param name="scopes">The required scopes</param>
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
            try
            {
                if (actionContext.Request.RequestUri.Scheme != Uri.UriSchemeHttps)
                {
                    actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden)
                    {
                        ReasonPhrase = "HTTPS Required"
                    };
                }
                base.OnAuthorization(actionContext);
                var authHeader = actionContext.Request.Headers.FirstOrDefault(x => x.Key.Equals("Authorization"));
                if (authHeader.Value.IsNull() || !authHeader.Value.Any())
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                    return;
                }
                var authHeaderValue = authHeader.Value.FirstOrDefault(x => x.StartsWith("Bearer "));
                if (authHeaderValue.IsNull())
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                    return;
                }

                // Have the DotNetOpenAuth resource server inspect the provided request using the configured keys
                // This checks both that the token is ok and that the token grants the scope required by
                // the required scope parameters to this attribute
                var resourceServer = new ResourceServer(new StandardAccessTokenAnalyzer(this.signatureVerifier.PublicKey, this.decrypter.PrivateKey));
                var principal = resourceServer.GetPrincipal(actionContext.Request, this.scopes);
                if (principal.IsNotNull())
                {
                    // Things look good.  Set principal for the resource to use in identifying the user so it can act accordingly
                    Thread.CurrentPrincipal = principal;
                    HttpContext.Current.User = principal;
                    // Dont understand why the call to GetPrincipal is setting actionContext.Response to be unauthorized
                    // even when the principal returned is non-null
                    // If I do this code the same way in a delegating handler, that doesn't happen
                    actionContext.Response = null;
                }
                else
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                }
            }
            catch (SecurityTokenValidationException)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
            catch (ProtocolFaultResponseException)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
            catch (Exception)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        #endregion
    }
}