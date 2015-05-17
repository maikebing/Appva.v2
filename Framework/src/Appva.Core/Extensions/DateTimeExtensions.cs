// <copyright file="DateTimeExtensions.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Core.Extensions
{
    #region Imports.

    using System;
    using System.Globalization;
    using Utilities;

    #endregion

    /// <summary>
    /// Extension helpers for date time.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Returns whether the date time is greater than the other date time.
        /// </summary>
        /// <param name="dateTime">The date time to be checked</param>
        /// <param name="other">The date time to compare</param>
        /// <returns>True if the date time is greater than the other</returns>
        public static bool IsGreaterThan(this DateTime dateTime, DateTime other)
        {
            return dateTime > other;
        }

        /// <summary>
        /// Returns whether the date time is less than the other date time.
        /// </summary>
        /// <param name="dateTime">The date time to be checked</param>
        /// <param name="other">The date time to compare</param>
        /// <returns>True if the date time is less than the other</returns>
        public static bool IsLessThan(this DateTime dateTime, DateTime other)
        {
            return dateTime < other;
        }

        /// <summary>
        /// Returns the last instant of the day.
        /// </summary>
        /// <param name="dateTime">The base date time</param>
        /// <returns>The last instance of the day e.g. Y-m-dT23:59:59:999</returns>
        public static DateTime LastInstantOfDay(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59, 999);
        }

        /// <summary>
        /// Returns a date time as UTC. 
        /// </summary>
        /// <param name="dateTime">The date time to be in UTC</param>
        /// <returns>A dat time in UTC</returns>
        public static DateTime ToUtc(this DateTime dateTime)
        {
            return (dateTime.Kind == DateTimeKind.Unspecified) ? new DateTime(dateTime.Ticks, DateTimeKind.Utc) : dateTime.ToUniversalTime();
        }


        //////////// FROM UTILS

        public static DateTime SetYear(this DateTime date, int? year)
        {
            if (!year.HasValue)
            {
                return date;
            }
            return new DateTime(year.Value, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Millisecond);
        }

        public static DateTime SetMonth(this DateTime date, bool condition, int? month)
        {
            if (! condition)
            {
                return date;
            }
            if (! month.HasValue)
            {
                return date;
            }
            return new DateTime(date.Year, month.Value, date.Day, date.Hour, date.Minute, date.Second, date.Millisecond);
        }

        /// <summary>
        /// Returns the a DateTime representing tomorrow.
        /// </summary>
        public static DateTime Tomorrow(this DateTime date)
        {
            return date.Date.AddDays(1);
        }

        /// <summary>
        /// Returns the a DateTime representing yesterday.
        /// </summary>
        public static DateTime Yesterday(this DateTime date)
        {
            return date.Date.AddDays(-1);
        }

        /// <summary>
        /// Checks if a DateTime is within two intervals
        /// </summary>
        public static bool Within(this DateTime date, DateTime intervalBefore, DateTime intervalAfter)
        {
            return (date >= intervalBefore && date <= intervalAfter);
        }

        public static DateTime NextMonth(this DateTime date)
        {
            return date.AddMonths(1);
        }

        public static DateTime PreviousMonth(this DateTime date)
        {
            return date.AddMonths(-1);
        }

        /// <summary>
        /// Returns the days in the current month.
        /// </summary>
        /// <param name="date"></param>
        public static int DaysInMonth(this DateTime date)
        {
            return DateTime.DaysInMonth(date.Year, date.Month);
        }

        public static DateTime FirstOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1, 0, 0, 0, 0);
        }

        public static DateTime LastOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month), 0, 0, 0, 0);
        }

        public static DateTime FirstDateOfWeek(this DateTime date)
        {
            var firstDayOfWeek = DateTimeUtilities.FirstDayOfWeek();
            while (date.DayOfWeek != firstDayOfWeek)
            {
                date = date.AddDays(-1);
            }
            return date;
        }

        public static int GetWeekNumber(this DateTime date)
        {
            if (date.DayOfWeek.Equals(DayOfWeek.Monday) || date.DayOfWeek.Equals(DayOfWeek.Tuesday) || date.DayOfWeek.Equals(DayOfWeek.Wednesday))
            {
                date = date.AddDays(3);
            }
            var cal = new GregorianCalendar(GregorianCalendarTypes.Localized);
            return cal.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

    }
}
