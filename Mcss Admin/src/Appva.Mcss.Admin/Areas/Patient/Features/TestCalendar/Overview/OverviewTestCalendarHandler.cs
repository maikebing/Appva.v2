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
        private IEventService events;
        private ITaxonFilterSessionHandler filtering;

        public OverviewTestCalendarHandler(IEventService events, ITaxonFilterSessionHandler filtering)
        {
            this.events = events;
            this.filtering = filtering;
        }

        public override OverviewTestCalendarModel Handle(OverviewTestCalendar message)
        {
            var filterTaxon = this.filtering.GetCurrentFilter();
            var events = this.events.FindEventsWithinPeriod(
                DateTime.Now.Date,
                DateTime.Now.Date.AddDays(7),
                orgFilter: filterTaxon);

            var delayedEvents = this.events.FindDelayedQuittanceEvents(filterTaxon);

            var allEvents = delayedEvents.Concat(events);

            return new OverviewTestCalendarModel
            {
                OngoingEvents = allEvents.Where(x => x.NeedsQuittance && !x.IsQuittanced && x.StartTime.Date <= DateTime.Now.Date).OrderBy(x => x.EndTime).ToList(),
                CommingEvents = allEvents.Where(x => x.NeedsQuittance && !x.IsQuittanced && x.StartTime.Date > DateTime.Now.Date).OrderBy(x => x.StartTime).ToList()
            };
        }
    }
}