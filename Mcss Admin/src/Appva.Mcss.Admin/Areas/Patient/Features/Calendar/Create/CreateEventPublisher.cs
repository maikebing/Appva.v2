// <copyright file="CreateEventPublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Models;
    using Appva.Mcss.Web.ViewModels;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class CreateEventPublisher : RequestHandler<EventViewModel, ListCalendar>
    {
        #region Private Variables.

        /// <summary>
        /// The <see cref="IPatientService"/>.
        /// </summary>
        private readonly IPatientService patientService;

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
        /// Initializes a new instance of the <see cref="CreateEventPublisher"/> class.
        /// </summary>
        public CreateEventPublisher(IPatientService patientService, IEventService eventService, ISettingsService settingsService)
        {
            this.patientService = patientService;
            this.eventService = eventService;
            this.settingsService = settingsService;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override ListCalendar Handle(EventViewModel message)
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
            return new ListCalendar
            {
                Id = patient.Id,
                Date = message.StartDate
            };
        }

        #endregion
    }
}