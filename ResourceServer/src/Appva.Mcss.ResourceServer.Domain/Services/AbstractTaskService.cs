// <copyright file="AbstractTaskService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.ResourceServer.Domain.Services
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using Domain.Repositories;
    using Mcss.Domain.Entities;
    using Mcss.Utils;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public abstract class AbstractTaskService
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ITaskRepository"/>.
        /// </summary>
        private readonly ITaskRepository taskRepository;

        /// <summary>
        /// The <see cref="ISequenceRepository"/>.
        /// </summary>
        private readonly ISequenceRepository sequenceRepository;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractTaskService"/> class.
        /// </summary>
        /// <param name="taskRepository">The <see cref="ITaskRepository"/>.</param>
        /// <param name="sequenceRepository">The <see cref="ISequenceRepository"/>.</param>
        protected AbstractTaskService(ITaskRepository taskRepository, ISequenceRepository sequenceRepository)
        {
            this.taskRepository = taskRepository;
            this.sequenceRepository = sequenceRepository;
        }

        #endregion

        #region Public Properties.

        /// <summary>
        /// Returns the <see cref="ITaskRepository"/>.
        /// </summary>
        protected ITaskRepository TaskRepository
        {
            get
            {
                return this.taskRepository;
            }
        }

        /// <summary>
        /// Returns the <see cref="ISequenceRepository"/>.
        /// </summary>
        protected ISequenceRepository SequenceRepository
        {
            get
            {
                return this.sequenceRepository;
            }
        }

        #endregion

        #region Public Static Functions.

        /// <summary>
        /// Returns whether a <see cref="DateTime"/> with an interval (days), e.g. 
        /// every other day, every third day, every fourth day, etc is occuring within an interval.
        /// </summary>
        /// <param name="any">TODO: any</param>
        /// <param name="startDate">TODO: startDate</param>
        /// <param name="endDate">TODO: endDate</param>
        /// <param name="intervalInDays">TODO: intervalInDays</param>
        /// <param name="dates">TODO: dates</param>
        /// <returns>TODO: returns</returns>
        public static bool IsOccurring(DateTime any, DateTime startDate, DateTime? endDate, int intervalInDays, string dates)
        {
            if (startDate > any || any > (endDate.HasValue ? endDate.Value.Date : any.Tomorrow()))
            {
                return false;
            }
            if (intervalInDays == 1)
            {
                return true;
            }
            if (dates.IsNotNull())
            {
                var strArray = dates.Split(',');
                foreach (var str in strArray)
                {
                    DateTime date = DateTime.Now;
                    if (DateTime.TryParse(str, out date))
                    {
                        if (any == date)
                        {
                            return true;
                        }
                    }
                }
            }
            if (intervalInDays == 0)
            {
                return false;
            }
            for (DateTime date = startDate; date <= any; date = date.AddDays(intervalInDays))
            {
                if (any == date)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Returns whether a event is occuring
        /// </summary>
        /// <param name="any">TODO: any</param>
        /// <param name="startDate">TODO: startDate</param>
        /// <param name="endDate">TODO: endDate</param>
        /// <param name="interval">TODO: interval</param>
        /// <param name="dates">TODO: dates</param>
        /// <param name="intervalFactor">TODO: intervalFactor</param>
        /// <param name="intervalIsDate">TODO: IntervalIsDate</param>
        /// <returns>TODO: returns</returns>
        public static bool EventIsOccurring(DateTime any, DateTime startDate, DateTime endDate, int interval, string dates, int intervalFactor, bool intervalIsDate)
        {
            if (startDate > any)
            {
                return false;
            }
            if (interval == 1)
            {
                return true;
            }
            var start = startDate;
            var end = endDate;
            if (interval == 0)
            {
                if (start.Date <= any.Date && end.Date >= any.Date)
                {
                    return true;
                }
                return false;
            }
            while (start.Date <= any.Date)
            {
                if (start.Date <= any.Date && end.Date >= any.Date)
                {
                    return true;
                }
                start = start.AddDays(IntervalInDays(interval, start, startDate, intervalFactor, !intervalIsDate));
                end = end.AddDays(IntervalInDays(interval, end, endDate, intervalFactor, !intervalIsDate));
            }
            return false;
        }

        /// <summary>
        /// Gives the interval in days.
        /// </summary>
        /// <param name="interval">TODO: interval</param>
        /// <param name="date">TODO: date</param>
        /// <param name="start">TODO: start</param>
        /// <param name="factor">TODO: factor</param>
        /// <param name="onDayOfWeek">TODO: onDayOfWeek</param>
        /// <returns>TODO: returns</returns>
        public static int IntervalInDays(int interval, DateTime date, DateTime start, int factor = 0, bool onDayOfWeek = false)
        {
            if (interval == 31)
            {
                int f = factor > 0 ? factor : 1;
                int retval = 0;
                for (int i = 0; i < f; i++)
                {
                    int days = 0;
                    if (date.Month == 1 && start.Day > DateTime.DaysInMonth(date.Year, 2))
                    {
                        days = DateTime.DaysInMonth(date.Year, date.Month) - (start.Day - DateTime.DaysInMonth(date.Year, 2));
                    }
                    else if (date.Month == 2 && start.Day > DateTime.DaysInMonth(date.Year, 2))
                    {
                        days = DateTime.DaysInMonth(date.Year, date.Month) + (start.Day - DateTime.DaysInMonth(date.Year, 2));
                    }
                    else if (start.Day == 31)
                    {
                        days = DateTime.DaysInMonth(date.AddMonths(1).Year, date.AddMonths(1).Month);
                    }
                    else
                    {
                        days = DateTime.DaysInMonth(date.Year, date.Month);
                    }
                    date = date.AddDays(days);
                    retval += days;
                }
                if (onDayOfWeek)
                {
                    return AdjustToDayOfWeek(retval, date, start);
                }
                return retval;
            }
            if (interval == 365)
            {
                if (DateTime.IsLeapYear(date.Year))
                {
                    return 366;
                }
                return 365;
            }
            if (factor > 0)
            {
                return interval.Multiply(factor);
            }
            return interval;
        }

        /// <summary>
        /// Adjust to specifik day in week.
        /// </summary>
        /// <param name="daysToAdd">TODO: daysToAdd</param>
        /// <param name="date">TODO: date</param>
        /// <param name="start">TODO: start</param>
        /// <returns>TODO: returns</returns>
        public static int AdjustToDayOfWeek(int daysToAdd, DateTime date, DateTime start)
        {
            DateTime newDate = new DateTime(date.Year, date.Month, 1, 0, 0, 0, 0);
            int weekDayInMonth = (start.Day - (start.Day % 7)) / 7;
            int current = 0;
            while (!((current == weekDayInMonth || newDate.DaysInMonth().Subtract(newDate.Day) < 8) && start.DayOfWeek == newDate.DayOfWeek))
            {
                newDate = newDate.AddDays(1);
                if (newDate.DayOfWeek == start.DayOfWeek)
                {
                    current = (newDate.Day - (newDate.Day % 7)) / 7;
                }
            }
            return daysToAdd.Add(Convert.ToInt32(newDate.Subtract(date).TotalDays));
        }

        #endregion

        #region Public Functions.

        /// <summary>
        /// TODO: Add a descriptive summary to increase readability.
        /// </summary>
        /// <param name="fromDate">TODO: fromDate</param>
        /// <param name="toDate">TODO: toDate</param>
        /// <param name="sequences">TODO: sequences</param>
        /// <param name="tasks">TODO: tasks</param>
        /// <param name="typeIds">TODO: typeIds</param>
        /// <returns>TODO: returns</returns>
        public IList<Task> FindTasks(DateTime fromDate, DateTime toDate, IList<Sequence> sequences, IList<Task> tasks, IList<string> typeIds)
        {
            var retval = new List<Task>();
            var datedSequences = this.FindSequences(fromDate, toDate, sequences, typeIds);
            foreach (var datedSequence in datedSequences)
            {
                if (datedSequence.Sequence.Schedule.ScheduleSettings.ScheduleType == ScheduleType.Action)
                {
                    var executingTimes = this.ExecutingTimes(datedSequence);
                    foreach (var time in executingTimes)
                    {
                        if (time <= toDate && time >= fromDate)
                        {
                            var task = this.GetTask(tasks, datedSequence.Sequence, time);
                            if (task != null)
                            {
                                retval.Add(task);
                            }
                            else
                            {
                                retval.Add(new Task()
                                {
                                    Name = datedSequence.Sequence.Name,
                                    Schedule = datedSequence.Sequence.Schedule,
                                    Sequence = datedSequence.Sequence,
                                    Patient = datedSequence.Sequence.Patient,
                                    Scheduled = time,
                                    RangeInMinutesBefore = datedSequence.Sequence.RangeInMinutesBefore,
                                    RangeInMinutesAfter = datedSequence.Sequence.RangeInMinutesAfter,
                                    IsReadyToExecute = true,
                                    OnNeedBasis = datedSequence.Sequence.OnNeedBasis
                                });
                            }
                        }
                    }
                }
                else if (datedSequence.Sequence.Schedule.ScheduleSettings.ScheduleType == ScheduleType.Calendar)
                {
                    var time = datedSequence.Sequence.EndDate.GetValueOrDefault();
                    var scheduled = time.AddDays(CalculateInterval(datedSequence.Date, datedSequence.Sequence));
                    var task = this.GetTask(tasks, datedSequence.Sequence, scheduled);
                    if (task != null)
                    {
                        retval.Add(task);
                    }
                    else
                    {
                        retval.Add(new Task
                        {
                            Name = datedSequence.Sequence.Name,
                            Schedule = datedSequence.Sequence.Schedule,
                            Sequence = datedSequence.Sequence,
                            Patient = datedSequence.Sequence.Patient,
                            Scheduled = scheduled,
                            RangeInMinutesBefore = datedSequence.Sequence.RangeInMinutesBefore,
                            RangeInMinutesAfter = datedSequence.Sequence.RangeInMinutesAfter,
                            IsReadyToExecute = true,
                            StartDate = datedSequence.Sequence.StartDate.AddDays(CalculateInterval(datedSequence.Date, datedSequence.Sequence)),
                            EndDate = scheduled,
                            OnNeedBasis = datedSequence.Sequence.OnNeedBasis,
                            Overview = datedSequence.Sequence.Overview,
                            Absent = datedSequence.Sequence.Absent,
                            AllDay = datedSequence.Sequence.AllDay
                        });
                    }
                }
            }
            retval.Sort((x, y) => DateTime.Compare(x.Scheduled, y.Scheduled));
            return retval;
        }

        /// <summary>
        /// Returns all Sequences for a date, e.g. Yesterday, Today, Tomorrow
        /// </summary>
        /// <param name="fromDate">TODO: fromDate</param>
        /// <param name="toDate">TODO: toDate</param>
        /// <param name="sequences">TODO: sequences</param>
        /// <param name="typeIds">TODO: typeIds</param>
        /// <returns>TODO: returns</returns>
        protected IList<DatedSequence> FindSequences(DateTime fromDate, DateTime toDate, IList<Sequence> sequences, IList<string> typeIds)
        {
            var retval = new List<DatedSequence>();
            foreach (var sequence in sequences)
            {
                for (DateTime date = fromDate; date <= toDate; date = date.AddDays(1))
                {
                    if (sequence.Schedule.ScheduleSettings.ScheduleType == ScheduleType.Action && typeIds.Contains("ordination"))
                    {
                        if (IsOccurring(date.Date, sequence.StartDate.Date, sequence.EndDate, sequence.Interval, sequence.Dates))
                        {
                            retval.Add(new DatedSequence()
                            {
                                Date = date,
                                Sequence = sequence
                            });
                        }
                    }
                    else if (sequence.Schedule.ScheduleSettings.ScheduleType == ScheduleType.Calendar && typeIds.Contains("calendar"))
                    {
                        if (EventIsOccurring(date.Date, sequence.StartDate.Date, sequence.EndDate.GetValueOrDefault(), sequence.Interval, sequence.Dates, intervalFactor: sequence.IntervalFactor, intervalIsDate: sequence.IntervalIsDate))
                        {
                            retval.Add(new DatedSequence()
                            {
                                Date = date,
                                Sequence = sequence
                            });
                        }
                    }
                }
            }
            return retval;
        }

        /// <summary>
        /// Extracts executing times from a <see cref="Sequence"/>
        /// </summary>
        /// <param name="datedSequence">TODO: datedSequence</param>
        /// <returns>TODO: returns</returns>
        protected IList<DateTime> ExecutingTimes(DatedSequence datedSequence)
        {
            int hour, minute;
            var retval = new List<DateTime>();
            if (! datedSequence.Sequence.OnNeedBasis)
            {
                if (datedSequence.Sequence.Times.IsNotEmpty())
                {
                    foreach (string hourStr in datedSequence.Sequence.Times.Split(','))
                    {
                        if (int.TryParse(hourStr, out hour))
                        {
                            hour = (hour == 24) ? 0 : hour;
                            retval.Add(datedSequence.Date.Date.AddHours(hour));
                        }
                    }
                }
                if (datedSequence.Sequence.Hour.IsNotEmpty().And(datedSequence.Sequence.Minute.IsNotEmpty()))
                {
                    if (int.TryParse(datedSequence.Sequence.Hour, out hour) && int.TryParse(datedSequence.Sequence.Minute, out minute))
                    {
                        retval.Add(datedSequence.Date.Date.AddHours(hour).AddMinutes(minute));
                    }
                }
            }
            else
            {
                retval.Add(datedSequence.Date);
            }
            return retval;
        }
        
        /// <summary>
        /// If a <see cref="Sequence"/> scheduled to run time is connected to a <see cref="Task"/>, then the  
        /// <see cref="Task"/> is set as delayed
        /// </summary>
        /// <param name="tasks">TODO: tasks</param>
        /// <param name="sequence">TODO: sequence</param>
        /// <param name="timeslot">TODO: timeslot</param>
        /// <returns>TODO: returns</returns>
        protected Task GetTask(IList<Task> tasks, Sequence sequence, DateTime timeslot)
        {
            foreach (var task in tasks)
            {
                if (task.Sequence.Id.Equals(sequence.Id) && task.Scheduled.Equals(timeslot))
                {
                    return task;
                }
            }
            return null;
        }
        
        /// <summary>
        /// Calculates the interval in days for a specific date and day
        /// </summary>
        /// <param name="any">TODO: any</param>
        /// <param name="sequence">TODO: sequence</param>
        /// <returns>TODO: returns</returns>
        private static double CalculateInterval(DateTime any, Sequence sequence)
        {
            if (any.Date >= sequence.StartDate.Date && any.Date <= sequence.EndDate.GetValueOrDefault().Date)
            {
                return 0;
            }
            var retval = ((any.Date - sequence.EndDate.GetValueOrDefault().Date).TotalDays + (any.Date - sequence.StartDate.Date).TotalDays) / 2;
            return retval;
        }

        #endregion

        #region Private Classes.

        /// <summary>
        /// TODO: Add a descriptive summary to increase readability.
        /// </summary>
        public class DatedSequence
        {
            /// <summary>
            /// TODO: Add a summary.
            /// </summary>
            public DateTime Date { get; set; }

            /// <summary>
            /// TODO: Add a summary.
            /// </summary>
            public Sequence Sequence { get; set; }
        }

        #endregion
    }
}