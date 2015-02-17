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

    #endregion

    /// <summary>
    /// Extension helpers for date time.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Returns whether the date time is greater than 
        /// the other date time.
        /// </summary>
        /// <param name="dateTime">The date time to be checked</param>
        /// <param name="other">The date time to compare</param>
        /// <returns>True if the date time is greater than the other</returns>
        public static bool IsGreaterThan(this DateTime dateTime, DateTime other)
        {
            return dateTime > other;
        }

        /// <summary>
        /// Returns whether the date time is less than 
        /// the other date time.
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
    }
}