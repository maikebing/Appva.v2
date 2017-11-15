// <copyright file="ListCalendarHandler.cs" company="Appva AB">
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
    using Appva.Core.Extensions;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Auditing;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Infrastructure;
    using Appva.Mcss.Web.ViewModels;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class ListCalendarHandler : RequestHandler<ListCalendar, EventListViewModel>
    {
        #region Private Variables.

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
        /// The <see cref="IPatientTransformer"/>.
        /// </summary>
        private readonly IPatientTransformer transformer;

        /// <summary>
        /// The <see cref="IAuditService"/>.
        /// </summary>
        private readonly IAuditService auditing;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ListCalendarHandler"/> class.
        /// </summary>
        public ListCalendarHandler(
            IEventService eventService,
            IPatientService patientService,
            ISettingsService settingsService,
            IPatientTransformer transformer,
            IAuditService auditing)
        {
            this.eventService = eventService;
            this.patientService = patientService;
            this.settingsService = settingsService;
            this.transformer = transformer;
            this.auditing = auditing;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override EventListViewModel Handle(ListCalendar message)
        {
            var date = message.Date.HasValue ? message.Date.Value.FirstOfMonth() : DateTime.Today.FirstOfMonth();

            if (message.Prev.IsNotNull())
            {
                date = message.Date.GetValueOrDefault().PreviousMonth();
            }
            if (message.Next.IsNotNull())
            {
                date = message.Date.GetValueOrDefault().NextMonth();
            }

            var categories = this.eventService.GetCategories();
            var filter = message.Filter ?? categories.Select(x => x.Id).ToArray();
            var patient = this.patientService.Get(message.Id);
            var events = this.eventService.FindWithinMonth(patient, date);
            this.auditing.Read(patient, "läste kalenderaktiviteter för användare {0}(REF:{1})", patient.FullName, patient.Id);

            var model = new EventListViewModel
            {
                Current = date,
                Next = date.NextMonth(),
                Previous = date.PreviousMonth(),
                Calendar = this.eventService.Calendar(date, events),
                Patient = this.transformer.ToPatient(patient),
                EventViewModel = new EventViewModel(),
                Categories = categories.ToDictionary(x => categories.IndexOf(x)),
                FilterList = filter.ToList(),
                CalendarSettings = this.settingsService.GetCalendarSettings(),
                CategorySettings = categories
            };

            return model;
        }

        #endregion
    }
}