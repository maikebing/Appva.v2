
using Appva.Core.Extensions;
using Appva.Cqrs;
using Appva.Mcss.Admin.Application.Auditing;
using Appva.Mcss.Admin.Application.Models;
using Appva.Mcss.Admin.Application.Services;
using Appva.Mcss.Admin.Application.Services.Settings;
using Appva.Mcss.Admin.Domain.Entities;
using Appva.Mcss.Admin.Infrastructure;
using Appva.Mcss.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appva.Mcss.Admin.Models.Handlers
{
    public class ListTestCalendarHandler : RequestHandler<ListTestCalendar, TestEventListViewModel>
    {
        #region Private Variables.

        /// <summary>
        /// The <see cref="ISequenceService"/>.
        /// </summary>
        private readonly ISequenceService sequenceService;

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

        // One service usage?
        private readonly ICalendarService service;
        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ListCalendarHandler"/> class.
        /// </summary>
        public ListTestCalendarHandler(
            ISequenceService sequenceService,
            IPatientService patientService,
            ISettingsService settingsService,
            IPatientTransformer transformer,
            IAuditService auditing,
            ICalendarService service)
        {
            this.sequenceService = sequenceService;
            this.patientService = patientService;
            this.settingsService = settingsService;
            this.transformer = transformer;
            this.auditing = auditing;
            this.service = service;
        }
                
        #endregion

        public override TestEventListViewModel Handle(ListTestCalendar message)
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

            // talk to 1 service instead?
            var categories = this.sequenceService.GetCategories();
            var filter = message.Filter ?? categories.Select(x => x.Id).ToArray();
            var patient = this.patientService.Get(message.Id);
            var events = this.sequenceService.FindWithinMonth(patient, date);
            this.auditing.Read(patient, "läste kalenderaktiviteter för användare {0}(REF:{1})", patient.FullName, patient.Id);

            return new TestEventListViewModel
            {
                Current = date,
                Next = date.NextMonth(),
                Previous = date.PreviousMonth(),
                Calendar = this.sequenceService.Calendar(date, events),
                Patient = this.transformer.ToPatient(patient),                
                Categories = categories.ToDictionary(x => categories.IndexOf(x)),
                FilterList = filter.ToList(),
                CalendarSettings = this.settingsService.GetCalendarSettings(),
                CategorySettings = categories
            };
        }
    }
}