// <copyright file="UpdateSequenceHandler.cs" company="Appva AB">
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
    using Appva.Core.Extensions;
    using Appva.Core.Resources;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Auditing;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Persistence;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Domain;
    using Appva.Mcss.Admin.Infrastructure;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class UpdateSequenceHandler : RequestHandler<UpdateSequence, UpdateSequenceForm>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IAuditService"/>.
        /// </summary>
        private readonly IAuditService auditService; /* NOT USED! */

        /// <summary>
        /// The <see cref="IRoleService"/>.
        /// </summary>
        private readonly IRoleService roleService;

        /// <summary>
        /// The <see cref="IDelegationService"/>.
        /// </summary>
        private readonly IDelegationService delegationService;

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext context;

        /// <summary>
        /// The <see cref="IInventoryService"/>.
        /// </summary>
        private readonly IInventoryService inventoryService;

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService settingsService;

        private readonly IPatientTransformer patientTransformer;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSequenceHandler"/> class.
        /// </summary>
        public UpdateSequenceHandler(
            ISettingsService settingsService, IRoleService roleService, IDelegationService delegationService,
            IInventoryService inventoryService, IPatientTransformer patientTransformer,
            IPersistenceContext context)
        {
            this.settingsService    = settingsService;
            this.roleService        = roleService;
            this.delegationService  = delegationService;
            this.inventoryService   = inventoryService;
            this.patientTransformer = patientTransformer;
            this.context            = context;
        }

        #endregion

        #region RequestHandler<CreateSequence, SequenceViewModel> Overrides.

        /// <inheritdoc />
        public override UpdateSequenceForm Handle(UpdateSequence message)
        {
            //// FIXME: Log here!
            Role role = null;
            var sequence      = this.context.Get<Sequence>(message.SequenceId);
            var patient       = sequence.Patient;
            var schedule      = sequence.Schedule;
            var temp = this.settingsService.Find<Dictionary<Guid, Guid>>(ApplicationSettings.TemporaryScheduleSettingsRoleMap);
            if (temp != null && temp.ContainsKey(schedule.ScheduleSettings.Id))
            {
                role = this.roleService.Find(temp[schedule.ScheduleSettings.Id]);
            }
            if (role == null)
            {
                role = this.roleService.Find(RoleTypes.Nurse);
            }
            return new UpdateSequenceForm
            {
                PatientId             = sequence.Patient.Id,
                ScheduleId            = sequence.Schedule.Id,
                Patient               = patientTransformer.ToPatient(sequence.Patient),
                Name                  = sequence.Name,
                Instruction           = sequence.Description,
                DelegationId          = sequence.Taxon == null ? (Guid?) null : sequence.Taxon.Id,
                Delegations           = sequence.Schedule.ScheduleSettings.DelegationTaxon != null ? this.delegationService.ListDelegationTaxons(byRoot: schedule.ScheduleSettings.DelegationTaxon.Id, includeRoots: false).Select(x => new SelectListItem { Text  = x.Name, Value = x.Id.ToString() }) : new List<SelectListItem>(),
                IsRequiredRole        = sequence.Role != null,
                RequiredRoleText      = sequence.Role != null ? sequence.Role.Name.ToLower() : role.Name.ToLower(),
                InventoryType         = sequence.Inventory == null ? InventoryState.New : InventoryState.Use,
                InventoryId           = sequence.Inventory == null ? (Guid?) null : sequence.Inventory.Id,
                Inventories           = sequence.Schedule.ScheduleSettings.HasInventory ? this.inventoryService.Search(patient.Id, true).Select(x => new SelectListItem() { Text = x.Description, Value = x.Id.ToString() }) : null,
                Type                  = sequence.Repeat.IsNeedBased ? SequenceType.NeedBased : sequence.Repeat.BoundsRange.Count() > 0 ? SequenceType.DateRange : SequenceType.Scheduled,
                StartDate             = (Date)  sequence.Repeat.StartAt,
                EndDate               = (Date?) sequence.Repeat.EndAt,
                IsPeriodWithTimeOfDay = (sequence.Repeat.StartAt.Hour > 0 || sequence.Repeat.StartAt.Minute > 0) || (sequence.Repeat.EndAt.HasValue && (sequence.Repeat.EndAt.Value.Hour > 0 || sequence.Repeat.EndAt.Value.Minute > 0)),
                StartHour             = sequence.Repeat.StartAt.Hour,
                StartMinute           = sequence.Repeat.StartAt.Minute,
                EndHour               = sequence.Repeat.EndAt.HasValue ? sequence.Repeat.EndAt.Value.Hour   : 23,
                EndMinute             = sequence.Repeat.EndAt.HasValue ? sequence.Repeat.EndAt.Value.Minute : 59,
                Dates                 = sequence.Repeat.BoundsRange.ToList(),
                Repetition            = sequence.Repeat.PeriodUnit.HasValue && sequence.Repeat.PeriodUnit.Value == UnitOfTime.Week ? Repetition.Weekly : Repetition.Daily,
                EverydayFrequency     = sequence.Repeat.PeriodUnit.HasValue && sequence.Repeat.PeriodUnit.Value == UnitOfTime.Day  ? sequence.Repeat.Period : 1, /* Check from interoperability */
                WeeklyFrequency       = sequence.Repeat.PeriodUnit.HasValue && sequence.Repeat.PeriodUnit.Value == UnitOfTime.Week ? sequence.Repeat.Period : 1, /* Check from interoperability */
                DaysOfWeek            = this.ToDaysOfWeek(sequence).ToList(),
                Times                 = this.ToTimesOfDay(sequence).OrderBy(x => (x.Hour < 6) ? x.Hour + 25 : x.Hour).ToList(), /* re-order to start with 06 ... 23, 00, 01, 02 */
                RangeInMinutesBefore  = sequence.Repeat.OffsetBefore,
                RangeInMinutesAfter   = sequence.Repeat.OffsetAfter
                //// IsOrderable               = sequence.Article != null,
                //// IsOrderableArticleEnabled = orderListConfiguration.HasMigratedArticles && schedule.ScheduleSettings.ArticleCategory != null
            };
        }

        private IEnumerable<DaysOfWeekModel> ToDaysOfWeek(Sequence sequence)
        {
            var retval = new List<DaysOfWeekModel>();
            foreach (var dayOfWeek in Appva.Domain.DayOfWeek.DaysOfWeek)
            {
                var selected = this.FindDayOfWeek(sequence, dayOfWeek.Code);
                if (selected == null)
                {
                    retval.Add(new DaysOfWeekModel { Code = dayOfWeek.Code });    
                    continue;
                }
                retval.Add(new DaysOfWeekModel { Code = selected.Value.Code, IsChecked = true });
            }
            return retval;
        }

        private Appva.Domain.DayOfWeek? FindDayOfWeek(Sequence sequence, string code)
        {
            foreach (var dayOfWeek in sequence.Repeat.DaysOfWeek)
            {
                if (dayOfWeek.Code == code)
                {
                    return dayOfWeek;
                }
            }
            return null;
        }

        private IEnumerable<TimeModel> ToTimesOfDay(Sequence sequence)
        {
            var retval = new List<TimeModel>();
            foreach (var timeOfDay in Appva.Domain.TimeOfDay.Hours)
            {
                var selected = this.FindTimeOfDay(sequence, timeOfDay.Hour);
                if (selected == null)
                {
                    retval.Add(new TimeModel { Hour = timeOfDay.Hour, Minute = 0 });
                    continue;
                }
                retval.Add(new TimeModel { Hour = selected.Value.Hour, Minute = selected.Value.Minute, IsChecked = true });
            }
            return retval;
        }

        private Appva.Domain.TimeOfDay? FindTimeOfDay(Sequence sequence, int hour)
        {
            foreach (var timeOfDay in sequence.Repeat.TimesOfDay)
            {
                if (timeOfDay.Hour == hour)
                {
                    return timeOfDay;
                }
            }
            return null;
        }

        #endregion

        #region Private Methods.

        /// <summary>
        /// TODO: MOVE?
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetDelegations(Schedule schedule)
        {
            var delegations = this.context.QueryOver<Taxon>()
                .Where(x => x.IsActive == true)
                .And(x => x.IsRoot == false)
                .And(x => x.Parent == schedule.ScheduleSettings.DelegationTaxon)
                .OrderBy(x => x.Weight).Asc
                .ThenBy(x => x.Name).Asc
                .JoinQueryOver<Taxonomy>(x => x.Taxonomy)
                    .Where(x => x.MachineName == TaxonomicSchema.Delegation.Id)
                .List();
            return delegations.Select(x => new SelectListItem
            {
                Text  = x.Name,
                Value = x.Id.ToString()
            });
        }

        #endregion
    }
}