﻿using Appva.Core.Extensions;
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
        private readonly IEventService eventService;
        private readonly ISettingsService settingsService;

        public EditTestCalendarHandler(IEventService eventService, ISettingsService settingsService)
        {
            this.eventService = eventService;
            this.settingsService = settingsService;
        }

        public override TestEventViewModel Handle(EditTestCalendar message)
        {
            var evt = this.eventService.Get(message.SequenceId);
            var categories = this.eventService.GetCategories();
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
                StartDate = evt.StartDate,
                EndDate = (DateTime)evt.EndDate,
                AllDay = evt.AllDay,
                StartTime = string.Format("{0:HH:mm}", evt.StartDate),
                EndTime = string.Format("{0:HH:mm}", evt.EndDate),
                Absent = evt.Absent,
                PauseAnyAlerts = evt.PauseAnyAlerts,
                Interval = evt.Interval,
                IntervalFactor = evt.IntervalFactor,
                SpecificDate = evt.IntervalIsDate,
                Signable = evt.CanRaiseAlert,
                VisibleOnOverview = evt.Overview,
                Category = evt.Schedule.ScheduleSettings.Id.ToString(),
                Categories = categorySelectlist,
                CalendarSettings = this.settingsService.GetCalendarSettings()
            };
        }
    }
}