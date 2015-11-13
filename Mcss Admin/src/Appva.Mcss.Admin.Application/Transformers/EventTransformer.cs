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
        public static IList<CalendarTask> ToEvent(IList<Task> tasks)
        {
            var retval = new List<CalendarTask>();
            foreach (var t in tasks)
            {
                retval.Add(ToEvent(t));
            }
            return retval;
        }

        public static CalendarTask ToEvent(Task t)
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
                RepeatAtGivenDay = t.Sequence.IntervalIsDate,
                PatientId = t.Patient.Id
            };
        }
    }
}