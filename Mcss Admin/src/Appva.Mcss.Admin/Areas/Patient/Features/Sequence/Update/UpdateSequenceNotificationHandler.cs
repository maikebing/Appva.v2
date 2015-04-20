// <copyright file="UpdateSequenceHandler.cs" company="Appva AB">
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

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class UpdateSequenceNotificationHandler : RequestHandler<UpdateSequence, SequenceViewModel>
    {
        #region Private Variables.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext context;

        /// <summary>
        /// The <see cref="ILogService"/>.
        /// </summary>
        private readonly ILogService logService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSequenceNotificationHandler"/> class.
        /// </summary>
        public UpdateSequenceNotificationHandler(IPersistenceContext context, ILogService logService)
        {
            this.context = context;
            this.logService = logService;
        }

        #endregion

        #region UpdateSequenceNotificationHandler<CreateSequence, SequenceViewModel> Overrides.

        /// <inheritdoc />
        public override SequenceViewModel Handle(CreateSequence message)
        {
            Account recipient = null;
            Taxon delegation = null;
            if (Model.Delegation.HasValue && !Model.Nurse)
            {
                delegation = this.context.Get<Taxon>(Model.Delegation.Value);
            }
            var sequence = CreateOrUpdate(delegation, recipient);
            this.context.Update(sequence);
            var currentUser = CurrentUser;
                LogService.Info(string.Format("Användare {0} ändrade {1} (REF: {2}) i {3} (REF: {4}).",
                    CurrentUser.UserName, sequence.Name, sequence.Id, Schedule.ScheduleSettings.Name, sequence.Schedule.Id),
                    CurrentUser, sequence.Patient, LogType.Write);
            
            Schedule.Modified = DateTime.Now;
            this.context.Update(Schedule);
        }

        #endregion

        #region Private Methods.

        /// <summary>
        /// TODO: MOVE?
        private Sequence CreateOrUpdate(Taxon delegation, Account recipient)
        {
            DateTime startDate = DateTimeUtilities.Now();
            DateTime? endDate = null;
            DateTime tempDate;
            Role requiredRole = null;
            if (Model.Dates.IsNotEmpty() && Model.Interval == 0)
            {
                var dates = Model.Dates.Split(',');
                if (dates.Count() > 0)
                {
                    if (!DateTime.TryParse(dates[0], out startDate))
                    {
                        startDate = DateTimeExt.Now();
                    }
                    if (DateTime.TryParse(dates[dates.Count() - 1], out tempDate))
                    {
                        endDate = tempDate;
                    }
                }
            }
            if (Model.Interval > 0)
            {
                Model.Dates = null;
            }
            if (Model.Nurse)
            {
                requiredRole = new RoleService(Session).GetByMachineName("_TITLE_N");
            }

            if (Model.OnNeedBasis)
            {
                if (Model.OnNeedBasisStartDate.HasValue)
                {
                    startDate = Model.OnNeedBasisStartDate.Value;
                }
                if (Model.OnNeedBasisEndDate.HasValue)
                {
                    endDate = Model.OnNeedBasisEndDate.Value;
                }
            }
            else
            {
                if (Model.StartDate.HasValue)
                {
                    startDate = Model.StartDate.Value;
                }
                if (Model.EndDate.HasValue)
                {
                    endDate = Model.EndDate.Value;
                }
            }
            
            if (Schedule.ScheduleSettings.HasInventory && Sequence.Inventory == null)
            {
                var inventory = new Inventory()
                {
                    CurrentLevel = (double)Sequence.StockAmount,
                    Description = Sequence.Name,
                    LastRecount = Sequence.LastStockAmountCalculation //TODO: If null today
                };
                Session.SaveOrUpdate(inventory);
                Sequence.Inventory = inventory;
            }
            Sequence.Name = Model.Name;
            Sequence.Description = Model.Description;
            Sequence.StartDate = startDate;
            Sequence.EndDate = endDate;
            Sequence.RangeInMinutesBefore = Model.RangeInMinutesBefore;
            Sequence.RangeInMinutesAfter = Model.RangeInMinutesAfter;
            Sequence.Times = string.Join(",", Model.Times.Where(x => x.Checked == true).Select(x => x.Id).ToArray());
            Sequence.Dates = Model.Dates;
            Sequence.Hour = Model.Hour;
            Sequence.Minute = Model.Minute;
            Sequence.Interval = (Model.OnNeedBasis) ? 1 : Model.Interval.Value;
            Sequence.OnNeedBasis = Model.OnNeedBasis;
            Sequence.Reminder = Model.Reminder;
            Sequence.ReminderInMinutesBefore = Model.ReminderInMinutesBefore;
            Sequence.ReminderRecipient = recipient;
            Sequence.Patient = Patient;
            Sequence.Schedule = Schedule;
            Sequence.Taxon = delegation;
            Sequence.Role = requiredRole;
            return Sequence;
            

        }

        #endregion
    }
}