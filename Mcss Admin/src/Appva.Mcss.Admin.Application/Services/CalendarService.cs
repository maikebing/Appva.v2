using Appva.Mcss.Admin.Application.Auditing;
using Appva.Mcss.Admin.Application.Services.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appva.Mcss.Admin.Domain.Entities;
using Appva.Mcss.Admin.Application.Models;
using Appva.Mcss.Admin.Application.Transformers;
using Appva.Core.Extensions;
using Appva.Persistence;

namespace Appva.Mcss.Admin.Application.Services
{
    public interface ICalendarService : IService
    {
        #region Fields

        #endregion
        IList<ScheduleSettings> GetCategories();
        Patient GetPatient(Guid id);
        IList<CalendarTask> FindWithinMonth(Patient pat, DateTime date);
        void AuditRead(Patient patient, string format, Guid patientId);
        Schedule GetSchedule(Guid scheduleId);
        IList<CalendarWeek> Calendar(DateTime date, IList<CalendarTask> events);
        IList<CalendarTask> FindSequencesWithinMonth(Schedule schedule, DateTime date);
    }

    public class CalendarService : ICalendarService
    {
        #region Variables

        /// <summary>
        /// The <see cref="IEventService"/>.
        /// </summary>
        private readonly IEventService eventService;

        /// <summary>
        /// The <see cref="IPatientService"/>.
        /// </summary>
        private readonly IPatientService patientService;

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The <see cref="IAuditService"/>.
        /// </summary>
        private readonly IAuditService auditing;


        private readonly ISequenceService sequenceService;
        private readonly IScheduleService scheduleService;
        private readonly IPersistenceContext persistenceContext;

        #endregion

        public CalendarService(IEventService eventService, IPatientService patientService, ISettingsService settingsService, IAuditService auditing, IScheduleService scheduleService, IPersistenceContext persistenceContext)
        {
            this.eventService = eventService;
            this.patientService = patientService;
            this.settingsService = settingsService;
            this.auditing = auditing;
            this.scheduleService = scheduleService;
            this.persistenceContext = persistenceContext;
        }

        public void AuditRead(Patient patient, string format, Guid patientId)
        {
            this.auditing.Read(patient, format, patientId);
        }

        public IList<CalendarTask> FindWithinMonth(Patient patient, DateTime date)
        {
            return this.eventService.FindWithinMonth(patient, date);
        }

        public IList<ScheduleSettings> GetCategories()
        {
            return this.eventService.GetCategories();
        }

        public Patient GetPatient(Guid id)
        {
            return this.patientService.Get(id);
        }

        public Schedule GetSchedule(Guid scheduleId)
        {
            return this.scheduleService.Get(scheduleId);
        }

