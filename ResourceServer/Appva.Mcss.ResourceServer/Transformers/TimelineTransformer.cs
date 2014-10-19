// <copyright file="TimelineTransformer.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a></author>
namespace Appva.Mcss.ResourceServer.Transformers
{
    #region Imports

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Mcss.Domain.Entities;
    using Appva.Mcss.ResourceServer.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public static class TimelineTransformer
    {
        /// <summary>
        /// TODO: Summary.
        /// </summary>
        /// <param name="date">TODO: date</param>
        /// <param name="groupingStrategy">TODO: groupingStrategy</param>
        /// <param name="tasks">TODO: tasks</param>
        /// <param name="historyLength">TODO: historyLength</param>
        /// <param name="patientsWithDelay">TODO: patientsWithDelay</param>
        /// <returns>TODO: returns</returns>
        public static TimelineGroupModel<TimelineTaxonGroupingStrategyModel> FixMe(DateTime date, string groupingStrategy, IList<Task> tasks, int historyLength, IList<Guid> patientsWithDelay)
        {
            var dateDict = FixMeToo(tasks);
            var grouping = new TimelineGroupModel<TimelineTaxonGroupingStrategyModel>
            {
                PreviousDate = DateTime.Now.AddDays(-historyLength).Date < date.Date ? string.Format("{0:yyyy-MM-dd}", date.AddDays(-1)) : null,
                CurrentDate = string.Format("{0:yyyy-MM-dd}", date),
                NextDate = string.Format("{0:yyyy-MM-dd}", date.AddDays(1)),
                GroupingStrategy = groupingStrategy,
            };
            var entities = new List<TimelineTaxonGroupingStrategyModel>();
            foreach (var item in dateDict)
            {
                foreach (var patientKv in item.Value)
                {
                    switch (groupingStrategy)
                    {
                        case "taxon":
                            entities.Add(TaxonTimeline(item, patientKv, patientsWithDelay));
                            break;
                        case "patient":
                            entities.AddRange(PatientTimeline(item, patientKv, date, patientsWithDelay));
                            break;
                    }
                }
            }
            grouping.Entities = entities.OrderBy(x => x.DateTimeScheduled).ToList();
            return grouping;
        }

        /// <summary>
        /// TODO: Summary.
        /// </summary>
        /// <param name="tasks">TODO: tasks</param>
        /// <returns>TODO: returns</returns>
        public static Dictionary<DateTime, Dictionary<Patient, Dictionary<ScheduleSettings, IList<Task>>>> FixMeToo(IList<Task> tasks)
        {
            var timeOfDayMap = new Dictionary<DateTime, Dictionary<Patient, Dictionary<ScheduleSettings, IList<Task>>>>();
            foreach (var item in tasks)
            {
                if (timeOfDayMap.ContainsKey(item.Scheduled))
                {
                    if (!timeOfDayMap[item.Scheduled].ContainsKey(item.Patient))
                    {
                        //// var map = new Dictionary<Patient,Dictionary<ScheduleSettings,IList<Task>>>();
                        //// map.Add(item.Patient, new Dictionary<ScheduleSettings,IList<Task>>());
                        timeOfDayMap[item.Scheduled].Add(item.Patient, new Dictionary<ScheduleSettings, IList<Task>>());
                    }
                    var scheduleMap = timeOfDayMap[item.Scheduled][item.Patient];
                    var scheduleSettings = item.Schedule.ScheduleSettings;
                    if (item.Schedule.ScheduleSettings.CombineWith != null)
                    {
                        scheduleSettings = item.Schedule.ScheduleSettings.CombineWith;
                    }

                    if (scheduleMap.ContainsKey(scheduleSettings))
                    {
                        scheduleMap[scheduleSettings].Add(item);
                    }
                    else
                    {
                        scheduleMap.Add(scheduleSettings, new List<Task> { item });
                    }
                }
                else
                {
                    var map = new Dictionary<Patient, Dictionary<ScheduleSettings, IList<Task>>>();
                    map.Add(item.Patient, new Dictionary<ScheduleSettings, IList<Task>>());
                    var scheduleSettings = item.Schedule.ScheduleSettings;
                    if (item.Schedule.ScheduleSettings.CombineWith != null)
                    {
                        scheduleSettings = item.Schedule.ScheduleSettings.CombineWith;
                    }
                    map[item.Patient].Add(scheduleSettings, new List<Task>() { item });
                    timeOfDayMap.Add(item.Scheduled, map);
                }
            }
            return timeOfDayMap;
        }

        /// <summary>
        /// TODO: Summary.
        /// </summary>
        /// <param name="item">TODO: item</param>
        /// <param name="patientKv">TODO: patientKv</param>
        /// <param name="patientsWithDelay">TODO: patientsWithDelay</param>
        /// <returns>TODO: returns</returns>
        private static TimelineTaxonGroupingStrategyModel TaxonTimeline(KeyValuePair<DateTime, Dictionary<Patient, Dictionary<ScheduleSettings, IList<Task>>>> item, KeyValuePair<Patient, Dictionary<ScheduleSettings, IList<Task>>> patientKv, IList<Guid> patientsWithDelay)
        {
            var patient = patientKv.Key;
            var groups = new List<string> { };
            var hasDelays = false;
            CompletedDetailsModel isCompleted = new CompletedDetailsModel();
            DateTime dateStart = item.Key;
            DateTime dateEnd = item.Key;
            foreach (var j in patientKv.Value)
            {
                groups.Add(j.Key.Name);
                foreach (var k in j.Value)
                {
                    if (dateStart > k.Scheduled.AddMinutes(-k.RangeInMinutesBefore))
                    {
                        dateStart = k.Scheduled.AddMinutes(-k.RangeInMinutesBefore);
                    }
                    if (dateEnd < k.Scheduled.AddMinutes(k.RangeInMinutesAfter))
                    {
                        dateEnd = k.Scheduled.AddMinutes(k.RangeInMinutesBefore);
                    }
                    if (k.Delayed && (!k.DelayHandled))
                    {
                        hasDelays = true;
                    }
                    if (!k.IsCompleted)
                    {
                        isCompleted = null;
                    }
                    if (k.IsCompleted && isCompleted != null)
                    {
                        if (isCompleted.Time < k.CompletedDate.GetValueOrDefault())
                        {
                            isCompleted.Time = k.CompletedDate.GetValueOrDefault();
                        }
                        if (!isCompleted.Accounts.Contains(k.CompletedBy.FullName))
                        {
                            isCompleted.Accounts.Add(k.CompletedBy.FullName);
                        }
                    }
                }
            }
            return new TimelineTaxonGroupingStrategyModel
            {
                DateTimeScheduled = string.Format("{0:u}", item.Key),
                DateTimeStart = string.Format("{0:u}", dateStart),
                DateTimeEnd = string.Format("{0:u}", dateEnd),
                Patient = new
                {
                    Id = patient.Id,
                    FullName = patient.FullName,
                    HasIncompleteTasks = patientsWithDelay.Contains(patient.Id)
                },
                Categories = groups,
                HasIncompleteTasks = hasDelays,
                Completed = isCompleted
            };
        }

        /// <summary>
        /// TODO: Summary.
        /// </summary>
        /// <param name="item">TODO: item</param>
        /// <param name="patientKv">TODO: patientKv</param>
        /// <param name="currentDate">TODO: currentDate</param>
        /// <param name="patientsWithDelay">TODO: patientsWithDelay</param>
        /// <returns>TODO: returns</returns>
        private static List<TimelineTaxonGroupingStrategyModel> PatientTimeline(KeyValuePair<DateTime, Dictionary<Patient, Dictionary<ScheduleSettings, IList<Task>>>> item, KeyValuePair<Patient, Dictionary<ScheduleSettings, IList<Task>>> patientKv, DateTime currentDate, IList<Guid> patientsWithDelay)
        {
            var patient = patientKv.Key;
            var hasDelays = false;
            CompletedDetailsModel isCompleted = new CompletedDetailsModel();
            var timelineObj = new List<TimelineTaxonGroupingStrategyModel>();
            DateTime dateStart = item.Key;
            DateTime dateEnd = item.Key;
            foreach (var j in patientKv.Value)
            {
                hasDelays = false;
                isCompleted = new CompletedDetailsModel();
                dateStart = item.Key;
                dateEnd = item.Key;
                foreach (var k in j.Value)
                {
                    if (dateStart > k.Scheduled.AddMinutes(-k.RangeInMinutesBefore))
                    {
                        dateStart = k.Scheduled.AddMinutes(-k.RangeInMinutesBefore);
                    }
                    if (dateEnd < k.Scheduled.AddMinutes(k.RangeInMinutesAfter))
                    {
                        dateEnd = k.Scheduled.AddMinutes(k.RangeInMinutesBefore);
                    }
                    if (k.Delayed && (!k.DelayHandled) && (!k.IsCompleted))
                    {
                        hasDelays = true;
                    }
                    if (! k.IsCompleted)
                    {
                        isCompleted = null;
                    }
                    if (k.IsCompleted && isCompleted != null)
                    {
                        if (isCompleted.Time < k.CompletedDate.GetValueOrDefault())
                        {
                            isCompleted.Time = k.CompletedDate.GetValueOrDefault();
                        }
                        if (!isCompleted.Accounts.Contains(k.CompletedBy.FullName))
                        {
                            isCompleted.Accounts.Add(k.CompletedBy.FullName);
                        }
                    }
                    if (j.Key.ScheduleType == ScheduleType.Calendar)
                    {
                        DateTime scheduled = currentDate.Date;
                        if (k.StartDate.GetValueOrDefault().Date == currentDate.Date)
                        {
                            scheduled = k.StartDate.GetValueOrDefault();
                        }
                        else if (k.EndDate.GetValueOrDefault().Date == currentDate.Date)
                        {
                            scheduled = k.EndDate.GetValueOrDefault();
                        }
                        timelineObj.Add(new TimelineTaxonGroupingStrategyModel()
                        {
                            DateTimeScheduled = string.Format("{0:u}", scheduled),
                            DateTimeStart = string.Format("{0:u}", k.StartDate),
                            DateTimeEnd = string.Format("{0:u}", k.EndDate.GetValueOrDefault()),
                            Categories = new List<string> { j.Key.Name },
                            HasIncompleteTasks = hasDelays,
                            Patient = new 
                            {
                                Id = patient.Id,
                                FullName = patient.FullName,
                                HasIncompleteTasks = patientsWithDelay.Contains(patient.Id)
                                //// FIXME: Maybe implement a new property on patient which indicates this to avoid flood of db
                            },
                            Completed = isCompleted,
                            TypeId = j.Key.ScheduleType == ScheduleType.Action ? "ordination" : "calendar",
                            Sequence = k.Sequence.Id
                        });
                    }
                }
                if (j.Key.ScheduleType == ScheduleType.Action)
                {
                    timelineObj.Add(new TimelineTaxonGroupingStrategyModel
                    {
                        DateTimeScheduled = string.Format("{0:u}", item.Key),
                        DateTimeStart = string.Format("{0:u}", dateStart),
                        DateTimeEnd = string.Format("{0:u}", dateEnd),
                        Categories = new List<string>() { j.Key.Name },
                        HasIncompleteTasks = hasDelays,
                        Patient = new 
                        {
                            Id = patient.Id,
                            FullName = patient.FullName,
                            HasIncompleteTasks = patientsWithDelay.Contains(patient.Id)
                            //// FIXME: MAybe implement a new property on patient which indicates this to avoid flood of db
                        },
                        Completed = isCompleted,
                        TypeId = j.Key.ScheduleType == ScheduleType.Action ? "ordination" : "calendar"
                    });
                }
            }
            return timelineObj;
        }
    }
}