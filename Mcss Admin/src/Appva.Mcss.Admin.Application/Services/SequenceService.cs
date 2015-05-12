// <copyright file="SequenceService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Services
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface ISequenceService : IService
    {
        Sequence Find(Guid id);

        void Create(
            Patient patient,
            DateTime startDate,
            DateTime? endDate,
            Schedule schedule,
            String description,
            bool? canRaiseAlert,
            int interval,
            int intervalFactor = 0,
            bool intervalIsDate = false,
            int rangeInMinutesBefore = 0,
            int rangeInMinutesAfter = 0,
            String name = null,
            String times = null,
            String dates = null,
            bool onNeedBasis = false,
            bool reminder = false,
            int remindInMinutesBefore = 0,
            Account reminderRecipient = null,
            Taxon taxon = null,
            Role requiredRole = null,
            bool overView = true,
            bool pauseAnyAlerts = false,
            bool absent = false,
            bool allDay = false
        );
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class SequenceService : ISequenceService
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext context;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="SequenceService"/> class.
        /// </summary>
        /// <param name="context">The <see cref="IPersistenceContext"/></param>
        public SequenceService(IPersistenceContext context)
        {
            this.context = context;
        }

        #endregion

        #region ISequenceService Members.

        public Sequence Find(Guid id)
        {
            return this.context.Get<Sequence>(id);
        }

        public void Create(
            Patient patient,
            DateTime startDate,
            DateTime? endDate,
            Schedule schedule,
            String description,
            bool? canRaiseAlert,
            int interval,
            int intervalFactor = 0,
            bool intervalIsDate = false,
            int rangeInMinutesBefore = 0,
            int rangeInMinutesAfter = 0,
            String name = null,
            String times = null,
            String dates = null,
            bool onNeedBasis = false,
            bool reminder = false,
            int remindInMinutesBefore = 0,
            Account reminderRecipient = null,
            Taxon taxon = null,
            Role requiredRole = null,
            bool overView = true,
            bool pauseAnyAlerts = false,
            bool absent = false,
            bool allDay = false
        )
        {
            if (schedule.ScheduleSettings.ScheduleType == ScheduleType.Action)
            {
                canRaiseAlert = schedule.ScheduleSettings.CanRaiseAlerts;
            }
            var sequence = new Sequence
            {
                Patient = patient,
                StartDate = startDate,
                EndDate = endDate,
                Schedule = schedule,
                Name = name,
                Description = description,
                RangeInMinutesAfter = rangeInMinutesAfter,
                RangeInMinutesBefore = rangeInMinutesBefore,
                Times = times,
                Dates = dates,
                Interval = interval,
                IntervalFactor = intervalFactor,
                IntervalIsDate = intervalIsDate,
                OnNeedBasis = onNeedBasis,
                Reminder = reminder,
                ReminderInMinutesBefore = remindInMinutesBefore,
                ReminderRecipient = reminderRecipient,
                Taxon = taxon,
                Role = requiredRole,
                CanRaiseAlert = (bool)canRaiseAlert,
                Overview = overView,
                PauseAnyAlerts = pauseAnyAlerts,
                Absent = absent,
                AllDay = allDay
            };
            this.context.Save(sequence);
            schedule.UpdatedAt = DateTime.Now;
            this.context.Update(schedule);
        }

        #endregion
    }
}