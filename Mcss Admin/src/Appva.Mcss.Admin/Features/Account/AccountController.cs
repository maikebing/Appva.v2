// <copyright file="AccountController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Features.Account
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Security;
    using Appva.Mcss.Admin.Models;
    using Appva.Mvc;
    using Appva.Mvc.Security;

    #endregion

    /// <summary>
    /// Controller for account specifics, e.g. forgot password, reset password,
    /// register password, etc.
    /// </summary>
    [RoutePrefix("account")]
    public sealed class AccountController : Controller
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IMediator"/>.
        /// </summary>
        private readonly IMediator mediator;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="mediator">The <see cref="IMediator"/></param>
        public AccountController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        #endregion

        #region Routes.

        //// Reset password routes.

        #region Reset.

        /// <summary>
        /// Returns a reset password form.
        /// </summary>
        /// <returns>A <see cref="Reset"/></returns>
        [Route("reset-password")]
        [Permissions(Permissions.Token.Reset)]
        [HttpGet, Hydrate]
        public ActionResult Reset()
        {
            return this.View();
        }

        /// <summary>
        /// Updates the user account password and symmetric key if the request is
        /// validated; otherwise denied. 
        /// </summary>
        /// <param name="model">The reset password form model</param>
        /// <returns>
        /// A redirect to <see cref="ResetSuccess"/> if successful; otherwise the 
        /// invalid results
        /// </returns>
        [Route("reset-password")]
        [Permissions(Permissions.Token.Reset)]
        [HttpPost, Validate, ValidateAntiForgeryToken]
        public ActionResult Reset(Reset model)
        {
            var success = this.mediator.Send(model);
            if (! success)
            {
                ModelState.AddModelError(string.Empty, string.Empty);
                return this.View(model);
            }
            return this.RedirectToAction("ResetSuccess");
        }

        /// <summary>
        /// Returns a reset password success message.
        /// </summary>
        /// <returns>A success message</returns>
        [Route("reset-password/success")]
        [AllowAnonymous, HttpGet]
        public ActionResult ResetSuccess()
        {
            return this.View();
        }

        /// <summary>
        /// Returns a reset password token expiration message.
        /// </summary>
        /// <returns>A token expiration message</returns>
        [Route("reset-password/expired")]
        [AllowAnonymous, HttpGet]
        public ActionResult ResetExpired()
        {
            return this.View();
        }

        #endregion

        //// Register password routes.

        #region Register.

        /// <summary>
        /// Returns a reset password form.
        /// </summary>
        /// <returns>A <see cref="Register"/></returns>
        [Route("register")]
        [Permissions(Permissions.Token.Register)]
        [HttpGet, Hydrate]
        public ActionResult Register()
        {
            return this.View();
        }

        /// <summary>
        /// Saves the user account password and symmetric key if the request is
        /// validated; otherwise denied. 
        /// </summary>
        /// <param name="model">The register password form model</param>
        /// <returns>
        /// A redirect to <see cref="RegisterSuccess"/> if successful; otherwise the 
        /// invalid results
        /// </returns>
        [Route("register")]
        [Permissions(Permissions.Token.Register)]
        [HttpPost, Validate, ValidateAntiForgeryToken]
        public ActionResult Register(Register model)
        {
            var success = this.mediator.Send(model);
            if (!success)
            {
                ModelState.AddModelError(string.Empty, string.Empty);
                return this.View(model);
            }
            return this.RedirectToAction("RegisterSuccess");
        }

        /// <summary>
        /// Returns a register password success message.
        /// </summary>
        /// <returns>A success message</returns>
        [Route("register/success")]
        [AllowAnonymous, HttpGet]
        public ActionResult RegisterSuccess()
        {
            return this.View();
        }

        /// <summary>
        /// Returns a register password token expiration message.
        /// </summary>
        /// <returns>A token expiration message</returns>
        [Route("register/expired")]
        [AllowAnonymous, HttpGet]
        public ActionResult RegisterExpired()
        {
            return this.View();
        }

        #endregion

        //// Forgot password routes.

        #region Forgot Password.

        /// <summary>
        /// Returns the forgot password form.
        /// </summary>
        /// <returns>The <see cref="Forgot"/></returns>
        [Route("forgot-password")]
        [AllowAnonymous, HttpGet, Hydrate]
        public ActionResult Forgot()
        {
            return this.View(new Forgot());
        }

        /// <summary>
        /// Sends a new reset token by e-mail if the e-mail and personal identity
        /// number is validated; otherwise denied. 
        /// </summary>
        /// <param name="model">The forgot password form model</param>
        /// <returns>
        /// A redirect to <see cref="ForgotSuccess"/> if successful; otherwise the 
        /// invalid results
        /// </returns>
        [Route("forgot-password")]
        [AllowAnonymous, HttpPost, Validate, ValidateAntiForgeryToken]
        public ActionResult Forgot(Forgot model)
        {
            if (! this.mediator.Send(model))
            {
                ModelState.AddModelError(string.Empty, string.Empty);
                return this.View(model);
            }
            this.TempData.Add("EmailAddress", model.HiddenEmailAddress);
            return this.RedirectToAction("ForgotSuccess");
        }

        /// <summary>
        /// Returns the forgot password success message.
        /// </summary>
        /// <returns>The success message</returns>
        [Route("forgot-password/success")]
        [AllowAnonymous, HttpGet]
        public ActionResult ForgotSuccess()
        {
            if (string.IsNullOrWhiteSpace(this.TempData["EmailAddress"] as string))
            {
                return this.RedirectToAction("Forgot");
            }
            return this.View();
        }

        #endregion

        #endregion
    }
}