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
    using Appva.Persistence;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Infrastructure;
    using Appva.Mcss.Admin.Application.Auditing;
    using Appva.Mcss.Admin.Application.Security.Identity;

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
        /// The <see cref="IAuditService"/>.
        /// </summary>
        private readonly IAuditService auditing;

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
        private readonly IIdentityService identitfyService;

		#endregion

		#region Constructor.

		/// <summary>
        /// Initializes a new instance of the <see cref="ListAlertHandler"/> class.
		/// </summary>
        /// <param name="settings">The <see cref="IPatientService"/> implementation</param>
        /// <param name="settings">The <see cref="ITaskService"/> implementation</param>
        /// <param name="settings">The <see cref="ILogService"/> implementation</param>
        public ListAlertHandler(IPatientService patientService, IIdentityService identitfyService,
            ITaskService taskService, IAuditService auditing, IPatientTransformer transformer, IPersistenceContext persistence)
		{
            this.identitfyService = identitfyService;
            this.patientService = patientService;
            this.transformer = transformer;
            this.taskService = taskService;
            this.auditing = auditing;
            this.persistence = persistence;
		}

		#endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override ListAlertModel Handle(ListAlert message)
        {
            var patient = this.patientService.Get(message.Id);
            var startDate = message.StartDate ?? DateTime.Now.FirstOfMonth();
            var endDate = message.EndDate ?? DateTime.Now.LastOfMonth();
            if (message.Year.HasValue)
            {
                startDate = new DateTime(message.Year.Value, 1, 1);
                endDate   = new DateTime(message.Year.Value, 12, 31);
            }
            if (message.Month.HasValue)
            {
                if (! message.Year.HasValue)
                {
                    message.Year = DateTime.Now.Year;
                }
                startDate = new DateTime(message.Year.Value, message.Month.Value, 1);
                endDate   = new DateTime(message.Year.Value, message.Month.Value, DateTime.DaysInMonth(message.Year.Value, message.Month.Value));
            }
            var list = new List<ScheduleSettings>();
            var account = this.persistence.Get<Account>(this.identitfyService.PrincipalId);
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
            }
            var notHandledDelays = this.taskService.FindDelaysByPatient(patient, false, list);
            var handledDelays = this.taskService.FindDelaysByPatient(patient, true, list)
                .Where(x => x.Scheduled >= startDate && x.Scheduled <= endDate)
                .ToList();
            this.auditing.Read(patient, "läste larm mellan datum {0:yyyy-MM-dd} och {1:yyyy-MM-dd}", startDate, endDate);
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