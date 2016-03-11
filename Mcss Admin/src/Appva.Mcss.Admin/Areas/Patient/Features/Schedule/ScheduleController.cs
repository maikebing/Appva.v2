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
    /// The schedule http controller.
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
        /// <param name="request">The list patient request</param>
        /// <returns>A <see cref="ScheduleListViewModel"/></returns>
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
        /// <param name="request">The schedule details request</param>
        /// <returns>A <see cref="ScheduleDetailsViewModel"/></returns>
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
        /// <param name="request">The create schedule request</param>
        /// <returns>A <see cref="CreateScheduleForm"/></returns>
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
        /// <param name="request">The create schedule request</param>
        /// <returns>A redirect to <see cref="ListSchedule"/></returns>
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
        /// <param name="request">The inactivate schedule request</param>
        /// <returns>A redirect to <see cref="ListSchedule"/></returns>
        [Route("inactivate/schedule/{id:guid}")]
        [HttpGet, Dispatch("List", "Schedule")]
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
        /// <param name="request">The signed schedule request</param>
        /// <returns>A <see cref="TaskListViewModel"/></returns>
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
        /// <param name="request">The delete task request</param>
        /// <returns>A redirect to <see cref="ListSchedule"/></returns>
        [Authorize(Roles = "_AA")]
        [Route("DeleteTask/{id:guid}/task/{taskId:guid}")]
        [HttpGet, Dispatch("List", "Schedule")]
        public ActionResult DeleteTask(DeleteTask request)
        {
            return this.View();
        }

        #endregion

        #region PrintPopUp View.

        /// <summary>
        /// Returns the print pop up.
        /// </summary>
        /// <param name="request">The print schedule request</param>
        /// <returns>A <see cref="SchedulePrintPopOverViewModel"/></returns>
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
        /// <returns>A pdf file</returns>
        [Route("PrintPopUp/{id:guid}/schedule/{scheduleId:guid}")]
        [HttpPost, Validate, ValidateAntiForgeryToken]
        [PermissionsAttribute(Permissions.Schedule.PrintValue)]
        public ActionResult PrintPopUp(Guid id, Guid scheduleId, SchedulePrintPopOverViewModel model)
        {
            if (model.Template == SchedulePrintTemplate.Table)
            {
                return this.RedirectToAction(
                    "PrintTable",
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
                return this.RedirectToAction(
                    "PrintSchema",
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
            return this.View(model);
        }

        /// <summary>
        /// Returns the print schema.
        /// </summary>
        /// <param name="request">The print schedule request</param>
        /// <returns>A pdf file</returns>
        [Route("PrintSchema/{id:guid}")]
        [HttpGet, Dispatch]
        [PermissionsAttribute(Permissions.Schedule.PrintValue)]
        public PdfFileResult PrintSchema(PrintSchemaSchedule request)
        {
            return this.PdfFile();
        }

        /// <summary>
        /// Returns the print table.
        /// </summary>
        /// <param name="request">The print table request</param>
        /// <returns>A pdf file</returns>
        [Route("PrintTable/{id:guid}")]
        [HttpGet, Dispatch]
        [PermissionsAttribute(Permissions.Schedule.PrintValue)]
        public PdfFileResult PrintTable(PrintTableSchedule request)
        {
            return this.PdfFile();
        }

        #endregion

        #region Report View.

        /// <summary>
        /// Returns the schedule report view.
        /// </summary>
        /// <param name="request">The schedule report request</param>
        /// <returns>A <see cref="ScheduleReportViewModel"/></returns>
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
            return this.View(this.mediator.Send<ScheduleReportViewModel>(new ReportSchedule
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
        /// <param name="request">The render chart request</param>
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
        /// <param name="request">The generate excel request</param>
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
        /// <param name="request">The verify uniqueness of the schedule request</param>
        /// <returns>JSON representation of true or false</returns>
        [Route("VerifyUnique/{id:guid}/schedule")]
        [HttpPost, Dispatch, OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public DispatchJsonResult VerifyUnique(VerifyIsUniqueSchedule request)
        {
            //// FIXME: This should have an anti forgery token, however its hidden so it must be added somehow to the javascript
            return this.JsonPost();
        }

        #endregion

        #endregion
    }
}