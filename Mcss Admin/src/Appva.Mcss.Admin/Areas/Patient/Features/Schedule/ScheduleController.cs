// <copyright file="ScheduleController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Patient.Features
{
    #region Imports.

    using System;
    using System.Web.Mvc;
    using System.Web.UI;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Infrastructure;
    using Appva.Mcss.Admin.Infrastructure.Attributes;
    using Appva.Mcss.Admin.Models;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Mvc;
    using Appva.Mvc.Security;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RouteArea("patient"), RoutePrefix("schedule")]
    public sealed class ScheduleController : Controller
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IMediator"/>.
        /// </summary>
        private readonly IMediator mediator;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduleController"/> class.
        /// </summary>
        /// <param name="mediator">The <see cref="IMediator"/></param>
        public ScheduleController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        #endregion

        #region Routes.

        #region List Schedules.

        /// <summary>
        /// Returns all schedules (lists) for a patient.
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("list/{id:guid}")]
        [HttpGet, Dispatch]
        [PermissionsAttribute(Permissions.Schedule.ReadValue)]
        public ActionResult List(ListSchedule request)
        {
            return this.View();
        }

        #endregion

        #region Details.

        /// <summary>
        /// Returns the schedule details for a specific schedule.
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <param name="scheduleId">The schedule id</param>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("details/patient/{id:guid}/schedule/{scheduleId:guid}")]
        [HttpGet, Dispatch]
        [PermissionsAttribute(Permissions.Sequence.ReadValue)]
        public ActionResult Details(DetailsSchedule request)
        {
            return this.View();
        }

        #endregion

        #region Create.

        /// <summary>
        /// Returns the create schedule view.
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("create/patient/{id:guid}")]
        [HttpGet, Dispatch]
        [PermissionsAttribute(Permissions.Schedule.CreateValue)]
        public ActionResult Create(CreateSchedule request)
        {
            return this.View();
        }

        /// <summary>
        /// Saves a new schedule if valid.
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <param name="model">The schedule model</param>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("create/patient/{id:guid}")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("List", "Schedule")]
        [PermissionsAttribute(Permissions.Schedule.CreateValue)]
        public ActionResult Create(CreateScheduleForm request)
        {
            //// TODO: Must fix this somehow --> ValidateNonDuplicates(model);
            return this.View();
        }

        #endregion

        #region Inactivate.

        /// <summary>
        /// Inactivates a schedule.
        /// </summary>
        /// <param name="id">The schedule id</param>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("inactivate/schedule/{id:guid}")]
        [HttpGet, /*Validate, ValidateAntiForgeryToken,*/ Dispatch("List", "Schedule")]
        [PermissionsAttribute(Permissions.Schedule.InactivateValue)]
        public ActionResult Inactivate(InactivateSchedule request)
        {
            return this.View();
        }

        #endregion

        #region Sign View.

        /// <summary>
        /// Returns a view which contains information about signed/unsigned/handled
        /// tasks.
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <param name="scheduleSettingsId">Optional schedule settings id</param>
        /// <param name="year">Optional year</param>
        /// <param name="month">Optional month</param>
        /// <param name="startDate">Optional start date</param>
        /// <param name="endDate">Optional end date</param>
        /// <param name="filterByAnomalies">Optional anomalies query filter - defaults to false</param>
        /// <param name="page">Optional page number - defaults to 1</param>
        /// <param name="filterByNeedsBasis">Optional on need based query filter - defaults to false</param>
        /// <param name="order"></param>
        /// <returns></returns>
        [Route("Sign/{id:guid}")]
        [HttpGet, Dispatch]
        [PermissionsAttribute(Permissions.Schedule.EventListValue)]
        public ActionResult Sign(SignSchedule request)
        {
            return this.View();
        }

        #endregion

        #region Delete Task

        /// <summary>
        /// Delete a task by id.
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <param name="taskId">The task id</param>
        /// <returns><see cref="ActionResult"/></returns>
        [Authorize(Roles = "_AA")]
        [Route("DeleteTask/{id:guid}/task/{taskId:guid}")]
        [HttpGet, /*Validate, ValidateAntiForgeryToken,*/ Dispatch("List", "Schedule")]
        public ActionResult DeleteTask(DeleteTask request)
        {
            return this.View();
        }

        #endregion

        #region PrintPopUp View.

        /// <summary>
        /// Returns the print pop up.
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <param name="scheduleSettingsId">The schedule settings id</param>
        /// <param name="startDate">Optional start date</param>
        /// <param name="endDate">Optional end date</param>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("PrintPopUp/{id:guid}/schedule/{scheduleSettingsId:guid}")]
        [HttpGet, Dispatch]
        [PermissionsAttribute(Permissions.Schedule.PrintValue)]
        public ActionResult PrintPopUp(PrintModelSchedule request)
        {
            return this.View();
        }

        /// <summary>
        /// Print pop up post redirect.
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <param name="scheduleId">The schedule settings id</param>
        /// <param name="model">The print model</param>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("PrintPopUp/{id:guid}/schedule/{scheduleId:guid}")]
        [HttpPost, Validate, ValidateAntiForgeryToken]
        [PermissionsAttribute(Permissions.Schedule.PrintValue)]
        public ActionResult PrintPopUp(Guid id, Guid scheduleId, SchedulePrintPopOverViewModel model)
        {
            if (model.Template == SchedulePrintTemplate.Table)
            {
                return this.RedirectToAction("PrintTable",
                    new
                    {
                        Id = id,
                        ScheduleSettingsId = scheduleId,
                        StartDate = model.PrintStartDate,
                        EndDate = model.PrintEndDate,
                        OnNeedBasis = model.OnNeedBasis,
                        StandardSequences = model.StandardSequneces
                    });
            }
            if (model.Template == SchedulePrintTemplate.Schema)
            {
                return this.RedirectToAction("PrintSchema",
                    new
                    {
                        Id = id,
                        ScheduleSettingsId = scheduleId,
                        StartDate = model.PrintStartDate,
                        EndDate = model.PrintEndDate,
                        OnNeedBasis = model.OnNeedBasis,
                        StandardSequences = model.StandardSequneces
                    });
            }
            return View(model);
        }

        /// <summary>
        /// Returns the print schema.
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <param name="scheduleSettingsId">The schedule settings id</param>
        /// <param name="startDate">The start date</param>
        /// <param name="endDate">The end date</param>
        /// <param name="OnNeedBasis">Whether or not need based</param>
        /// <param name="StandardSequences">Whether or not standard sequences</param>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("PrintSchema/{id:guid}")]
        [HttpGet, Dispatch]
        [PermissionsAttribute(Permissions.Schedule.PrintValue)]
        public ActionResult PrintSchema(PrintSchemaSchedule request)
        {
            return this.View();
        }

        /// <summary>
        /// Returns the print table.
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <param name="scheduleSettingsId">The schedule settings id</param>
        /// <param name="startDate">The start date</param>
        /// <param name="endDate">The end date</param>
        /// <param name="OnNeedBasis">Whether or not need based</param>
        /// <param name="StandardSequences">Whether or not standard sequences</param>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("PrintTable/{id:guid}")]
        [HttpGet, Dispatch]
        [PermissionsAttribute(Permissions.Schedule.PrintValue)]
        public ActionResult PrintTable(PrintTableSchedule request)
        {
            return this.View();
        }

        #endregion

        #region Report View.

        /// <summary>
        /// Returns the schedule report view.
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <param name="sId">Optional schedule settings id</param>
        /// <param name="startDate">Optional start date, defaults to first day of the month</param>
        /// <param name="endDate">Optional end date, defaults to last instant of today</param>
        /// <param name="page">Optional page number, defaults to 1</param>
        /// <returns>A schedule report view</returns>
        [Route("ScheduleReport/{id:guid}")]
        [HttpGet, Dispatch]
        [PermissionsAttribute(Permissions.Schedule.ReportValue)]
        public ActionResult ScheduleReport(ReportSchedule request)
        {
            return this.View();
        }

        /// <summary>
        /// Post back redirect for schedule report.
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <param name="model">The schedule model</param>
        /// <returns>A redirect to schedule report</returns>
        
        [Route("ScheduleReport/{id:guid}")]
        [HttpPost, Validate, ValidateAntiForgeryToken]
        [PermissionsAttribute(Permissions.Schedule.ReportValue)]
        public ActionResult ScheduleReport(Guid id, ScheduleReportViewModel model)
        {
            return View(this.mediator.Send<ScheduleReportViewModel>(new ReportSchedule
            {
                Id = id,
                ScheduleSettingsId = model.Schedule,
                StartDate = model.StartDate,
                EndDate = model.EndDate
            }));
        }

        /// <summary>
        /// Returns the chart json data.
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <param name="sId">The schedule settings id</param>
        /// <param name="startDate">The start date</param>
        /// <param name="endDate">The end date</param>
        /// <returns>JSON data for charts</returns>
        [Route("chart/{id:guid}")]
        [HttpGet, Dispatch, OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public DispatchJsonResult Chart(RenderChart request)
        {
            return this.JsonGet();
        }

        /// <summary>
        /// Returns an Excel file.
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <param name="sId">The schedule settings id</param>
        /// <param name="startDate">The start date</param>
        /// <param name="endDate">The end date</param>
        /// <returns>A <see cref="FileContentResult"/></returns>
        [Route("Excel/{id:guid}")]
        [HttpGet, Dispatch]
        [PermissionsAttribute(Permissions.Schedule.ReportValue)]
        public DispatchExcelFileContentResult Excel(GenerateExcel request)
        {
            return this.ExcelFile();
        }

        #endregion

        #region Json

        /// <summary>
        /// Javascript helper for checking unique schedules.
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <param name="scheduleSetting">The schedule settings id</param>
        /// <returns>JSON representation of true or false</returns>
        [Route("VerifyUnique/{id:guid}/schedule")]
        [HttpPost, Dispatch, /*Validate, ValidateAntiForgeryToken,*/ OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public DispatchJsonResult VerifyUnique(VerifyIsUniqueSchedule request)
        {
            //// FIXME: This should have an anti forgery token, however its hidden so it must be added somehow to the javascript
            return this.JsonPost();
        }

        #endregion

        #endregion
    }    
}