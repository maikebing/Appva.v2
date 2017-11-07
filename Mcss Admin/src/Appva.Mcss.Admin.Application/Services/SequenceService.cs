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
    using Appva.Mcss.Admin.Domain.Repositories;
    using Appva.Mcss.Admin.Application.Auditing;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface ISequenceService : IService
    {
        #region Fields

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

        void CreateNeedBasedSequence(Schedule schedule, string name, string description, DateTime startDate, bool onNeedBasis, 
            DateTime? endDate, Taxon taxon = null, Role role = null, int rangeInMinutesBefore = 0, int rangeInMinutesAfter = 0, Inventory inventory = null);

        void CreateIntervalBasedSequence(Schedule schedule, string name, string description, DateTime startDate, int interval, string times,
            DateTime? endDate, Taxon taxon = null, Role role = null, int rangeInMinutesBefore = 0, int rangeInMinutesAfter = 0, Inventory inventory = null);

        void CreateDatesBasedSequence(Schedule schedule, string name, string description, DateTime startDate, DateTime? endDate, string dates, string times,
            Taxon taxon = null, Role role = null, int rangeInMinutesBefore = 0, int rangeInMinutesAfter = 0, Inventory inventory = null);

        void CreateEventBasedSequence(Schedule schedule, string name, string description, DateTime startDate, DateTime? endDate,
            int interval = 0, int intervalFactor = 0, bool intervalIsDate = false, bool reminder = false, bool canRaiseAlert = false, bool overview = false, bool allDay = false, bool absent = false);




        //void Create(
        //    Patient patient,
        //    DateTime startDate,
        //    DateTime? endDate,
        //    Schedule schedule,
        //    String description,
        //    bool? canRaiseAlert,
        //    int interval,
        //    int intervalFactor = 0,
        //    bool intervalIsDate = false,
        //    int rangeInMinutesBefore = 0,
        //    int rangeInMinutesAfter = 0,
        //    String name = null,
        //    String times = null,
        //    String dates = null,
        //    bool onNeedBasis = false,
        //    bool reminder = false,
        //    int remindInMinutesBefore = 0,
        //    Account reminderRecipient = null,
        //    Taxon taxon = null,
        //    Role requiredRole = null,
        //    bool overView = true,
        //    bool pauseAnyAlerts = false,
        //    bool absent = false,
        //    bool allDay = false
        //);

        #endregion
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class SequenceService : ISequenceService
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ISequenceRepository"/>
        /// </summary>
        private readonly ISequenceRepository sequenceRepository;

        /// <summary>
        /// The <see cref="IScheduleRepository"/>
        /// </summary>
        private readonly IScheduleRepository scheduleRepository;

        //private readonly IScheduleService scheduleService;

        /// <summary>
        /// The <see cref="IAuditService"/>
        /// </summary>
        private readonly IAuditService auditService;

        private readonly ITaxonRepository taxonRepository;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="SequenceService"/> class.
        /// </summary>
        /// <param name="context">The <see cref="IPersistenceContext"/></param>
        public SequenceService(ISequenceRepository sequenceRepository, IScheduleRepository scheduleRepository, IAuditService auditService, ITaxonRepository taxonRepository)
        {
            this.sequenceRepository = sequenceRepository;
            this.scheduleRepository = scheduleRepository;
            this.auditService = auditService;
            this.taxonRepository = taxonRepository;
        }

        #endregion

        #region ISequenceRepository Members.

        /// <inheritdoc />
        public void CreateNeedBasedSequence(Schedule schedule, string name, string description, DateTime startDate, bool onNeedBasis, 
            DateTime? endDate, Taxon taxon = null, Role role = null, int rangeInMinutesBefore = 0, int rangeInMinutesAfter = 0, Inventory inventory = null)
        {
            this.sequenceRepository.Save(new Sequence(schedule, name, description, startDate, onNeedBasis, endDate, taxon, role, rangeInMinutesBefore, rangeInMinutesAfter, inventory));
            this.scheduleRepository.Update(schedule);
        }

        /// <inheritdoc />
        public void CreateIntervalBasedSequence(Schedule schedule, string name, string description, DateTime startDate, int interval, string times,
            DateTime? endDate, Taxon taxon = null, Role role = null, int rangeInMinutesBefore = 0, int rangeInMinutesAfter = 0, Inventory inventory = null)
        {
            this.sequenceRepository.Save(new Sequence(schedule, name, description, startDate, interval, times, endDate, taxon, role, rangeInMinutesBefore, rangeInMinutesAfter, inventory));
            this.scheduleRepository.Update(schedule);
        }

        /// <inheritdoc />
        public void CreateDatesBasedSequence(Schedule schedule, string name, string description, DateTime startDate, DateTime? endDate, string dates, string times,
            Taxon taxon = null, Role role = null, int rangeInMinutesBefore = 0, int rangeInMinutesAfter = 0, Inventory inventory = null)
        {
            this.sequenceRepository.Save(new Sequence(schedule, name, description, startDate, endDate, dates, times, taxon, role, rangeInMinutesBefore, rangeInMinutesAfter, inventory));
            this.scheduleRepository.Update(schedule);
        }

        /// <inheritdoc />
        public void CreateEventBasedSequence(Schedule schedule, string name, string description, DateTime startDate, DateTime? endDate,
            int interval = 0, int intervalFactor = 0, bool intervalIsDate = false, bool reminder = false, bool canRaiseAlert = false, bool overview = false, bool allDay = false, bool absent = false)
        {
            if (schedule.ScheduleSettings.ScheduleType == ScheduleType.Action)
            {
                canRaiseAlert = schedule.ScheduleSettings.CanRaiseAlerts;
            }
            this.sequenceRepository.Save(new Sequence(schedule, name, description, startDate, endDate, interval, intervalFactor, intervalIsDate, reminder, canRaiseAlert, overview, allDay, absent));
            this.scheduleRepository.Update(schedule);
        }

        /// <inheritdoc />
        public Sequence Find(Guid id)
        {
            return this.sequenceRepository.Get(id);
        }

        /// <inheritdoc />
        public void Update(Sequence sequence)
        {
            this.auditService.Update(
                sequence.Patient,
                "ändrade {0} (REF: {1}) i {2} (REF: {3}).",
                 sequence.Name,
                 sequence.Id,
                 sequence.Schedule.ScheduleSettings.Name,
                 sequence.Schedule.Id);

            this.sequenceRepository.Update(sequence);
        }

        #endregion

        #region IScheduleRepository Members

        /// <inheritdoc />
        public Schedule GetSchedule(Guid scheduleId)
        {
            return this.scheduleRepository.Get(scheduleId);
        }

        #endregion

        #region ITaxonRepository Members

        /// <inheritdoc />
        public Taxon GetTaxon(Guid taxonId)
        {
            return this.taxonRepository.Get(taxonId);
        }

        #endregion
    }
}