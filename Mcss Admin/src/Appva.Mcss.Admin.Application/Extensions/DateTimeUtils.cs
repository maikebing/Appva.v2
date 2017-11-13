// <copyright file="DateTimeUtils.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Extensions
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Core.Extensions;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Domain;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class DateTimeUtils
    {
        /// <summary>
        /// Converts a comma separated string to a list of <c>DateTime</c>
        /// </summary>
        /// <param name="dates">The comma separated date string</param>
        /// <returns>A list of <see cref="DateTime"/></returns>
        public static IEnumerable<DateTime> Parse(string dates)
        {
            if (string.IsNullOrWhiteSpace(dates))
            {
                return new List<DateTime>();
            }
            return DateTimeUtils.Parse(dates.Split(','));
        }

        /// <summary>
        /// Parses a list of strings representing dates
        /// </summary>
        /// <param name="dateStrings">List of strings representing dates</param>
        /// <returns>List of dates</returns>
        public static IEnumerable<DateTime> Parse(IList<string> dateStrings)
        {
            if (dateStrings.IsNull())
            {
                return new List<DateTime>();
            }
            DateTime dateTime;
            List<DateTime> dateTimes = new List<DateTime>();
            foreach (var date in dateStrings)
            {
                if (DateTime.TryParse(date, out dateTime))
                {
                    dateTimes.Add(dateTime);
                }
            }
            return dateTimes;
        }

        /// <summary>
        /// Gets the earliest and latest date from a list of dates
        /// </summary>
        /// <param name="dates">List of <code>DateTime</code></param>
        /// <param name="earliest">Out param for the earliest time in list</param>
        /// <param name="latest">Out param for the latest time in list</param>
        public static void GetEarliestAndLatestDateFrom(IList<DateTime> dates, out DateTime earliest, out DateTime? latest)
        {
            if (dates.IsNotNull() && dates.Count() > 0)
            {
                earliest = dates.Min();
                latest = dates.Max();
            }
            else
            {
                earliest = DateTime.Now.Date;
                latest = DateTime.Now.AddDays(1).Date;
            }

        }

        /// <summary>
        /// Gets the earliest and latest date from a list of strings representing dates 
        /// </summary>
        /// <param name="dateStrings">List of strings representing dates</param>
        /// <param name="earliest">Out param for the earliest time in list</param>
        /// <param name="latest">Out param for the latest time in list</param>
        public static void GetEarliestAndLatestDateFrom(IList<string> dateStrings, out DateTime earliest, out DateTime? latest)
        {
            var dates = DateTimeUtils.Parse(dateStrings).ToList();
            GetEarliestAndLatestDateFrom(dates, out earliest, out latest);
        }

        /// <summary>
        /// Returns whether or not the specified date is covered within the start and end
        /// date time span.
        /// </summary>
        /// <param name="date">The date to check is within date span</param>
        /// <param name="start">The start date</param>
        /// <param name="end">The end date</param>
        /// <param name="interval">The interval in days</param>
        /// <param name="factor">The interval factor</param>
        /// <param name="alternativeRange">An alternative list of dates to check</param>
        /// <returns>True if the date is within the date span; otherwise false</returns>
        public static bool IsDateOccurringWithinSpan(DateTime date, DateTime start, DateTime? end, int interval = 0, int factor = 0, IList<DateTime> alternativeRange = null)
        {
            if (date < start || date > end)
            {
                return false;
            }
            if (interval <= 1)
            {
                return true;
            }
            if (alternativeRange != null && alternativeRange.Count > 0)
            {
                return alternativeRange.Contains(date);
            }
            for (var current = start; current <= date; current = current.AddDays(IntervalInDays(interval, current, start, factor)))
            {
                if (current == date)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks if a date with an interval, e.g. is every other day, every third day, every fourth day,
        /// is occuring on another date.
        /// </summary>
        /// <param name="any">The date to check</param>
        /// <param name="startDate">The start date</param>
        /// <param name="endDate">The end date</param>
        /// <param name="interval">The interval in days</param>
        /// <param name="dates">Alternative dates</param>
        /// <param name="scheduleType">The schedule type</param>
        /// <param name="intervalFactor">The interval factor</param>
        /// <param name="intervalIsDate">Whether or not the interval is date or not</param>
        /// <returns>True if the date is within the date span; otherwise false</returns>
        public static bool IsOccurring(DateTime any, DateTime startDate, DateTime? endDate, int interval, string dates, ScheduleType scheduleType, int intervalFactor = 0, bool intervalIsDate = true)
        {
            if (
                startDate > any ||
                (any > (endDate.HasValue ? endDate.Value.Date : any.Tomorrow()) && scheduleType != ScheduleType.Calendar)
            )
            {
                return false;
            }
            if (interval == 1)
            {
                return true;
            }
            if (dates.IsNotNull())
            {
                var strArray = dates.Split(',');
                foreach (var str in strArray)
                {
                    DateTime date = DateTime.Now;
                    if (DateTime.TryParse(str, out date))
                    {
                        if (any == date)
                        {
                            return true;
                        }
                    }
                }
            }
            if (interval == 0)
            {
                if (scheduleType == ScheduleType.Calendar && any == endDate.GetValueOrDefault().Date)
                {
                    return true;
                }
                return false;
            }
            if (scheduleType.Equals(ScheduleType.Calendar))
            {
                startDate = endDate.GetValueOrDefault().Date;
            }
            for (var date = startDate; date <= any; date = date.AddDays(IntervalInDays(interval, date, startDate, intervalFactor, !intervalIsDate)))
            {
                if (any == date)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Returns whether or not the date is covered by an event.
        /// </summary>
        /// <param name="any">The date to check</param>
        /// <param name="startDate">The start date</param>
        /// <param name="endDate">The end date</param>
        /// <param name="interval">The interval in days</param>
        /// <param name="dates">Alternative dates</param>
        /// <param name="intervalFactor">The interval factor</param>
        /// <param name="intervalIsDate">Whether or not the interval is date or not</param>
        /// <returns>True if the date is covered by an event; otherwise false</returns>
        public static bool DateIsCoveredByEvent(DateTime any, DateTime startDate, DateTime endDate, int interval, string dates, int intervalFactor, bool intervalIsDate)
        {
            if (startDate > any)
            {
                return false;
            }
            if (interval == 1)
            {
                return true;
            }
            var start = startDate;
            var end = endDate;
            if (interval == 0)
            {
                if (start.Date <= any.Date && end.Date >= any.Date)
                {
                    return true;
                }
                return false;
            }
            while (start.Date <= any.Date)
            {
                if (start.Date <= any.Date && end.Date >= any.Date)
                {
                    return true;
                }
                start = start.AddDays(IntervalInDays(interval, start.Date, startDate.Date, intervalFactor, !intervalIsDate));
                end = end.AddDays(IntervalInDays(interval, end.Date, endDate.Date, intervalFactor, !intervalIsDate));
            }
            return false;
        }

        /// <summary>
        /// TODO: what does this function do?
        /// </summary>
        /// <param name="interval">The initial interval?</param>
        /// <param name="date">The date to check?</param>
        /// <param name="start">The start date</param>
        /// <param name="factor">The factor</param>
        /// <param name="onDayOfWeek">TODO: what does this do?</param>
        /// <returns>The interval in days</returns>
        public static int IntervalInDays(int interval, DateTime date, DateTime start, int factor = 0, bool onDayOfWeek = false)
        {
            if (interval == 31)
            {
                int f = factor > 0 ? factor : 1;
                int retval = 0;
                for (int i = 0; i < f; i++)
                {
                    int days = 0;
                    if (date.Month == 1 && start.Day > DateTime.DaysInMonth(date.Year, 2))
                    {
                        days = DateTime.DaysInMonth(date.Year, date.Month) - (start.Day - DateTime.DaysInMonth(date.Year, 2));
                    }
                    else if (date.Month == 2 && start.Day > DateTime.DaysInMonth(date.Year, 2))
                    {
                        days = DateTime.DaysInMonth(date.Year, date.Month) + (start.Day - DateTime.DaysInMonth(date.Year, 2));
                    }
                    else if (start.Day == 31)
                    {
                        days = DateTime.DaysInMonth(date.AddMonths(1).Year, date.AddMonths(1).Month);
                    }
                    else
                    {
                        days = DateTime.DaysInMonth(date.Year, date.Month);
                    }
                    date = date.AddDays(days);
                    retval += days;
                }
                if (onDayOfWeek)
                {
                    return AdjustToDayOfWeek(retval, date, start);
                }
                return retval;
            }
            if (interval == 365)
            {
                if (DateTime.IsLeapYear(date.Year))
                {
                    return 366;
                }
                return 365;
            }
            if (factor > 0)
            {
                return interval * factor;
            }
            return interval;
        }

        /// <summary>
        /// TODO: what does this function do?
        /// </summary>
        /// <param name="daysToAdd">TODO: what is daysToAdd?</param>
        /// <param name="date">TODO: what is date?</param>
        /// <param name="start">TODO: what is start?</param>
        /// <returns>TODO: does this return and why?</returns>
        public static int AdjustToDayOfWeek(int daysToAdd, DateTime date, DateTime start)
        {
            DateTime newDate = date.FirstOfMonth();
            int weekDayInMonth = (start.Day - (start.Day % 7)) / 7;
            int current = 0;
            while (!((current == weekDayInMonth || newDate.DaysInMonth() - newDate.Day < 8) && start.DayOfWeek == newDate.DayOfWeek))
            {
                newDate = newDate.AddDays(1);
                if (newDate.DayOfWeek == start.DayOfWeek)
                {
                    current = (newDate.Day - (newDate.Day % 7)) / 7;
                }
            }
            return daysToAdd + Convert.ToInt32(newDate.Subtract(date).TotalDays);
        }
    }
}