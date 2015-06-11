// <copyright file="InstallController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Areas.Admin.Controllers
{
    #region Imports.

    using System;
    using System.Web.Mvc;
    using Appva.Cqrs;
    using Appva.Mcss.AuthorizationServer.Code;
    using Appva.Mcss.AuthorizationServer.Infrastructure;
    using Appva.Mcss.AuthorizationServer.Models;
    using Appva.Mvc;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public class InstallController : AdminController
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="InstallController"/> class.
        /// </summary>
        /// <param name="mediator">The <see cref="IMediator"/></param>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public InstallController(IMediator mediator, IPersistenceContext persistenceContext)
            : base(mediator, persistenceContext)
        {
        }

        #endregion

        #region Routes.

        #region Install.

        /// <summary>
        /// Installs defaults.
        /// </summary>
        /// <returns><see cref="ActionResult"/></returns>
        [HttpGet, AllowAnonymous, Route("install"), Dispatch(typeof(NoParameter<Install>))]
        public ActionResult Install()
        {
            return View();
        }

        /// <summary>
        /// Installs defaults.
        /// </summary>
        /// <returns><see cref="ActionResult"/></returns>
        [HttpPost, AllowAnonymous, Validate, ValidateAntiForgeryToken, Dispatch("Install", "Install"), Route("install")]
        public ActionResult Install(Install request)
        {
            return View();
        }

        #endregion

        #region Upgrade.

        /// <summary>
        /// Upgrade defaults.
        /// </summary>
        /// <returns><see cref="ActionResult"/></returns>
        [HttpGet, AllowAnonymous, Route("upgrade"), Dispatch(typeof(NoParameter<Upgrade>))]
        public ActionResult Upgrade()
        {
            return View();
        }

        /// <summary>
        /// Installs defaults.
        /// </summary>
        /// <returns><see cref="ActionResult"/></returns>
        [HttpPost, AllowAnonymous, Validate, ValidateAntiForgeryToken, Dispatch("Upgrade", "Install"), Route("upgrade")]
        public ActionResult Upgrade(Upgrade request)
        {
            return View();
        }

        #endregion

        #endregion
    }
}