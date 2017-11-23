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
    using Appva.Mcss.Admin.Areas.Models;
    using Appva.Mcss.Admin.Domain.Entities;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class CreateEventPublisher : RequestHandler<CreateEventModel, ListCalendar>
    {
        #region Private Variables.

        /// <summary>
        /// The <see cref="IPatientService"/>.
        /// </summary>
        private readonly IPatientService patientService;

        /// <summary>
        /// The <see cref="ISequenceService"/>.
        /// </summary>
        private readonly ISequenceService sequenceService;

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The <see cref="IScheduleService"/>.
        /// </summary>
        private readonly IScheduleService scheduleService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateEventPublisher"/> class.
        /// </summary>
        public CreateEventPublisher(IPatientService patientService, ISequenceService sequenceService, ISettingsService settingsService, IScheduleService scheduleService)
        {
            this.patientService = patientService;
            this.sequenceService = sequenceService;
            this.settingsService = settingsService;
            this.scheduleService = scheduleService;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override ListCalendar Handle(CreateEventModel message)
        {
            var patient = this.patientService.Get(message.Id);
            if (message.Category.Equals("new"))
            {
                message.Category = this.sequenceService.CreateCategory(message.NewCategory).ToString();
            }

            this.sequenceService.CreateEventBasedSequence(
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