// <copyright file="CreateSequenceViewHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Patient.Features
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
    internal sealed class CreateSequencePostHandler : RequestHandler<SequenceViewModel, SequenceViewModel>
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
        /// The <see cref="ILogService"/>.
        /// </summary>
        private readonly ILogService logService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSequencePostHandler"/> class.
        /// </summary>
        public CreateSequencePostHandler(IPersistenceContext context, IRoleService roleService, ILogService logService)
        {
            this.context = context;
            this.roleService = roleService;
            this.logService = logService;
        }

        #endregion

        #region RequestHandler<CreateSequence, SequenceViewModel> Overrides.

        /// <inheritdoc />
        public override SequenceViewModel Handle(SequenceViewModel message)
        {
            var schedule = this.context.Get<Schedule>(message.ScheduleId);
            Account recipient = null;
            Taxon delegation = null;
            if (message.Delegation.HasValue && ! message.Nurse)
            {
                delegation = this.context.Get<Taxon>(message.Delegation.Value);
            }
            var sequence = this.CreateOrUpdate(message, delegation, recipient);

            this.context.Save(sequence);
            /*
            var currentUser = CurrentUser;
            LogService.Info(string.Format("Användare {0} skapade {1} (REF: {2}) i {3} (REF: {4}).",
                    CurrentUser.UserName, sequence.Name, sequence.Id, schedule.ScheduleSettings.Name, sequence.Schedule.Id),
                    CurrentUser, sequence.Patient, LogType.Write);
            */
            schedule.UpdatedAt = DateTime.Now;
            this.context.Update(schedule);
        }

        #endregion

        #region Private Methods.

        /// <summary>
        /// TODO: MOVE?
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        private Sequence CreateOrUpdate(SequenceViewModel message, Taxon delegation, Account recipient)
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
            Inventory inventory = schedule.ScheduleSettings.HasInventory ? new Inventory()
            {
                CurrentLevel = 0,
                Description = message.Name,
                Transactions = new List<InventoryTransactionItem>()
            } : null;
            if (inventory != null)
            {
                if (schedule.ScheduleSettings.CountInventory)
                {
                    inventory.LastRecount = DateTime.Now;
                }
                this.context.Update(inventory);
            }
            return new Sequence()
            {
                Name = message.Name,
                Description = message.Description,
                StartDate = startDate,
                EndDate = endDate,
                RangeInMinutesBefore = message.RangeInMinutesBefore,
                RangeInMinutesAfter = message.RangeInMinutesAfter,
                Times = string.Join(",", message.Times.Where(x => x.Checked == true).Select(x => x.Id).ToArray()),
                Dates = message.Dates,
                Hour = message.Hour,
                Minute = message.Minute,
                Interval = (message.OnNeedBasis) ? 1 : message.Interval.Value,
                OnNeedBasis = message.OnNeedBasis,
                Reminder = message.Reminder,
                ReminderInMinutesBefore = message.ReminderInMinutesBefore,
                ReminderRecipient = recipient,
                Patient = patient,
                Schedule = schedule,
                Taxon = delegation,
                Role = requiredRole,
                Inventory = inventory
            };
        }

        #endregion
    }
}