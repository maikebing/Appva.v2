// <copyright file="OrderController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.alvegard@appva.se">Richard Alvegard</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Patient.Features.Medication
{
    #region Imports.

    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Models;
    using Appva.Mvc.Security;
    using System.Threading.Tasks;
    using Appva.Ehm.Exceptions;
    using Appva.Mcss.Admin.Infrastructure;
    using Appva.Mcss.Admin.Domain.Entities;
    using System.Collections.Generic;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Infrastructure.Attributes;
using Appva.Mcss.Admin.Application.Security;
    using System.IdentityModel.Tokens;
    using Appva.Mcss.Admin.Areas.Patient.Models;
    using Appva.Cqrs;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RouteArea("patient"), RoutePrefix("{id:guid}/medication")]
    [PermissionsAttribute(Permissions.Medication.ReadValue)]
    [RequiredAuthenticationMethodAttribute(AuthenticationMethods.Smartcard, "EhmErrorSithsRequired")]
    public sealed class MedicationController : Controller
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IMedicationService"/>.
        /// </summary>
        private readonly IMedicationService medicationService;

        /// <summary>
        /// The <see cref="IPatientService"/>.
        /// </summary>
        private readonly IPatientService patientService;

        /// <summary>
        /// The <see cref="IPatientTransformer"/>
        /// </summary>
        private readonly IPatientTransformer patientTransformer;

        /// <summary>
        /// The <see cref="IScheduleService"/>
        /// </summary>
        private readonly IScheduleService scheduleService;

        /// <summary>
        /// The <see cref="IDelegationService"/>
        /// </summary>
        private readonly IDelegationService delegationService;

        /// <summary>
        /// The <see cref="ISequenceService"/>
        /// </summary>
        private readonly ISequenceService sequenceService;

        /// <summary>
        /// The <see cref="IRoleService"/>
        /// </summary>
        private readonly IRoleService roleService;

        /// <summary>
        /// The <see cref="ITaxonomyService"/>
        /// </summary>
        private readonly ITaxonomyService taxonService;

        /// <summary>
        /// The <see cref="IMediator"/>
        /// </summary>
        private readonly IMediator mediator;

        #endregion

        #region Const.

        private static IList<int> times = new List<int> {
            6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 1, 2, 3, 4, 5
        };

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="medicationSevice"></param>
        public MedicationController(
            IMedicationService medicationSevice, 
            IPatientService patientService, 
            IPatientTransformer patientTransformer,
            IScheduleService scheduleService,
            IDelegationService delegationService,
            ISequenceService sequenceService,
            IRoleService roleService,
            ITaxonomyService taxonService,
            IMediator mediator)
        {
            this.medicationService  = medicationSevice;
            this.patientService     = patientService;
            this.patientTransformer = patientTransformer;
            this.scheduleService    = scheduleService;
            this.delegationService  = delegationService;
            this.sequenceService    = sequenceService;
            this.roleService        = roleService;
            this.taxonService       = taxonService;
            this.mediator           = mediator;
        }

        #endregion

        #region Routes.

        #region List

        /// <summary>
        /// Lists all medications for a patient
        /// </summary>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("")]
        [HttpGet]
        [PermissionsAttribute(Permissions.Medication.ReadValue)]
        public async Task<ActionResult> List(ListMedicationRequest request)
        {
            try
            {
                var response = await this.mediator.SendAsync(request);
                return this.View(response);
            }
            catch (EhmException e)
            {
                return this.HandleError(e, request.Id);                
            }   
        }

        #endregion

        #region Details

        /// <summary>
        /// Details for a medication
        /// </summary>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("{ordinationId}")]
        [HttpGet]
        [PermissionsAttribute(Permissions.Medication.ReadValue)]
        public async Task<ActionResult> Details(DetailsMedicationRequest request)
        {
            try
            {
                var response = await this.mediator.SendAsync(request);
                return this.View(response);
            }
            catch (EhmException e)
            {
                return this.HandleError(e, request.Id);
            }
        }

        #endregion

        #region Create.

        /// <summary>
        /// Details for a medication
        /// </summary>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("create/{ordinationId}")]
        [HttpGet]
        [PermissionsAttribute(Permissions.Sequence.CreateValue)]
        public async Task<ActionResult> Create(CreateMedicationRequest request)
        {
            try
            {
                var response = await this.mediator.SendAsync(request);
                return this.View(response);
            }
            catch (EhmException e)
            {
                return this.HandleError(e, request.Id);
            }
        }

        /// <summary>
        /// Details for a medication
        /// </summary>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("create/{ordinationId}")]
        [HttpPost]
        [PermissionsAttribute(Permissions.Sequence.CreateValue)]
        public async Task<ActionResult> Create(CreateMedicationModel request)
        {
            try
            {
                var response = await this.mediator.SendAsync(request);
                return this.RedirectToAction("Details", response);  
            }
            catch (EhmException e)
            {
                return this.HandleError(e, request.Id);
            }
        }

        #endregion

        #region Select schedule 

        /// <summary>
        /// Details for a medication
        /// </summary>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("select-schedule/{ordinationId}")]
        [HttpGet]
        [PermissionsAttribute(Permissions.Sequence.CreateValue)]
        public ActionResult SelectSchedule(SelectScheduleMedicationRequest request)
        {
            var response = this.mediator.Send(request);
            return this.View(response);
        }

        /// <summary>
        /// Details for a medication
        /// </summary>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("select-schedule/{ordinationId}")]
        [HttpPost]
        [PermissionsAttribute(Permissions.Sequence.CreateValue)]
        public ActionResult SelectSchedule(SelectScheduleMedicationModel request)
        {
            return this.RedirectToAction("Create", new { Id = request.Id, OrdinationId = request.OrdinationId, Schedule = request.Schedule });
        }

        #endregion

        #region Overview

        /// <summary>
        /// Details for a medication
        /// TODO: This is still just a dummy to present the concept. Should be implemented later.
        /// </summary>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("~/patient/medication/overview")]
        [HttpGet]
        [PermissionsAttribute(Permissions.Medication.OverviewValue)]
        public PartialViewResult Overview()
        {
            var patient = this.patientService.Get(new Guid("f4f9ce58-28e3-41b3-ac2e-a0f700fac669"));
            var patientModel = this.patientTransformer.ToPatient(patient);
            return this.PartialView(new MedicationOverviewModel
            {
                Patients = new List<PatientViewModel>() { patientModel }
            });
        }

        #endregion

        #endregion

        #region Private helpers.

        /// <summary>
        /// Gets the times.
        /// </summary>
        /// <param name="times">The times.</param>
        /// <param name="selected">The selected.</param>
        /// <returns></returns>
        private IList<CheckBoxViewModel> GetTimes(IList<int> times, IList<int> selected)
        {
            return times.Select(x => new CheckBoxViewModel()
            {
                Id = x,
                Checked = selected.Contains(x)
            }).ToList();
        }

        /// <summary>
        /// Handles the error.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        private ActionResult HandleError(EhmException e, Guid id)
        {
            var patient         = this.patientService.Get(id);
            var patientModel    = this.patientTransformer.ToPatient(patient);

            var actions = new Dictionary<Type, Func<ActionResult>> {
                {typeof(EhmUnauthorizedException),      () => this.View("EhmUnauthorized", patientModel) },
                {typeof(EhmPatientNotFoundException),   () => this.View("EhmErrorPatientNotFound", patientModel) },
                {typeof(EhmBadRequestException),        () => this.View("EhmError", patientModel) }
            };

            var type = e.GetType();
            if (!actions.ContainsKey(type))
            {
                throw e;
            }

            var action = actions[type];
            return action();
        }

        #endregion
    }
}