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
    using Appva.Domain;

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

        public Sequence(Schedule schedule, string name, string description, Repeat repeat, 
            Taxon taxon = null, Role role = null, Inventory inventory = null, RefillModel refillModel = null,
            bool overview = false, bool canRaiseAlert = false, bool pauseAnyAlerts = false, bool absent = false, 
            bool reminder = false)
        {
            Requires.NotNull(schedule, "schedule");
            Requires.NotNullOrEmpty(name, "name");
            Requires.NotNull(repeat, "repeat");

            this.Schedule = schedule;
            this.Patient = schedule.Patient;
            this.Name = name;
            this.Description = description;
            this.Repeat = repeat;
            this.Taxon = taxon;
            this.Role = role;
            this.Inventory = inventory;
            this.RefillInfo = refillModel;
            this.Overview = overview;
            this.CanRaiseAlert = canRaiseAlert;
            this.PauseAnyAlerts = pauseAnyAlerts;
            this.Absent = absent;
            this.Reminder = reminder;
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
        /// The <see cref="Repeat"/> object (rules for repititative events/tasks).
        /// </summary>
        public virtual Repeat Repeat
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
        public virtual DateTime GetNextDateInSequence(DateTime dateTime)
        {
            var date = (Date)dateTime;
            var next = this.Repeat.Next(date);
            return next;
        
            //return this.Repeat.Next((Date) date);
            
            /*var factor = this.Repeat.IntervalFactor < 1 ? 1 : this.IntervalFactor;

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
            return date.AddDays(this.Interval);*/
        }

        #endregion
    }
}