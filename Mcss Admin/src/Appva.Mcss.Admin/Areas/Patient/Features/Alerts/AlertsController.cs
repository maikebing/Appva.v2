// <copyright file="AlertsController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Practitioner.Features.Alerts
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Web.Controllers;
    using Appva.Mcss.Web.Mappers;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Persistence;
    using NHibernate.Criterion;
    using NHibernate.Transform;
    using Appva.Core.Extensions;
    using Appva.Mcss.Admin.Infrastructure.Controllers;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Commands;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class AlertsController : IdentityController
    {
        #region Private Variables.

        private readonly IPersistenceContext context;
        private readonly ILogService logService;
        private readonly ITaskService taskService;
        private readonly IPatientService patientService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AlertsController"/> class.
        /// </summary>
        public AlertsController(
            IMediator mediator, 
            IIdentityService identities, 
            IAccountService accounts, 
            IPatientService patientService,
            ITaskService taskService,
            IPersistenceContext context, 
            ILogService logService)
            : base(mediator, identities, accounts)
        {
            this.context = context;
            this.logService = logService;
            this.patientService = patientService;
            this.taskService = taskService;
        }

        #endregion

        #region Routes.

        #region List View.

        /// <summary>
        /// Returns a list of alerts that are either handled or to
        /// be handled.
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <param name="year">Optional year filter</param>
        /// <param name="month">Optional month filter</param>
        /// <param name="startDate">Optional start date</param>
        /// <param name="endDate">Optional end date</param>
        /// <returns><see cref="ActionResult"/></returns>
        [HttpGet]
        public ActionResult List(Guid id, int? year, int? month, DateTime? startDate, DateTime? endDate)
        {
            var patient = this.patientService.Get(id);
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
                EndDate = new DateTime(year.Value, month.Value, DateTime.DaysInMonth(year.Value, month.Value));
            }
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
            var notHandledDelays = this.taskService.FindDelaysByPatient(patient, false, list);
            var handledDelays = this.taskService.FindDelaysByPatient(patient, true, list).Where(x => x.Scheduled >= StartDate && x.Scheduled <= EndDate).ToList();
            var user = this.Identity();
            this.logService.Info("Användare {0} läste larm mellan datum {1:yyyy-MM-dd} och {2:yyyy-MM-dd}".FormatWith(user.FullName, StartDate, EndDate), user, patient, LogType.Read);
            return View(new AlertListViewModel
            {
                Patient = PatientMapper.ToPatientViewModel(this.context, patient),
                TaskCurrentMap = TaskUtils.MapTimeOfDayAndSchedule(notHandledDelays),
                TaskEarlierMap = TaskUtils.MapTimeOfDayAndSchedule(handledDelays),
                Years = DateTimeUtils.GetYearSelectList(patient.CreatedAt.Year, StartDate.Year == EndDate.Year ? StartDate.Year : 0),
                Months = StartDate.Month == EndDate.Month ? DateTimeUtils.GetMonthSelectList(StartDate.Month) : DateTimeUtils.GetMonthSelectList(),
                StartDate = StartDate,
                EndDate = EndDate
            });
        }

        #endregion

        #region Handle Alert View.

        /// <summary>
        /// Sets a single alert status from unhandled to handled.
        /// </summary>
        /// <param name="id">The task id</param>
        /// <param name="startDate">Optional start date for redirect</param>
        /// <param name="endDate">Optional end date for redirect</param>
        /// <returns><see cref="ActionResult"/></returns>
        [HttpGet]
        public ActionResult HandleAlert(Guid id, DateTime? startDate, DateTime? endDate)
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
            var task = this.taskService.Get(id);
            this.taskService.HandleAnyAlert(account, task, list);
            return this.RedirectToAction("List", "Alert", new
            {
                id = task.Patient.Id,
                startDate = startDate,
                endDate = endDate
            });
        }

        #endregion

        #region Handle All Alert View

        /// <summary>
        /// Sets all alerts statuses from unhandled to handled. 
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <param name="startDate">Optional start date for redirect</param>
        /// <param name="endDate">Optional end date for redirect</param>
        /// <returns><see cref="ActionResult"/></returns>
        [HttpGet]
        public ActionResult HandleAllAlerts(Guid id, DateTime? startDate, DateTime? endDate)
        {
            var account = Identity();
            var patient = this.patientService.Get(id);
            this.taskService.HandleAnyAlert(account, patient);
            return this.RedirectToAction("List", "Alert", new
            {
                id = id,
                startDate = startDate,
                endDate = endDate
            });
        }

        #endregion

        #region Overview Gadget

        /// <summary>
        /// Partial view used on the overview to show all patient with alerts.
        /// </summary>
        /// <param name="status">Either "notsigned" or empty</param>
        /// <returns><see cref="PartialViewResult"/></returns>
        public PartialViewResult Overview(string status = "notsigned")
        {
            var taxon = FilterCache.Get(this.context);
            if (!FilterCache.HasCache())
            {
                taxon = FilterCache.GetOrSet(Identity(), this.context);
            }
            Taxon taxonAlias = null;
            var query = this.context.QueryOver<Patient>()
                .Where(x => x.IsActive)
                .And(x => !x.Deceased)
                .And(x => x.HasUnattendedTasks)
                .JoinAlias(x => x.Taxon, () => taxonAlias)
                    .Where(Restrictions.On<Taxon>(x => taxonAlias.Path)
                    .IsLike(taxon.Id.ToString(), MatchMode.Anywhere));
            var countAll = query.RowCount();
            IList<Patient> patients = null;
            if (!status.Equals("notsigned"))
            {
                patients = query.List();
            }
            Task task = null;
            query.Inner.JoinAlias(x => x.Tasks, () => task)
                .Where(() => task.IsActive)
                .And(() => task.Delayed)
                .And(() => task.DelayHandled == false)
                .And(() => task.IsCompleted == false)
                .TransformUsing(new DistinctRootEntityResultTransformer());
            if (patients == null)
            {
                patients = query.List();
            }
            var countNotSigned = query.Select(Projections.CountDistinct<Patient>(x => x.Id))
                .FutureValue<int>()
                .Value;
            return PartialView(new AlertOverviewViewModel
            {
                Patients = PatientMapper.ToListOfPatientViewModel(this.context, patients),
                CountAll = countAll,
                CountNotSigned = countNotSigned
            });
        }

        #endregion

        #endregion
    }
}