// <copyright file="CreateSequenceFormHandler.cs" company="Appva AB">
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
    using Appva.Domain;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class CreateSequenceFormHandler : RequestHandler<CreateSequencePostRequest, DetailsSchedule>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext context;

        /// <summary>
        /// The <see cref="ISequenceService"/>.
        /// </summary>
        private readonly ISequenceService sequenceService;

        /// <summary>
        /// The <see cref="IRoleService"/>.
        /// </summary>
        private readonly IRoleService roleService;

        /// <summary>
        /// The <see cref="IAuditService"/>.
        /// </summary>
        private readonly IAuditService auditing;

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The <see cref="IInventoryService"/>.
        /// </summary>
        private readonly IInventoryService inventories;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSequenceFormHandler"/> class.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="inventories"></param>
        /// <param name="roleService"></param>
        /// <param name="auditing"></param>
        public CreateSequenceFormHandler(IPersistenceContext context, ISequenceService sequenceService, IInventoryService inventories, IRoleService roleService, ISettingsService settingsService, IAuditService auditing)
        {
            this.context         = context;
            this.sequenceService = sequenceService;
            this.inventories     = inventories;
            this.roleService     = roleService;
            this.auditing        = auditing;
            this.settingsService = settingsService;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override DetailsSchedule Handle(CreateSequencePostRequest message)
        {
            var schedule = this.context.Get<Schedule>(message.ScheduleId);
            Taxon delegation = null;
            if (message.DelegationId.HasValue && ! message.IsRequiredRole)
            {
                delegation = this.context.Get<Taxon>(message.DelegationId.Value);
            }
            if (message.Dates.IsNotEmpty())
            {
                TimingSchedule.New(message.GetSelectedDates(), message.GetSelectedTimesOfDay());
            }
            if (message.IsNeedBased)
            {
                TimingSchedule.New(message.GetSelectedPeriod(), message.GetSelectedFrequency(), message.GetSelectedTimesOfDay());
                //// do all need based stuff first.
            }

            this.CreateOrUpdateSequence(message, schedule, delegation);
            //this.context.Save(sequence);

            //this.auditing.Create(
            //    sequence.Patient,
            //    "skapade {0} (REF: {1}) i {2} (REF: {3}).",
            //    sequence.Name, 
            //    sequence.Id, 
            //    schedule.ScheduleSettings.Name, 
            //    sequence.Schedule.Id);

            return new DetailsSchedule
                {
                    Id = message.PatientId,
                    ScheduleId = message.ScheduleId
                };
        }

        #endregion

        #region Private Methods.

        private void CreateOrUpdateSequence(CreateSequencePostRequest message, Schedule schedule, Taxon delegation)
        {
            DateTime startDate = DateTimeUtilities.Now();
            DateTime? endDate = null;
            Role requiredRole = null;
            
            if (message.Dates.IsNotEmpty())
            {
                TimingSchedule.New(message.GetSelectedDates(), message.GetSelectedTimesOfDay());
            }
            //// Start with need based.
            if (message.IsNeedBased)
            {
                //TimingSchedule.New();
                ////TimingSchedule.New(Period.New())
                //// do all need based stuff first.
            }


            IEnumerable<TimeOfDay> timesOfDay = new List<TimeOfDay>();
            //// WTF? This is not an if, but is required! 
            if (message.Times.Count(x => x.Checked == true) > 0)
            {
                timesOfDay = message.Times.Where(x => x.Checked == true).Select(x => TimeOfDay.Parse(x.Id + ":00"));
            }

            if (message.IsRequiredRole)
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

            Inventory inventory = null;
            if (schedule.ScheduleSettings.HasInventory)
            {
                if (message.CreateNewInventory)
                {
                    message.InventoryId = this.inventories.Create(message.Name, null, null, schedule.Patient);
                }
                inventory = this.inventories.Find(message.InventoryId.GetValueOrDefault());
            }

            if (message.IsNeedBased == true)
            {
                //startDate = message.OnNeedBasisStartDate.HasValue ? message.OnNeedBasisStartDate.Value : startDate;
                //endDate = message.OnNeedBasisEndDate.HasValue ? message.OnNeedBasisEndDate.Value : endDate;

                //this.sequenceService.CreateNeedBasedSequence(schedule, message.Name, message.Description, startDate, endDate, delegation, requiredRole, message.RangeInMinutesBefore, message.RangeInMinutesAfter, inventory);
            }
            else if (message.Interval.HasValue && message.Interval.Value > 0 && message.Dates.IsNull())
            {
                startDate = message.StartDate.HasValue ? message.StartDate.Value : startDate;
                endDate = message.EndDate.HasValue ? message.EndDate.Value : endDate;
                //this.sequenceService.CreateIntervalBasedSequence(schedule, message.Name, message.Description, startDate, message.Interval.Value, timesOfDay, endDate, delegation, requiredRole, message.RangeInMinutesBefore, message.RangeInMinutesAfter, inventory);
            }
            else
            {
                //DateTimeUtils.GetEarliestAndLatestDateFrom(message.Dates.Split(','), out startDate, out endDate);
                //this.sequenceService.CreateDatesBasedSequence(schedule, message.Name, message.Description, startDate, endDate, message.Dates, timesOfDay, delegation, requiredRole, message.RangeInMinutesBefore, message.RangeInMinutesAfter, inventory);
            }
        }

        /// <summary>
        /// TODO: MOVE?
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        private void CreateOrUpdate(CreateSequencePostRequest message, Schedule schedule, Taxon delegation)
        {
            DateTime startDate = DateTimeUtilities.Now();
            DateTime? endDate = null;
            Role requiredRole = null;
            if (message.Dates.IsNotEmpty() && message.Interval == 0)
            {
                //DateTimeUtils.GetEarliestAndLatestDateFrom(message.Dates.Split(','), out startDate, out endDate);
            }
            if (message.Interval > 0)
            {
                message.Dates = null;
            }
            if (message.IsRequiredRole)
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

            if (message.IsNeedBased == true)
            {
                /*if (message.OnNeedBasisStartDate.HasValue)
                {
                    startDate = message.OnNeedBasisStartDate.Value;
                }
                if (message.OnNeedBasisEndDate.HasValue)
                {
                    endDate = message.OnNeedBasisEndDate.Value;
                }*/
            }
            else
            {
                if (message.StartDate.HasValue)
                {
                    startDate = message.StartDate.Value;
                }
                if (message.EndDate.HasValue)
                {
                    endDate = message.EndDate.Value;
                }
            }

            Inventory inventory = null;
            if (schedule.ScheduleSettings.HasInventory)
            {
                if (message.CreateNewInventory)
                {
                    message.InventoryId = this.inventories.Create(message.Name, null, null, schedule.Patient);
                }
                inventory = this.inventories.Find(message.InventoryId.GetValueOrDefault());
            }
            
            //return new Sequence()
            //{
            //    CreatedAt = DateTime.Now,
            //    UpdatedAt = DateTime.Now,
            //    IsActive = true,
            //    Name = message.Name,
            //    Description = message.Description,
            //    StartDate = startDate,
            //    EndDate = endDate,
            //    RangeInMinutesBefore = message.RangeInMinutesBefore,
            //    RangeInMinutesAfter = message.RangeInMinutesAfter,
            //    Times = string.Join(",", message.Times.Where(x => x.Checked == true).Select(x => x.Id).ToArray()),
            //    Dates = message.Dates,
            //    Interval = (message.OnNeedBasis) ? 1 : message.Interval.Value,
            //    OnNeedBasis = message.OnNeedBasis,
            //    Reminder = message.Reminder,
            //    ReminderInMinutesBefore = message.ReminderInMinutesBefore,
            //    /*ReminderRecipient = recipient,*/
            //    Patient = schedule.Patient,
            //    Schedule = schedule,
            //    Taxon = delegation,
            //    Role = requiredRole,
            //    Inventory = inventory
            //};
        }

   

        #endregion
    }
}