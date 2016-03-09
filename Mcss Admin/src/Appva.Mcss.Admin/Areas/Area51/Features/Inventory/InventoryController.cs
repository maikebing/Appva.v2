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
using Appva.Mcss.Admin.Areas.Area51.Models;
using Appva.Mcss.Admin.Infrastructure.Attributes;
using Appva.Mcss.Admin.Infrastructure.Models;
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

        #region List

        [Route("list")]
        [HttpGet, Dispatch(typeof(Parameterless<ListInventoriesModel>))]
        public ActionResult List()
        {
            return this.View();
        }

        #endregion

        #region Add

        /// <summary>
        /// Add inventory unit
        /// </summary>
        /// <returns></returns>
        [Route("add")]
        [HttpGet, Dispatch(typeof(Parameterless<AddInventoryModel>))]
        public ActionResult Add()
        {
            return this.View();
        }

        /// <summary>
        /// Post for add inventory unit
        /// </summary>
        /// <returns></returns>
        [Route("add")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("list","inventory")]
        public ActionResult Add(AddInventoryModel request)
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