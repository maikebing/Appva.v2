using Appva.Mcss.Admin.Application.Auditing;
using Appva.Mcss.Admin.Application.Services.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appva.Mcss.Admin.Domain.Entities;
using Appva.Mcss.Admin.Application.Models;

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

        #endregion

        public CalendarService(IEventService eventService, IPatientService patientService, ISettingsService settingsService, IAuditService auditing, IScheduleService scheduleService)
        {
            this.eventService = eventService;
            this.patientService = patientService;
            this.settingsService = settingsService;
            this.auditing = auditing;
            this.scheduleService = scheduleService;
        }

        public void AuditRead(Patient patient, string format, Guid patientId)
        {
            this.auditing.Read(patient, format, patientId);
        }

        public IList<CalendarWeek> Calendar(DateTime date, IList<CalendarTask> events)
        {
            return this.eventService.Calendar(date, events);
        }

        public IList<CalendarTask> FindSequencesWithinMonth(Schedule schedule, DateTime date)
        {

            return this.eventService.FindSequencesWithinMonth(schedule, date);
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
    }
}
