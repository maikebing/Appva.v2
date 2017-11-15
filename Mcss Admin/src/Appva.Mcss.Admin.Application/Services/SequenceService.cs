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
    using Appva.Domain;

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

        void CreateNeedBasedSequence(Schedule schedule, string name, string description, DateTime startDate, 
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
        public void CreateNeedBasedSequence(Schedule schedule, string name, string description, DateTime startDate, 
            DateTime? endDate, Taxon taxon = null, Role role = null, int rangeInMinutesBefore = 0, int rangeInMinutesAfter = 0, Inventory inventory = null)
        {
            var repeat = new Repeat(
                startAt: (Date) startDate,
                endAt: (Date) endDate,
                period: null,
                periodUnit: UnitOfTime.Day,
                duration: null,
                durationUnit: null, 
                offsetBefore: rangeInMinutesBefore, 
                offsetAfter: rangeInMinutesAfter, 
                isNeedBased: true, 
                timesOfDay: null, 
                daysOfWeek: null, 
                flags: null, 
                boundsRange: null                
            );
            this.sequenceRepository.Save(new Sequence(schedule, name, description, repeat, taxon, role, inventory));
            this.scheduleRepository.Update(schedule);
        }

        /// <inheritdoc />
        public void CreateIntervalBasedSequence(Schedule schedule, string name, string description, DateTime startDate, int interval, string times,
            DateTime? endDate, Taxon taxon = null, Role role = null, int rangeInMinutesBefore = 0, int rangeInMinutesAfter = 0, Inventory inventory = null)
        {
            if (endDate.HasValue == false)
            {

            }

            var repeat = new Repeat(
                startDate,
                endDate,
                interval,
                UnitOfTime.Day,
                //null,
                //null,
                rangeInMinutesBefore,
                rangeInMinutesAfter,
                times.Split(',').Select(x => TimeOfDay.Parse(x)).ToList(),
                null,
                false,
                true,
                false
            );

            this.sequenceRepository.Save(new Sequence(schedule, name, description, repeat, taxon, role, inventory));
            this.scheduleRepository.Update(schedule);
        }

        /// <inheritdoc />
        public void CreateDatesBasedSequence(Schedule schedule, string name, string description, DateTime startDate, DateTime? endDate, string dates, string times,
            Taxon taxon = null, Role role = null, int rangeInMinutesBefore = 0, int rangeInMinutesAfter = 0, Inventory inventory = null)
        {
            var repeat = new Repeat(
                (Date) startDate,
                (Date) endDate,
                null,
                UnitOfTime.Day,
                null,
                null,
                rangeInMinutesBefore,
                rangeInMinutesAfter,
                false,
                times.Split(',').Select(x => TimeOfDay.Parse(x)).ToList(),
                null,
                null,
                dates.Split(',').Select(x => Date.Parse(x)).ToList()
            );

            this.sequenceRepository.Save(new Sequence(schedule, name, description, repeat, taxon, role, inventory));
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
            // DateTime startAt, DateTime endAt, int interval, int intervalFactor, int offsetBefore, int offsetAfter, List<TimeOfDay> timesOfDay, List<Date> boundsRange, bool isNeedBased, bool isIntervalDate, bool isAllDay;
            var repeat = new Repeat(
                startAt: startDate,
                endAt: endDate,
                interval: interval,
                intervalFactor: intervalFactor,
                offsetBefore: 0,
                offsetAfter: 0,
                timesOfDay: null,
                boundsRange: null,
                isNeedBased: false,
                isIntervalDate: intervalIsDate,
                isAllDay: allDay
            );

            //var repeat = new Repeat(
            //    startAt: (Date)startDate,
            //    endAt: (Date)endDate,
            //    period: interval,
            //    periodUnit: UnitOfTime.Parse(intervalFactor.ToString()),
            //    duration: null,
            //    durationUnit: null,
            //    offsetBefore: 0,
            //    offsetAfter: 0,
            //    isNeedBased: false,
            //    timesOfDay: null,
            //    daysOfWeek: null,
            //    flags: null,
            //    boundsRange: null
            //);
            this.sequenceRepository.Save(new Sequence(
                schedule: schedule,
                name: name,
                description: description, 
                repeat: repeat,
                taxon: null,
                role: null,
                inventory: null,
                refillModel: null,
                overview: overview,
                canRaiseAlert: canRaiseAlert,
                pauseAnyAlerts: false,
                absent: absent, 
                reminder: reminder)); // interval, intervalFactor, intervalIsDate,
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