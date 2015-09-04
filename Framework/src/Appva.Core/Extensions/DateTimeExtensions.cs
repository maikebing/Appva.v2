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
        /// <returns>The last instance of the day e.g. <c>Y-m-dT23:59:59:999</c></returns>
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

        /// <summary>
        /// Returns the a date time representing tomorrow.
        /// FIXME: Should this be zero:ed out?
        /// </summary>
        /// <param name="dateTime">The date time to be altered</param>
        /// <returns>A date time <c>+1</c> day</returns>
        public static DateTime Tomorrow(this DateTime dateTime)
        {
            return dateTime.Date.AddDays(1);
        }

        /// <summary>
        /// Returns the a date time representing yesterday.
        /// FIXME: Should this be zero:ed out?
        /// </summary>
        /// <param name="dateTime">The date time to be altered</param>
        /// <returns>A date time <c>-1</c> day</returns>
        public static DateTime Yesterday(this DateTime dateTime)
        {
            return dateTime.Date.AddDays(-1);
        }

        /// <summary>
        /// Checks if a DateTime is within two intervals
        /// </summary>
        /// <param name="dateTime">The date time to be checked</param>
        /// <param name="intervalBefore">The date time lower bound</param>
        /// <param name="intervalAfter">The date time higher bound</param>
        /// <returns>True if the date time is within the lower and higher bounds</returns>
        public static bool Within(this DateTime dateTime, DateTime intervalBefore, DateTime intervalAfter)
        {
            return dateTime >= intervalBefore && dateTime <= intervalAfter;
        }

        /// <summary>
        /// Returns the a date time representing next month.
        /// </summary>
        /// <param name="dateTime">The date time to be altered</param>
        /// <returns>A date time <c>+1</c> month</returns>
        public static DateTime NextMonth(this DateTime dateTime)
        {
            return dateTime.AddMonths(1);
        }

        /// <summary>
        /// Returns the a date time representing previous month.
        /// </summary>
        /// <param name="dateTime">The date time to be altered</param>
        /// <returns>A date time <c>-1</c> month</returns>
        public static DateTime PreviousMonth(this DateTime dateTime)
        {
            return dateTime.AddMonths(-1);
        }

        /// <summary>
        /// Returns the days in the date time month.
        /// </summary>
        /// <param name="dateTime">The date time to be checked</param>
        /// <returns>The amount of days in the date time month</returns>
        public static int DaysInMonth(this DateTime dateTime)
        {
            return DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
        }

        /// <summary>
        /// Returns a date time representing the 1st of the month.
        /// FIXME: Should this be zero:ed out?
        /// </summary>
        /// <param name="dateTime">The date time to be altered</param>
        /// <returns>A date time for the first of the month</returns>
        public static DateTime FirstOfMonth(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1, 0, 0, 0, 0);
        }

        /// <summary>
        /// Returns a date time representing the last day of the month.
        /// FIXME: Isn't last of month 23:59:59:999?
        /// </summary>
        /// <param name="dateTime">The date time to be altered</param>
        /// <returns>A date time for the last day of the month</returns>
        public static DateTime LastOfMonth(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.DaysInMonth(), 0, 0, 0, 0);
        }

        /// <summary>
        /// Returns the first date time of the week.
        /// FIXME: FirstDateOfWeek probably quicker to do one .AddDays(...):
        /// var fdow = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
        /// var diff = dateTime.DayOfWeek - fdow;
        /// if (diff lower than 0)
        /// {
        ///     diff += 7;
        /// }
        /// return dateTime.AddDays(-1 * diff);
        /// </summary>
        /// <param name="dateTime">The date time to be altered</param>
        /// <returns>The first date time of the week</returns>
        public static DateTime FirstDateOfWeek(this DateTime dateTime)
        {
            var firstDayOfWeek = DateTimeUtilities.FirstDayOfWeek();
            while (dateTime.DayOfWeek != firstDayOfWeek)
            {
                dateTime = dateTime.AddDays(-1);
            }
            return dateTime;
        }

        /// <summary>
        /// Returns the date time week number.
        /// FIXME: GetWeekNumber: This calculation looks suspicious, e.g. DateOfWeek.Monday etc,
        /// should probably be more like CultureInfo.CurrentCulture.DateTimeFormat.Calendar.GetDayOfWeek(dateTime).
        /// </summary>
        /// <param name="dateTime">The date time to calculate week number</param>
        /// <returns>The date time week number</returns>
        public static int GetWeekNumber(this DateTime dateTime)
        {
            if (dateTime.DayOfWeek.Equals(DayOfWeek.Monday) || dateTime.DayOfWeek.Equals(DayOfWeek.Tuesday) || dateTime.DayOfWeek.Equals(DayOfWeek.Wednesday))
            {
                dateTime = dateTime.AddDays(3);
            }
            var cal = new GregorianCalendar(GregorianCalendarTypes.Localized);
            return cal.GetWeekOfYear(dateTime, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }
    }
}
