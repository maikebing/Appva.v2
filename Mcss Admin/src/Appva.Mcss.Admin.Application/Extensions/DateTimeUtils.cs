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
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Core.Extensions;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class DateTimeUtils
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dates"></param>
        /// <returns></returns>
        public static IList<DateTime> FromStringToDateTime(string dates)
        {
            var result = new List<DateTime>();
            if (string.IsNullOrWhiteSpace(dates))
            {
                return result;
            }
            foreach (var dateString in dates.Split(','))
            {
                DateTime date;
                if (DateTime.TryParse(dateString, out date))
                {
                    result.Add(date);
                }
            }
            return result;
        }

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
        /// <param name="any"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="intervalInDays"></param>
        /// <param name="dates"></param>
        public static bool IsOccurring(DateTime any, DateTime startDate, DateTime? endDate, int interval, string dates, ScheduleType scheduleType, int intervalFactor = 0, bool IntervalIsDate = true)
        {
            if (
                startDate > any ||
                (any > ((endDate.HasValue) ? endDate.Value.Date : any.Tomorrow()) && scheduleType != ScheduleType.Calendar)
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
            for (DateTime date = startDate; date <= any; date = date.AddDays(IntervalInDays(interval, date, startDate, intervalFactor, !IntervalIsDate)))
            {
                if (any == date)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool DateIsCoveredByEvent(DateTime any, DateTime startDate, DateTime endDate, int interval, string dates, int intervalFactor, bool IntervalIsDate)
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
                start = start.AddDays(IntervalInDays(interval, start, startDate, intervalFactor, !IntervalIsDate));
                end = end.AddDays(IntervalInDays(interval, end, endDate, intervalFactor, !IntervalIsDate));
            }
            return false;
        }

        public static int IntervalInDays(int interval, DateTime date, DateTime start, int factor = 0, bool onDayOfWeek = false)
        {
            if (interval == 31)
            {
                int f = (factor > 0 ? factor : 1);
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

        public static int AdjustToDayOfWeek(int daysToAdd, DateTime date, DateTime start)
        {
            DateTime newDate = date.FirstOfMonth();
            int weekDayInMonth = ((start.Day - (start.Day % 7)) / 7);
            int current = 0;
            while (!((current == weekDayInMonth || newDate.DaysInMonth() - newDate.Day < 8) && start.DayOfWeek == newDate.DayOfWeek))
            {
                newDate = newDate.AddDays(1);
                if (newDate.DayOfWeek == start.DayOfWeek)
                {
                    current = ((newDate.Day - (newDate.Day % 7)) / 7);
                }
            }
            return daysToAdd + (Convert.ToInt32(newDate.Subtract(date).TotalDays));
        }
    }
}