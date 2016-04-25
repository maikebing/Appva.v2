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

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class UpdateSequenceFormHandler : RequestHandler<UpdateSequenceForm, DetailsSchedule>
    {
        #region Private Variables.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext context;

        /// <summary>
        /// The <see cref="IRoleService"/>.
        /// </summary>
        private readonly IRoleService roleService;

        /// <summary>
        /// The <see cref="IAuditService"/>.
        /// </summary>
        private readonly IAuditService auditService;

        /// <summary>
        /// The <see cref="IInventoryService"/>.
        /// </summary>
        private readonly IInventoryService inventoryService;

        /// <summary>
        /// The <see cref="ISequenceService"/>.
        /// </summary>
        private readonly ISequenceService sequenceService;


        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSequenceFormHandler"/> class.
        /// </summary>
        public UpdateSequenceFormHandler(IAuditService auditService, IPersistenceContext context, ISequenceService sequenceService, IRoleService roleService, IInventoryService inventoryService)
        {
            this.auditService       = auditService;
            this.context            = context;
            this.roleService        = roleService;
            this.sequenceService    = sequenceService;
            this.inventoryService   = inventoryService;
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
            this.context.Update(sequence);
            this.auditService.Update(
                sequence.Patient,
                "ändrade {0} (REF: {1}) i {2} (REF: {3}).",
                 sequence.Name, 
                 sequence.Id, 
                 schedule.ScheduleSettings.Name, 
                 sequence.Schedule.Id);
            schedule.UpdatedAt = DateTime.Now;
            this.context.Update(schedule);
            return new DetailsSchedule
            {
                Id         = message.Id,
                ScheduleId = schedule.Id
            };
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
            DateTime tempDate;
            Role requiredRole = null;
            if (model.Dates.IsNotEmpty() && model.Interval == 0)
            {
                var dates = model.Dates.Split(',');
                if (dates.Count() > 0)
                {
                    if (! DateTime.TryParse(dates[0], out startDate))
                    {
                        startDate = DateTimeUtilities.Now();
                    }
                    if (DateTime.TryParse(dates[dates.Count() - 1], out tempDate))
                    {
                        endDate = tempDate;
                    }
                }
            }
            if (model.Interval > 0)
            {
                model.Dates = null;
            }
            if (model.Nurse)
            {
                requiredRole = this.roleService.Find(RoleTypes.Nurse);
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
            ////sequence.Patient = Patient; // unnecassary
            sequence.Schedule = schedule;
            sequence.Taxon = delegation;
            sequence.Role = requiredRole;
            return sequence;
        }

        #endregion
    }
}