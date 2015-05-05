// <copyright file="ReportScheduleHandler.cs" company="Appva AB">
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
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Infrastructure;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Persistence;
    using NHibernate.Transform;
    using Appva.Core.Extensions;

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
        /// The <see cref="IScheduleService"/>.
        /// </summary>
        private readonly IScheduleService scheduleService;

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistence;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportScheduleHandler"/> class.
        /// </summary>
        public ReportScheduleHandler(IPatientService patientService, IScheduleService scheduleService, 
            IPatientTransformer transformer,
            IPersistenceContext persistence)
        {
            this.patientService = patientService;
            this.transformer = transformer;
            this.scheduleService = scheduleService;
            this.persistence = persistence;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override ScheduleReportViewModel Handle(ReportSchedule message)
        {
            var page = message.Page ?? 1;
            var patient = this.patientService.Get(message.Id);
            var scheduleSettings = new List<ScheduleSettings>();
            var schedules = this.persistence.QueryOver<Schedule>().Where(x => x.Patient.Id == patient.Id).List();
            foreach (var schedule in schedules)
            {
                if (!scheduleSettings.Contains(schedule.ScheduleSettings))
                {
                    scheduleSettings.Add(schedule.ScheduleSettings);
                }
            }
            var startDate = message.StartDate ?? DateTime.Now.FirstOfMonth();
            var endDate = message.EndDate ?? DateTime.Now.LastInstantOfDay();
            //var account = Identity();
            //this.logService.Info(string.Format("Användare {0} läste rapport mellan {1:yyyy-MM-dd} och {2:yyyy-MM-dd} för boende {3} (REF: {4}).", account.UserName, startDate, endDate, patient.FullName, patient.Id), account, patient, LogType.Read);
            return new ScheduleReportViewModel
            {
                Patient = this.transformer.ToPatient(patient),
                Schedule = message.ScheduleSettingsId,
                Schedules = scheduleSettings,
                StartDate = startDate.Date,
                EndDate = endDate.Date,
                /*Report = ExecuteCommand<ReportViewModel>(new CreateReportCommand<ScheduleReportFilter>
                {
                    Page = page,
                    StartDate = startDate.Value,
                    EndDate = endDate.Value,
                    Filter = new ScheduleReportFilter
                    {
                        PatientId = patient.Id,
                        ScheduleSettingsId = sId
                    }
                })*/
            };
        }

        #endregion
    }
}