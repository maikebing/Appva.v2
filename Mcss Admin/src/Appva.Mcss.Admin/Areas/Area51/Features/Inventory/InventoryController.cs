// <copyright file="InventoryController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Area51.Features.Inventory
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
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
    public sealed class InventoryController : Controller
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IMediator"/>.
        /// </summary>
        private readonly IMediator mediator;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryController"/> class.
        /// </summary>
        public InventoryController()
        {
        }

        #endregion

        #region Routes.

        [Route("index")]
        [HttpGet]
        public ActionResult Index()
        {
            return this.View();
        }

        [Route("add-patients")]
        [HttpPost, Validate, ValidateAntiForgeryToken]
        [AlertSuccess("Patienter tillagda på alla inventories")]
        public ActionResult AddPatients()
        {
            this.mediator.Publish(new AddVersion162Notice());
            return this.RedirectToAction("Index");
        }

        #endregion
    }
}