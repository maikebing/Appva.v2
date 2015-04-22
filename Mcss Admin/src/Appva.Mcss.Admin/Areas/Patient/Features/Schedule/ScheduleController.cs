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
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Web.Mappers;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Persistence;
    using Appva.Core.Extensions;
    using NHibernate.Transform;
    using Appva.Core.Utilities;
    using System.Web.UI;
    using Appva.Mcss.Admin.Infrastructure.Controllers;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Web.Controllers;
    using Appva.Mcss.Admin.Commands;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RouteArea("Patient"), RoutePrefix("Schedule")]
    public sealed class ScheduleController : IdentityController
    {
        #region Private Variables.

        private readonly IPersistenceContext context;
        private readonly ILogService logService;
        private readonly IScheduleService scheduleService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduleController"/> class.
        /// </summary>
        public ScheduleController(
            IMediator mediator, 
            IIdentityService identities,
            IAccountService accounts, 
            IScheduleService scheduleService, IPersistenceContext context, ILogService logService)
            : base(mediator, identities, accounts)
        {
            this.context = context;
            this.logService = logService;
            this.scheduleService = scheduleService;
        }

        #endregion

        #region Routes.

        #region List View.

        /// <summary>
        /// Returns all schedules (lists) for a patient.
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <returns><see cref="ActionResult"/></returns>
        [HttpGet]
        [Route("List/{id:guid}")]
        public ActionResult List(Guid id)
        {
            var account = Identity();
            var roles = account.Roles;
            var list = new List<ScheduleSettings>();
            foreach (var role in roles)
            {
                var ss = role.ScheduleSettings;
                foreach (var schedule in ss)
                {
                    if (schedule.ScheduleType == ScheduleType.Action)
                    {
                        list.Add(schedule);
                    }
                }
            }
            var patient = this.context.Get<Patient>(id);
            var query = this.context.QueryOver<Schedule>()
                .Where(s => s.Patient.Id == patient.Id && s.IsActive == true)
                .JoinQueryOver<ScheduleSettings>(s => s.ScheduleSettings)
                    .Where(s => s.ScheduleType == ScheduleType.Action);
            
            if (list.Count > 0)
            {
                query.WhereRestrictionOn(x => x.Id).IsIn(list.Select(x => x.Id).ToArray());
            }
            var schedules = query.List();
            this.logService.Info("Användare {0} läste signeringslistor för boende {1} (REF: {2}).".FormatWith(account.UserName, patient.FullName, patient.Id), account, patient, LogType.Read);
            return View(new ScheduleListViewModel
            {
                Patient = PatientMapper.ToPatientViewModel(this.context, patient),
                Schedules = schedules
            });
        }

        #endregion

        #region Details View.

        /// <summary>
        /// Returns the schedule details for a specific schedule.
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <param name="scheduleId">The schedule id</param>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("Details/patient/{id:guid}/schedule/{scheduleId:guid}")]
        public ActionResult Details(Guid id, Guid scheduleId)
        {
            var patient = this.context.Get<Patient>(id);
            var schedule = this.context.Get<Schedule>(scheduleId);
            var items = this.context.QueryOver<Sequence>()
                .Where(x => x.IsActive == true)
                .And(x => x.Schedule.Id == scheduleId)
                .Fetch(x => x.Inventory).Eager
                .TransformUsing(new DistinctRootEntityResultTransformer())
                .List();
            var account = Identity();
            this.logService.Info(string.Format("Användare {0} läste signeringslista {1} (REF: {2}) för boende {3} (REF: {4}).", account.UserName, schedule.ScheduleSettings.Name, schedule.Id, patient.FullName, patient.Id), account, patient, LogType.Read);
            return View(new ScheduleDetailsViewModel
            {
                Patient = PatientMapper.ToPatientViewModel(this.context, patient),
                Schedule = schedule,
                ScheduleItems = items
            });
        }

        #endregion

        #region Create View.

        /// <summary>
        /// Returns the create schedule view.
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("Create/patient/{id:guid}")]
        public ActionResult Create(Guid id)
        {
            var account = Identity();
            var roles = account.Roles;
            var list = new List<ScheduleSettings>();
            foreach (var role in roles)
            {
                var ss = role.ScheduleSettings;
                foreach (var schedule in ss)
                {
                    if (schedule.ScheduleType == ScheduleType.Action)
                    {
                        list.Add(schedule);
                    }
                }
            }
            var query = this.context.QueryOver<ScheduleSettings>()
                    .Where(s => s.ScheduleType == ScheduleType.Action)
                    .OrderBy(x => x.Name).Asc;
            if (list.Count > 0)
            {
                query.WhereRestrictionOn(x => x.Id).IsIn(list.Select(x => x.Id).ToArray());
            }
            var items = query.List()
                    .Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }).ToList();

            return View(new ScheduleViewModel
            {
                Id = id,
                Items = items
            });
        }

        /// <summary>
        /// Saves a new schedule if valid.
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <param name="model">The schedule model</param>
        /// <returns><see cref="ActionResult"/></returns>
        [HttpPost, ValidateAntiForgeryToken]
        [Route("Create/patient/{id:guid}")]
        public ActionResult Create(Guid id, ScheduleViewModel model)
        {
            var patient = this.context.Get<Patient>(id);
            ValidateNonDuplicates(model);
            if (ModelState.IsValid)
            {
                var settings = this.context.Get<ScheduleSettings>(model.ScheduleSetting);
                var schedule = new Schedule
                {
                    Patient = patient,
                    ScheduleSettings = settings
                };
                this.context.Save(schedule);
                var currentUser = Identity();
                this.logService.Info(string.Format("Användare {0} skapade lista {1} (REF: {2}).", currentUser.UserName, settings.Name, schedule.Id), currentUser, patient, LogType.Write);
                return this.RedirectToAction("List", new { Id = id });
            }
            return View(model);
        }

        #endregion

        #region Inactivate View.

        /// <summary>
        /// Inactivates a schedule.
        /// </summary>
        /// <param name="id">The schedule id</param>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("Inactivate/schedule/{id:guid}")]
        public ActionResult Inactivate(Guid id)
        {
            var schedule = this.context.Get<Schedule>(id);
            this.ExecuteCommand(new InactivateOrActivateCommand<Schedule>()
            {
                Id = id
            });
            var currentUser = Identity();
            this.logService.Info(string.Format("Användare {0} inaktiverade lista {1} (REF: {2}).", currentUser.UserName, schedule.ScheduleSettings.Name, schedule.Id), currentUser, schedule.Patient, LogType.Write);
            return this.RedirectToAction("List", new { Id = schedule.Patient.Id });
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
        public ActionResult Sign(
            Guid id,
            Guid? scheduleSettingsId,
            int? year,
            int? month,
            DateTime? startDate,
            DateTime? endDate,
            bool filterByAnomalies = false,
            int page = 1,
            bool filterByNeedsBasis = false,
            OrderTasksBy order = OrderTasksBy.Day
        )
        {
            var account = Identity();
            var roles = account.Roles;
            var list = new List<ScheduleSettings>();
            foreach (var role in roles)
            {
                var ss = role.ScheduleSettings;
                foreach (var schedule in ss)
                {
                    if (schedule.ScheduleType == ScheduleType.Action)
                    {
                        list.Add(schedule);
                    }
                }
            }
            var patient = this.context.Get<Patient>(id);
            var StartDate = startDate.HasValue ? startDate.Value : DateTime.Now.FirstOfMonth();
            var EndDate = endDate.HasValue ? endDate.Value : DateTime.Now.LastOfMonth().LastInstantOfDay();
            if (year.HasValue)
            {
                StartDate = new DateTime(year.Value, 1, 1);
                EndDate = new DateTime(year.Value, 12, 31);
            }
            if (month.HasValue)
            {
                if (!year.HasValue)
                {
                    year = DateTime.Now.Year;
                }
                StartDate = new DateTime(year.Value, month.Value, 1);
                EndDate = new DateTime(year.Value, month.Value, DateTime.DaysInMonth(year.Value, month.Value)).LastInstantOfDay();
            }
            this.logService.Info(string.Format("Användare {0} läste signeringar mellan {1:yyyy-MM-dd} och {2:yyyy-MM-dd} för boende {3} (REF: {4}).", account.UserName, StartDate, EndDate, patient.FullName, patient.Id), account, patient, LogType.Read);
            var scheduleSettings = new List<ScheduleSettings>();
            var query = this.context.QueryOver<Schedule>().Where(x => x.Patient.Id == id);
            if (list.Count > 0)
            {
                query.JoinQueryOver<ScheduleSettings>(x => x.ScheduleSettings)
                    .WhereRestrictionOn(x => x.Id).IsIn(list.Select(x => x.Id).ToArray());
            }
            var schedules = query.List();
            foreach (var schedule in schedules)
            {
                if (!scheduleSettings.Contains(schedule.ScheduleSettings))
                {
                    scheduleSettings.Add(schedule.ScheduleSettings);
                }
            }
            if (!scheduleSettingsId.HasValue)
            {
                if (schedules.Count > 0)
                {
                    scheduleSettingsId = schedules.First().ScheduleSettings.Id;
                }
                else
                {
                    return View(new TaskListViewModel
                    {
                        Patient = PatientMapper.ToPatientViewModel(this.context, patient),
                        Schedules = new List<ScheduleSettings>()
                    });
                }
            }
            return View(new TaskListViewModel
            {
                Patient = PatientMapper.ToPatientViewModel(this.context, patient),
                Schedules = scheduleSettings,
                Schedule = this.context.Get<ScheduleSettings>(scheduleSettingsId),
                Search = ExecuteCommand<SearchViewModel<Task>>(new SearchTaskCommand
                {
                    PatientId = patient.Id,
                    ScheduleSettingsId = scheduleSettingsId.Value,
                    StartDate = StartDate,
                    EndDate = EndDate,
                    FilterByAnomalies = filterByAnomalies,
                    FilterByNeedsBasis = filterByNeedsBasis,
                    PageNumber = page,
                    PageSize = 30,
                    Order = order
                }),
                FilterByAnomalies = filterByAnomalies,
                FilterByNeedsBasis = filterByNeedsBasis,
                StartDate = StartDate,
                EndDate = EndDate,
                Years = DateTimeUtils.GetYearSelectList(patient.CreatedAt.Year, StartDate.Year == EndDate.Year ? StartDate.Year : 0),
                Months = StartDate.Month == EndDate.Month ? DateTimeUtils.GetMonthSelectList(StartDate.Month) : DateTimeUtils.GetMonthSelectList(),
                Order = order,
                Year = year,
                Month = month
            });
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
        public ActionResult DeleteTask(Guid id, Guid taskId)
        {
            var task = this.context.Get<Task>(taskId);
            this.context.Delete(task);
            return RedirectToAction("Sign", new
            {
                id = id
            });
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
        [Route("PrintPopUp/{id:guid}/schedule/{scheduleSettingsId:guid}/{startDate?}/{endDate?}")]
        public ActionResult PrintPopUp(Guid id, Guid scheduleSettingsId, DateTime? startDate, DateTime? endDate)
        {
            var patient = this.context.Get<Patient>(id);
            var StartDate = startDate.HasValue && !startDate.Equals(patient.CreatedAt) ? startDate.Value.Date : DateTime.Now.FirstOfMonth();
            var EndDate = endDate.HasValue && !startDate.Equals(patient.CreatedAt) ? endDate.Value.Date : DateTime.Now.FirstOfMonth().AddDays(DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month) - 1);
            return View(new SchedulePrintPopOverViewModel
            {
                Id = patient.Id,
                ScheduleSettingsId = scheduleSettingsId,
                PrintStartDate = StartDate,
                PrintEndDate = EndDate
            });
        }

        /// <summary>
        /// Print pop up post redirect.
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <param name="scheduleId">The schedule settings id</param>
        /// <param name="model">The print model</param>
        /// <returns><see cref="ActionResult"/></returns>
        [HttpPost, ValidateAntiForgeryToken]
        [Route("PrintPopUp/{id:guid}/schedule/{scheduleId:guid}")]
        public ActionResult PrintPopUp(Guid id, Guid scheduleId, SchedulePrintPopOverViewModel model)
        {
            if (ModelState.IsValid)
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
                else if (model.Template == SchedulePrintTemplate.Schema)
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
        public ActionResult PrintSchema(
            Guid id,
            Guid? scheduleSettingsId,
            DateTime startDate,
            DateTime endDate,
            bool OnNeedBasis,
            bool StandardSequences
        )
        {
            var toDate = endDate.LastInstantOfDay();
            var patient = this.context.Get<Patient>(id);
            var query = this.context.QueryOver<Task>()
                .Where(x => x.Patient.Id == patient.Id)
                .And(x => x.Scheduled >= startDate && x.Scheduled <= toDate)
                .And(x => x.IsActive)
                .Fetch(x => x.StatusTaxon).Eager
                .TransformUsing(new DistinctRootEntityResultTransformer());
            if (scheduleSettingsId.HasValue)
            {
                query.JoinQueryOver<Schedule>(x => x.Schedule)
                .JoinQueryOver<ScheduleSettings>(x => x.ScheduleSettings)
                .Where(x => x.Id == scheduleSettingsId.Value);
            }
            if (!OnNeedBasis)
                query.AndNot(x => x.OnNeedBasis);
            if (!StandardSequences)
                query.And(x => x.OnNeedBasis);
            var printSchedule = this.scheduleService.PrintSchedule(query.List());
            var schedule = this.context.Get<ScheduleSettings>(scheduleSettingsId);
            var statusTaxons = schedule.StatusTaxons.Count == 0 ? this.context.QueryOver<Taxon>().Where(x => x.IsActive && x.IsRoot).JoinQueryOver<Taxonomy>(x => x.Taxonomy).Where(x => x.MachineName == "SST").List() : schedule.StatusTaxons.ToList();
            var account = Identity();
            this.logService.Info(string.Format("Användare {0} skapade utskrift av signeringslista {1} för boende {2} (REF: {3}).", account.UserName, schedule.Name, patient.FullName, patient.Id), account, patient, LogType.Read);
            return View("PrintSchema", new PrintViewModel
            {
                Patient = patient,
                PrintSchedule = printSchedule,
                From = startDate,
                To = toDate,
                Schedule = schedule,
                StatusTaxons = statusTaxons,
                EmptySchema = false
            });
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
        public ActionResult PrintTable(
            Guid id,
            Guid? scheduleSettingsId,
            DateTime startDate,
            DateTime endDate,
            bool OnNeedBasis,
            bool StandardSequences
        )
        {
            var toDate = endDate.LastInstantOfDay();
            var accountsMap = this.context.QueryOver<Account>().List().ToDictionary(x => x.Id, x => x);
            var patient = this.context.Get<Patient>(id);
            var query = this.context.QueryOver<Task>().Where(x => x.Patient.Id == patient.Id)
                .And(x => x.Scheduled >= startDate && x.Scheduled <= toDate)
                .And(x => x.IsActive)
                .Fetch(x => x.StatusTaxon).Eager
                .TransformUsing(new DistinctRootEntityResultTransformer());
            if (scheduleSettingsId.HasValue)
            {
                query.JoinQueryOver<Schedule>(x => x.Schedule)
                .JoinQueryOver<ScheduleSettings>(x => x.ScheduleSettings)
                .Where(x => x.Id == scheduleSettingsId.Value);
            }
            if (!OnNeedBasis)
                query.AndNot(x => x.OnNeedBasis);
            if (!StandardSequences)
                query.And(x => x.OnNeedBasis);
            var account = Identity();
            this.logService.Info(string.Format("Användare {0} skapade utskrift av signeringslista för boende {1} (REF: {2}).", account.UserName, patient.FullName, patient.Id), account, patient, LogType.Read);
            return View(new ScheduleTablePrintViewModel
            {
                Patient = patient,
                StartDate = startDate,
                EndDate = toDate,
                Tasks = query.List<Task>()
            });
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
        public ActionResult ScheduleReport(Guid id, Guid? sId, DateTime? startDate, DateTime? endDate, int? page = 1)
        {
            var patient = this.context.Get<Patient>(id);
            var scheduleSettings = new List<ScheduleSettings>();
            var schedules = this.context.QueryOver<Schedule>().Where(x => x.Patient.Id == id).List();
            foreach (var schedule in schedules)
            {
                if (!scheduleSettings.Contains(schedule.ScheduleSettings))
                {
                    scheduleSettings.Add(schedule.ScheduleSettings);
                }
            }
            startDate = (startDate.HasValue) ? startDate.Value : DateTimeUtilities.Now().AddDays(-DateTimeUtilities.Now().DaysInMonth());
            endDate = (endDate.HasValue) ? endDate.Value.LastInstantOfDay() : DateTimeUtilities.Now().LastInstantOfDay();
            var account = Identity();
            this.logService.Info(string.Format("Användare {0} läste rapport mellan {1:yyyy-MM-dd} och {2:yyyy-MM-dd} för boende {3} (REF: {4}).", account.UserName, startDate, endDate, patient.FullName, patient.Id), account, patient, LogType.Read);
            return View(new ScheduleReportViewModel
            {
                Patient = PatientMapper.ToPatientViewModel(this.context, patient),
                Schedule = sId,
                Schedules = scheduleSettings,
                StartDate = startDate.Value.Date,
                EndDate = endDate.Value.Date,
                Report = ExecuteCommand<ReportViewModel>(new CreateReportCommand<ScheduleReportFilter>
                {
                    Page = page,
                    StartDate = startDate.Value,
                    EndDate = endDate.Value,
                    Filter = new ScheduleReportFilter
                    {
                        PatientId = patient.Id,
                        ScheduleSettingsId = sId
                    }
                })
            });
        }

        /// <summary>
        /// Post back redirect for schedule report.
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <param name="model">The schedule model</param>
        /// <returns>A redirect to schedule report</returns>
        [HttpPost]
        [Route("ScheduleReport/{id:guid}")]
        public ActionResult ScheduleReport(Guid id, ScheduleReportViewModel model)
        {
            return ScheduleReport(id, model.Schedule, model.StartDate, model.EndDate);
        }

        /// <summary>
        /// Returns the chart json data.
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <param name="sId">The schedule settings id</param>
        /// <param name="startDate">The start date</param>
        /// <param name="endDate">The end date</param>
        /// <returns>JSON data for charts</returns>
        [HttpGet, OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        [Route("Chart/{id:guid}")]
        public ActionResult Chart(Guid id, Guid? sId, DateTime startDate, DateTime endDate)
        {
            return Json(ExecuteCommand<List<object[]>>(new CreateChartCommand<ScheduleReportFilter>
            {
                StartDate = startDate,
                EndDate = endDate,
                Filter = new ScheduleReportFilter
                {
                    PatientId = id,
                    ScheduleSettingsId = sId
                }
            }), JsonRequestBehavior.AllowGet);
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
        private string Status(Task task)
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
        }

        #endregion

        #region Json

        /// <summary>
        /// Javascript helper for checking unique schedules.
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <param name="scheduleSetting">The schedule settings id</param>
        /// <returns>JSON representation of true or false</returns>
        [HttpPost, OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        [Route("VerifyUnique/{id:guid}/schedule/{scheduleSetting:guid}")]
        public JsonResult VerifyUnique(Guid id, Guid scheduleSetting)
        {
            var schedules = this.context.QueryOver<Schedule>()
                    .Where(x => x.Patient.Id == id)
                    .And(x => x.ScheduleSettings.Id == scheduleSetting)
                    .And(x => x.IsActive == true)
                    .List().Count;
            return Json(schedules.Equals(0), JsonRequestBehavior.DenyGet);
        }

        #endregion

        #region Private Helper Functions.

        /// <summary>
        /// Schedule validator for checking duplicates.
        /// </summary>
        /// <param name="model">The schedule model</param>
        private void ValidateNonDuplicates(ScheduleViewModel model)
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
        }

        #endregion
    }

        #endregion
}