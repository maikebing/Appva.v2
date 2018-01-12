// <copyright file="CreateSequenceHandler.cs" company="Appva AB">
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
    using Appva.Core.Resources;
    using Appva.Cqrs;
    using Appva.Domain;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Infrastructure;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class CreateSequenceHandler : RequestHandler<CreateSequence, CreateSequencePostRequest>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext context;

        /// <summary>
        /// The <see cref="IDelegationService"/>.
        /// </summary>
        private readonly IDelegationService delegationService;

        /// <summary>
        /// The <see cref="IInventoryService"/>
        /// </summary>
        private readonly IInventoryService inventoryService;

        /// <summary>
        /// The <see cref="IRoleService"/>
        /// </summary>
        private readonly IRoleService roleService;

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The <see cref="IPatientTransformer"/>.
        /// </summary>
        private readonly IPatientTransformer patientTransformer;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSequenceHandler"/> class.
        /// </summary>
        public CreateSequenceHandler(
            ISettingsService settingsService,   IRoleService roleService, IDelegationService delegationService,
            IInventoryService inventoryService, IPatientTransformer patientTransformer, IPersistenceContext context)
        {
            this.settingsService    = settingsService;
            this.roleService        = roleService;
            this.delegationService  = delegationService;
            this.inventoryService   = inventoryService;
            this.patientTransformer = patientTransformer;
            this.context            = context;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override CreateSequencePostRequest Handle(CreateSequence message)
        {
            /* LOG */
            Role requiredRole = null;
            var patient       = this.context.Get<Patient>(message.PatientId);
            var schedule      = this.context.Get<Schedule>(message.ScheduleId);
            var temp = this.settingsService.Find<Dictionary<Guid, Guid>>(ApplicationSettings.TemporaryScheduleSettingsRoleMap);
            if (temp != null && temp.ContainsKey(schedule.ScheduleSettings.Id))
            {
                requiredRole = this.roleService.Find(temp[schedule.ScheduleSettings.Id]);
            }
            if (requiredRole == null)
            {
                requiredRole = this.roleService.Find(RoleTypes.Nurse);
            }
            return new CreateSequencePostRequest
            {
                PatientId             = patient.Id,
                ScheduleId            = schedule.Id,
                Patient               = patientTransformer.ToPatient(patient),
                Name                  = null,
                Instruction           = null,
                DelegationId          = null,
                Delegations           = schedule.ScheduleSettings.DelegationTaxon != null ? this.delegationService.ListDelegationTaxons(byRoot: schedule.ScheduleSettings.DelegationTaxon.Id, includeRoots: false).Select(x => new SelectListItem { Text  = x.Name, Value = x.Id.ToString() }) : new List<SelectListItem>(),
                IsRequiredRole        = false,
                RequiredRoleText      = requiredRole.Name.ToLower(),
                InventoryType         = InventoryState.New,
                InventoryId           = null,
                Inventories           = schedule.ScheduleSettings.HasInventory ? this.inventoryService.Search(patient.Id, true).Select(x => new SelectListItem() { Text = x.Description, Value = x.Id.ToString() }) : null,
                Type                  = SequenceType.Scheduled,
                StartDate             = Date.Today,
                EndDate               = null,
                IsPeriodWithTimeOfDay = false,
                StartHour             = TimeOfDay.Now.Hour,
                StartMinute           = TimeOfDay.Now.Minute,
                EndHour               = 23,
                EndMinute             = 59,
                Dates                 = null, /*new List<Date>(),*/
                Repetition            = Repetition.Daily,
                EverydayFrequency     = 1,
                WeeklyFrequency       = 1,
                DaysOfWeek            = Appva.Domain.DayOfWeek.DaysOfWeek.Select(x => new DaysOfWeekModel { Code = x.Code }).ToList(),
                Times                 = TimeOfDay.Hours.Select(x => new TimeModel { Hour = x.Hour, Minute = 0 }).OrderBy(x => (x.Hour < 6) ? x.Hour + 25 : x.Hour).ToList(), /* re-order to start with 06 ... 23, 00, 01, 02 */
                RangeInMinutesBefore  = 0,
                RangeInMinutesAfter   = 0
            };
        }

        #endregion
    }
}