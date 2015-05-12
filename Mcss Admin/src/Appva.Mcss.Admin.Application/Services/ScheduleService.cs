﻿// <copyright file="ScheduleService.cs" company="Appva AB">
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
        PrintSchedule PrintSchedule(IList<Task> tasks);
        PrintSchedule PrintSchedule(
            DateTime startDate,
            DateTime endDate,
            IList<Sequence> sequences
        );
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ScheduleService : IScheduleService
    {
        #region Variables.

        private readonly ILogService logService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduleService"/> class.
        /// </summary>
        public ScheduleService(ILogService logService)
        {
            this.logService = logService;
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
                                Scheduled = findEventsForDay ? sequence.EndDate.GetValueOrDefault().AddDays(CalculateInterval(any, sequence)) : time,
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
                                StartDate = sequence.StartDate.AddDays(CalculateInterval(any, sequence)),
                                EndDate = sequence.EndDate.GetValueOrDefault().AddDays(CalculateInterval(any, sequence))

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

        private static double CalculateInterval(DateTime any, Sequence sequence)
        {
            if (any.Within(sequence.StartDate, sequence.EndDate.GetValueOrDefault()))
            {
                return 0;
            }
            var retval = ((any.Date - sequence.EndDate.GetValueOrDefault().Date).TotalDays + (any.Date - sequence.StartDate.Date).TotalDays) / 2;
            return retval;
        }

        /// <summary>
        /// Creates task from the given sequences between endDate and startDate. 
        /// Sorts the created tasks into the PrintSchedule structure
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="sequences"></param>
        /// <returns></returns>
        public PrintSchedule PrintSchedule(
            DateTime startDate,
            DateTime endDate,
            IList<Sequence> sequences
        )
        {
            var currentDate = startDate;
            var tasks = new List<Task>();

            while (currentDate <= endDate)
            {
                tasks.AddRange(FindTasks(currentDate, new List<Schedule>(), sequences, new List<Task>(), new List<Task>()));
                currentDate = currentDate.AddDays(1);
            }
            return PrintSchedule(tasks);
        }

        /// <summary>
        /// Sorts all tasks into the PrintSchedule structure
        /// </summary>
        /// <param name="tasks"></param>
        /// <returns></returns>
        public PrintSchedule PrintSchedule(IList<Task> tasks)
        {
            PrintSchedule printSchedule = new PrintSchedule();

            foreach (var task in tasks)
            {
                var currentMonth = task.Scheduled.FirstOfMonth();
                if (!printSchedule.Sequences.ContainsKey(currentMonth))
                {
                    printSchedule.Sequences.Add(currentMonth, new Dictionary<string, PrintSequence>());
                    printSchedule.Signatures.Add(currentMonth, new Dictionary<string, string>());
                }
                var uid = string.Format("{0} {1:HH:mm}", task.Sequence.Id, task.Scheduled);
                if (task.OnNeedBasis)
                {
                    var i = 1;
                    uid = string.Format("{0} {1}", uid, i);
                    while (printSchedule.Sequences[currentMonth].ContainsKey(uid)
                        && printSchedule.Sequences[currentMonth][uid].Days.ContainsKey(task.Scheduled.Day))
                    {
                        i++;
                        uid = string.Format("{0} {1}", uid, i);
                    }
                }
                if (!printSchedule.Sequences[currentMonth].ContainsKey(uid))
                {
                    var ps = new PrintSequence()
                    {
                        UID = uid,
                        Name = task.Name,
                        Time = string.Format("{0:HH:mm}", task.Scheduled),
                        OnNeedBasis = task.OnNeedBasis,
                        Days = new Dictionary<int, Task>(),
                        Instruction = task.Sequence.Description
                    };
                    printSchedule.Sequences[currentMonth].Add(uid, ps);
                }
                try
                {
                    printSchedule.Sequences[currentMonth][uid].Days.Add(task.Scheduled.Day, task);
                }
                catch (Exception)
                {
                    try
                    {
                        /*var tenant = TenantService.Instance().Current();
                        new Email(new Message()
                        {
                            To = Application.EmailExceptionsTo,
                            Subject = string.Format("Database contains duplicated task."),
                            Body = string.Format("<h2>Two tasks belonging covering the same slot for the same timeslot exist.</h2><p><strong>Task-id:</strong> {0}.</p> <p><strong>Tenant:</strong> {1}.", task.Id, tenant.Name),
                        }).SendAsync();

                        LogService.TakeAction(
                            string.Format("Task whit id={0} is duplicated in the db. Another task belonging to the same sequence, covering the same timeslot exist.", task.Id),
                            Current(),
                            task.Patient,
                            LogType.None
                        );*/
                    }
                    catch (Exception)
                    {
                        // The SMTP should never bring the system down.
                        // TODO: handle this!
                    }
                }
                if (task.IsCompleted)
                {
                    var id = string.Format("{0};{1}", task.CompletedBy.FullName, task.CompletedBy.Id);
                    if (!printSchedule.Signatures[currentMonth].ContainsKey(id))
                    {
                        var signature = string.Format("{0}{1}", task.CompletedBy.FirstName.Substring(0, 1), task.CompletedBy.LastName.Substring(0, 1));
                        if (printSchedule.Signatures[currentMonth].ContainsValue(signature))
                        {
                            var counter = 2;
                            while (printSchedule.Signatures[currentMonth].ContainsValue(string.Format("{0}{1}", signature, counter)))
                            {
                                counter++;
                            }
                            signature = string.Format("{0}{1}", signature, counter);
                        }
                        printSchedule.Signatures[currentMonth].Add(id, signature);
                    }
                }
            }
            return printSchedule;
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
                    DateTimeUtils.DateIsCoveredByEvent(date.Date, sequence.StartDate.Date, sequence.EndDate.GetValueOrDefault(), sequence.Interval, sequence.Dates, intervalFactor: sequence.IntervalFactor, IntervalIsDate: sequence.IntervalIsDate) :
                    DateTimeUtils.IsOccurring(date.Date, sequence.StartDate.Date, sequence.EndDate, sequence.Interval, sequence.Dates, sequence.Schedule.ScheduleSettings.ScheduleType, intervalFactor: sequence.IntervalFactor, IntervalIsDate: sequence.IntervalIsDate);
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