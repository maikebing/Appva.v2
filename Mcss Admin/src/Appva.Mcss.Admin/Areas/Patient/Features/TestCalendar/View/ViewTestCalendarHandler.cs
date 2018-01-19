using Appva.Core.Extensions;
using Appva.Cqrs;
using Appva.Mcss.Admin.Application.Models;
using Appva.Mcss.Admin.Application.Services;
using Appva.Mcss.Admin.Domain.Entities;
using Appva.Mcss.Admin.Infrastructure;
using Appva.Mcss.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appva.Domain;
using Appva.Persistence;
using Appva.Mcss.Admin.Areas.Patient.Features.TestCalender.Models;
using Appva.Mcss.Admin.Areas.Patient.Features.TestCalendar.Models;

namespace Appva.Mcss.Admin.Models.Handlers
{
    public class ViewTestCalendarHandler : RequestHandler<ViewTestCalendar, ViewTestCalendarModel>
    {
        private readonly IPatientTransformer patientTransformer;
        private readonly IPersistenceContext context;

        public ViewTestCalendarHandler(IPatientTransformer patientTransformer, IPersistenceContext context)
        {
            this.patientTransformer = patientTransformer;
            this.context = context;
        }

        public override ViewTestCalendarModel Handle(ViewTestCalendar message)
        {
            var date     = (message.Date ?? Date.Today).FirstOfMonth;
            var schedule = this.context.Get<Schedule>(message.ScheduleId);
            var events = this.FindSequencesWithinMonth(schedule.Id, date).OrderBy(x => x.Start).ToList();
            var calendar = this.Calendar(date, events);

            var model = new ViewTestCalendarModel();
            model.Name       = schedule.ScheduleSettings.Name;
            model.ScheduleId = schedule.Id;
            model.Patient    = this.patientTransformer.ToPatient(schedule.Patient);
            model.Current    = date;
            model.Previous   = date.AddMonths(-1);
            model.Next       = date.AddMonths(1);
            model.Calendar   = calendar;
            return model;
        }

        public IEnumerable<TestCalendarTask> FindSequencesWithinMonth(Guid scheduleId, Date firstOfMonth)
        {
            var nextMonth = firstOfMonth.AddMonths(1);
            var sequences = this.context.QueryOver<Sequence>()
                .Where(x => x.IsActive)
                  .And(x => x.Schedule.Id == scheduleId)
                  .And(x => x.Repeat.StartAt < firstOfMonth.AddMonths(1)) /* Simply check by earlier than next month. */
                  .And(x => x.Repeat.EndAt == null || x.Repeat.EndAt >= firstOfMonth) 
                .List();
            var retval = new List<TestCalendarTask>();
            foreach (var sequence in sequences)
            {
                Date nextAt = firstOfMonth;
                while (nextAt < nextMonth)
                {
                    if (! sequence.Repeat.OccurAt(nextAt))
                    {
                        nextAt = nextAt.AddDays(1);
                        continue;
                    }
                    /*if (sequence.Repeat.TimesOfDay.Count() == 0)
                    {
                        retval.Add(this.CreateEvent(nextAt, sequence));
                    }*/
                    foreach (var timeOfDay in sequence.Repeat.TimesOfDay)
                    {
                        //// Ignore any time slots which has passed (if the start time is greater)
                        //// Ignore any offsets in this case as well.
                        var time = nextAt.ToDateTime(timeOfDay);
                        if (sequence.Repeat.StartAt > time || (sequence.Repeat.EndAt ?? DateTime.MaxValue) < time)
                        {
                            continue;
                        }
                        retval.Add(this.CreateEvent(time, sequence));
                    }
                    nextAt = nextAt.AddDays(1);
                }
            }
            return retval.OrderBy(x => x.Start);
        }


        public IList<TestCalendarWeek> Calendar(DateTime date, IList<TestCalendarTask> events)
        {
            var retval  = new List<TestCalendarWeek>();
            var current = date;
            while (current <= date.LastOfMonth())
            {
                retval.Add(CreateWeek(current, events));
                current = current.FirstDateOfWeek().AddDays(7);
            }
            return retval;
        }

        private TestCalendarTask CreateEvent(DateTime date, Sequence sequence)
        {
            return new TestCalendarTask
            {
                Repeat      = sequence.Repeat,
                Start       = date.Add(TimeSpan.FromMinutes(-sequence.Repeat.OffsetBefore)),
                End         = date.Add(TimeSpan.FromMinutes(sequence.Repeat.OffsetAfter)),
                Name        = sequence.Name,
                Description = sequence.Description,
                SequenceId  = sequence.Id,
                PatientId   = sequence.Patient.Id,
                PatientName = sequence.Patient.FullName
            };
        }
        private TestCalendarWeek CreateWeek(DateTime date, IList<TestCalendarTask> events)
        {
            var week = new TestCalendarWeek
            {
                WeekNumber = date.GetWeekNumber(),
                Days = new List<TestCalendarDay>(),
                AllEvents = new List<TestCalendarTask>()
            };

            var current = date.FirstDateOfWeek();
            for (var day = 0; day < 7; day++)
            {
                var d = this.CreateDay(current, date.Month, events);
                week.Days.Add(d);
                week.AllEvents.AddRange(d.Events);
                current = current.AddDays(1);
            }

            return week;
        }

        private TestCalendarDay CreateDay(DateTime date, int currentMonthDisplayed, IList<TestCalendarTask> events)
        {
            if (events.IsNull())
            {
                events = new List<TestCalendarTask>();
            }
            return new TestCalendarDay
            {
                IsWithinMonth = date.Month == currentMonthDisplayed,
                IsToday = date.Equals(DateTime.Today),
                Events = date.DayOfWeek.Equals(System.DayOfWeek.Monday) ? events.Where(x => x.Start.Date <= date.Date && x.End.Date >= date.Date).ToList() : events.Where(x => x.Start.Date == date.Date).ToList(),
                NumberOfEvents = events.Where(x => x.Start.Date <= date.Date && x.End.Date >= date.Date).Count(),
                Date = (Date) date
            };
        }


    }
}