﻿// <copyright file="AuthenticationController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Features.Authentication
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Appva.Mcss.Admin.Application.Security;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Features.Authentication.Forgot;
    using Appva.Mvc;
    using Appva.Tenant.Identity;
    using Appva.Core.Extensions;
    using System.Web;
    using System.IO;
    using System.Web.Routing;
    using Appva.Mcss.Admin.Application.Services.Menus;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Models;
    using Appva.Mcss.Admin.Infrastructure.Attributes;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RoutePrefix("auth")]
    public sealed class AuthenticationController : Controller
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ITenantService"/>.
        /// </summary>
        private readonly ITenantService tenants;

        /// <summary>
        /// The <see cref="ISithsAuthentication"/>.
        /// </summary>
        private readonly ISithsAuthentication siths;

        /// <summary>
        /// The <see cref="IFormsAuthentication"/>.
        /// </summary>
        private readonly IFormsAuthentication authentication;

        /// <summary>
        /// The <see cref="IMenuService"/>.
        /// </summary>
        private readonly IMenuService menus;

        /// <summary>
        /// The <see cref="IAccountService"/>.
        /// </summary>
        private readonly IAccountService accountService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationController"/> class.
        /// </summary>
        /// <param name="tenants">The <see cref="ITenantService"/></param>
        /// <param name="siths">The <see cref="ISithsAuthentication"/></param>
        /// <param name="authentication">The <see cref="IFormsAuthentication"/></param>
        public AuthenticationController(ITenantService tenants, ISithsAuthentication siths, IFormsAuthentication authentication, IMenuService menus,
            IAccountService accountService)
        {
            this.tenants = tenants;
            this.siths = siths;
            this.authentication = authentication;
            this.menus = menus;
            this.accountService = accountService;
        }

        #endregion

        #region Routes.

        #region Sign In.

        /// <summary>
        /// Returns the sign in form view.
        /// </summary>
        /// <returns>The sign in form</returns>
        [Route("sign-in")]
        [AllowAnonymous, HttpGet, Hydrate, Dispatch]
        public ActionResult SignIn(SignIn request)
        {
            return this.View();
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
            if (this.authentication.AuthenticateWithUserNameAndPassword(request.UserName, request.Password, out result))
            {
                this.authentication.SignIn(result.Identity);
                if (result.Identity.LastPasswordChangedDate.HasValue)
                {
                    this.RedirectToAction("ChangePassword", "Authenticate");
                }
                if (request.ReturnUrl.IsNotEmpty() && Url.IsLocalUrl(request.ReturnUrl))
                {
                    var item = this.RedirectToFirstBest(request.ReturnUrl);
                    //// If we find the selected then we can redirect otherwise
                    return this.RedirectToAction(item.Action, item.Controller, new
                    {
                        Area = item.Area
                    });
                }
                var defaultUrl = Url.Action("Index", "Dashboard", new
                {
                    Area = "Dashboard"
                });
                return this.Redirect(defaultUrl);
            }
            if (result.IsFailureDueToIdentityLockout)
            {
                return this.RedirectToAction("Lockout");
            }
            ModelState.AddModelError("", "");
            return this.View();
        }

        #endregion

        #region Sign Out.

        /// <summary>
        /// Returns the sign in form view.
        /// </summary>
        /// <returns>The sign in form</returns>
        [HttpGet, Route("sign-out")]
        public ActionResult SignOut()
        {
            this.authentication.SignOut();
            return this.RedirectToAction("SignIn", "Authentication");
        }

        #endregion

        #region External Login (SITHS).

        /// <summary>
        /// Redirects to the Siths Identity Provider (IdP) login page.
        /// </summary>
        /// <returns>A redirect to the external login</returns>
        [Route("sign-in/external/siths")]
        [AllowAnonymous, HttpGet]
        public async Task<ActionResult> SignInSiths()
        {
            var url = await this.siths.ExternalLoginUrlAsync();
            return this.Redirect(url.ToString());
        }

        /// <summary>
        /// The Siths Identity Provider (IdP) token response authentication.
        /// </summary>
        /// <param name="token">The response token</param>
        /// <returns>A redirect to the external login</returns>
        [Route("sign-in/external/siths/token/{token}")]
        [AllowAnonymous, HttpGet]
        public async Task<ActionResult> SignInSithsViaToken(string token)
        {
            var result = await this.siths.AuthenticateTokenAsync(token);
            if (result.IsAuthorized)
            {
                this.authentication.SignIn(result.Identity);
                var defaultUrl = Url.Action("Index", "Dashboard", new
                {
                    Area = "Dashboard"
                });
            }
            //// If everything fails; start over!
            return this.RedirectToAction("SignInSiths");
        }

        #endregion

        #region Lock Out.

        /// <summary>
        /// Returns the sign in form view.
        /// </summary>
        /// <returns>The sign in form</returns>
        [AllowAnonymous, HttpGet, Route("lock-out")]
        public ActionResult Lockout()
        {
            return this.View();
        }

        #endregion

        #region Forgot Password.

        /// <summary>
        /// Returns the sign in form view.
        /// </summary>
        /// <returns>The sign in form</returns>
        [AllowAnonymous, HttpGet, Hydrate, Route("forgot-password")]
        public ActionResult Forgot()
        {
            ITenantIdentity identity = null;
            this.tenants.TryIdentifyTenant(out identity);
            return this.View(new ForgotPassword
            {
                Tenant = identity.Name
            });
        }

        /// <summary>
        /// Signs in the user if successfully authenticated.
        /// </summary>
        /// <returns>A redirect to authorized return url or authorized menu</returns>
        [AllowAnonymous, HttpPost, Validate, ValidateAntiForgeryToken, Route("forgot-password"), AlertSuccess("Ett nytt lösenord har skickats")]
        public ActionResult Forgot(ForgotPassword model)
        {
            if (this.accountService.ForgotPassword(model.Email, model.PersonalIdentityNumber))
            {
                return this.RedirectToAction("SignIn", "Authentication");
            }
            ModelState.AddModelError(string.Empty, "Personnummer eller e-post är felaktigt.");
            return this.View(model);
        }

        #endregion

        #region Private Methods.

        //// Rename and move this to appropriate place.
        private IMenuItem RedirectToFirstBest(string redirectUrl)
        {
            
            var query = string.Empty;
            var uri = new Uri(new Uri(this.GetWebAppRoot()), redirectUrl);
            var url = uri.ToString();
            var index = url.IndexOf('?');
            if (index != -1)
            {
                url = url.Substring(0, index);
                query = url.Substring(index + 1);
            }
            var request = new HttpRequest(null, url, query);
            var response = new HttpResponse(new StringWriter());
            var httpContext = new HttpContext(request, new HttpResponse(new StringWriter()));

            var routeData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(httpContext));
            if (routeData != null)
            {
                if (routeData.Values.ContainsKey("MS_DirectRouteMatches"))
                {
                    routeData = ((IEnumerable<RouteData>)routeData.Values["MS_DirectRouteMatches"]).First();
                }
            }
            var values = routeData.Values;
            var controllerName = values["controller"] as string;
            var actionName = values["action"] as string;
            var areaName = routeData.DataTokens["area"] as string;
            var list = this.menus.Render("https://schema.appva.se/ui/menu", actionName, controllerName, areaName);
            return list.First();
        }

        private string GetWebAppRoot()
        {
            string host = (this.Request.Url.IsDefaultPort) ?
                this.Request.Url.Host :
                this.Request.Url.Authority;
            host = String.Format("{0}://{1}", this.Request.Url.Scheme, host);
            if (this.Request.ApplicationPath == "/")
                return host;
            else
                return host + this.Request.ApplicationPath;
        }

        #endregion

        #endregion
    }
}