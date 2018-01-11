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
    using Appva.Core.Utilities;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Persistence;
    using Appva.Core.Extensions;
    using Appva.Core.Resources;
    using Appva.Mcss.Admin.Application.Auditing;
    using Appva.Mcss.Admin.Application.Extensions;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class UpdateSequenceFormHandler : RequestHandler<UpdateSequenceForm, DetailsSchedule>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext context;

        /// <summary>
        /// The <see cref="IRoleService"/>.
        /// </summary>
        private readonly IRoleService roleService;

        /// <summary>
        /// The <see cref="IInventoryService"/>.
        /// </summary>
        private readonly IInventoryService inventoryService;

        /// <summary>
        /// The <see cref="ISequenceService"/>.
        /// </summary>
        private readonly ISequenceService sequenceService;

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The dosage service <see cref="IDosageObservationService"/>.
        /// </summary>
        private readonly IDosageObservationService dosageService;

        /// <summary>
        /// The <see cref="IAdministrationService"/>.
        /// </summary>
        private readonly IAdministrationService administrationService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSequenceFormHandler"/> class.
        /// </summary>
        public UpdateSequenceFormHandler(IPersistenceContext context, ISequenceService sequenceService, IRoleService roleService, ISettingsService settingsService, IInventoryService inventoryService, IDosageObservationService dosageService, IAdministrationService administrationService)
        {
            this.context               = context;
            this.roleService           = roleService;
            this.sequenceService       = sequenceService;
            this.inventoryService      = inventoryService;
            this.settingsService       = settingsService;
            this.dosageService         = dosageService;
            this.administrationService = administrationService;
        }

        #endregion

        #region UpdateSequenceNotificationHandler Overrides.

        /// <inheritdoc />
        public override DetailsSchedule Handle(UpdateSequenceForm message)
        {
            var sequence = this.sequenceService.Find(message.SequenceId);
            var schedule = this.context.Get<Schedule>(sequence.Schedule.Id);
            Taxon delegation  = null;
            if (message.Delegation.HasValue && !message.Nurse)
            {
                delegation = this.context.Get<Taxon>(message.Delegation.Value);
            }

            this.CreateOrUpdate(message, sequence, schedule, delegation, null);

            if (sequence.Schedule.ScheduleSettings.IsCollectingGivenDosage && MedicationAdministration.Units.Any(x => x.Code == message.SelectedDosageScale))
            {
                var unit = MedicationAdministration.Units.Where(x => x.Code == message.SelectedDosageScale).FirstOrDefault();
                this.CreateOrUpdateDosageObservation(sequence, unit);
            }

            this.sequenceService.Update(sequence);
            
            schedule.UpdatedAt = DateTime.Now;
            this.context.Update(schedule);
            return new DetailsSchedule
            {
                Id         = message.Id,
                ScheduleId = schedule.Id
            };
        }

        /// <summary>
        /// Updates the dosage observation.
        /// </summary>
        /// <param name="sequence">The sequence<see cref="Sequence" />.</param>
        /// <param name="unit">The <see cref="UnitOfMeasurement"/>.</param>
        /// <param name="customValues">A list of custom values (optional).</param>
        private void CreateOrUpdateDosageObservation(Sequence sequence, UnitOfMeasurement unit, IList<double> customValues = null)
        {
            if (sequence.Administration == null)
            {
                //// UNRESOLVED: Figure out a better way to handle customValues.
                var administration = AdministrationFactory.CreateNew(sequence, unit, customValues);
                this.administrationService.Save(administration);
                sequence.Administration = administration;
                return;
            }
            //// UNRESOLVED: Not the best way to solve this, make it look better
            this.administrationService.Update(((MedicationAdministration)sequence.Administration.Unproxied).Update(unit, customValues));
        }

        #endregion

        #region Private Methods.

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="sequence"></param>
        /// <param name="schedule"></param>
        /// <param name="delegation"></param>
        /// <param name="recipient"></param>
        /// <returns></returns>
        private Sequence CreateOrUpdate(UpdateSequenceForm model, Sequence sequence, Schedule schedule, Taxon delegation, Account recipient)
        {
            DateTime startDate = DateTimeUtilities.Now();
            DateTime? endDate  = null;
            Role requiredRole = null;
            if (model.Dates.IsNotEmpty() && model.Interval == 0)
            {
                var dates = model.Dates.Split(',');
                DateTimeUtils.GetEarliestAndLatestDateFrom(dates, out startDate, out endDate);
            }
            if (model.Interval > 0)
            {
                model.Dates = null;
            }
            if (model.Nurse)
            {
                //// Temporary mapping
                var temp = this.settingsService.Find<Dictionary<Guid, Guid>>(ApplicationSettings.TemporaryScheduleSettingsRoleMap);
                if (temp != null && temp.ContainsKey(schedule.ScheduleSettings.Id))
                {
                    requiredRole = this.roleService.Find(temp[schedule.ScheduleSettings.Id]);
                }
                if (requiredRole == null)
                {
                    requiredRole = this.roleService.Find(RoleTypes.Nurse);
                }
            }
            if (model.OnNeedBasis)
            {
                if (model.OnNeedBasisStartDate.HasValue)
                {
                    startDate = model.OnNeedBasisStartDate.Value;
                }
                if (model.OnNeedBasisEndDate.HasValue)
                {
                    endDate = model.OnNeedBasisEndDate.Value;
                }
            }
            else
            {
                if (model.StartDate.HasValue)
                {
                    startDate = model.StartDate.Value;
                }
                if (model.EndDate.HasValue)
                {
                    endDate = model.EndDate.Value;
                }
            }

            if (schedule.ScheduleSettings.HasInventory)
            {
                sequence.Inventory = this.inventoryService.Find(model.Inventory.GetValueOrDefault());
            }

            sequence.Name = model.Name;
            sequence.Description = model.Description;
            sequence.StartDate = startDate;
            sequence.EndDate = endDate;
            sequence.RangeInMinutesBefore = model.RangeInMinutesBefore;
            sequence.RangeInMinutesAfter = model.RangeInMinutesAfter;
            sequence.Times = string.Join(",", model.Times.Where(x => x.Checked == true).Select(x => x.Id).ToArray());
            sequence.Dates = model.Dates;
            sequence.Interval = model.OnNeedBasis ? 1 : model.Interval.Value;
            sequence.OnNeedBasis = model.OnNeedBasis;
            sequence.Reminder = model.Reminder;
            sequence.ReminderInMinutesBefore = model.ReminderInMinutesBefore;
            sequence.ReminderRecipient = recipient; //// FIXME: This is always NULL why is it here at all? 
            sequence.Schedule = schedule;
            sequence.Taxon = delegation;
            sequence.Role = requiredRole;
            return sequence;
        }

        #endregion
    }
}