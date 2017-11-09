// <copyright file="EventTransformer.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Transformers
{
    #region Imports.

    using Appva.Mcss.Admin.Application.Models;
using Appva.Mcss.Admin.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public static class EventTransformer
    {
        /// <summary>
        /// Converts Tasks to CalendarTasks
        /// </summary>
        /// <param name="tasks"></param>
        /// <returns></returns>
        public static IList<CalendarTask> TasksToEvent(IList<Task> tasks)
        {
            var retval = new List<CalendarTask>();
            foreach (var t in tasks)
            {
                retval.Add(TasksToEvent(t));
            }
            return retval;
        }

        /// <summary>
        /// Convert a Task to a CalendarTask
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static CalendarTask TasksToEvent(Task t)
        {
            return new CalendarTask
            {
                Id = (t.Id == null || t.Id == Guid.Empty) ? Guid.NewGuid() : t.Id,
                TaskId = t.Id,
                StartTime = t.StartDate.GetValueOrDefault(),
                EndTime = t.EndDate.GetValueOrDefault(),
                Description = t.Sequence.Description,
                CategoryName = t.Schedule.ScheduleSettings.Name,
                Color = t.Schedule.ScheduleSettings.Color,
                SequenceId = t.Sequence.Id,
                CategoryId = t.Schedule.ScheduleSettings.Id,
                IsFullDayEvent = t.AllDay,
                NeedsQuittance = t.Overview,
                IsQuittanced = t.Quittanced,
                QuittancedBy = t.QuittancedBy,
                NeedsSignature = t.CanRaiseAlert,
                Signature = t.IsCompleted ? new SignatureModel(t.CompletedBy, t.CompletedDate.Value, t.StatusTaxon) : null,
                Interval = t.Sequence.Interval,
                IntervalFactor = t.Sequence.IntervalFactor,
                RepeatAtGivenDate = t.Sequence.IntervalIsDate,
                PatientId = t.Patient.Id,
                PatientName = t.Patient.FullName
            };
        }

        //internal static CalendarTask SequenceToEvent(Sequence sequence, DateTime startDate, DateTime endDate)
        //{
        //    return new CalendarTask
        //    {
        //        Id = Guid.NewGuid(),
        //        StartTime = startDate,
        //        EndTime = endDate,
        //        Description = sequence.Description,
        //        CategoryName = sequence.Schedule.ScheduleSettings.Name,
        //        Color = sequence.Schedule.ScheduleSettings.Color,
        //        SequenceId = sequence.Id,
        //        CategoryId = sequence.Schedule.ScheduleSettings.Id,
        //        IsFullDayEvent = sequence.AllDay,
        //        NeedsQuittance = sequence.Overview,
        //        NeedsSignature = sequence.CanRaiseAlert,
        //        Interval = sequence.Interval,
        //        IntervalFactor = sequence.IntervalFactor,
        //        RepeatAtGivenDate = sequence.IntervalIsDate,
        //        PatientId = sequence.Patient.Id,
        //        PatientName = sequence.Patient.FullName
        //    };
        //}

        public static CalendarTask SequenceToEvent(Sequence sequence, DateTime startDate, DateTime? endDate)
        {
            return new CalendarTask
            {
                Id = Guid.NewGuid(),
                StartTime = startDate,
                EndTime = endDate.HasValue ? endDate.Value : startDate.AddMonths(3),
                Description = sequence.Description,
                CategoryName = sequence.Schedule.ScheduleSettings.Name,
                Color = sequence.Schedule.ScheduleSettings.Color,
                SequenceId = sequence.Id,
                CategoryId = sequence.Schedule.ScheduleSettings.Id,
                IsFullDayEvent = sequence.AllDay,
                NeedsQuittance = sequence.Overview,
                NeedsSignature = sequence.CanRaiseAlert,
                Interval = sequence.Interval,
                IntervalFactor = sequence.IntervalFactor != 0? sequence.IntervalFactor : 1,
                RepeatAtGivenDate = sequence.IntervalIsDate,
                PatientId = sequence.Patient.Id,
                PatientName = sequence.Patient.FullName
            };
        }
    }
}