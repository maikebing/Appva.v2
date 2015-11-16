// <copyright file="Calendar.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using Appva.Mcss.Admin.Domain.Entities;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class CalendarDay
    {
        /// <summary>
        /// Whether or not the calendar event is within month.
        /// </summary>
        public bool IsWithinMonth
        {
            get;
            set;
        }

        /// <summary>
        /// Whether or not the calendar event is today.
        /// </summary>
        public bool IsToday
        {
            get;
            set;
        }

        /// <summary>
        /// The calendar event date.
        /// </summary>
        public DateTime Date
        {
            get;
            set;
        }

        /// <summary>
        /// A list of events starting this day
        /// </summary>
        public IList<CalendarTask> Events
        {
            get;
            set;
        }

        /// <summary>
        /// Number of events
        /// </summary>
        public int NumberOfEvents
        {
            get;
            set;
        }
    }
}