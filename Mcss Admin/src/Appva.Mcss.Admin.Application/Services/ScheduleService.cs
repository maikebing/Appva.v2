// <copyright file="ScheduleService.cs" company="Appva AB">
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
    using Appva.Core.Extensions;
    using Appva.Core.Utilities;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Extensions;
    using Appva.Persistence;
    using Appva.Mcss.Admin.Application.Security.Identity;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface IScheduleService : IService
    {
        IList<Task> FindTasks(
            DateTime start,
            DateTime end,
            IList<Schedule> schedules,
            IList<Sequence> sequences,
            IList<Task> tasks,
            IList<Task> delayedTasks);

        IList<Task> FindTasks(
                DateTime any,
                IList<Schedule> schedules,
                IList<Sequence> sequences,
                IList<Task> tasks,
                IList<Task> delayedTasks,
                IList<Task> events = null,
                bool findEventsForDay = false
            );

        /// <summary>
        /// Lists all schedules by given patient
        /// </summary>
        /// <param name="byPatient"></param>
        /// <returns></returns>
        IList<Schedule> List(Guid byPatient);

        /// <summary>
        /// Gets all schedulesettings in  the system or for a given patient/account
        /// </summary>
        /// <param name="Account"></param>
        /// <param name="Patient"></param>
        /// <returns></returns>
        IList<ScheduleSettings> GetSchedules(Guid? Account = null, Guid? Patient = null);

        /// <summary>
        /// Gets a schedulesetting
        /// </summary>
        /// <param name="Account"></param>
        /// <param name="Patient"></param>
        /// <returns></returns>
        ScheduleSettings GetScheduleSettings(Guid id);

        /// <summary>
        /// Updates a schedule setting
        /// </summary>
        /// <param name="schedule"></param>
        void UpdateScheduleSetting(ScheduleSettings schedule);

        /// <summary>
        /// Creates a new schedule setting
        /// </summary>
        /// <param name="schedule"></param>
        void SaveScheduleSetting(ScheduleSettings schedule);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ScheduleService : IScheduleService
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ILogService"/>.
        /// </summary>
        private readonly ILogService logService;

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistence;

        /// <summary>
        /// The <see cref="IIdentityService"/>.
        /// </summary>
        private readonly IIdentityService identity;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduleService"/> class.
        /// </summary>
        public ScheduleService(
            ILogService logService, 
            IPersistenceContext persistence,
            IIdentityService identity)
        {
            this.logService     = logService;
            this.persistence    = persistence;
            this.identity       = identity;
        }

        #endregion

        #region IScheduleService members

        /// <inheritdoc />
        public IList<Schedule> List(Guid byPatient)
        {
            var schedulePermissions = this.identity.SchedulePermissions().Select(x => new Guid(x.Value)).ToArray();
            var query = this.persistence.QueryOver<Schedule>()
                .Where(s => s.Patient.Id == byPatient && s.IsActive == true)
                .JoinQueryOver<ScheduleSettings>(s => s.ScheduleSettings)
                    .WhereRestrictionOn(x => x.Id).IsIn(schedulePermissions)
                    .And(s => s.ScheduleType == ScheduleType.Action);

            return query.List();
        }

        #endregion

        public IList<Task> FindTasks(
            DateTime start,
            DateTime end,
            IList<Schedule> schedules,
            IList<Sequence> sequences,
            IList<Task> tasks,
            IList<Task> delayedTasks
        )
        {
            var retval = new List<Task>();
            while (start <= end)
            {
                if (start.Date < DateTime.Now.Date)
                {
                    retval.AddRange(tasks.Where(x => x.Scheduled.Date == start.Date).ToList());
                }
                else
                {
                    retval.AddRange(FindTasks(start, schedules, sequences, tasks, delayedTasks));
                }
                start = start.AddDays(1);
            }
            var severalMonthSeqs = sequences.Where(x => (x.StartDate.Month < x.EndDate.GetValueOrDefault().Month) || (x.StartDate.Year < x.EndDate.GetValueOrDefault().Year)).ToList();
            foreach (var seq in severalMonthSeqs)
            {
                if (seq.Interval != 0)
                {
                    var startDate = new DateTime(start.Year, start.Month, seq.StartDate.Day, seq.StartDate.Hour, seq.StartDate.Minute, seq.StartDate.Second);
                    retval.AddRange(FindTasks(seq.EndDate.GetValueOrDefault().AddDays((startDate - start).TotalDays), schedules, new List<Sequence>() { seq }, tasks, new List<Task>()));
                }
                else
                {
                    retval.AddRange(FindTasks(seq.EndDate.GetValueOrDefault(), schedules, new List<Sequence>() { seq }, tasks, new List<Task>()));
                }
            }

            return retval;

        }

        /// <summary>
        /// Returns all <see cref="Task"/> for a date
        /// </summary>
        /// <param name="schedules">List of <see cref="Schedule"/></param>
        /// <param name="sequences">List of <see cref="Sequence"/></param>
        /// <param name="tasks">List of <see cref="Task"/> for a date</param>
        /// <param name="events">List of <see cref="Event"/> for a date where <see cref="Event.PauseTask"/> is true</param>
        public IList<Task> FindTasks(
                DateTime any,
                IList<Schedule> schedules,
                IList<Sequence> sequences,
                IList<Task> tasks,
                IList<Task> delayedTasks,
                IList<Task> events = null,
                bool findEventsForDay = false
            )
        {
            if (events.IsNull())
            {
                events = new List<Task>();
            }
            DateTime timeOfDay = DateTimeUtilities.Now();
            var retval = new List<Task>();
            foreach (var sequence in FindSequences(any, sequences, tasks, findEventsForDay))
            {
                var schedule = GetSchedule(sequence.Schedule.Id, schedules);
                foreach (DateTime time in ExecutingTimes(any, sequence))
                {
                    if (!IsPaused(events, sequence, time, any))
                    {
                        var task = GetTask(tasks, sequence, time);
                        if (task.IsNull())
                        {
                            retval.Add(new Task()
                            {
                                Name = sequence.Name,
                                Schedule = schedule.IsNotNull() ? schedule : sequence.Schedule,
                                Sequence = sequence,
                                Patient = sequence.Patient,
                                Scheduled = time,
                                RangeInMinutesBefore = sequence.RangeInMinutesBefore,
                                RangeInMinutesAfter = sequence.RangeInMinutesAfter,
                                IsReadyToExecute = true,
                                Delayed = IsDelayed(delayedTasks, sequence, time),
                                OnNeedBasis = sequence.OnNeedBasis,
                                Absent = sequence.Absent,
                                AllDay = sequence.AllDay,
                                CanRaiseAlert = sequence.CanRaiseAlert,
                                Overview = sequence.Overview,
                                PauseAnyAlerts = sequence.PauseAnyAlerts,
                                Taxon = sequence.Patient.Taxon,
                                StartDate = sequence.Schedule.ScheduleSettings.ScheduleType == ScheduleType.Calendar ? sequence.StartDate.AddDays(CalculateInterval(time, sequence)) : sequence.StartDate,
                                EndDate = sequence.Schedule.ScheduleSettings.ScheduleType == ScheduleType.Calendar ? sequence.EndDate.GetValueOrDefault().AddDays(CalculateInterval(time, sequence)) : sequence.EndDate

                                //IsReadyToExecute = (timeOfDay.Within(
                                //    time.AddMinutes(-sequence.RangeInMinutesBefore),
                                //    time.AddMinutes(sequence.RangeInMinutesAfter)
                                //)) ? true : timeOfDay > time
                            });
                        }
                        else
                        {
                            retval.Add(task);
                        }
                    }
                }
            }
            retval.Sort((x, y) => DateTime.Compare(x.Scheduled, y.Scheduled));
            return retval;
        }

        private static double CalculateInterval(DateTime scheduled, Sequence sequence)
        {
            if (scheduled.Within(sequence.StartDate, sequence.EndDate.GetValueOrDefault()))
            {
                return 0;
            }
            var retval = (scheduled.Date - sequence.EndDate.GetValueOrDefault().Date).TotalDays;
            return retval;
        }

        /// <inheritdoc />
        public IList<ScheduleSettings> GetSchedules(Guid? Account = null, Guid? Patient = null)
        {
            var query = this.persistence.QueryOver<ScheduleSettings>().Where(x => x.IsActive == true);

            return query.List();
        }

        /// <inheritdoc />
        public ScheduleSettings GetScheduleSettings(Guid id)
        {
            return this.persistence.Get<ScheduleSettings>(id);
        }

        /// <inheritdoc />
        public void UpdateScheduleSetting(ScheduleSettings schedule) 
        {
            schedule.UpdatedAt = DateTime.Now;
            this.persistence.Update<ScheduleSettings>(schedule);
        }

        /// <inheritdoc />
        public void SaveScheduleSetting(ScheduleSettings schedule)
        {
            this.persistence.Save<ScheduleSettings>(schedule);
        }

        /// <summary>
        /// Checks if the <see cref="Patient"/> connected to a <see cref="Sequence"/> has any events
        /// that pauses a <see cref="Task"/>
        /// </summary>
        /// <param name="events"></param>
        /// <param name="sequence"></param>
        /// <param name="timeslot"></param>
        /// <param name="any">A Date reference</param>
        protected bool IsPaused(IList<Task> events, Sequence sequence, DateTime timeslot, DateTime any)
        {
            DateTime now = DateTimeUtilities.Now().Date;
            if (sequence.Schedule.ScheduleSettings.ScheduleType == ScheduleType.Calendar)
            {
                return false;
            }
            foreach (var evt in events)
            {
                if (evt.Patient.Id.Equals(sequence.Patient.Id) && evt.PauseAnyAlerts)
                {
                    if (evt.AllDay)
                    {
                        return true;
                    }
                    else
                    {
                        if (!any.Date.Equals(evt.StartDate.GetValueOrDefault().Date) && !any.Date.Equals(evt.EndDate.GetValueOrDefault().Date))
                        {
                            // Since it is between start date and end date it actually means
                            // it's from 00:00:00 - 23:59:59:99999 - just return true - start 
                            // and end time only applies to the actual start and end day.
                            return true;
                        }
                        if (any.Date.Equals(evt.StartDate.GetValueOrDefault().Date) && !any.Equals(evt.EndDate.GetValueOrDefault().Date))
                        {
                            // If it is the start day and not the end day, we just check the
                            // start time to 23:59:59:99990.
                            if (timeslot.Within(evt.StartDate.GetValueOrDefault(), any.Date.LastInstantOfDay()))
                            {
                                return true;
                            }
                        }
                        if (!any.Date.Equals(evt.StartDate.GetValueOrDefault().Date) && any.Date.Equals(evt.EndDate.GetValueOrDefault().Date))
                        {
                            // Similar to above, if its the end date, we just check from 00:00
                            // to the end time.
                            if (timeslot.Within(any.Date, evt.EndDate.GetValueOrDefault()))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// If a <see cref="Sequence"/> scheduled to run time is connected to a <see cref="Task"/>, then the  
        /// <see cref="Task"/> has been completed
        /// </summary>
        /// <param name="tasks"></param>
        /// <param name="sequence"></param>
        /// <param name="timeslot"></param>
        protected bool IsCompleted(IList<Task> tasks, Sequence sequence, DateTime timeslot)
        {
            foreach (var task in tasks)
            {
                if (task.IsCompleted && task.Sequence.Id.Equals(sequence.Id) && task.Scheduled.Equals(timeslot))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// If a <see cref="Sequence"/> scheduled to run time is connected to a <see cref="Task"/>, then the  
        /// <see cref="Task"/> is set as delayed
        /// </summary>
        /// <param name="tasks"></param>
        /// <param name="sequence"></param>
        /// <param name="timeslot"></param>
        protected bool IsDelayed(IList<Task> tasks, Sequence sequence, DateTime timeslot)
        {
            foreach (var task in tasks)
            {
                if (task.Delayed && task.Sequence.Id.Equals(sequence.Id) && task.Scheduled.Equals(timeslot))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Extracts executing times from a <see cref="Sequence"/>
        /// </summary>
        /// <param name="any"></param>
        /// <param name="sequence"></param>
        protected IList<DateTime> ExecutingTimes(DateTime any, Sequence sequence)
        {
            int Hour;
            int Minute;
            IList<DateTime> retval = new List<DateTime>();
            if (sequence.Schedule.ScheduleSettings.ScheduleType == ScheduleType.Calendar)
            {
                var endDate = sequence.EndDate.GetValueOrDefault();
                Hour = endDate.Hour;
                Minute = endDate.Minute;
                retval.Add(new DateTime(any.Year, any.Month, any.Day, Hour, Minute, 0));
                return retval;
            }
            if (!sequence.OnNeedBasis)
            {
                if (!String.IsNullOrEmpty(sequence.Times))
                {
                    foreach (string hour in sequence.Times.Split(','))
                    {
                        if (Int32.TryParse(hour, out Hour))
                        {
                            Hour = (Hour == 24) ? 0 : Hour;
                            retval.Add(any.Date.AddHours(Hour));
                        }
                    }
                }
                if (!String.IsNullOrEmpty(sequence.Hour) && !String.IsNullOrEmpty(sequence.Minute))
                {
                    if (Int32.TryParse(sequence.Hour, out Hour) && Int32.TryParse(sequence.Minute, out Minute))
                    {
                        retval.Add(any.Date.AddHours(Hour).AddMinutes(Minute));
                    }
                }
            }
            else
            {
                retval.Add(any);
            }
            return retval;
        }

        

        /// <summary>
        /// Returns all Sequences for a date, e.g. Yesterday, Today, Tomorrow
        /// </summary>
        /// <param name="date"></param>
        /// <param name="sequences"></param>
        protected IList<Sequence> FindSequences(DateTime date, IList<Sequence> sequences, IList<Task> tasks = null, bool findEventsForDay = false)
        {
            IList<Sequence> retval = new List<Sequence>();
            foreach (var sequence in sequences)
            {
                var isOccuring = findEventsForDay ?
                    DateTimeUtils.DateIsCoveredByEvent(date.Date, sequence.StartDate.Date, sequence.EndDate.GetValueOrDefault(), sequence.Interval, sequence.Dates, intervalFactor: sequence.IntervalFactor, intervalIsDate: sequence.IntervalIsDate) :
                    DateTimeUtils.IsOccurring(date.Date, sequence.StartDate.Date, sequence.EndDate, sequence.Interval, sequence.Dates, sequence.Schedule.ScheduleSettings.ScheduleType, intervalFactor: sequence.IntervalFactor, intervalIsDate: sequence.IntervalIsDate);
                if (isOccuring)
                {
                    retval.Add(sequence);
                }
                else if (sequence.Schedule.ScheduleSettings.ScheduleType == ScheduleType.Calendar && tasks.IsNotNull())
                {
                    if (tasks.Where(x => x.Scheduled.Date == date.Date && x.Sequence == sequence).ToList().Count > 0)
                    {
                        retval.Add(sequence);
                    }
                }
            }
            return retval;
        }

        /// <summary>
        /// Returns a <see cref="Task"/> for a time and a <see cref="Sequence"/>
        /// </summary>
        /// <param name="tasks"></param>
        /// <param name="sequence"></param>
        /// <param name="timeslot"></param>
        protected Task GetTask(IList<Task> tasks, Sequence sequence, DateTime timeslot)
        {
            foreach (var task in tasks)
            {
                if (sequence.Schedule.ScheduleSettings.ScheduleType == ScheduleType.Action)
                {
                    if (task.Sequence.Id.Equals(sequence.Id) && task.Scheduled.Equals(timeslot))
                    {
                        return task;
                    }
                }
                else
                {
                    if (task.Sequence.Id.Equals(sequence.Id) && task.Scheduled.Date.Equals(timeslot.Date))
                    {
                        return task;
                    }
                }

            }
            return null;
        }

        /// <summary>
        /// Returns a <see cref="Schedule"/> by id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Id"></param>
        /// <param name="schedules"></param>
        protected Schedule GetSchedule(Guid Id, IList<Schedule> schedules)
        {
            foreach (var schedule in schedules)
            {
                if (schedule.Id.Equals(Id))
                {
                    return schedule;
                }
            }
            return null;
        }
    }
}