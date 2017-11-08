// <copyright file="Sequence.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Common.Domain;
    using Validation;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class Sequence : AggregateRoot<Sequence>
    {
        #region Variables.

        /// <summary>
        /// Weekly interval.
        /// </summary>
        private const int Weekly  = 7;

        /// <summary>
        /// Monthly interval.
        /// </summary>
        private const int Monthly = 31;

        /// <summary>
        /// Yearly interval.
        /// </summary>
        private const int Yearly  = 365;

        #endregion

        #region Constructor.

        /// <summary>
        /// Creates a new instance of Sequence.
        /// On need basis
        /// </summary>
        /// <param name="schedule"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="startDate"></param>
        /// <param name="onNeedBasis"></param>
        /// <param name="endDate"></param>
        /// <param name="taxon"></param>
        /// <param name="role"></param>
        /// <param name="rangeInMinutesBefore"></param>
        /// <param name="rangeInMinutesAfter"></param>
        /// <param name="inventory"></param>
        public Sequence(Schedule schedule, string name, string description, DateTime startDate, 
            DateTime? endDate, Taxon taxon = null, Role role = null, int rangeInMinutesBefore = 0, int rangeInMinutesAfter = 0, Inventory inventory = null)
        {
            Requires.NotNull(schedule, "schedule");
            Requires.NotNullOrEmpty(name, "name");
            Requires.Equals((startDate >= DateTime.Now), true);

            this.Schedule = schedule;
            this.Patient = schedule.Patient;
            this.Name = name;
            this.Description = description;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.OnNeedBasis = true; // in this context this should always be set to true.
            this.Taxon = taxon;
            this.Role = role;
            this.RangeInMinutesBefore = rangeInMinutesBefore;
            this.RangeInMinutesAfter = rangeInMinutesAfter;
            this.Inventory = inventory;
        }
        
        /// <summary>
        /// Creates a new instance of Sequence.
        /// A reoccurring sequence based on interval
        /// </summary>
        /// <param name="schedule"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="startDate"></param>
        /// <param name="interval"></param>
        /// <param name="intervalFactor"></param>
        /// <param name="times"></param>
        /// <param name="intervalIsDate"></param>
        /// <param name="endDate"></param>
        /// <param name="taxon"></param>
        /// <param name="role"></param>
        /// <param name="rangeInMinutesBefore"></param>
        /// <param name="rangeInMinutesAfter"></param>
        /// <param name="inventory"></param>
        public Sequence(Schedule schedule, string name, string description, DateTime startDate, int interval, string times,
            DateTime? endDate, Taxon taxon = null, Role role = null, int rangeInMinutesBefore = 0, int rangeInMinutesAfter = 0, Inventory inventory = null)
        {
            Requires.NotNull(schedule, "schedule");
            Requires.NotNullOrEmpty(name, "name");
            Requires.Equals(startDate >= DateTime.Now, true);
            Requires.Equals((interval > 0), true);
            Requires.NotNullOrEmpty(times, "times");

            this.Schedule = schedule;
            this.Patient = schedule.Patient;
            this.Name = name;
            this.Description = description;
            this.StartDate = startDate;
            this.Interval = interval;
            this.Times = times;
            //this.IntervalIsDate = intervalIsDate;
            this.EndDate = endDate;
            this.Taxon = taxon;
            this.Role = role;
            this.RangeInMinutesBefore = rangeInMinutesBefore;
            this.RangeInMinutesAfter = rangeInMinutesAfter;
            this.Inventory = inventory;
        }

        /// <summary>
        /// Creates a new instance of Sequence.
        /// A reoccurring sequence with specific given dates
        /// </summary>
        /// <param name="schedule">Requried.</param>
        /// <param name="name">Required.</param>
        /// <param name="description">Required.</param>
        /// <param name="startDate">Required.</param>
        /// <param name="endDate">Required.</param>
        /// <param name="dates">Required.</param>
        /// <param name="times">Required.</param>
        /// <param name="taxon">Optional. Default = null.</param>
        /// <param name="role">Optional. Default = null.</param>
        /// <param name="rangeInMinutesBefore">Optional. Default = 0.</param>
        /// <param name="rangeInMinutesAfter">Optional. Default = 0.</param>
        /// <param name="inventory">Optional. Default = null.</param>
        public Sequence(Schedule schedule, string name, string description, DateTime startDate, DateTime? endDate, string dates, string times,
            Taxon taxon = null, Role role = null, int rangeInMinutesBefore = 0, int rangeInMinutesAfter = 0, Inventory inventory = null)
        {
            Requires.NotNull(schedule, "schedule");
            Requires.NotNullOrEmpty(name, "name");
            Requires.Equals(startDate >= DateTime.Now, true);
            Requires.Equals(endDate.HasValue, true);
            Requires.Equals(endDate.Value >= startDate, true);
            Requires.NotNullOrEmpty(dates, "dates");
            Requires.NotNullOrEmpty(times, "times");

            this.Schedule = schedule;
            this.Patient = schedule.Patient;
            this.Name = name;
            this.Description = description;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.Dates = dates; // check the first and last date in this string. move logic here from service.
            this.Times = times;
            this.Taxon = taxon;
            this.Role = role;
            this.RangeInMinutesBefore = rangeInMinutesBefore;
            this.RangeInMinutesAfter = rangeInMinutesAfter;
            this.Inventory = inventory;
        }

        /// <summary>
        /// Creates an instance of Sequence
        /// A calender event
        /// </summary>
        /// <param name="schedule">Required.</param>
        /// <param name="name">Required.</param>
        /// <param name="description">Required.</param>
        /// <param name="startDate">Required.</param>
        /// <param name="endDate">Required.</param>
        /// <param name="interval">Optional, default = 0</param>
        /// <param name="intervalFactor">Optional, default = 0</param>
        /// <param name="intervalIsDate">Optional, default = false</param>
        /// <param name="reminder">Optional, default = false</param>
        /// <param name="canRaiseAlert">Optional, default = false</param>
        /// <param name="overview">Optional, default = false</param>
        /// <param name="allDay">Optional, default = false</param>
        /// <param name="absent">Optional, default = false</param>
        public Sequence(Schedule schedule, string name, string description, DateTime startDate, DateTime? endDate,
            int interval = 0, int intervalFactor = 0, bool intervalIsDate = false, bool reminder = false, bool canRaiseAlert = false, bool overview = false, bool allDay = false, bool absent = false)
        {
            Requires.NotNull(schedule, "schedule");
            Requires.NotNullOrEmpty(name, "name");
            Requires.NotNullOrEmpty(description, "description");
            Requires.Equals(startDate >= DateTime.Now, true);
            Requires.Equals(endDate.HasValue, true);
            Requires.Equals(endDate.Value >= startDate, true);

            this.Schedule = schedule;
            this.Patient = schedule.Patient;
            this.Name = name;
            this.Description = description;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.Interval = interval;
            this.IntervalFactor = intervalFactor;
            this.IntervalIsDate = intervalIsDate;
            this.Reminder = reminder;
            this.CanRaiseAlert = canRaiseAlert;
            this.Overview = overview;
            this.AllDay = allDay;
            this.Absent = absent;
        }


        // My own scribbles
        protected internal Sequence(
            string name,  // always required
            string description,  // optional?
            DateTime startDate, // always required
            DateTime? endDate,  // optional
            int rangeInMinutesBefore, // optional, default = 0
            int rangeInMinutesAfter, 
            string times, 
            string dates, 
            //string hour, 
            //string minute, 
            int interval,
            int intervalFactor,
            bool intervalIsDate,
            bool onNeedBasis,
            bool reminder,
            int reminderInMinutesBefore,
            Account reminderRecipient,
            //DateTime? lastReminderSent,
            //int? stockAmount,
            //DateTime? lastStockAmountCalculation,
            Patient patient,
            Schedule schedule,
            Taxon taxon,
            Role role,
            bool overview,
            bool canRaiseAlert,
            bool pauseAnyAlerts,
            bool allDay,
            bool absent,
            RefillModel refillInfo,
            Inventory inventory)
        {
            Requires.NotNull(name, "name");
            Requires.NotNull(schedule, "schedule");

            this.Name = name;
            this.StartDate = startDate;
            this.Schedule = schedule;

            this.Description = description;
            this.EndDate = endDate;
            this.RangeInMinutesBefore = rangeInMinutesBefore;
            this.RangeInMinutesAfter = rangeInMinutesAfter;
            this.Times = times;
            this.Dates = dates;
            //this.Hour = hour;
            //this.Minute = minute;
            this.Interval = interval;
            this.IntervalFactor = intervalFactor;
            this.IntervalIsDate = intervalIsDate;
            this.OnNeedBasis = onNeedBasis;

            this.Reminder = reminder;
            this.ReminderInMinutesBefore = reminderInMinutesBefore;
            this.ReminderRecipient = reminderRecipient;
            //this.LastReminderSent = lastReminderSent; // no refs
            //this.StockAmount = stockAmount; // no refs
            //this.LastStockAmountCalculation = lastStockAmountCalculation; // no refs
            this.Patient = schedule.Patient;
            this.Taxon = taxon;
            this.Role = role;

            // used in any construction of class. 
            this.Overview = overview;
            this.CanRaiseAlert = canRaiseAlert;
            this.PauseAnyAlerts = pauseAnyAlerts;
            this.AllDay = allDay;
            this.Absent = absent;
            this.RefillInfo = refillInfo;
            this.Inventory = inventory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sequence"/> class.
        /// </summary>
        protected internal Sequence()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// Name of the item, e.g. "Do something"
        /// </summary>
        public virtual string Name
        {
            get;
            set;
        }

        /// <summary>
        /// The desciption, such as instructions 
        /// </summary>
        public virtual string Description
        {
            get;
            set;
        }

        /// <summary>
        /// A start date of the <see cref="Sequence"/>
        /// </summary>
        /// <remarks>Must be set</remarks>
        public virtual DateTime StartDate
        {
            get;
            set;
        }

        /// <summary>
        /// If an end date is not set this <see cref="Sequence"/> will never expire
        /// </summary>
        public virtual DateTime? EndDate
        {
            get;
            set;
        }

        /// <summary>
        /// Indicates the range in minutes before any of the tasks are ready to be processed
        /// </summary>
        public virtual int RangeInMinutesBefore
        {
            get;
            set;
        }

        /// <summary>
        /// Indicates the range in minutes after any of the tasks are ready to be processed
        /// </summary
        public virtual int RangeInMinutesAfter
        {
            get;
            set;
        }

        /// <summary>
        /// A set of fixed hours, e.g. 13, 14, 15, this sequence will trigger
        /// </summary>
        public virtual string Times
        {
            get;
            set;
        }

        /// <summary>
        /// A set of fixed dates this sequence will trigger
        /// </summary>
        /// <remarks>This member will set the start date</remarks>
        public virtual string Dates
        {
            get;
            set;
        }

        /// <summary>
        /// The hour of day
        /// </summary>
        public virtual string Hour // Always null?
        {
            get;
            set;
        }

        /// <summary>
        /// The minute of hour
        /// </summary>
        public virtual string Minute // Always null?
        {
            get;
            set;
        }

        /// <summary>
        /// The interval for the <see cref="Sequence"/>
        /// 1: Everyday
        /// 2: Every other day
        /// 3: Every third day
        /// 4: Every fourth day
        /// etc.
        /// </summary>
        public virtual int Interval
        {
            get;
            set;
        }

        /// <summary>
        /// Factor for interval
        /// </summary>
        public virtual int IntervalFactor
        {
            get;
            set;
        }

        /// <summary>
        /// If true: occurence on specific date, if false: occurence adjusted to specific day in week
        /// </summary>
        public virtual bool IntervalIsDate
        {
            get;
            set;
        }

        /// <summary>
        /// These tasks will always be ready to start, e.g. a task "give pain killers" can be given when
        /// necassary and on need basis.
        /// </summary>
        public virtual bool OnNeedBasis
        {
            get;
            set;
        }

        /// <summary>
        /// If a reminder should be e.g. e-mailed to the <see cref="ReminderRecipient"/>.
        /// </summary>
        public virtual bool Reminder
        {
            get;
            set;
        }

        /// <summary>
        /// Minutes before <see cref="RangeInMinutesBefore"/> a reminder will be sent.
        /// </summary>
        public virtual int ReminderInMinutesBefore
        {
            get;
            set;
        }

        /// <summary>
        /// Recipient of a reminder.
        /// </summary>
        public virtual Account ReminderRecipient // Questions raised around this property
        {
            get;
            set;
        }

        /// <summary>
        /// When the last reminder was sent out.
        /// </summary>
        /// <remarks>The sequence scheduled date</remarks>
        private DateTime? LastReminderSent // No references
        {
            get;
            set;
        }

        /// <summary>
        /// Used only for keeping track of e.g. stock amount of medicine X.
        /// </summary>
        public virtual int? StockAmount // no references
        {
            get;
            set;
        }

        /// <summary>
        /// Last time the stock amount was recalculated.
        /// </summary>
        public virtual DateTime? LastStockAmountCalculation // no references
        {
            get;
            set;
        }

        /// <summary>
        /// The Patient
        /// </summary>
        public virtual Patient Patient
        {
            get;
            set;
        }

        /// <summary>
        /// The Schedule
        /// </summary>
        public virtual Schedule Schedule
        {
            get;
            set;
        }

        /// <summary>
        /// The Delegation <see cref="Taxon"/> permission for any <see cref="Task"/>
        /// </summary>
        public virtual Taxon Taxon
        {
            get;
            set;
        }

        /// <summary>
        /// The Role <see cref="Taxon"/> permission for any <see cref="Task"/>
        /// </summary>
        public virtual Role Role
        {
            get;
            set;
        }

        /// <summary>
        /// If <see cref="Tasks"/> from this sequence should be visible on the overview
        /// </summary>
        public virtual bool Overview
        {
            get;
            set;
        }

        /// <summary>
        /// Overrides CanRaiseAlerts in <see cref="ScheduleSettings"/> for Calendar-sequences
        /// </summary>
        public virtual bool CanRaiseAlert
        {
            get;
            set;
        }

        /// <summary>
        /// If true, then alerts from tasks wont be produced.
        /// </summary>
        public virtual bool PauseAnyAlerts
        {
            get;
            set;
        }

        /// <summary>
        /// If is all day or not.
        /// </summary>
        public virtual bool AllDay
        {
            get;
            set;
        }

        /// <summary>
        /// If this event is an abscence or not.
        /// </summary>
        public virtual bool Absent
        {
            get;
            set;
        }

        /// <summary>
        /// Information about Refill and ordering status
        /// </summary>
        public virtual RefillModel RefillInfo
        {
            get;
            set;
        }

        /// <summary>
        /// The inventory
        /// </summary>
        public virtual Inventory Inventory
        {
            get;
            set;
        } 

        #endregion

        #region Members

        /// <summary>
        /// Returns the next date in the sequence from the given date
        /// </summary>
        /// <param name="date">Current date in sequence</param>
        /// <returns>Next date in sequence</returns>
        public virtual DateTime GetNextDateInSequence(DateTime date)
        {
            var factor = this.IntervalFactor < 1 ? 1 : this.IntervalFactor;

            if (this.Interval.Equals(Weekly))
            {
                var days = factor * 7;
                return date.AddDays(days);
            }

            if (this.Interval.Equals(Yearly))
            {
                return date.AddYears(1);
            }

            if (this.Interval.Equals(Monthly))
            {
                //// Repeat on given date (eg. 10 every month)
                if (this.IntervalIsDate)
                {
                    return date.AddMonths(factor);
                }
                //// Repeat on given day (eg. 2 monday every month)
                else
                {
                    var newDate = date.AddMonths(factor);

                    while (newDate.Day % 7 != 0)
                    {
                        if (newDate.DayOfWeek == date.DayOfWeek)
                        {
                            return newDate;
                        }
                        newDate = newDate.AddDays(1);
                    }
                    if (newDate.DayOfWeek == date.DayOfWeek)
                    {
                        return newDate;
                    }
                    newDate = newDate.AddDays(-1);
                    while (newDate.Day % 7 != 0)
                    {
                        if (newDate.DayOfWeek == date.DayOfWeek)
                        {
                            return newDate;
                        }
                        newDate = newDate.AddDays(-1);
                    }

                    return newDate;
                }
            }

            //// Interval is specified in dates
            return date.AddDays(this.Interval);
        }

        #endregion
    }
}