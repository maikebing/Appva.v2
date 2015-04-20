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

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface IEventService
    {
        Sequence Get(Guid id);
        IList<Task> FindWithinMonth(Patient patient, DateTime date);
        Guid CreateCategory(string name);
        IList<ScheduleSettings> GetCategories();
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
        IList<Calendar> Calendar(DateTime date, IList<Task> events);
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
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext context;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="EventService"/> class.
        /// </summary>
        /// <param name="scheduleService">The <see cref="IScheduleService"/></param>
        /// <param name="sequenceService">The <see cref="ISequenceService"/></param>
        /// <param name="identityService">The <see cref="IIdentityService"/></param>
        /// <param name="accountService">The <see cref="IAccountService"/></param>
        /// <param name="context">The <see cref="IPersistenceContext"/></param>
        public EventService(
            IScheduleService scheduleService, 
            ISequenceService sequenceService,
            IIdentityService identityService,
            IAccountService accountService,
            IPersistenceContext context)
        {
            this.scheduleService = scheduleService;
            this.sequenceService = sequenceService;
            this.identityService = identityService;
            this.accountService = accountService;
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
        public IList<Task> FindWithinMonth(Patient patient, DateTime date)
        {
            var account = this.accountService.Find(this.identityService.PrincipalId);
            var roles = account.Roles;
            var list = new List<ScheduleSettings>();
            foreach (var role in roles)
            {
                var schedules = role.ScheduleSettings;
                foreach (var schedule in schedules)
                {
                    if (schedule.ScheduleType == ScheduleType.Calendar)
                    {
                        list.Add(schedule);
                    }
                }
            }

            var firstInMonth = date.FirstOfMonth();
            var lastInMonth = firstInMonth.AddDays(DateTime.DaysInMonth(firstInMonth.Year, firstInMonth.Month));
            var q1 = this.context.QueryOver<Sequence>()
                .Where(x => x.Patient == patient)
                .And(x => x.IsActive)
                .And(x => x.Interval != 0 || x.EndDate >= firstInMonth || x.StartDate <= lastInMonth)
                .JoinQueryOver<Schedule>(x => x.Schedule)
                .JoinQueryOver<ScheduleSettings>(s => s.ScheduleSettings)
                    .Where(s => s.ScheduleType == ScheduleType.Calendar);
            if (list.Count > 0)
            {
                q1.WhereRestrictionOn(x => x.Id).IsIn(list.Select(x => x.Id).ToArray());
            }
            var sequences = q1.List();
            var q2 = this.context.QueryOver<Task>()
                .Where(x => x.IsActive)
                .And(x => x.Patient == patient)
                .And(x => x.Scheduled >= firstInMonth && x.Scheduled <= lastInMonth)
                .JoinQueryOver<Schedule>(x => x.Schedule)
                .JoinQueryOver<ScheduleSettings>(x => x.ScheduleSettings)
                    .Where(x => x.ScheduleType == ScheduleType.Calendar);
            if (list.Count > 0)
            {
                q2.WhereRestrictionOn(x => x.Id).IsIn(list.Select(x => x.Id).ToArray());
            }
            var tasks = q2.List();
            var retval = this.scheduleService.FindTasks(firstInMonth, lastInMonth, new List<Schedule>(), sequences, tasks, new List<Task>());
            foreach (var task in retval.Where(x => x.StartDate.GetValueOrDefault().Date != x.EndDate.GetValueOrDefault().Date).ToList())
            {
                for (DateTime d = task.StartDate.GetValueOrDefault().AddDays(1); d.Date < task.EndDate.GetValueOrDefault().Date; d = d.AddDays(1))
                {
                    var newTask = CloneActivity(task);
                    newTask.Scheduled = d;
                    newTask.AllDay = true;
                    retval.Add(newTask);
                }
                var firstTask = CloneActivity(task);
                firstTask.EndDate = firstTask.StartDate.GetValueOrDefault().LastInstantOfDay();
                firstTask.Scheduled = firstTask.StartDate.GetValueOrDefault().LastInstantOfDay();
                retval.Add(firstTask);
                task.StartDate = task.StartDate.Value.Date;
                task.Scheduled = task.EndDate.GetValueOrDefault();
            }

            return retval;

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
                Name = task.Name,
                Absent = task.Absent,
                AllDay = task.AllDay,
                CanRaiseAlert = task.CanRaiseAlert,
                IsCompleted = task.IsCompleted,
                Delayed = task.Delayed,
                DelayHandled = task.DelayHandled,
                EndDate = task.EndDate,
                Overview = task.Overview,
                Patient = task.Patient,
                CompletedBy = task.CompletedBy,
                CompletedDate = task.CompletedDate,
                CurrentEscalationLevel = task.CurrentEscalationLevel,
                PauseAnyAlerts = task.PauseAnyAlerts,
                DelayHandledBy = task.DelayHandledBy,
                Schedule = task.Schedule,
                Scheduled = task.Scheduled,
                Sequence = task.Sequence,
                StartDate = task.StartDate,
                Status = task.Status,
                Taxon = task.Taxon
            };
        }

        /// <summary>
        /// Creates a category
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Guid CreateCategory(string name)
        {
            var category = new ScheduleSettings()
            {
                Name = name,
                ScheduleType = ScheduleType.Calendar,
                CanRaiseAlerts = false,
                CountInventory = false,
                HasInventory = false,
                HasSetupDrugsPanel = false,
                IsPausable = false,
                MachineName = name.Substring(0, 4),
                NurseConfirmDeviation = false
            };
            var retval = this.context.Save(category);
            return (Guid)retval;
        }

        public IList<ScheduleSettings> GetCategories()
        {
            var account = this.accountService.Find(this.identityService.PrincipalId);
            var roles = account.Roles;
            var list = new List<ScheduleSettings>();
            foreach (var role in roles)
            {
                var schedules = role.ScheduleSettings;
                foreach (var schedule in schedules)
                {
                    if (schedule.ScheduleType == ScheduleType.Calendar)
                    {
                        list.Add(schedule);
                    }
                }
            }
            var query = this.context.QueryOver<ScheduleSettings>()
                .Where(x => x.IsActive)
                .And(x => x.ScheduleType == ScheduleType.Calendar)
                .OrderBy(x => x.Name).Asc;
            if (list.Count > 0)
            {
                query.WhereRestrictionOn(x => x.Id).IsIn(list.Select(x => x.Id).ToArray());
            }
            return query.List();
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
            Schedule schedule = this.context.QueryOver<Schedule>()
                .Where(x => x.Patient == patient)
                .And(x => x.IsActive)
                .JoinQueryOver<ScheduleSettings>(x => x.ScheduleSettings)
                    .Where(x => x.Id == scheduleSettingsId)
                .SingleOrDefault();

            if (schedule.IsNull())
            {
                schedule = new Schedule()
                {
                    Patient = patient,
                    ScheduleSettings = this.context.Get<ScheduleSettings>(scheduleSettingsId)
                };
            }
            if (isAllDay)
            {
                startTime = "00:00";
                endTime = "23:59";
            }
            var start = new DateTime(
                startDate.Year,
                startDate.Month,
                startDate.Day,
                Int32.Parse(startTime.Split(':')[0]),
                Int32.Parse(startTime.Split(':')[1]),
                0
            );
            var end = new DateTime(
                endDate.Year,
                endDate.Month,
                endDate.Day,
                Int32.Parse(endTime.Split(':')[0]),
                Int32.Parse(endTime.Split(':')[1]),
                0
            );
            this.sequenceService.Create(
                patient,
                start,
                end,
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
                    schedule = new Schedule()
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
            evt.StartDate = new DateTime(startDate.Year, startDate.Month, startDate.Day, Int32.Parse(startTime.Split(':')[0]), Int32.Parse(startTime.Split(':')[1]), 0);
            evt.EndDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, Int32.Parse(endTime.Split(':')[0]), Int32.Parse(endTime.Split(':')[1]), 0);
            evt.Interval = interval;
            evt.IntervalFactor = intervalFactor;
            evt.IntervalIsDate = intervalIsDate;
            evt.CanRaiseAlert = canRaiseAlert;
            evt.Overview = overview;
            evt.AllDay = isAllDay;
            evt.PauseAnyAlerts = pauseAlerts;
            evt.Absent = absent;
            this.context.Update(evt);
            /*var currentUser = this.accountService.Find(this.identityService.PrincipalId);;
            LogService.Info(string.Format("Användare {0} ändrade aktiviteten {1} till {2:yyyy-MM-dd HH:mm} - {3:yyyy-MM-dd HH:mm} (REF: {4}).",
                currentUser.UserName,
                evt.Description,
                evt.StartDate,
                evt.EndDate,
                evt.Id),
                currentUser, evt.Patient, LogType.Write);*/
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

        public IList<Calendar> Calendar(DateTime date, IList<Task> events)
        {
            var retval = new List<Calendar>();
            var daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
            retval.AddRange(CalendarDaysBefore(date, daysInMonth));
            retval.AddRange(CalendarDaysInMonth(date, daysInMonth, events));
            retval.AddRange(CalendarDaysAfter(date, daysInMonth));
            return retval;
        }

        private IList<Calendar> CalendarDaysInMonth(DateTime date, int daysInMonth, IList<Task> events)
        {
            IList<Calendar> retval = new List<Calendar>();
            for (var i = 0; i < daysInMonth; i++)
            {
                var day = date.AddDays(i);
                var calendar = new Calendar()
                {
                    IsWithinMonth = true,
                    IsToday = day.Equals(DateTime.Today),
                    Events = events.Where(x => x.Scheduled.Date == day.Date).ToList(),
                    Date = day
                };
                retval.Add(calendar);
            }
            return retval;
        }

        private IList<Calendar> CalendarDaysBefore(DateTime date, int daysInMonth)
        {
            var retval = new List<Calendar>();
            var days = date.AddDays(-(DateTimeUtilities.FirstDayOfWeek() - date.DayOfWeek)).Subtract(date).Days;
            days = (days.LessThan(0)) ? days.Add(7) : days;
            for (var i = 0; i < days; i++)
            {
                retval.Add(new Calendar()
                {
                    Date = date.AddDays(i.Add(1).Negate())
                });
            }
            retval.Reverse();
            return retval;
        }

        private IList<Calendar> CalendarDaysAfter(DateTime date, int daysInMonth)
        {
            IList<Calendar> retval = new List<Calendar>();
            var lastDateOfMonth = date.AddDays(daysInMonth.Subtract(1));
            var days = lastDateOfMonth.AddDays((DateTimeUtilities.FirstDayOfWeek() - lastDateOfMonth.DayOfWeek).Add(7)).Subtract(date).Days - daysInMonth;
            days = (days.Equals(7)) ? 0 : days;
            for (var i = 0; i < days; i++)
            {
                retval.Add(new Calendar()
                {
                    Date = lastDateOfMonth.AddDays(i.Add(1))
                });
            }
            return retval;
        }

        #endregion

        #region Private Functions.

        /// <summary>
        /// Returns the hour from a string, e.g. hour 14 from 14:26.
        /// </summary>
        /// <param name="time"></param>
        private int? GetHour(string time)
        {
            return GetTime(time, 0);
        }

        /// <summary>
        /// Returns the minutes from a string, e.g. minutes 26 from 14:26.
        /// </summary>
        /// <param name="time"></param>
        private int? GetMinute(string time)
        {
            return GetTime(time, 1);
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
                var times = time.Split(':');
                if (times.Count() == 2)
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

        #endregion
    }
}