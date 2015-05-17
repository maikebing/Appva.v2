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
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Appva.Mcss.Admin.Application.Security;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Features.Authentication.Forgot;
    using Appva.Mcss.Admin.Features.Shared.Models;
    using Appva.Mvc;
    using Appva.Tenant.Identity;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RoutePrefix("auth")]
    public sealed class AuthenticationController : Controller
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ITenantService"/> implementation.
        /// </summary>
        private readonly ITenantService tenants;

        /// <summary>
        /// The <see cref="IFormsAuthentication"/> implementation.
        /// </summary>
        private readonly IFormsAuthentication authentication;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationController"/> class.
        /// </summary>
        /// <param name="tenants">The <see cref="ITenantService"/> implementation</param>
        /// <param name="authentication">The <see cref="IFormsAuthentication"/> implementation</param>
        public AuthenticationController(ITenantService tenants, IFormsAuthentication authentication)
        {
            this.tenants = tenants;
            this.authentication = authentication;
        }

        #endregion

        #region Routes.

        #region Sign In.

        /// <summary>
        /// Returns the sign in form view.
        /// </summary>
        /// <returns>The sign in form</returns>
        [AllowAnonymous, HttpGet, Hydrate, Route("sign-in")]
        public ActionResult SignIn()
        {
            ITenantIdentity identity = null;
            this.tenants.TryIdentifyTenant(out identity);
            return this.View(new SignIn
                {
                    Tenant = identity.Name
                });
        }

        /// <summary>
        /// Signs in the user if successfully authenticated.
        /// </summary>
        /// <returns>A redirect to authorized return url or authorized menu</returns>
        [AllowAnonymous, HttpPost, Validate, ValidateAntiForgeryToken, Route("sign-in")]
        public ActionResult SignIn(SignIn model)
        {
            IAuthenticationResult result;
            if (this.authentication.AuthenticateWithUserNameAndPassword(model.UserName, model.Password, out result))
            {
                this.authentication.SignIn(result.Identity);
                return this.RedirectToAction("Index", "Home");
            }
            if (result.IsFailureDueToIdentityLockout)
            {
                return this.RedirectToAction("Lockout");
            }
            ModelState.AddModelError("", "");
            //return this.RedirectToAction("SignIn");
            return this.View();
        }

        #endregion

        #region Sign Out.

        /// <summary>
        /// Returns the sign in form view.
        /// </summary>
        /// <returns>The sign in form</returns>
        [HttpPost, Validate, Route("sign-out")]
        public ActionResult SignOut()
        {
            this.authentication.SignOut();
            return this.RedirectToAction("SignIn", "Authentication");
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
            if (false)
            {
                return this.RedirectToAction("SignIn", "Authentication");
            }
            ModelState.AddModelError(string.Empty, "Personnummer eller e-post är felaktigt.");
            return this.View(model);
        }

        #endregion

        #endregion
    }
}