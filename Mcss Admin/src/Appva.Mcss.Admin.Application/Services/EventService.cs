// <copyright file="EventService.cs" company="Appva AB">
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
    using Appva.Core.Extensions;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Core.Utilities;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Application.Auditing;
    using Appva.Mcss.Admin.Application.Transformers;
    using Appva.Core.Logging;
    using Appva.Mcss.Admin.Domain.Models;
    using NHibernate.Criterion;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface IEventService : IService
    {
        Sequence Get(Guid id);
        IList<CalendarTask> FindWithinMonth(Patient patient, DateTime date);
        IList<CalendarTask> FindEventsWithinPeriod(DateTime start, DateTime end, Patient patient = null, ITaxon orgFilter = null);
        IList<CalendarTask> FindDelayedQuittanceEvents(ITaxon orgFilter = null);
        Guid CreateCategory(string name);

        /// <summary>
        /// Gets all categories based on users permission 
        /// </summary>
        /// <param name="forceGetAllCategories">Used to force listing of all categories, overrides permissions</param>
        /// <returns>List of <see cref="ScheduleSettings"/></returns>
        IList<ScheduleSettings> GetCategories(bool forceGetAllCategories = false);

        void Create(
            Guid scheduleSettingsId,
            Patient patient,
            string description,
            DateTime startDate,
            DateTime endDate,
            string startTime,
            string endTime,
            int interval,
            int intervalFactor,
            bool intervalIsDate,
            bool canRaiseAlert,
            bool overview,
            bool isAllDay,
            bool pauseAlerts,
            bool absent
        );

        void Update(
            Guid eventId,
            Guid scheduleSettingsId,
            string description,
            DateTime startDate,
            DateTime endDate,
            string startTime,
            string endTime,
            int interval,
            int intervalFactor,
            bool intervalIsDate,
            bool canRaiseAlert,
            bool overview,
            bool isAllDay,
            bool pauseAlerts,
            bool absent
        );

        void CreateTask(
            Guid eventId,
            Guid scheduleSettingsId,
            string description,
            DateTime startDate,
            DateTime endDate,
            string startTime,
            string endTime,
            int interval,
            bool canRaiseAlert,
            bool overview,
            bool isAllDay,
            bool pauseAlerts,
            bool absent
        );

        void DeleteSequence(Sequence sequence);
        void DeleteActivity(Task task);
        IList<CalendarWeek> Calendar(DateTime date, IList<CalendarTask> events);

        /// <summary>
        /// Gets the activity for a specific date
        /// </summary>
        /// <param name="Sequence"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        CalendarTask GetActivityInSequence(Guid Sequence, DateTime date);

        /// <summary>
        /// Returns a calendar category
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        CalendarCategory Category(Guid id);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class EventService : IEventService
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IScheduleService"/>.
        /// </summary>
        private readonly IScheduleService scheduleService;

        /// <summary>
        /// The <see cref="ISequenceService"/>.
        /// </summary>
        private readonly ISequenceService sequenceService;

        /// <summary>
        /// The <see cref="IIdentityService"/>.
        /// </summary>
        private readonly IIdentityService identityService;

        /// <summary>
        /// The <see cref="IAccountService"/>.
        /// </summary>
        private readonly IAccountService accountService;

        /// <summary>
        /// The <see cref="ITaskService"/>
        /// </summary>
        private readonly ITaskService taskService;

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext context;

        /// <summary>
        /// The <see cref="IAuditService"/>.
        /// </summary>
        private readonly IAuditService auditing;

        /// <summary>
        /// The <see cref="ILog"/>.
        /// </summary>
        private static readonly ILog Log = LogProvider.For<EventService>();

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="EventService"/> class.
        /// </summary>
        /// <param name="scheduleService">The <see cref="IScheduleService"/></param>
        /// <param name="sequenceService">The <see cref="ISequenceService"/></param>
        /// <param name="identityService">The <see cref="IIdentityService"/></param>
        /// <param name="accountService">The <see cref="IAccountService"/></param>
        /// <param name="taskService">The <see cref="ITaskService"/></param>
        /// <param name="context">The <see cref="IPersistenceContext"/></param>
        public EventService(
            IScheduleService scheduleService,
            ISequenceService sequenceService,
            IIdentityService identityService,
            IAccountService accountService,
            ITaskService taskService,
            IPersistenceContext context,
            IAuditService auditing)
        {
            this.scheduleService = scheduleService;
            this.sequenceService = sequenceService;
            this.identityService = identityService;
            this.accountService = accountService;
            this.taskService = taskService;
            this.auditing = auditing;
            this.context = context;
        }

        #endregion

        #region Public Functions.

        /// <summary>
        /// Returns a single event by id.
        /// </summary>
        /// <param name="id"></param>
        public Sequence Get(Guid id)
        {
            return this.context.Get<Sequence>(id);
        }

        /// <summary>
        /// Returns all events for a patient within specified month and year.
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="date"></param>
        public IList<CalendarTask> FindWithinMonth(Patient patient, DateTime date)
        {
            var firstInMonth = date.FirstOfMonth();
            var lastInMonth  = firstInMonth.AddDays(DateTime.DaysInMonth(firstInMonth.Year, firstInMonth.Month));
            return this.FindEventsWithinPeriod(firstInMonth, lastInMonth, patient: patient);
        }

        /// <summary>
        /// Returns all events for a patient within specified period.
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public IList<CalendarTask> FindEventsWithinPeriod(DateTime start, DateTime end, Patient patient = null, ITaxon orgFilter = null)
        {
            var account          = this.accountService.Find(this.identityService.PrincipalId);
            var scheduleSettings = TaskService.CalendarRoleScheduleSettingsList(account);
            Taxon taxonAlias     = null;
            Patient patientAlias = null;
            //// Create the queries
            var sequences = this.context.QueryOver<Sequence>()
                .Where(x => x.IsActive)
                  .And(x => x.Interval != 0 || (x.EndDate >= start && x.StartDate <= end));
             var tasks = this.context.QueryOver<Task>()
                .Where(x => x.IsActive)
                  .And(x => x.EndDate >= start && x.StartDate <= end);
            var tasksFromCanceledSequence = this.context.QueryOver<Task>()
                .Where(x => x.IsActive)
                  .And(x => x.EndDate >= start && x.StartDate <= end);
            //// If filtered by patient, add to query
            if(patient != null)
            {
                sequences = sequences.Where(x => x.Patient == patient);
                tasks = tasks.Where(x => x.Patient == patient);
                tasksFromCanceledSequence = tasksFromCanceledSequence.Where(x => x.Patient == patient);
            }
            //// If org-filter is Active, filter on taxon
            if(orgFilter != null)
            {
                sequences = sequences.JoinAlias(x => x.Patient, () => patientAlias)
                    .JoinAlias(() => patientAlias.Taxon, () => taxonAlias)
                    .Where(Restrictions.On<Taxon>(x => taxonAlias.Path)
                        .IsLike(orgFilter.Id.ToString(), MatchMode.Anywhere))
                    .And(() => patientAlias.IsActive && !patientAlias.Deceased);
                tasks = tasks.JoinAlias(x => x.Patient, () => patientAlias)
                    .JoinAlias(() => patientAlias.Taxon, () => taxonAlias)
                    .Where(Restrictions.On<Taxon>(x => taxonAlias.Path)
                        .IsLike(orgFilter.Id.ToString(), MatchMode.Anywhere))
                    .And(() => patientAlias.IsActive && !patientAlias.Deceased);
                tasksFromCanceledSequence = tasksFromCanceledSequence.JoinAlias(x => x.Patient, () => patientAlias)
                    .JoinAlias(() => patientAlias.Taxon, () => taxonAlias)
                    .Where(Restrictions.On<Taxon>(x => taxonAlias.Path)
                        .IsLike(orgFilter.Id.ToString(), MatchMode.Anywhere))
                    .And(() => patientAlias.IsActive && !patientAlias.Deceased);
            }
            var sequencesListed = sequences.JoinQueryOver<Schedule>(x => x.Schedule)
                .JoinQueryOver<ScheduleSettings>(x => x.ScheduleSettings)
                    .WhereRestrictionOn(x => x.Id).IsIn(scheduleSettings.Select(x => x.Id).ToArray())
                    .And(s => s.ScheduleType == ScheduleType.Calendar)
                .List();
            var tasksListed = tasks.JoinQueryOver<Schedule>(x => x.Schedule)
                .JoinQueryOver<ScheduleSettings>(x => x.ScheduleSettings)
                    .WhereRestrictionOn(x => x.Id).IsIn(scheduleSettings.Select(x => x.Id).ToArray())
                    .And(s => s.ScheduleType == ScheduleType.Calendar)
                .List();
            var tasksFromCanceledSequenceListed = tasksFromCanceledSequence.JoinQueryOver<Sequence>(x => x.Sequence)
                    .Where(x => !x.IsActive)
                    .JoinQueryOver<Schedule>(x => x.Schedule)
                    .JoinQueryOver<ScheduleSettings>(x => x.ScheduleSettings)
                        .WhereRestrictionOn(x => x.Id).IsIn(scheduleSettings.Select(x => x.Id).ToArray())
                        .And(s => s.ScheduleType == ScheduleType.Calendar)
                    .List();
            var retval = new List<CalendarTask>();
            retval.AddRange(EventTransformer.TasksToEvent(tasksFromCanceledSequenceListed));
            foreach (var sequence in sequencesListed)
            {
                retval.AddRange(GetActivitiesWithinPeriodFor(sequence, start, end, tasksListed.Where(x => x.Sequence.Id == sequence.Id).ToList()));
            }
            return retval;
        }

        /// <summary>
        /// Finds all not quittanced activities before today
        /// </summary>
        /// <param name="orgFilter"></param>
        /// <returns></returns>
        public IList<CalendarTask> FindDelayedQuittanceEvents(ITaxon orgFilter = null)
        {
            var account          = this.accountService.Find(this.identityService.PrincipalId);
            var scheduleSettings = TaskService.CalendarRoleScheduleSettingsList(account);
            Taxon taxonAlias     = null;
            Patient patientAlias = null;
            var tasks = this.context.QueryOver<Task>()
                .Where(x => x.IsActive)
                  .And(x => x.EndDate < DateTime.Now.Date)
                .JoinAlias(x => x.Patient, () => patientAlias)
                    .Where(() => patientAlias.IsActive)
                      .And(() => !patientAlias.Deceased)
                .JoinAlias(() => patientAlias.Taxon, () => taxonAlias)
                    .Where(Restrictions.On<Taxon>(x => taxonAlias.Path)
                        .IsLike(orgFilter.Id.ToString(), MatchMode.Anywhere))
                .JoinQueryOver<Schedule>(x => x.Schedule)
                .JoinQueryOver<ScheduleSettings>(x => x.ScheduleSettings)
                    .WhereRestrictionOn(x => x.Id).IsIn(scheduleSettings.Select(x => x.Id).ToArray())
                      .And(s => s.ScheduleType == ScheduleType.Calendar)
                .List();
            return EventTransformer.TasksToEvent(tasks);

        }

        /// <summary>
        /// Clone an activity task
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        private static Task CloneActivity(Task task)
        {
            return new Task(task.Id)
            {
                Name                   = task.Name,
                Absent                 = task.Absent,
                AllDay                 = task.AllDay,
                CanRaiseAlert          = task.CanRaiseAlert,
                IsCompleted            = task.IsCompleted,
                Delayed                = task.Delayed,
                DelayHandled           = task.DelayHandled,
                EndDate                = task.EndDate,
                Overview               = task.Overview,
                Patient                = task.Patient,
                CompletedBy            = task.CompletedBy,
                CompletedDate          = task.CompletedDate,
                CurrentEscalationLevel = task.CurrentEscalationLevel,
                PauseAnyAlerts         = task.PauseAnyAlerts,
                DelayHandledBy         = task.DelayHandledBy,
                Schedule               = task.Schedule,
                Scheduled              = task.Scheduled,
                Sequence               = task.Sequence,
                StartDate              = task.StartDate,
                Status                 = task.Status,
                Taxon                  = task.Taxon
            };
        }

        /// <summary>
        /// Creates a category
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Guid CreateCategory(string name)
        {
            var category = new ScheduleSettings
            {
                Name                  = name,
                ScheduleType          = ScheduleType.Calendar,
                CanRaiseAlerts        = false,
                CountInventory        = false,
                HasInventory          = false,
                HasSetupDrugsPanel    = false,
                IsPausable            = false,
                MachineName           = name.Substring(0, 4),
                NurseConfirmDeviation = false
            };
            var retval = this.context.Save(category);
            return (Guid) retval;
        }

        /// <inheritdoc />
        public IList<ScheduleSettings> GetCategories(bool forceGetAllCategories = false)
        {
            var account = this.accountService.Find(this.identityService.PrincipalId);
            var categories = this.context.QueryOver<ScheduleSettings>()
                .Where(x => x.ScheduleType == ScheduleType.Calendar)
                .And(x => x.IsActive);

            if (!forceGetAllCategories)
            {
                var scheduleSettings = TaskService.CalendarRoleScheduleSettingsList(account);
                categories.WhereRestrictionOn(x => x.Id).IsIn(scheduleSettings.Select(x => x.Id).ToArray());
            }

            return categories.OrderBy(x => x.Name).Asc
                .List();
        }

        /// <summary>
        /// Creates an event.
        /// </summary>
        public void Create(
            Guid scheduleSettingsId,
            Patient patient,
            string description,
            DateTime startDate,
            DateTime endDate,
            string startTime,
            string endTime,
            int interval,
            int intervalFactor,
            bool intervalIsDate,
            bool canRaiseAlert,
            bool overview,
            bool isAllDay,
            bool pauseAlerts,
            bool absent
        )
        {
            var schedule = this.context.QueryOver<Schedule>()
                .Where(x => x.Patient == patient)
                  .And(x => x.IsActive)
                .JoinQueryOver<ScheduleSettings>(x => x.ScheduleSettings)
                    .Where(x => x.Id == scheduleSettingsId)
                .SingleOrDefault();
            if (schedule.IsNull())
            {
                var scheduleID = this.context.Save<Schedule>(new Schedule()
                {
                    Patient = patient,
                    ScheduleSettings = this.context.Get<ScheduleSettings>(scheduleSettingsId)
                });
                schedule = this.context.Get<Schedule>(scheduleID);
            }
            this.sequenceService.Create(
                patient,
                this.GetDateTimeWithHourAndMinutes(startDate, startTime, isAllDay ? "00:00" : null),
                this.GetDateTimeWithHourAndMinutes(endDate,   endTime,   isAllDay ? "23:59" : null),
                schedule,
                description,
                canRaiseAlert,
                interval,
                intervalFactor,
                intervalIsDate,
                name: schedule.ScheduleSettings.Name,
                overView: overview,
                pauseAnyAlerts: pauseAlerts,
                absent: absent,
                allDay: isAllDay
            );
        }

        /// <summary>
        /// Updates an event.
        /// </summary>
        /// <param name="evt"></param>
        /// <param name="description"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="isAllDay"></param>
        /// <param name="pauseAlerts"></param>
        /// <param name="absent"></param>
        public void Update(
            Guid eventId,
            Guid scheduleSettingsId,
            string description,
            DateTime startDate,
            DateTime endDate,
            string startTime,
            string endTime,
            int interval,
            int intervalFactor,
            bool intervalIsDate,
            bool canRaiseAlert,
            bool overview,
            bool isAllDay,
            bool pauseAlerts,
            bool absent
        )
        {
            var evt = Get(eventId);
            if (scheduleSettingsId.NotEqual(evt.Schedule.ScheduleSettings.Id))
            {
                var schedule = this.context.QueryOver<Schedule>()
                    .Where(x => x.Patient == evt.Patient)
                    .And(x => x.IsActive)
                    .JoinQueryOver<ScheduleSettings>(x => x.ScheduleSettings)
                        .Where(x => x.Id == scheduleSettingsId)
                    .SingleOrDefault();
                if (schedule.IsNull())
                {
                    schedule = new Schedule
                    {
                        Patient = evt.Patient,
                        ScheduleSettings = this.context.Get<ScheduleSettings>(scheduleSettingsId)
                    };
                    this.context.Update(schedule);
                }
                evt.Schedule = schedule;
            }
            evt.Name = evt.Schedule.ScheduleSettings.Name;
            evt.Description = description;
            evt.StartDate = this.GetDateTimeWithHourAndMinutes(startDate, startTime, isAllDay ? "00:00" : null);
            evt.EndDate   = this.GetDateTimeWithHourAndMinutes(endDate,   endTime,   isAllDay ? "23:59" : null);
            evt.Interval  = interval;
            evt.IntervalFactor = intervalFactor;
            evt.IntervalIsDate = intervalIsDate;
            evt.CanRaiseAlert = canRaiseAlert;
            evt.Overview = overview;
            evt.AllDay = isAllDay;
            evt.PauseAnyAlerts = pauseAlerts;
            evt.Absent = absent;
            this.context.Update(evt);
            this.auditing.Update(
                evt.Patient,
                "ändrade aktiviteten {0} till {1:yyyy-MM-dd HH:mm} - {2:yyyy-MM-dd HH:mm} (REF: {3}).",
                evt.Description,
                evt.StartDate,
                evt.EndDate,
                evt.Id);
        }

        /// <summary>
        /// Creates a singel activity beloging to a sequence
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="scheduleSettingsId"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="interval"></param>
        /// <param name="canRaiseAlert"></param>
        /// <param name="overview"></param>
        /// <param name="isAllDay"></param>
        /// <param name="pauseAlerts"></param>
        /// <param name="absent"></param>
        public void CreateTask(
            Guid eventId,
            Guid scheduleSettingsId,
            string description,
            DateTime startDate,
            DateTime endDate,
            string startTime,
            string endTime,
            int interval,
            bool canRaiseAlert,
            bool overview,
            bool isAllDay,
            bool pauseAlerts,
            bool absent
        )
        {
            var sequence = Get(eventId);
            var task = new Task();
            task.IsActive = true;
            task.CreatedAt = DateTime.Now;
            task.UpdatedAt = DateTime.Now;
            task.Sequence = sequence;
            task.Patient = sequence.Patient;
            task.Schedule = sequence.Schedule;
            task.Taxon = sequence.Patient.Taxon;
            task.Status = 0;
            task.RangeInMinutesAfter = 0;
            task.RangeInMinutesBefore = 0;
            task.Absent = absent;
            task.AllDay = isAllDay;
            task.CanRaiseAlert = canRaiseAlert;
            task.Overview = overview;
            task.PauseAnyAlerts = pauseAlerts;
            task.StartDate = startDate.AddHours(Double.Parse(startTime.Split(':')[0])).AddMinutes(Double.Parse(startTime.Split(':')[1]));
            task.EndDate = endDate.AddHours(Double.Parse(endTime.Split(':')[0])).AddMinutes(Double.Parse(endTime.Split(':')[1]));
            task.Scheduled = (DateTime)task.EndDate;
            this.context.Save(task);
        }

        public void DeleteSequence(Sequence sequence)
        {
            sequence.IsActive = false;
            this.context.Update(sequence);
        }

        public void DeleteActivity(Task task)
        {
            this.context.Delete(task);
        }

        public IList<CalendarWeek> Calendar(DateTime date, IList<CalendarTask> events)
        {
            var retval = new List<CalendarWeek>();
            var current = date;
            while (current <= date.LastOfMonth())
            {
                retval.Add(CreateWeek(current, events));
                current = current.FirstDateOfWeek().AddDays(7);
            }

            return retval;
        }

        private CalendarWeek CreateWeek(DateTime date, IList<CalendarTask> events)
        {
            var week = new CalendarWeek()
            {
                WeekNumber = date.GetWeekNumber(),
                Days = new List<CalendarDay>(),
                AllEvents = new List<CalendarTask>()
            };

            var current = date.FirstDateOfWeek();
            for (var day = 0; day < 7; day++)
            {
                var d = this.CreateDay(current, date.Month, events);
                week.Days.Add(d);
                week.AllEvents.AddRange(d.Events);
                current = current.AddDays(1);
            }

            return week;
        }

        private CalendarDay CreateDay(DateTime date, int currentMonthDisplayed, IList<CalendarTask> events)
        {
            if (events.IsNull())
            {
                events = new List<CalendarTask>();
            }
            return new CalendarDay()
            {
                IsWithinMonth = date.Month == currentMonthDisplayed,
                IsToday = date.Equals(DateTime.Today),
                Events = date.DayOfWeek.Equals(DayOfWeek.Monday) ? events.Where(x => x.StartTime.Date <= date.Date && x.EndTime.Date >= date.Date).ToList() : events.Where(x => x.StartTime.Date == date.Date).ToList(),
                NumberOfEvents = events.Where(x => x.StartTime.Date <= date.Date && x.EndTime.Date >= date.Date).Count(),
                Date = date
            };
        }

        private IList<CalendarDay> CalendarDaysInMonth(DateTime date, int daysInMonth, IList<CalendarTask> events)
        {
            IList<CalendarDay> retval = new List<CalendarDay>();
            for (var i = 0; i < daysInMonth; i++)
            {
                var day = date.AddDays(i);
                var calendar = new CalendarDay()
                {
                    IsWithinMonth = true,
                    IsToday = day.Equals(DateTime.Today),
                    Events = events.Where(x => x.StartTime.Date == day.Date).ToList(),
                    Date = day
                };
                retval.Add(calendar);
            }
            return retval;
        }

        private IList<CalendarDay> CalendarDaysBefore(DateTime date, int daysInMonth)
        {
            var retval = new List<CalendarDay>();
            var days = date.AddDays(-(DateTimeUtilities.FirstDayOfWeek() - date.DayOfWeek)).Subtract(date).Days;
            days = (days.LessThan(0)) ? days.Add(7) : days;
            for (var i = 0; i < days; i++)
            {
                retval.Add(new CalendarDay()
                {
                    Date = date.AddDays(i.Add(1).Negate())
                });
            }
            retval.Reverse();
            return retval;
        }

        private IList<CalendarDay> CalendarDaysAfter(DateTime date, int daysInMonth)
        {
            IList<CalendarDay> retval = new List<CalendarDay>();
            var lastDateOfMonth = date.AddDays(daysInMonth.Subtract(1));
            var days = lastDateOfMonth.AddDays((DateTimeUtilities.FirstDayOfWeek() - lastDateOfMonth.DayOfWeek).Add(7)).Subtract(date).Days - daysInMonth;
            days = (days.Equals(7)) ? 0 : days;
            for (var i = 0; i < days; i++)
            {
                retval.Add(new CalendarDay()
                {
                    Date = lastDateOfMonth.AddDays(i.Add(1))
                });
            }
            return retval;
        }

        /// <inheritdoc />
        public CalendarTask GetActivityInSequence(Guid sequence, DateTime date)
        {
            var task = this.taskService.List(new ListTaskModel {
                SequenceId           = sequence,
                EndDate              = date,
                StartDate            = date,
                IncludeCalendarTasks = true
            }).Items.FirstOrDefault();
            if(task.IsNotNull())
            {
                return EventTransformer.TasksToEvent(task);
            }
            var s = this.sequenceService.Find(sequence);
            var endDate = s.EndDate.GetValueOrDefault();
            while (endDate <= date)
            {
                if (endDate.Date.Equals(date.Date))
                {
                    return EventTransformer.SequenceToEvent(s, endDate.AddDays((s.StartDate - s.EndDate.GetValueOrDefault()).TotalDays), endDate);
                }
                endDate = s.GetNextDateInSequence(endDate);
            }
            throw new Exception("There is no event for this sequence on given date");
        }

        /// <inheritdoc />
        public CalendarCategory Category(Guid id)
        {
            var setting = this.scheduleService.GetScheduleSettings(id);
            return new CalendarCategory
            {
                Id              = setting.Id,
                Name            = setting.Name,
                Absence         = setting.Absence,
                Color           = setting.Color,
                StatusTaxons    = setting.StatusTaxons,
                NurseConfirmDeviation       = setting.NurseConfirmDeviation,
                ConfirmDevitationMessage    = new ConfirmDeviationMessage(setting.NurseConfirmDeviationMessage, setting.SpecificNurseConfirmDeviation)
            };
        }

        #endregion

        #region Private Functions.

        private DateTime GetDateTimeWithHourAndMinutes(DateTime dateTime, string timeStr, string defaultTimeStr = null)
        {
            if (CanGetValueFromTimeString(timeStr))
            {
                return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, this.GetHour(timeStr).Value, this.GetMinute(timeStr).Value, 0);
            }
            if (defaultTimeStr.IsNotEmpty())
            {
                return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, this.GetHour(defaultTimeStr).Value, this.GetMinute(defaultTimeStr).Value, 0);
            }
            throw new ArgumentException(string.Format("Timestring {0} is incorrect", timeStr));
        }

        public bool CanGetValueFromTimeString(string timeStr)
        {
            return this.GetHour(timeStr) != null && this.GetMinute(timeStr) != null;
        }

        /// <summary>
        /// Returns the hour from a string, e.g. hour 14 from 14:26.
        /// </summary>
        /// <param name="time"></param>
        private int? GetHour(string time)
        {
            return this.GetTime(time, 0);
        }

        /// <summary>
        /// Returns the minutes from a string, e.g. minutes 26 from 14:26.
        /// </summary>
        /// <param name="time"></param>
        private int? GetMinute(string time)
        {
            return this.GetTime(time, 1);
        }

        /// <summary>
        /// Splits a time by ":" and returns the index. 0 eguals hours and 1 equals minutes.
        /// </summary>
        /// <param name="time"></param>
        /// <param name="index"></param>
        private int? GetTime(string time, int index)
        {
            if (time.IsNotEmpty())
            {
                var times = time.Split(':', '.');
                if (times.Length == 2)
                {
                    int retval;
                    if (int.TryParse(times[index], out retval))
                    {
                        return retval;
                    }
                }
            }
            return null;
        }

        private static IList<CalendarTask> GetActivitiesWithinPeriodFor(Sequence sequence, DateTime periodStart, DateTime periodEnd, IList<Task> tasks)
        {
            var startDate = sequence.StartDate;
            var endDate = sequence.EndDate.GetValueOrDefault();

            var retval = new List<CalendarTask>();
            
            while(startDate <= periodEnd)
            {
                if (endDate >= periodStart)
                {
                    var calendarTask = GetCalendarTaskFor(sequence, startDate, endDate, tasks);
                    if (calendarTask.IsNotNull())
                    {
                        retval.Add(calendarTask);
                    }
                }
                if (sequence.Interval == 0)
                {
                    break;
                }
                startDate = sequence.GetNextDateInSequence(startDate);
                endDate = sequence.GetNextDateInSequence(endDate);                
            }

            return retval;
        }

        

        private static CalendarTask GetCalendarTaskFor(Sequence sequence, DateTime startDate, DateTime endDate, IList<Task> tasks)
        {
            var task = tasks.Where(x => x.Sequence.Id == sequence.Id && x.Scheduled == endDate && x.IsActive).FirstOrDefault();
            if (task.IsNotNull())
            {
                return EventTransformer.TasksToEvent(task);
            }
            else if (endDate > DateTime.Now)
            {
                return EventTransformer.SequenceToEvent(sequence, startDate, endDate);
            }
            return null;
        }

        #endregion 
    }
}