        public IList<CalendarTask> FindSequencesWithinMonth(Schedule schedule, DateTime date)
        {
            var firstInMonth = date.FirstOfMonth();
            var lastInMonth = firstInMonth.AddDays(DateTime.DaysInMonth(firstInMonth.Year, firstInMonth.Month));
            var sequences = this.persistenceContext.QueryOver<Sequence>()
                .Where(x => x.IsActive)
                  .And(x => x.Schedule.Id == schedule.Id)
                  .And(x => x.Patient.Id == schedule.Patient.Id)
                  .And(x => x.StartDate <= lastInMonth) // if endate == null?
                  .And(x => x.EndDate == null || x.EndDate >= firstInMonth)
                .List();

            var sequenceList = new List<Sequence>();

            var retval = new List<CalendarTask>();

            // THE LOOP FROM HELL!!!
            foreach (var sequence in sequences)
            {
                if (sequence.OnNeedBasis == true)
                {
                    // lista dessa utanför kalendern kanske? nu visar den bara upp sig en dag.
                    retval.Add(this.SequenceToEvent(sequence, sequence.StartDate, sequence.EndDate));
                }
                else
                {
                    var before = new TimeSpan(0, sequence.RangeInMinutesBefore, 0);
                    var after = new TimeSpan(0, sequence.RangeInMinutesAfter, 0);
                    var sequenceTimes = sequence.Times.Split(',');

                    // titta på dates, plocka ut datumen och skapa en calendertask varje datum som är inom tidsspannet.
                    // alternativt räkna ut nästa iteration..
                    // loopa sålänge datumen ligger inom tidsspannet
                    if (sequence.Dates != null && DateTime.TryParse(sequence.Dates.Split(',')[0], out DateTime firstDate) == true)
                    {
                        var sequenceDates = sequence.Dates.Split(',');

                        for (int d = 0; d < sequenceDates.Length; d++)
                        {
                            var theDate = DateTime.Parse(sequenceDates[d]);

                            // kolla om datumet ligger inom månaden.. innan task skapas
                            if (theDate >= firstInMonth && theDate <= lastInMonth)
                            {
                                for (int t = 0; t < sequenceTimes.Length; t++)
                                {
                                    retval.Add(this.SequenceToEvent(sequence, theDate.AddHours(double.Parse(sequenceTimes[t])).Subtract(before), theDate.AddHours(double.Parse(sequenceTimes[t])).Add(after)));
                                }
                            }
                        }
                    }
                    else
                    {
                        var intervalDate = sequence.StartDate;

                        // kan vara bra med någon bättre matematik för att sortera och räkna datum
                        while (intervalDate < lastInMonth)
                        {
                            if (intervalDate.Subtract(before) >= firstInMonth && intervalDate.Add(after) <= lastInMonth) // hmm vad händer om datum/tid är 2017-12-31 23:50 och after är 15 minuter?
                            {
                                for (int t = 0; t < sequenceTimes.Length; t++)
                                {
                                    retval.Add(this.SequenceToEvent(sequence, intervalDate.AddHours(double.Parse(sequenceTimes[t])).Subtract(before), intervalDate.AddHours(double.Parse(sequenceTimes[t])).Add(after)));
                                }
                            }
                            intervalDate = intervalDate.AddDays(sequence.Interval);
                        }
                    }
                }
            }
            return retval;
        }

        public IList<CalendarWeek> Calendar(DateTime date, IList<CalendarTask> events)
        {
            var retval = new List<CalendarWeek>();
            var current = date;
            while (current <= date.LastOfMonth())
            {
                retval.Add(CreateWeek(current, events));
                current = current.FirstDateOfWeek().AddDays(7);
            }

            return retval;
        }

        private CalendarTask SequenceToEvent(Sequence sequence, DateTime startDate, DateTime? endDate)
        {
            return new CalendarTask
            {
                Id = Guid.NewGuid(),
                StartTime = startDate,
                EndTime = endDate.HasValue ? endDate.Value : startDate,
                Description = sequence.Description,
                CategoryName = sequence.Name,
                Color = sequence.Schedule.ScheduleSettings.Color,
                SequenceId = sequence.Id,
                CategoryId = sequence.Schedule.ScheduleSettings.Id,
                IsFullDayEvent = sequence.AllDay,
                NeedsQuittance = sequence.Overview,
                NeedsSignature = sequence.CanRaiseAlert,
                Interval = sequence.Interval,
                IntervalFactor = sequence.IntervalFactor != 0 ? sequence.IntervalFactor : 1,
                RepeatAtGivenDate = sequence.IntervalIsDate,
                PatientId = sequence.Patient.Id,
                PatientName = sequence.Patient.FullName
            };
        }

        private CalendarWeek CreateWeek(DateTime date, IList<CalendarTask> events)
        {
            var week = new CalendarWeek()
            {
                WeekNumber = date.GetWeekNumber(),
                Days = new List<CalendarDay>(),
                AllEvents = new List<CalendarTask>()
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

        private CalendarDay CreateDay(DateTime date, int currentMonthDisplayed, IList<CalendarTask> events)
        {
            if (events.IsNull())
            {
                events = new List<CalendarTask>();
            }
            return new CalendarDay()
            {
                IsWithinMonth = date.Month == currentMonthDisplayed,
                IsToday = date.Equals(DateTime.Today),
                Events = date.DayOfWeek.Equals(DayOfWeek.Monday) ? events.Where(x => x.StartTime.Date <= date.Date && x.EndTime.Date >= date.Date).ToList() : events.Where(x => x.StartTime.Date == date.Date).ToList(),
                NumberOfEvents = events.Where(x => x.StartTime.Date <= date.Date && x.EndTime.Date >= date.Date).Count(),
                Date = date
            };
        }
    }
}
