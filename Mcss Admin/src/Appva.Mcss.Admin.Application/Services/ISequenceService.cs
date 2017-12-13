// <copyright file="ISequenceService.cs" company="Appva AB">
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
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Domain.Entities;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface ISequenceService : IService
    {
        /// <summary>
        /// Finds a sequence by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Sequence Find(Guid id);

        /// <summary>
        /// Updates the sequence
        /// </summary>
        /// <param name="sequence"></param>
        void Update(Sequence sequence);

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
            bool allDay = false,
            IList<Medication> medications = null,
            Inventory inventory = null
        );

        /// <summary>
        /// Lists the by article.
        /// </summary>
        /// <param name="articleId">The article identifier.</param>
        /// <returns></returns>
        IList<Sequence> ListByArticle(Guid articleId);

        void CreateNeedBasedSequence(Schedule schedule, string name, string description, DateTime startDate, 
            DateTime? endDate, Taxon taxon = null, Role role = null, int rangeInMinutesBefore = 0, int rangeInMinutesAfter = 0, Inventory inventory = null);

        void CreateIntervalBasedSequence(Schedule schedule, string name, string description, DateTime startDate, int interval, string times,
            DateTime? endDate, Taxon taxon = null, Role role = null, int rangeInMinutesBefore = 0, int rangeInMinutesAfter = 0, Inventory inventory = null);

        void CreateDatesBasedSequence(Schedule schedule, string name, string description, DateTime startDate, DateTime? endDate, string dates, string times,
            Taxon taxon = null, Role role = null, int rangeInMinutesBefore = 0, int rangeInMinutesAfter = 0, Inventory inventory = null);

        void CreateEventBasedSequence(Schedule schedule, string name, string description, DateTime startDate, DateTime? endDate,
            int interval = 0, int intervalFactor = 0, bool intervalIsDate = false, bool reminder = false, bool canRaiseAlert = false, bool overview = false, bool allDay = false, bool absent = false);

        // HACK: refactor handlers/publisher in order to remove this.
        void CreateEventBasedSequence(Guid scheduleSettingsId, Patient patient, string description,
            DateTime startDate, DateTime endDate,
            string startTime, string endTime, int interval, int intervalFactor,
            bool intervalIsDate, bool canRaiseAlert, bool overview, bool isAllDay,
            bool pauseAlerts, bool absent);

        // HACK: refactor handlers/publisher in order to remove this.
        void Update(Guid eventId, Guid scheduleSettingsId, string description, DateTime startDate, DateTime endDate,
            string startTime, string endTime, int interval, int intervalFactor,
            bool intervalIsDate, bool canRaiseAlert, bool overview, bool isAllDay, bool pauseAlerts, bool absent);

        IList<ScheduleSettings> GetCategories(bool forceGetAllCategories = false);
        Guid CreateCategory(string name);
        void Delete(Sequence sequence);
        void CreateTask(Guid eventId, Guid scheduleSettingsId, string description, DateTime startDate, DateTime endDate, string startTime, string endTime, int interval, bool canRaiseAlert, bool overview, bool isAllDay, bool pauseAlerts, bool absent);
        void DeleteActivity(Task task);
        IList<CalendarTask> FindWithinMonth(Patient patient, DateTime date);
        IList<CalendarTask> FindEventsWithinPeriod(DateTime start, DateTime end, Patient patient = null, ITaxon orgFilter = null);
        IList<CalendarTask> FindDelayedQuittanceEvents(ITaxon orgFilter = null);
        IList<CalendarWeek> Calendar(DateTime date, IList<CalendarTask> events);
        CalendarCategory Category(Guid id);
        CalendarTask GetActivityInSequence(Guid sequence, DateTime date);
    }
}