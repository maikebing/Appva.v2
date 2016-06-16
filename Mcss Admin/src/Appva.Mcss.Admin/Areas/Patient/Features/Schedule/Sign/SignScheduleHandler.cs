// <copyright file="SignScheduleHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Core.Extensions;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Auditing;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Commands;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Infrastructure;
    using Appva.Mcss.Web.Controllers;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Persistence;
    using NHibernate.Transform;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class SignScheduleHandler : RequestHandler<SignSchedule, TaskListViewModel>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IAuditService"/>.
        /// </summary>
        private readonly IAuditService auditing;

        /// <summary>
        /// The <see cref="IPatientService"/>.
        /// </summary>
        private readonly IPatientService patientService;

        /// <summary>
        /// The <see cref="IPatientTransformer"/>.
        /// </summary>
        private readonly IPatientTransformer transformer;

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistence;

        /// <summary>
        /// The <see cref="IIdentityService"/>.
        /// </summary>
        private readonly IIdentityService identityService;

        /// <summary>
        /// The <see cref="IAccountService"/>.
        /// </summary>
        private readonly IAccountService accountService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="SignScheduleHandler"/> class.
        /// </summary>
        public SignScheduleHandler(IIdentityService identityService, IAccountService accountService, IAuditService auditing, IPatientService patientService, IPatientTransformer transformer, IPersistenceContext persistence)
        {
            this.auditing = auditing;
            this.patientService = patientService;
            this.transformer = transformer;
            this.persistence = persistence;
            this.accountService = accountService;
            this.identityService = identityService;
        }

        #endregion

        #region RequestHandler Overrides.

        public override TaskListViewModel Handle(SignSchedule message)
        {
            var account = this.accountService.Find(this.identityService.PrincipalId);
            var list = TaskService.GetRoleScheduleSettingsList(account);
            var patient = this.patientService.Get(message.Id);
            var page = message.Page ?? 1;
            var filterByAnomalies = message.FilterByAnomalies ?? false;
            var filterByNeedsBasis = message.FilterByNeedsBasis ?? false;
            var startDate = message.StartDate ?? DateTime.Now.FirstOfMonth();
            var endDate = message.EndDate ?? DateTime.Now.LastOfMonth().LastInstantOfDay();
            if (message.Year.HasValue)
            {
                startDate = new DateTime(message.Year.Value, 1, 1);
                endDate = new DateTime(message.Year.Value, 12, 31);
            }
            if (message.Month.HasValue)
            {
                if (! message.Year.HasValue)
                {
                    message.Year = DateTime.Now.Year;
                }
                startDate = new DateTime(message.Year.Value, message.Month.Value, 1);
                endDate = new DateTime(message.Year.Value, message.Month.Value, 
                    DateTime.DaysInMonth(message.Year.Value, message.Month.Value)).LastInstantOfDay();
            }
            this.auditing.Read(
                patient,
                "läste signeringar mellan {0:yyyy-MM-dd} och {1:yyyy-MM-dd} för boende {2} (REF: {3}).", 
                startDate, 
                endDate, 
                patient.FullName, 
                patient.Id);
            var scheduleSettings = TaskService.GetRoleScheduleSettingsList(account);
            var schedules = this.persistence.QueryOver<Schedule>().Where(x => x.Patient.Id == message.Id)
                            .JoinQueryOver<ScheduleSettings>(x => x.ScheduleSettings)
                            .WhereRestrictionOn(x => x.Id).IsIn(scheduleSettings.Select(x => x.Id).ToArray())
                            .List();
            if (! message.ScheduleSettingsId.HasValue)
            {
                if (schedules.Count > 0)
                {
                    message.ScheduleSettingsId = schedules.First().ScheduleSettings.Id;
                }
                else
                {
                    return new TaskListViewModel
                    {
                        Patient = this.transformer.ToPatient(patient),
                        Schedules = new List<ScheduleSettings>()
                    };
                }
            }
            var scheduleSetting = this.persistence.Get<ScheduleSettings>(message.ScheduleSettingsId);
            return new TaskListViewModel
            {
                Patient = this.transformer.ToPatient(patient),
                Schedules = schedules.Select(x => x.ScheduleSettings).Distinct().ToList<ScheduleSettings>(),
                Schedule = scheduleSetting,
                Search = this.Search(new SearchTaskCommand
                {
                    PatientId = patient.Id,
                    ScheduleSettingsId = scheduleSetting.Id,
                    StartDate = startDate,
                    EndDate = endDate,
                    FilterByAnomalies = filterByAnomalies,
                    FilterByNeedsBasis = filterByNeedsBasis,
                    PageNumber = page,
                    PageSize = 30,
                    Order = message.Order
                }),
                FilterByAnomalies = filterByAnomalies,
                FilterByNeedsBasis = filterByNeedsBasis,
                StartDate = startDate,
                EndDate = endDate,
                Years = DateTimeUtils.GetYearSelectList(patient.CreatedAt.Year, startDate.Year == endDate.Year ? startDate.Year : 0),
                Months = startDate.Month == endDate.Month ? DateTimeUtils.GetMonthSelectList(startDate.Month) : DateTimeUtils.GetMonthSelectList(),
                Order = message.Order,
                Year = message.Year,
                Month = message.Month
            };
        }

        public SearchViewModel<Task> Search(SearchTaskCommand message)
        {
            var scheduleSetting = this.persistence.Get<ScheduleSettings>(message.ScheduleSettingsId);
            var query = this.persistence.QueryOver<Task>()
                .Where(x => x.Patient.Id == message.PatientId)
                  .And(x => x.Inventory.Increased == null)
                  .And(x => x.Inventory.RecalculatedLevel == null)
                  .And(x => x.Scheduled <= message.EndDate)
                .Fetch(x => x.StatusTaxon).Eager
                .TransformUsing(new DistinctRootEntityResultTransformer());
            switch (message.Order)
            {
                case OrderTasksBy.Day:
                    query = query.OrderBy(x => x.Scheduled).Desc;
                    break;
                case OrderTasksBy.Medecin:
                    query = query.OrderBy(x => x.Name).Asc.ThenBy(x => x.Scheduled).Desc;
                    break;
                case OrderTasksBy.Scheduled:
                    query = query.OrderBy(x => x.Scheduled).Desc;
                    break;
                case OrderTasksBy.SignedBy:
                    Account cb = null;
                    query = query.Left.JoinAlias(x => x.CompletedBy, () => cb).OrderBy(() => cb.LastName).Asc.ThenBy(x => x.Scheduled).Desc;
                    break;
                case OrderTasksBy.Status:
                    Taxon st = null;
                    query = query.Left.JoinAlias(x => x.StatusTaxon, () => st).OrderBy(() => st.Weight).Asc.ThenBy(x => x.Scheduled).Desc;
                    break;
                case OrderTasksBy.Time:
                    query = query.OrderBy(x => x.CompletedDate).Desc;
                    break;
            };
            if (message.StartDate.HasValue)
            {
                query.Where(x => x.Scheduled >= message.StartDate);
            }
            if (message.FilterByNeedsBasis)
            {
                query.Where(x => x.OnNeedBasis == true);
            }
            if (message.FilterByAnomalies)
            {
                query.Where(s => s.Status > 1 && s.Status < 5 || s.Delayed == true);
            }
            if (scheduleSetting.ScheduleType == ScheduleType.Calendar)
            {
                query.Where(x => x.CanRaiseAlert);
            }
            query.JoinQueryOver<Schedule>(x => x.Schedule)
                .JoinQueryOver<ScheduleSettings>(x => x.ScheduleSettings)
                .Where(x => x.Id == message.ScheduleSettingsId);
            var items = query.Skip((message.PageNumber - 1) * message.PageSize).Take(message.PageSize).Future().ToList();
            var totalCount = query.ToRowCountQuery().FutureValue<int>();
            return new SearchViewModel<Task>
            {
                Items          = items,
                PageNumber     = message.PageNumber,
                PageSize       = message.PageSize,
                TotalItemCount = totalCount.Value
            };
        }
        #endregion
    }
}