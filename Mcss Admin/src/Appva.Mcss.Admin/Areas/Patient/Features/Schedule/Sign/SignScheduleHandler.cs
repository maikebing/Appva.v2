﻿// <copyright file="SignScheduleHandler.cs" company="Appva AB">
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
    using System.Web.Mvc;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Infrastructure;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Persistence;
    using NHibernate.Transform;
    using Appva.Core.Extensions;
    using Appva.Mcss.Web.Controllers;
    using Appva.Core.Utilities;
    using Appva.Mcss.Admin.Commands;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class SignScheduleHandler : RequestHandler<SignSchedule, TaskListViewModel>
    {
        #region Variables.

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

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="SignScheduleHandler"/> class.
        /// </summary>
        public SignScheduleHandler(IPatientService patientService, IPatientTransformer transformer, IPersistenceContext persistence)
        {
            this.patientService = patientService;
            this.transformer = transformer;
            this.persistence = persistence;
        }

        #endregion

        #region RequestHandler Overrides.

        public override TaskListViewModel Handle(SignSchedule message)
        {
            ///var account = Identity();
            //var roles = account.Roles;
            var list = new List<ScheduleSettings>();
            /*foreach (var role in roles)
            {
                var ss = role.ScheduleSettings;
                foreach (var schedule in ss)
                {
                    if (schedule.ScheduleType == ScheduleType.Action)
                    {
                        list.Add(schedule);
                    }
                }
            }*/
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
            //this.logService.Info(string.Format("Användare {0} läste signeringar mellan {1:yyyy-MM-dd} och {2:yyyy-MM-dd} för boende {3} (REF: {4}).", account.UserName, StartDate, EndDate, patient.FullName, patient.Id), account, patient, LogType.Read);
            var scheduleSettings = new List<ScheduleSettings>();
            var query = this.persistence.QueryOver<Schedule>().Where(x => x.Patient.Id == message.Id);
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
                Schedules = scheduleSettings,
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

        public
            SearchViewModel<Task> Search(SearchTaskCommand message)
        {
            var scheduleSetting = this.persistence.Get<ScheduleSettings>(message.ScheduleSettingsId);
            var query = this.persistence.QueryOver<Task>()
                .Where(x => x.Patient.Id == message.PatientId)
                //.And(x => x.Inventory.Increased == null) // why is this not working, uncommented because of error?
                //.And(x => x.Inventory.RecalculatedLevel == null) // why is this not working, uncommented because of error?
                .And(x => x.UpdatedAt <= message.EndDate)
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
                    query = query.OrderBy(x => x.Status).Asc.ThenBy(x => x.Scheduled).Desc;
                    query = query.Left.JoinAlias(x => x.StatusTaxon, () => st).OrderBy(() => st.Weight).Asc.ThenBy(x => x.Scheduled).Desc;
                    break;
                case OrderTasksBy.Time:
                    query = query.OrderBy(x => x.UpdatedAt).Desc;
                    break;
            };

            if (message.StartDate.HasValue)
            {
                query.Where(x => x.UpdatedAt >= message.StartDate);
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
                Items = items,
                PageNumber = message.PageNumber,
                PageSize = message.PageSize,
                TotalItemCount = totalCount.Value
            };
        }
        #endregion
    }
}