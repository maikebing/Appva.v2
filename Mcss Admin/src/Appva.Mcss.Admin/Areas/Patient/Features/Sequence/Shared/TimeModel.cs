// <copyright file="TimeModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using Appva.Domain;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class TimeModel
    {
        public int Hour
        {
            get;
            set;
        }
        public int Minute
        {
            get;
            set;
        }
        public bool IsChecked
        {
            get;
            set;
        }

        public PartOfDay PartOfDay
        {
            get;
            set;
        }
        public TimeOfDay ToTimeOfDay()
        {
            return new TimeOfDay(this.Hour, this.Minute);
        }
    }

    public class DaysOfWeekModel
    {
        public string Code
        {
            get;
            set;
        }

        public bool IsChecked
        {
            get;
            set;
        }
    }
}