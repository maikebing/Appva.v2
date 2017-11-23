using Appva.Cqrs;
using Appva.Mcss.Admin.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appva.Mcss.Admin.Models.Handlers
{
    public class OverviewTestCalendarHandler : RequestHandler<OverviewTestCalendar, OverviewTestCalendarModel>
    {
        private readonly ISequenceService sequenceService;
        private ITaxonFilterSessionHandler filtering;

        public OverviewTestCalendarHandler(ISequenceService sequenceService, ITaxonFilterSessionHandler filtering)
        {
            this.sequenceService = sequenceService;
            this.filtering = filtering;
        }

        public override OverviewTestCalendarModel Handle(OverviewTestCalendar message)
        {
            var filterTaxon = this.filtering.GetCurrentFilter();
            var events = this.sequenceService.FindEventsWithinPeriod(
                DateTime.Now.Date,
                DateTime.Now.Date.AddDays(7),
                orgFilter: filterTaxon);

            var delayedEvents = this.sequenceService.FindDelayedQuittanceEvents(filterTaxon);

            var allEvents = delayedEvents.Concat(events);

            return new OverviewTestCalendarModel
            {
                OngoingEvents = allEvents.Where(x => x.NeedsQuittance && !x.IsQuittanced && x.StartTime.Date <= DateTime.Now.Date).OrderBy(x => x.EndTime).ToList(),
                CommingEvents = allEvents.Where(x => x.NeedsQuittance && !x.IsQuittanced && x.StartTime.Date > DateTime.Now.Date).OrderBy(x => x.StartTime).ToList()
            };
        }
    }
}