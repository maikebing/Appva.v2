// <copyright file="TokenController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Area51.Features.Token
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Models;
    using Appva.Mvc;
    using Appva.Mvc.Security;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RouteArea("area51"), RoutePrefix("token")]
    [Permissions(Permissions.Area51.ReadValue)]
    public sealed class TokenController : Controller
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IMediator"/>.
        /// </summary>
        private readonly IMediator mediator;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenController"/> class.
        /// </summary>
        /// <param name="authentication">The <see cref="IMediator"/></param>
        public TokenController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        #endregion

        #region Routes.

        [Route("index")]
        [HttpGet]
        public ActionResult Index()
        {
            return this.View();
        }

        [Route("Install")]
        [HttpPost, Validate, ValidateAntiForgeryToken]
        [AlertSuccess("JWT token konfiguration är nu installerat!")]
        public ActionResult Install()
        {
            this.mediator.Publish(new InstallToken());
            return this.RedirectToAction("Index");
        }

        #endregion
    }
}