﻿// <copyright file="TenaController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Patient.Features.Tena
{
    #region Imports.

    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Infrastructure;
    using Appva.Mcss.Admin.Infrastructure.Attributes;
    using Appva.Mcss.Admin.Models;
    using Appva.Mvc;
    using Appva.Mvc.Security;
    using Newtonsoft.Json;
    using Appva.Mcss.Admin.Models.Handlers;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RouteArea("patient"), RoutePrefix("{id:guid}/tena")]
    public sealed class TenaController : Controller
    {
        #region Variables.

        /// <summary>
        /// The tena service
        /// </summary>
        private ITenaService tenaService;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="TenaController"/> class.
        /// </summary>
        /// <param name="tenaService">The tena service.</param>
        public TenaController(ITenaService tenaService)
        {
            this.tenaService = tenaService;
        }
        
        #endregion

        #region Routes.

        #region List.

        /// <summary>
        /// Displays the observations list or the activation view.
        /// </summary>
        /// <param name="request">The <see cref="ListTena"/>.</param>
        /// <returns>A <see cref="ActionResult"/>.</returns>
        [Route("list")]
        [HttpGet, Dispatch]
        [PermissionsAttribute(Permissions.Tena.ReadValue)]
        public ActionResult List(ListTena request)
        {
            return this.View();
        }

        #endregion

        #region FindAsync

        /// <summary>
        /// Find as an asynchronous operation.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        [Route("findasync")]
        [HttpGet]
        [PermissionsAttribute(Permissions.Tena.CreateValue)]
        public async Task<ActionResult> FindAsync(FindTenaId request)
        {
            var response = await this.tenaService.GetResidentAsync(request.ExternalId);
            var model = JsonConvert.SerializeObject(response);

            return this.Content(model);
        }

        #endregion

        #region Activate.

        /// <summary>
        /// Activates TENA for a patient.
        /// </summary>
        /// <param name="request">The <see cref="ActivateTenaId"/>.</param>
        /// <returns>A <see cref="ListTena"/>.</returns>
        [Route("activate")]
        [HttpPost, Dispatch("list", "tena")]
        [PermissionsAttribute(Permissions.Tena.ActivateValue)]
        public ActionResult Activate(ActivateTenaId request)
        {
            return this.View();
        }

        #endregion

        #region Check

        /// <summary>
        /// Validating the starting date selected
        /// </summary>
        /// <param name="request">The <see cref="CheckDate"/>.</param>
        /// <returns>A <see cref="JsonResult"/>.</returns>
        [Route("check")]
        [HttpGet, Dispatch]
        [PermissionsAttribute(Permissions.Tena.ReadValue)]
        public DispatchJsonResult Check(CheckDate request)
        {
            return this.JsonGet();
        }

        #endregion

        #region Create

        /// <summary>
        /// GET Creates a Tena Observation Period
        /// </summary>
        /// <param name="request">The <see cref="CreateTenaObserverPeriod"/>.</param>
        /// <returns>A <see cref="ActionResult"/>.</returns>
        [Route("create")]
        [HttpGet, Dispatch]
        [PermissionsAttribute(Permissions.Tena.CreateValue)]
        public ActionResult Create(CreateTenaObserverPeriod request)
        {
            return this.View();
        }

        /// <summary>
        /// POST Creates a Tena Observation Period
        /// </summary>
        /// <param name="request">The <see cref="CreateTenaObserverPeriodModel"/>.</param>
        /// <returns>A <see cref="ActionResult"/>.</returns>
        [Route("create")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("list", "tena")]
        [PermissionsAttribute(Permissions.Tena.CreateValue)]
        public ActionResult Create(CreateTenaObserverPeriodModel request)
        {
            return this.View();
        }

        #endregion

        #region View

        /// <summary>
        /// View a list with Tena Observation Measurements
        /// </summary>
        /// <param name="request">The <see cref="ViewTenaMeasurements"/>.</param>
        /// <returns>A <see cref="ActionResult"/>.</returns>
        [Route("view")]
        [HttpGet, Dispatch]
        [PermissionsAttribute(Permissions.Tena.ReadValue)]
        public ActionResult View(ViewTenaMeasurements request)
        {
            return this.View();
        }

        #endregion

        #region UploadAsync

        /// <summary>
        /// Upload Tena Observation to designated API endpoint
        /// </summary>
        /// <param name="request">The <see cref="UploadTenaObserverPeriod"/>.</param>
        /// <returns>A <see cref="ActionResult"/>.</returns>
        [Route("upload")]
        [HttpGet]
        [PermissionsAttribute(Permissions.Tena.CreateValue)]
        public async Task<ActionResult> Upload(UploadTenaObserverPeriod request)
        {
            var handler = new UploadTenaObserverPeriodHandlerAsync(this.tenaService);
            //var response = await this.tenaService.PostManualEventAsync(request.PeriodId);
            var model = await handler.HandleAsync(request);

            return this.View(model);
        }
        #endregion

        #endregion
    }
}