// <copyright file="TimeSpanDifference.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Admin.Utils.Html
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public static class TimeSpanDifference
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <param name="today"></param>
        /// <param name="yesterday"></param>
        /// <returns></returns>
        public static string ToShortDateTime(DateTime date, string today, string yesterday)
        {
            if (date.Date.Equals(DateTime.Now.Date))
            {
                return today;
            }
            if (date.Date.Equals(DateTime.Now.AddDays(-1).Date))
            {
                return yesterday;
            }
            return date.Date.ToShortDateString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static string ToReadableText(DateTime start, DateTime end)
        {
            return ToReadableText(end.Subtract(start));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timespan"></param>
        /// <returns></returns>
        public static string ToReadableText(TimeSpan timespan)
        {
            var minutes = Math.Abs(timespan.Minutes);
            var hours = Math.Abs(timespan.Hours);
            var days = Math.Abs(timespan.Days);
            var retval = string.Empty;
            if (days > 0)
            {
                retval += string.Format("{0} {1}", days, (days > 1) ? "dagar" : "dag");
            }
            if (hours > 0)
            {
                retval += string.Format("{0}{1} {2}", (days > 0) ? ", " : "", hours, (hours > 1) ? "timmar" : "timma");
            }
            if (minutes > 0)
            {
                retval += string.Format("{0}{1} {2}", (hours > 0) ? " och " : "", minutes, (minutes > 1) ? "minuter" : "minut", (hours > 0) ? " " : "");
            }
            return retval;
        }

        public static string ToReadableFromToday(DateTime time)
        {
            var timeSpan = DateTime.Now.Date.Subtract(time.Date);
            switch (timeSpan.Days)
            {
                case 0:
                    return "Idag";
                case 1:
                    return "Igår";
                default:
                    return string.Format("{0} dagar sedan", timeSpan.Days);
            }
        }
    }
}