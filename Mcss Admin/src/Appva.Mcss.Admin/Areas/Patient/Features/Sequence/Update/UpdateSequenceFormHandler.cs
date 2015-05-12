﻿// <copyright file="UpdateSequenceHandler.cs" company="Appva AB">
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
        /// The <see cref="ISequenceService"/>.
        /// </summary>
        private readonly ISequenceService sequenceService;

        /// <summary>
        /// The <see cref="IRoleService"/>.
        /// </summary>
        private readonly IRoleService roleService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSequenceFormHandler"/> class.
        /// </summary>
        public UpdateSequenceFormHandler(IPersistenceContext context, ISequenceService sequenceService, IRoleService roleService)
        {
            this.context = context;
            this.roleService = roleService;
            this.sequenceService = sequenceService;
        }

        #endregion

        #region UpdateSequenceNotificationHandler Overrides.

        /// <inheritdoc />
        public override DetailsSchedule Handle(UpdateSequenceForm message)
        {
            var sequence = this.sequenceService.Find(message.SequenceId);
            var schedule = this.context.Get<Schedule>(message.ScheduleId);
            Account recipient = null;
            Taxon delegation = null;
            if (message.Delegation.HasValue && !message.Nurse)
            {
                delegation = this.context.Get<Taxon>(message.Delegation.Value);
            }
            this.CreateOrUpdate(message, sequence, schedule, delegation, recipient);
            this.context.Update(sequence);
            /*var currentUser = CurrentUser;
                LogService.Info(string.Format("Användare {0} ändrade {1} (REF: {2}) i {3} (REF: {4}).",
                    CurrentUser.UserName, sequence.Name, sequence.Id, Schedule.ScheduleSettings.Name, sequence.Schedule.Id),
                    CurrentUser, sequence.Patient, LogType.Write);
            */
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
        private Sequence CreateOrUpdate(UpdateSequenceForm model, Sequence sequence, Schedule schedule, Taxon delegation, Account recipient)
        {
            DateTime startDate = DateTimeUtilities.Now();
            DateTime? endDate = null;
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

            if (schedule.ScheduleSettings.HasInventory && sequence.Inventory == null)
            {
                var inventory = new Inventory()
                {
                    CurrentLevel = (double) sequence.StockAmount,
                    Description = sequence.Name,
                    LastRecount = sequence.LastStockAmountCalculation //TODO: If null today
                };
                this.context.Save(inventory);
                sequence.Inventory = inventory;
            }
            sequence.Name = model.Name;
            sequence.Description = model.Description;
            sequence.StartDate = startDate;
            sequence.EndDate = endDate;
            sequence.RangeInMinutesBefore = model.RangeInMinutesBefore;
            sequence.RangeInMinutesAfter = model.RangeInMinutesAfter;
            sequence.Times = string.Join(",", model.Times.Where(x => x.Checked == true).Select(x => x.Id).ToArray());
            sequence.Dates = model.Dates;
            sequence.Hour = model.Hour;
            sequence.Minute = model.Minute;
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