// <copyright file="EditEventSequenceHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Models;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Core.Extensions;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class EditEventSequenceHandler : RequestHandler<EditEventSequence, EventViewModel>
    {
        #region Private Variables.

        /// <summary>
        /// The <see cref="IEventService"/>.
        /// </summary>
        private readonly IEventService eventService;

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService settingsService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="EditEventSequenceHandler"/> class.
        /// </summary>
        public EditEventSequenceHandler(IEventService eventService, ISettingsService settingsService)
        {
            this.eventService = eventService;
            this.settingsService = settingsService;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override EventViewModel Handle(EditEventSequence message)
        {
            var evt = this.eventService.Get(message.SequenceId);
            var categories = this.eventService.GetCategories();
            var categorySelectlist = categories.IsNotNull() ? categories.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList() : new List<SelectListItem>();
            return new EventViewModel
            {
                Id                = evt.Patient.Id,
                PatientId         = evt.Patient.Id,
                SequenceId        = evt.Id,
                ChoosedDate       = message.Date,
                Description       = evt.Description,
                StartDate         = evt.Repeat.StartAt,
                EndDate           = (DateTime) evt.Repeat.EndAt,
                AllDay            = evt.Repeat.IsAllDay,
                StartTime         = string.Format("{0:HH:mm}", evt.Repeat.StartAt),
                EndTime           = string.Format("{0:HH:mm}", evt.Repeat.EndAt),
                Absent            = evt.Absent,
                PauseAnyAlerts    = evt.PauseAnyAlerts,
                Interval          = evt.Repeat.Interval,
                IntervalFactor    = evt.Repeat.IntervalFactor,
                SpecificDate      = evt.Repeat.IsIntervalDate,
                Signable          = evt.CanRaiseAlert,
                VisibleOnOverview = evt.Overview,
                Category          = evt.Schedule.ScheduleSettings.Id.ToString(),
                Categories        = categorySelectlist,
                CalendarSettings  = this.settingsService.GetCalendarSettings()
            };
        }

        #endregion
    }
}