// <copyright file="Sequence.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class Sequence : AggregateRoot
    {
        #region Variables.

        /// <summary>
        /// Weekly interval.
        /// </summary>
        private const int Weekly  = 7;

        /// <summary>
        /// Monthlu interval.
        /// </summary>
        private const int Monthly = 31;

        /// <summary>
        /// Yearly interval.
        /// </summary>
        private const int Yearly  = 365;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="Sequence"/> class.
        /// </summary>
        public Sequence()
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
        public virtual string Hour
        {
            get;
            set;
        }

        /// <summary>
        /// The minute of hour
        /// </summary>
        public virtual string Minute
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
        public virtual Account ReminderRecipient
        {
            get;
            set;
        }

        /// <summary>
        /// When the last reminder was sent out.
        /// </summary>
        /// <remarks>The sequence scheduled date</remarks>
        public virtual DateTime? LastReminderSent
        {
            get;
            set;
        }

        /// <summary>
        /// Used only for keeping track of e.g. stock amount of medicine X.
        /// </summary>
        public virtual int? StockAmount
        {
            get;
            set;
        }

        /// <summary>
        /// Last time the stock amount was recalculated.
        /// </summary>
        public virtual DateTime? LastStockAmountCalculation
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

        #region Public Methods.

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