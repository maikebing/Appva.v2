// <copyright file="Controller1.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Area51.Features.Notifications
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Areas.Area51.Features.Models;
    using Appva.Mcss.Admin.Models;
    using Appva.Mvc;
    using Appva.Mvc.Security;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RouteArea("area51"), RoutePrefix("notifications")]
    [Permissions(Permissions.Area51.ReadValue)]
    public sealed class NotificationsController : Controller
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IMediator"/>.
        /// </summary>
        private readonly IMediator mediator;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Controller1"/> class.
        /// </summary>
        public NotificationsController(IMediator mediator)
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

        /*[Route("AddVersion162")]
        [HttpPost, Validate, ValidateAntiForgeryToken]
        [AlertSuccess("Notis för uppdatering är nu installerad!")]
        public ActionResult AddVersion162()
        {
            this.mediator.Publish(new AddVersion162Notice());
            return this.RedirectToAction("Index");
        }

        [Route("AddPdf")]
        [HttpPost, Validate, ValidateAntiForgeryToken]
        [AlertSuccess("Notis för PDF uppdatering är nu installerad!")]
        public ActionResult AddPdf()
        {
            this.mediator.Publish(new AddPdfNotice());
            return this.RedirectToAction("Index");
        }*/

        [Route("AddChristmasGreeting2016")]
        [HttpPost, Validate, ValidateAntiForgeryToken]
        [AlertSuccess("Notis för julhälsning 2016 uppdatering är nu installerad!")]
        public ActionResult AddChristmasGreeting2016()
        {
            this.mediator.Publish(new AddChristmas2016Notice());
            return this.RedirectToAction("Index");
        }

        #endregion
    }
}