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
    using Appva.Mcss.Admin.Infrastructure;
    using Appva.Mcss.Admin.Infrastructure.Attributes;
    using Appva.Mcss.Admin.Models;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RouteArea("patient"), RoutePrefix("schedule")]
    public sealed class ScheduleController : Controller
    {
        #region Routes.

        #region List Schedules.

        /// <summary>
        /// Returns all schedules (lists) for a patient.
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("list/{id:guid}")]
        [HttpGet, Dispatch]
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
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("List", "Schedule")]
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
        //[Authorize(Roles = RoleUtils.AppvaAccount)]
        [Route("DeleteTask/{id:guid}/task/{taskId:guid}")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("List", "Schedule")]
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
        public ActionResult ScheduleReport(Guid id, ScheduleReportViewModel model)
        {
            return this.RedirectToAction("ScheduleReport", new ReportSchedule
            {
                Id = id,
                ScheduleSettingsId = model.Schedule,
                StartDate = model.StartDate,
                EndDate = model.EndDate
            });
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
        /*[HttpGet]
        public FileContentResult Excel(Guid id, Guid? sId, DateTime startDate, DateTime endDate)
        {
            var query = this.context.QueryOver<Task>()
                .Where(x => x.IsActive == true)
                .And(x => x.OnNeedBasis == false)
                .And(x => x.Scheduled >= startDate)
                .And(x => x.Scheduled <= endDate.LastInstantOfDay())
                .Fetch(x => x.Patient).Eager
                .TransformUsing(new DistinctRootEntityResultTransformer())
                .OrderBy(x => x.UpdatedAt).Desc;
            var patient = this.context.Get<Patient>(id);
            var account = Identity();
            this.logService.Info(string.Format("Användare {0} skapade excellista för boende {1} (REF: {2}).", account.UserName, patient.FullName, patient.Id), account, patient, LogType.Read);
            new ScheduleReportFilter
            {
                PatientId = id,
                ScheduleSettingsId = sId
            }.Filter(query);
            var tasks = query.List();
            var excel = new ExcelWriter<Task, ExcelTaskModel>
            {
                Mapping = x => new ExcelTaskModel
                {
                    Task = x.Name,
                    TaskCompletedOnDate = x.IsCompleted ? x.CompletedDate.Value.Date : x.Modified, //// FIXME: Its either completed or null.
                    TaskCompletedOnTime = (x.Delayed && x.CompletedBy.IsNull()) ? "Ej given" : string.Format("{0} {1:HH:mm}", "kl", x.CompletedDate),
                    TaskScheduledOnDate = x.Scheduled.Date,
                    TaskScheduledOnTime = string.Format("{0} {1:HH:mm}", "kl", x.Scheduled),
                    MinutesBefore = x.RangeInMinutesBefore,
                    MinutesAfter = x.RangeInMinutesAfter,
                    PatientFullName = x.Patient.FullName,
                    CompletedBy = x.CompletedBy.IsNotNull() ? x.CompletedBy.FullName : "",
                    TaskCompletionStatus = Status(x)
                },
                TemplatePath = Server.MapPath(@"\Templates\Template.xls")
            };
            var bytes = excel.Generate(tasks);
            return File(bytes, "application/vnd.ms-excel",
                string.Format("Rapport-{0}-{1}.xls", TenantIdentity().Name.Replace(" ", "-"),
                DateTime.Now.ToFileTimeUtc()));
        }*/

        /// <summary>
        /// Returns the task status as string.
        /// TODO: Refactor!
        /// </summary>
        /// <param name="task">The task</param>
        /// <returns>A string representation of the task status</returns>
        /*private string Status(Task task)
        {
            if (task.StatusTaxon != null)
            {
                if (task.Delayed && task.StatusTaxon.Weight < 2)
                {
                    return string.Format("{0} för sent", task.StatusTaxon.Name);
                }
                else
                {
                    return task.StatusTaxon.Name;
                }
            }
            if (task.Status.Equals(1))
            {
                if (task.Delayed)
                {
                    return "Given för sent";
                }
                return "OK";
            }
            else if (task.Status.Equals(2))
            {
                return "Delvis given";
            }
            else if (task.Status.Equals(3))
            {
                return "Ej given";
            }
            else if (task.Status.Equals(4))
            {
                return "Kan ej ta";
            }
            else if (task.Status.Equals(5))
            {
                return "Medskickad";
            }
            else if (task.Status.Equals(6))
            {
                return "Räknad mängd stämmer ej med saldo";
            }
            if (task.Status.Equals(0) || task.Delayed)
            {
                if (task.DelayHandled)
                {
                    return "Larm åtgärdat";
                }
                return "Ej given";
            }
            return string.Empty;
        }*/

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

        #region Private Helper Functions.

        /// <summary>
        /// Schedule validator for checking duplicates.
        /// </summary>
        /// <param name="model">The schedule model</param>
        /*private void ValidateNonDuplicates(ScheduleViewModel model)
        {
            var schedules = this.context.QueryOver<Schedule>()
                    .Where(x => x.Patient.Id == model.Id)
                    .And(x => x.ScheduleSettings.Id == model.ScheduleSetting)
                    .And(x => x.IsActive == true)
                    .List().Count;
            if (schedules > 0)
            {
                ModelState.AddModelError("ScheduleSetting", "Denna lista finns sedan tidigare inlagd.");
            }
        }*/

        #endregion
    }

        #endregion
}