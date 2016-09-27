// <copyright file="AuthenticationController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Features.Authentication
{
    #region Imports.

    using System;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Security;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Models;
    using Appva.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RoutePrefix("auth")]
    public sealed class AuthenticationController : Controller
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ISithsAuthentication"/>.
        /// </summary>
        private readonly ISithsAuthentication siths;

        /// <summary>
        /// The <see cref="IFormsAuthentication"/>.
        /// </summary>
        private readonly IFormsAuthentication authentication;

        /// <summary>
        /// The <see cref="IAccountService"/>.
        /// </summary>
        private readonly ISettingsService settings;

        /// <summary>
        /// The <see cref="IMediator"/>.
        /// </summary>
        private readonly IMediator mediator;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationController"/> class.
        /// </summary>
        /// <param name="siths">The <see cref="ISithsAuthentication"/></param>
        /// <param name="authentication">The <see cref="IFormsAuthentication"/></param>
        /// <param name="settings">The <see cref="ISettingsService"/></param>
        /// <param name="mediator">The <see cref="IMediator"/></param>
        public AuthenticationController(
            ISithsAuthentication siths,
            IFormsAuthentication authentication,
            ISettingsService settings,
            IMediator mediator)
        {
            this.siths = siths;
            this.authentication = authentication;
            this.settings = settings;
            this.mediator = mediator;
        }

        #endregion

        #region Routes.

        #region Sign in.

        /// <summary>
        /// Returns the sign in form view.
        /// </summary>
        /// <returns>The <see cref="SignInForm"/></returns>
        [Route("sign-in")]
        [AllowAnonymous, HttpGet, Hydrate]
        public ActionResult SignIn(SignIn request)
        {
            if (this.settings.IsSithsAuthorizationEnabled())
            {
                return this.RedirectToAction("SignInSiths");
            }
            return this.View(new SignInForm
            {
                ReturnUrl = request.ReturnUrl
            });
        }

        /// <summary>
        /// Signs in the user if successfully authenticated.
        /// </summary>
        /// <returns>A redirect to authorized return url or authorized menu</returns>
        [Route("sign-in")]
        [AllowAnonymous, HttpPost, Validate, ValidateAntiForgeryToken]
        public ActionResult SignIn(SignInForm request)
        {
            IAuthenticationResult result;
            if (! this.authentication.AuthenticateWithUserNameAndPassword(request.UserName, request.Password, out result))
            {
                if (result.IsFailureDueToIdentityLockout)
                {
                    return this.RedirectToAction("Lockout");
                }
                ModelState.AddModelError(string.Empty, string.Empty);
                return this.View();
            }
            this.authentication.SignIn(result.Identity);
            if (! result.Identity.LastPasswordChangedDate.HasValue)
            {
                return this.RedirectToAction("ChangePassword", "Accounts", new
                {
                    Area = "Practitioner"
                });
            }
            return this.RedirectToAction("Index", "Home", new
            {
                ReturnUrl = request.ReturnUrl
            });
        }

        #endregion

        #region Sign out.

        /// <summary>
        /// Returns the sign in form view.
        /// </summary>
        /// <returns>The sign in form</returns>
        [Route("sign-out")]
        [HttpGet]
        public ActionResult SignOut()
        {
            this.authentication.SignOut();
            return this.View();
        }

        #endregion

        #region External login (siths).

        /// <summary>
        /// Redirects to the Siths Identity Provider (IdP) login page.
        /// </summary>
        /// <returns>A redirect to the external login</returns>
        [Route("sign-in/external/siths")]
        [AllowAnonymous, HttpGet]
        public async Task<ActionResult> SignInSiths()
        {
            var urlHelper   = new UrlHelper(this.Request.RequestContext);
            var callback    = urlHelper.Action("SignInSithsViaToken", "Authentication", new RouteValueDictionary { { "Area", string.Empty } }, this.Request.Url.Scheme);
            var response    = await this.siths.ExternalLoginUrlAsync(new Uri(callback));
            return this.Redirect(response.RedirectUri.ToString());
        }

        /// <summary>
        /// The Siths Identity Provider (IdP) token response authentication.
        /// </summary>
        /// <param name="grandidsession">The response session ID.</param>
        /// <returns>A redirect to the external login</returns>
        [Route("sign-in/external/siths/token")]
        [AllowAnonymous, HttpGet]
        public async Task<ActionResult> SignInSithsViaToken(string grandidsession)
        {
            var result = await this.siths.AuthenticateTokenAsync(grandidsession);
            if (! result.IsAuthorized)
            {
                return this.RedirectToAction("SignInSithsFailed");
            }
            this.authentication.SignIn(result.Identity);
            await this.siths.LogoutAsync(grandidsession);
            return this.RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Returns a siths-auth failed message
        /// </summary>
        /// <returns>Sign-in failed message</returns>
        [Route("sign-in/external/siths/failed")]
        [AllowAnonymous, HttpGet]
        public ActionResult SignInSithsFailed()
        {
            return this.View();
        }

        #endregion

        #region Lock out.

        /// <summary>
        /// Returns the sign in form view.
        /// </summary>
        /// <returns>The sign in form</returns>
        [Route("lock-out")]
        [AllowAnonymous, HttpGet]
        public ActionResult Lockout()
        {
            return this.View();
        }

        #endregion

        #region Redirect for deprecated invalid urls.

        /// <summary>
        /// Redirects to the new authentication URL if the deprecated URL is used.
        /// </summary>
        /// <returns>A redirect to correct sign in</returns>
        [Route("~/Authenticate/LogIn")]
        [AllowAnonymous, HttpGet]
        public ActionResult RedirectForOldAuthenticationLoginUrl(SignIn request)
        {
            return this.RedirectToAction("SignIn", request);
        }

        #endregion

        #endregion
    }
}