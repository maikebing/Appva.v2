// <copyright file="TaskUtils.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Commands
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using Appva.Mcss.Admin.Domain.Entities;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public static class TaskUtils
    {
        public static Dictionary<DateTime, Dictionary<Schedule, IList<Task>>> MapTimeOfDayAndSchedule(IList<Task> items)
        {
            var TimeOfDayMap = new Dictionary<DateTime, Dictionary<Schedule, IList<Task>>>();
            foreach (var item in items)
            {
                if (TimeOfDayMap.ContainsKey(item.Scheduled))
                {
                    var ScheduleMap = TimeOfDayMap[item.Scheduled];
                    if (ScheduleMap.ContainsKey(item.Schedule))
                    {
                        ScheduleMap[item.Schedule].Add(item);
                    }
                    else
                    {
                        ScheduleMap.Add(item.Schedule, new List<Task>() { item });
                    }
                }
                else
                {
                    var Map = new Dictionary<Schedule, IList<Task>>();
                    Map.Add(item.Schedule, new List<Task>() { item });
                    TimeOfDayMap.Add(item.Scheduled, Map);
                }
            }
            return TimeOfDayMap;
        }
    }
}