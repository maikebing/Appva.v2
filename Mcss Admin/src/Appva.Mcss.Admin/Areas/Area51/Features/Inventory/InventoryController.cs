// <copyright file="InventoryController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Area51.Features.Inventory
{
    #region Imports.

    using System.Web.Mvc;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Areas.Area51.Models;
    using Appva.Mcss.Admin.Infrastructure.Attributes;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Appva.Mvc;
    using Appva.Mvc.Security;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RouteArea("area51"), RoutePrefix("inventory")]
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
        /// <param name="mediator">The <see cref="IMediator"/></param>
        public InventoryController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        #endregion

        #region Routes.

        #region Index

        [Route("index")]
        [HttpGet]
        public ActionResult Index()
        {
            return this.View();
        }

        #endregion

        [Route("add-patients")]
        [HttpPost, Validate, ValidateAntiForgeryToken]
        [AlertSuccess("Patienter tillagda på alla inventories")]
        public ActionResult AddPatients()
        {
            this.mediator.Publish(new AddPatientsNotice());
            return this.RedirectToAction("Index");
        }

        #endregion
    }
}