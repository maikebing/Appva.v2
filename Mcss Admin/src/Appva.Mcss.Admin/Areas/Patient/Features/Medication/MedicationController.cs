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

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RouteArea("patient"), RoutePrefix("{id:guid}/medication")]
    [PermissionsAttribute(Permissions.Medication.ReadValue)]
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
            IScheduleService scheduleService)
        {
            this.medicationService  = medicationSevice;
            this.patientService     = patientService;
            this.patientTransformer = patientTransformer;
            this.scheduleService    = scheduleService;
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
        public async Task<ActionResult> List(Guid id)
        {
            try
            {
                var patient         = this.patientService.Get(id);
                var patientModel    = this.patientTransformer.ToPatient(patient);
                var list            = await this.medicationService.List(id);
                return this.View(new ListMedicationModel 
                { 
                    Patient = patientModel,
                    Medications = list
                });
            }
            catch (EhmBadRequestException e)
            {
                return this.View("EhmError");
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
                var patient      = this.patientService.Get(request.Id);
                var patientModel = this.patientTransformer.ToPatient(patient);
                var medication   = await this.medicationService.Find(request.OrdinationId, request.Id);
                return this.View(new DetailsMedicationModel
                {
                    Medication  = medication,
                    Patient     = patientModel
                });
            }
            catch (EhmBadRequestException e)
            {
                return this.View("EhmError");
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
                var medication = await this.medicationService.Find(request.OrdinationId, request.Id);
                var schedules  = this.scheduleService.List(request.Id);
                return this.View(new CreateMedicationModel
                {
                    //Schedules = schedules.Select(x => new SelectListItem { Text = x.ScheduleSettings.Name, Value = x.Id.ToString() }).ToList(),
                });
            }
            catch (EhmBadRequestException e)
            {
                return this.View("EhmError");
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
            var schedules = this.scheduleService.List(request.Id);
            return this.View(new SelectScheduleMedicationModel
            {
                Id = request.Id,
                OrdinationId = request.OrdinationId,
                Schedules = schedules.Select(x => new SelectListItem { Text = x.ScheduleSettings.Name, Value = x.Id.ToString() }).ToList(),
            });
        } 

        #endregion

        #endregion
    }
}