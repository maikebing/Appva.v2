// <copyright file="CalendarWeek.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class CalendarWeek
    {
        #region Properties

        /// <summary>
        /// The number of the week
        /// </summary>
        public int WeekNumber
        {
            get;
            set;
        }

        /// <summary>
        /// The days within the week
        /// </summary>
        public IList<CalendarDay> Days
        {
            get;
            set;
        }

        public List<CalendarTask> AllEvents
        {
            get;
            set;
        }

        #endregion
    }
}