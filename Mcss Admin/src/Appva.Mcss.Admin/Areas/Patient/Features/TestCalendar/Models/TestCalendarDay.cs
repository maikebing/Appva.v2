// <copyright file="TestCalendarDay.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Patient.Features.TestCalendar.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Appva.Domain;
    using Appva.Mcss.Admin.Areas.Patient.Features.TestCalender.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class TestCalendarDay
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
        public Date Date
        {
            get;
            set;
        }

        /// <summary>
        /// A list of events starting this day
        /// </summary>
        public IList<TestCalendarTask> Events
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