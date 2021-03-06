﻿// <copyright file="ReportScheduleHandler.cs" company="Appva AB">
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
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Models;
    using Appva.Mcss.Admin.Infrastructure;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class ReportScheduleHandler : RequestHandler<ReportSchedule, ScheduleReportViewModel>
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
        /// The <see cref="IAccountService"/>.
        /// </summary>
        private readonly IAccountService accountService;

        /// <summary>
        /// The <see cref="IIdentityService"/>.
        /// </summary>
        private readonly IIdentityService identityService;
        
        /// <summary>
        /// The <see cref="ITaskService"/>.
        /// </summary>
        private readonly ITaskService tasks;

        /// <summary>
        /// The <see cref="IReportService"/>.
        /// </summary>
        private readonly IReportService reportService;

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistence;

        /// <summary>
        /// The <see cref="IAuditService"/>.
        /// </summary>
        private readonly IAuditService auditing;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportScheduleHandler"/> class.
        /// </summary>
        public ReportScheduleHandler(
            IAuditService auditing, 
            IPatientService patientService,
            IAccountService accountService,
            IIdentityService identityService,
            ITaskService taskService,
            IReportService reportService,
            IPatientTransformer transformer,
            IPersistenceContext persistence)
        {
            this.auditing = auditing;
            this.patientService = patientService;
            this.transformer = transformer;
            this.accountService = accountService;
            this.identityService = identityService;
            this.tasks = taskService;
            this.reportService = reportService;
            this.persistence = persistence;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override ScheduleReportViewModel Handle(ReportSchedule message)
        {
            var page = message.Page ?? 1;
            var patient = this.patientService.Get(message.Id);
            var account = this.accountService.Find(this.identityService.PrincipalId);
            var scheduleSettings = TaskService.GetRoleScheduleSettingsList(account);
            var schedules = this.persistence.QueryOver<Schedule>()
                .Where(x => x.Patient.Id == patient.Id)
                .JoinQueryOver<ScheduleSettings>(x => x.ScheduleSettings)
                    .WhereRestrictionOn(x => x.Id).IsIn(scheduleSettings.Select(x => x.Id).ToArray())
                    .List();
            var startDate = message.StartDate.GetValueOrDefault(DateTime.Now.AddMonths(-1)).Date;
            var endDate = message.EndDate.GetValueOrDefault(DateTime.Now).LastInstantOfDay();
            this.auditing.Read(
                patient, 
                "läste rapport mellan {0:yyyy-MM-dd} och {1:yyyy-MM-dd} för boende {2} (REF: {3}).", 
                startDate, 
                endDate, 
                patient.FullName, 
                patient.Id);
            return new ScheduleReportViewModel
            {
                Patient = this.transformer.ToPatient(patient),
                Schedule = message.ScheduleSettingsId,
                Schedules = scheduleSettings,
                StartDate = startDate.Date,
                EndDate = endDate.Date,
                Report = this.reportService.GetReportData( new ChartDataFilter
                {
                    StartDate = startDate,
                    EndDate = endDate,
                    ScheduleSetting = message.ScheduleSettingsId,
                    Patient = patient.Id
                }),
                Tasks = this.tasks.List(new ListTaskModel
                {
                    StartDate = startDate,
                    EndDate = endDate,
                    PatientId = patient.Id,
                    ScheduleSettingId = message.ScheduleSettingsId
                }, page, 30)
            };
        }

        #endregion
    }
}