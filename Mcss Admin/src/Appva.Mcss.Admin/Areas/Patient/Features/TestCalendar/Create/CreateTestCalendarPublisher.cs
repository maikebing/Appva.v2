using Appva.Cqrs;
using Appva.Mcss.Admin.Application.Services;
using Appva.Mcss.Admin.Application.Services.Settings;
using Appva.Mcss.Admin.Areas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appva.Mcss.Admin.Models.Handlers
{
    public class CreateTestCalendarPublisher : RequestHandler<CreateTestCalendar, ListTestCalendar>
    {
        private readonly IPatientService patientService;
        private readonly IEventService eventService;
        private readonly ISettingsService settingsService;

        public CreateTestCalendarPublisher(IPatientService patientService, IEventService eventService, ISettingsService settingsService)
        {
            this.patientService = patientService;
            this.eventService = eventService;
            this.settingsService = settingsService;
        }

        public override ListTestCalendar Handle(CreateTestCalendar message)
        {
            var patient = this.patientService.Get(message.Id);
            if (message.Category.Equals("new"))
            {
                message.Category = this.eventService.CreateCategory(message.NewCategory).ToString();
            }
            this.eventService.Create(
                new Guid(message.Category),
                patient,
                message.Description,
                message.StartDate,
                message.EndDate,
                message.StartTime,
                message.EndTime,
                message.Interval,
                message.IntervalFactor,
                message.SpecificDate,
                message.Signable,
                message.VisibleOnOverview,
                message.AllDay,
                message.PauseAnyAlerts,
                message.Absent
            );
            return new ListTestCalendar
            {
                Id = patient.Id,
                Date = message.StartDate
            };
        }
    }
}