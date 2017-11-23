using Appva.Core.Extensions;
using Appva.Cqrs;
using Appva.Mcss.Admin.Application.Services;
using Appva.Mcss.Admin.Application.Services.Settings;
using Appva.Mcss.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Appva.Mcss.Admin.Models.Handlers
{
    public class EditTestCalendarHandler : RequestHandler<EditTestCalendar, TestEventViewModel>
    {
        private readonly ISequenceService sequenceService;
        private readonly ISettingsService settingsService;

        public EditTestCalendarHandler(ISequenceService sequenceService, ISettingsService settingsService)
        {
            this.sequenceService = sequenceService;
            this.settingsService = settingsService;
        }

        public override TestEventViewModel Handle(EditTestCalendar message)
        {
            var evt = this.sequenceService.Find(message.SequenceId);
            var categories = this.sequenceService.GetCategories();
            var categorySelectlist = categories.IsNotNull() ? categories.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList() : new List<SelectListItem>();

            return new TestEventViewModel
            {
                Id = evt.Patient.Id,
                PatientId = evt.Patient.Id,
                SequenceId = evt.Id,
                ChoosedDate = message.Date,
                Description = evt.Description,
                StartDate = evt.Repeat.StartAt,
                EndDate = (DateTime)evt.Repeat.EndAt,
                AllDay = evt.Repeat.IsAllDay,
                StartTime = string.Format("{0:HH:mm}", evt.Repeat.StartAt),
                EndTime = string.Format("{0:HH:mm}", evt.Repeat.EndAt),
                Absent = evt.Absent,
                PauseAnyAlerts = evt.PauseAnyAlerts,
                Interval = evt.Repeat.Interval,
                IntervalFactor = evt.Repeat.IntervalFactor,
                SpecificDate = evt.Repeat.IsIntervalDate,
                Signable = evt.CanRaiseAlert,
                VisibleOnOverview = evt.Overview,
                Category = evt.Schedule.ScheduleSettings.Id.ToString(),
                Categories = categorySelectlist,
                CalendarSettings = this.settingsService.GetCalendarSettings()
            };
        }
    }
}