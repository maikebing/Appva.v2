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

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class CreateSequenceFormHandler : RequestHandler<CreateSequenceForm, DetailsSchedule>
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
        private readonly IAuditService auditing;

        /// <summary>
        /// The <see cref="IInventoryService"/>.
        /// </summary>
        private readonly IInventoryService inventories;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSequenceFormHandler"/> class.
        /// </summary>
        public CreateSequenceFormHandler(IPersistenceContext context, IInventoryService inventories, IRoleService roleService, IAuditService auditing)
        {
            this.context = context;
            this.inventories = inventories;
            this.roleService = roleService;
            this.auditing = auditing;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override DetailsSchedule Handle(CreateSequenceForm message)
        {
            var schedule = this.context.Get<Schedule>(message.ScheduleId);
            Taxon delegation = null;
            if (message.Delegation.HasValue && ! message.Nurse)
            {
                delegation = this.context.Get<Taxon>(message.Delegation.Value);
            }
            var sequence = this.CreateOrUpdate(message, schedule, delegation);
            this.context.Save(sequence);

            this.auditing.Create(
                sequence.Patient,
                "skapade {0} (REF: {1}) i {2} (REF: {3}).",
                sequence.Name, 
                sequence.Id, 
                schedule.ScheduleSettings.Name, 
                sequence.Schedule.Id);
            schedule.UpdatedAt = DateTime.Now;
            this.context.Update(schedule);
            return new DetailsSchedule
                {
                    Id = message.Id,
                    ScheduleId = message.ScheduleId
                };
        }

        #endregion

        #region Private Methods.

        /// <summary>
        /// TODO: MOVE?
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        private Sequence CreateOrUpdate(CreateSequenceForm message, Schedule schedule, Taxon delegation)
        {
            DateTime startDate = DateTimeUtilities.Now();
            DateTime? endDate = null;
            DateTime tempDate;
            Role requiredRole = null;
            if (message.Dates.IsNotEmpty() && message.Interval == 0)
            {
                var dates = message.Dates.Split(',');
                if (dates.Count() > 0)
                {
                    if (!DateTime.TryParse(dates[0], out startDate))
                    {
                        startDate = DateTimeUtilities.Now();
                    }
                    if (DateTime.TryParse(dates[dates.Count() - 1], out tempDate))
                    {
                        endDate = tempDate;
                    }
                }
            }
            if (message.Interval > 0)
            {
                message.Dates = null;
            }
            if (message.Nurse)
            {
                requiredRole = this.roleService.Find(RoleTypes.Nurse);
            }

            if (message.OnNeedBasis)
            {
                if (message.OnNeedBasisStartDate.HasValue)
                {
                    startDate = message.OnNeedBasisStartDate.Value;
                }
                if (message.OnNeedBasisEndDate.HasValue)
                {
                    endDate = message.OnNeedBasisEndDate.Value;
                }
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
            if(schedule.ScheduleSettings.HasInventory){
                if(message.CreateNewInventory)
                {
                    message.Inventory = this.inventories.Create(message.Name, null, null, schedule.Patient);
                }
                inventory = this.inventories.Find(message.Inventory.GetValueOrDefault());
            }
           
            return new Sequence()
            {
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsActive = true,
                Name = message.Name,
                Description = message.Description,
                StartDate = startDate,
                EndDate = endDate,
                RangeInMinutesBefore = message.RangeInMinutesBefore,
                RangeInMinutesAfter = message.RangeInMinutesAfter,
                Times = string.Join(",", message.Times.Where(x => x.Checked == true).Select(x => x.Id).ToArray()),
                Dates = message.Dates,
                Interval = (message.OnNeedBasis) ? 1 : message.Interval.Value,
                OnNeedBasis = message.OnNeedBasis,
                Reminder = message.Reminder,
                ReminderInMinutesBefore = message.ReminderInMinutesBefore,
                /*ReminderRecipient = recipient,*/
                Patient = schedule.Patient,
                Schedule = schedule,
                Taxon = delegation,
                Role = requiredRole,
                Inventory = inventory
            };
        }

        #endregion
    }
}