// <copyright file="OAuthController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.OAuthServer.Controllers
{
    #region Imports.

    using DotNetOpenAuth.Messaging;
    using DotNetOpenAuth.OAuth2;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using Appva.Core.Extensions;
    using Appva.Persistence;
    using Appva.Mcss.AuthorizationServer.Domain.Entities;
    using Appva.Mvc;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    [RoutePrefix("v1")]
    public sealed class OAuthController : Controller
    {
        #region Private Variables.

        /// <summary>
        /// The <see cref="AuthorizationServer"/>
        /// </summary>
        private readonly AuthorizationServer authorizationServer;

        /// <summary>
        /// The <see cref="IPersistenceContext"/>
        /// </summary>
        private readonly IPersistenceContext persistenceContext;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="OAuthController"/> class.
        /// </summary>
        /// <param name="authorizationServerHost">An implementation of <see cref="IAuthorizationServerHost"/></param>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public OAuthController(IAuthorizationServerHost authorizationServerHost, IPersistenceContext persistenceContext)
        {
            this.authorizationServer = new AuthorizationServer(authorizationServerHost);
            this.persistenceContext = persistenceContext;
        }

        #endregion

        #region Routes.

        /// <summary>
        /// The OAuth 2.0 token endpoint.
        /// </summary>
        /// <returns>The response to the Client.</returns>
        [Route("token"), Validate]
        public ActionResult Token()
        {
            var request = this.authorizationServer.HandleTokenRequest(this.Request);
            return request.AsActionResultMvc5();
        }

        /// <summary>
        /// Prompts the user to authorize a client to access the user's private data.
        /// </summary>
        /// <returns>The browser HTML response that prompts the user to authorize the client.</returns>
        [Authorize, AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post),
         Header("x-frame-options", "SAMEORIGIN"), Route("authorize")]
        public ActionResult Authorize()
        {
            var request = this.authorizationServer.ReadAuthorizationRequest(Request);
            if (request == null)
            {
                throw new HttpException((int) HttpStatusCode.BadRequest, "Missing authorization request.");
            }
            var client = this.persistenceContext.QueryOver<Client>()
                .Where(x => x.Identifier == request.ClientIdentifier)
                .SingleOrDefault();
            if (client.IsNull())
            {
                throw new HttpException((int) HttpStatusCode.BadRequest, "No client.");
            }
            var model = new
            {
                ClientApp = client.Name,
                Scope = request.Scope,
                AuthorizationRequest = request,
            };

            return View(model);
        }

        /// <summary>
        /// Processes the user's response as to whether to authorize a Client to access his/her private data.
        /// </summary>
        /// <param name="isApproved">if set to <c>true</c>, the user has authorized the Client; <c>false</c> otherwise.</param>
        /// <returns>HTML response that redirects the browser to the Client.</returns>
        /*[Authorize, HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> AuthorizeResponse(bool isApproved)
        {
            var pendingRequest = await this.authorizationServer.ReadAuthorizationRequestAsync(Request, Response.ClientDisconnectedToken);
            if (pendingRequest == null) {
                throw new HttpException((int) HttpStatusCode.BadRequest, "Missing authorization request.");
            }

            IDirectedProtocolMessage response;
            if (isApproved) {
                // The authorization we file in our database lasts until the user explicitly revokes it.
                // You can cause the authorization to expire by setting the ExpirationDateUTC
                // property in the below created ClientAuthorization.
                var client = MvcApplication.DataContext.Clients.First(c => c.ClientIdentifier == pendingRequest.ClientIdentifier);
                client.ClientAuthorizations.Add(
                    new ClientAuthorization
                    {
                        Scope = OAuthUtilities.JoinScopes(pendingRequest.Scope),
                        User = MvcApplication.LoggedInUser,
                        CreatedOnUtc = DateTime.UtcNow,
                    });
                MvcApplication.DataContext.SubmitChanges(); // submit now so that this new row can be retrieved later in this same HTTP request

                // In this simple sample, the user either agrees to the entire scope requested by the client or none of it.  
                // But in a real app, you could grant a reduced scope of access to the client by passing a scope parameter to this method.
                response = this.authorizationServer.PrepareApproveAuthorizationRequest(pendingRequest, User.Identity.Name);
            } else {
                response = this.authorizationServer.PrepareRejectAuthorizationRequest(pendingRequest);
            }

            var preparedResponse = await this.authorizationServer.Channel.PrepareResponseAsync(response, Response.ClientDisconnectedToken);
            return preparedResponse.AsActionResult();
        }*/

        #endregion
    }
}