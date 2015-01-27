// <copyright file="AuthController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Areas.Admin.Controllers
{
    #region Imports.

    using System.Linq;
    using System.Web.Mvc;
    using Appva.Cqrs;
    using Appva.Mcss.AuthorizationServer.Code;
    using Appva.Mcss.AuthorizationServer.Models;
    using Appva.Mvc.Filters;
    using Appva.Persistence;
    using Appva.Mcss.AuthorizationServer.Application;
    using Appva.Core.Extensions;
    using Entities = Appva.Mcss.AuthorizationServer.Domain.Entities;
    using Appva.Mcss.AuthorizationServer.Domain.Services;
    using Appva.Mvc.Security;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    [ContentSecurityPolicy, StrictTransportSecurity, XssProtection, ContentTypeOptions]
    public class AuthController : AdminController
    {
        #region Variables.

        /// <summary>
        /// 
        /// </summary>
        private readonly IAuthenticationService authenticationService;

        /// <summary>
        /// 
        /// </summary>
        private readonly IUserService userService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="mediator">The <see cref="IAuthenticationService"/></param>
        /// <param name="mediator">The <see cref="IUserService"/></param>
        /// <param name="mediator">The <see cref="IMediator"/></param>
        /// <param name="mediator">The <see cref="IPersistenceContext"/></param>
        public AuthController(IAuthenticationService authenticationService, IUserService userService, IMediator mediator, IPersistenceContext persistenceContext)
            : base(mediator, persistenceContext)
        {
            this.authenticationService = authenticationService;
            this.userService = userService;
        }

        #endregion

        #region Routes.

        #region Login.

        /// <summary>
        /// 
        /// </summary>
        /// <param name="redirect"></param>
        /// <returns></returns>
        [HttpGet, AllowAnonymous, Route("auth/login")]
        public ActionResult Login(string returnUrl)
        {
            return View(new LoginRequest()
            {
                ReturnUrl = returnUrl
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, AllowAnonymous, Validate, ValidateAntiForgeryToken, Route("auth/login")]
        public ActionResult Login(LoginRequest request)
        {
            Entities.User user;
            if (this.userService.AuthenticateWithPersonalIdentityNumber(request.UserName, request.Password, "oauth", out user))
            {
                if (user.Roles != null && user.Roles.Where(x => x.Key.Equals("admin_god")).Any())
                {
                    this.authenticationService.SignIn(user, true);
                    if (request.ReturnUrl.IsNotEmpty() && Url.IsLocalUrl(request.ReturnUrl))
                    {
                        return Redirect(request.ReturnUrl);
                    }
                    return RedirectToAction("Dashboard", "Dashboard");
                }
            }
            ModelState.AddModelError(string.Empty, "Invalid Username or Password");
            return View(request);
        }

        #endregion

        #endregion
    }
}