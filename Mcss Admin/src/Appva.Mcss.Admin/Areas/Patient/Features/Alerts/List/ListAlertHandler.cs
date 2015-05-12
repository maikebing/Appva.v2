// <copyright file="ListAlertHandler.cs" company="Appva AB">
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
    using Appva.Mcss.Web.ViewModels;
    using Appva.Core.Extensions;
    using Appva.Core.Utilities;
    using Appva.Mcss.Admin.Commands;
    using Appva.Mcss.Web.Mappers;
    using Appva.Persistence;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Infrastructure;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class ListAlertHandler : RequestHandler<ListAlert, ListAlertModel>
    {
        #region Variables.

		/// <summary>
        /// The <see cref="IPatientService"/>.
		/// </summary>
        private readonly IPatientService patientService;

        /// <summary>
        /// The <see cref="ITaskService"/>.
        /// </summary>
        private readonly ITaskService taskService;

        /// <summary>
        /// The <see cref="ILogService"/>.
        /// </summary>
        private readonly ILogService logService;

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
        /// Initializes a new instance of the <see cref="ListAlertHandler"/> class.
		/// </summary>
        /// <param name="settings">The <see cref="IPatientService"/> implementation</param>
        /// <param name="settings">The <see cref="ITaskService"/> implementation</param>
        /// <param name="settings">The <see cref="ILogService"/> implementation</param>
        public ListAlertHandler(IPatientService patientService, 
            ITaskService taskService, ILogService logService, IPatientTransformer transformer, IPersistenceContext persistence)
		{
            this.patientService = patientService;
            this.transformer = transformer;
            this.taskService = taskService;
            this.logService = logService;
            this.persistence = persistence;
		}

		#endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override ListAlertModel Handle(ListAlert message)
        {
            var patient = this.patientService.Get(message.Id);
            var changeMonth = message.Year.HasValue && !message.Month.HasValue;
            var startDate = DateTime.Now.SetYear(message.Year).SetMonth(changeMonth, message.Month ?? 1).FirstOfMonth();
            var endDate = DateTime.Now.SetYear(message.Year).SetMonth(changeMonth, message.Month ?? 12).LastOfMonth().LastInstantOfDay();

            var list = new List<ScheduleSettings>();
            /*
            var account = Identity();
            var roles = account.Roles;
            
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
            }*/
            var notHandledDelays = this.taskService.FindDelaysByPatient(patient, false, list);
            var handledDelays = this.taskService.FindDelaysByPatient(patient, true, list)
                .Where(x => x.Scheduled >= startDate && x.Scheduled <= endDate)
                .ToList();
            //this.logService.Info("Användare {0} läste larm mellan datum {1:yyyy-MM-dd} och {2:yyyy-MM-dd}".FormatWith(user.FullName, startDate, endDate), user, patient, LogType.Read);
            return new ListAlertModel
            {
                Patient = this.transformer.ToPatient(patient),
                TaskCurrentMap = TaskUtils.MapTimeOfDayAndSchedule(notHandledDelays),
                TaskEarlierMap = TaskUtils.MapTimeOfDayAndSchedule(handledDelays),
                Years = DateTimeUtils.GetYearSelectList(patient.CreatedAt.Year, startDate.Year == endDate.Year ? startDate.Year : 0),
                Months = startDate.Month == endDate.Month ? DateTimeUtils.GetMonthSelectList(startDate.Month) : DateTimeUtils.GetMonthSelectList(),
                StartDate = startDate,
                EndDate = endDate,
                Year = message.Year,
                Month = message.Month
            };
        }

        #endregion
    }
}