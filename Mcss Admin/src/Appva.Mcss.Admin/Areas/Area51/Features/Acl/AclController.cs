// <copyright file="AclController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Controllers
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Infrastructure.Attributes;
    using Appva.Mcss.Admin.Models;
    using Appva.Mvc.Filters;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RouteArea("area51"), RoutePrefix("acl")]
    public sealed class AclController : Controller
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IMediator"/>.
        /// </summary>
        private readonly IMediator mediator;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationController"/> class.
        /// </summary>
        /// <param name="authentication">The <see cref="IMediator"/></param>
        public AclController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        #endregion

        #region Routes.

        #region Access Control.

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("index")]
        public ActionResult Index()
        {
            return this.View();
        }

        #endregion

        #region Install Access Control.

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("Install")]
        [HttpPost, Validate, ValidateAntiForgeryToken]
        [AlertSuccess("Roller och behörigheter är nu installerat!")]
        public ActionResult Install()
        {
            this.mediator.Publish(new InstallAcl());
            return this.RedirectToAction("Index");
        }

        #endregion

        #region Activate Access Control.

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("activate")]
        [HttpPost, Validate, ValidateAntiForgeryToken]
        [AlertSuccess("Roller och behörigheter är nu aktiverade!")]
        public ActionResult Activate()
        {
            this.mediator.Publish(new ActivateAcl());
            return this.RedirectToAction("Index");
        }

        #endregion

        #endregion
    }
}