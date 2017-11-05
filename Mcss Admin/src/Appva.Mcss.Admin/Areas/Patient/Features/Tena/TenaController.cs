// <copyright file="TenaController.cs" company="Appva AB">
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
    using Appva.Core.Extensions;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Infrastructure;
    using Appva.Mcss.Admin.Infrastructure.Attributes;
    using Appva.Mcss.Admin.Models;
    using Appva.Mvc;
    using Appva.Mvc.Security;
    using Newtonsoft.Json;
    using Appva.Mcss.Admin.Models.Handlers;
using Appva.Cqrs;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RouteArea("patient"), RoutePrefix("{id:guid}/tena")]
    [PermissionsAttribute(Permissions.Tena.ReadValue)]
    public sealed class TenaController : Controller
    {
        #region Variables.

        /// <summary>
        /// The tena service
        /// </summary>
        private ITenaService tenaService;

        /// <summary>
        /// The <see cref="IMediator"/>.
        /// </summary>
        private readonly IMediator mediator;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="TenaController"/> class.
        /// </summary>
        /// <param name="tenaService">The tena service.</param>
        public TenaController(IMediator mediator, ITenaService tenaService)
        {
            this.tenaService = tenaService;
            this.mediator = mediator;
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
        [HttpGet]
        [PermissionsAttribute(Permissions.Tena.ReadValue)]
        public ActionResult List(ListTena request)
        {
            var model = this.mediator.Send(request);
            if (model.IsNull())
            {
                return this.RedirectToAction("register", new { Id = request.Id});
            }
            if(model.Periods == null || model.Periods.Count == 0)
            {
                return this.View("_EmptyList", model);
            }
            return this.View(model);
        }

        #endregion

        #region Get resident

        /// <summary>
        /// Gets the resident from Tena Identifi
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;ActionResult&gt;.</returns>
        [Route("resident")]
        [HttpGet]
        [PermissionsAttribute(Permissions.Tena.CreateValue)]
        public async Task<ActionResult> GetResident(GetResident request)
        {
            var response = await this.tenaService.GetResidentAsync(request.ExternalId);
            var model = JsonConvert.SerializeObject(response);

            return this.Content(model);
        }

        #endregion

        #region Register patient.

        /// <summary>
        /// Activates TENA Identifi for a patient.
        /// </summary>
        /// <param name="request">The <see cref="RegisterTenaPatientId"/>.</param>
        /// <returns>A <see cref="ListTena"/>.</returns>
        [Route("register")]
        [HttpGet, Dispatch]
        [PermissionsAttribute(Permissions.Tena.RegisterValue)]
        public ActionResult Register(Identity<RegisterTenaPatientId> request)
        {
            return this.View();
        }

        /// <summary>
        /// Activates TENA Identifi for a patient.
        /// </summary>
        /// <param name="request">The <see cref="RegisterTenaPatientId"/>.</param>
        /// <returns>A <see cref="ListTena"/>.</returns>
        [Route("register")]
        [HttpPost, Dispatch("list", "tena")]
        [PermissionsAttribute(Permissions.Tena.RegisterValue)]
        public ActionResult Register(RegisterTenaPatientId request)
        {
            return this.View();
        }

        #endregion

        #region Validate date.

        /// <summary>
        /// Validating the starting date selected
        /// </summary>
        /// <param name="request">The <see cref="CheckDate"/>.</param>
        /// <returns>A <see cref="JsonResult"/>.</returns>
        [Route("validate-date")]
        [HttpGet, Dispatch]
        [PermissionsAttribute(Permissions.Tena.ReadValue)]
        public DispatchJsonResult ValidateDate(CheckDate request)
        {
            return this.JsonGet();
        }

        #endregion

        #region Create period

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

        #region Upload to Identifi

        /// <summary>
        /// Upload Tena Observation to designated API endpoint
        /// </summary>
        /// <param name="request">The <see cref="UploadTenaObserverPeriod"/>.</param>
        /// <returns>A <see cref="ActionResult"/>.</returns>
        [Route("upload")]
        [HttpGet]
        [PermissionsAttribute(Permissions.Tena.CreateValue)]
        public async Task<ActionResult> UploadToIdentifi(UploadTenaObserverPeriod request)
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