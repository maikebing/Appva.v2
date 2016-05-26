// <copyright file="CalendarOverviewHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Models.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class CalendarOverviewHandler : RequestHandler<CalendarOverview, CalendarOverviewModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IEventService" />
        /// </summary>
        private IEventService events;

        /// <summary>
        /// The <see cref="ITaxonFilterSessionHandler"/>
        /// </summary>
        private ITaxonFilterSessionHandler filtering;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CalendarOverviewHandler"/> class.
        /// </summary>
        public CalendarOverviewHandler(IEventService events, ITaxonFilterSessionHandler filtering)
        {
            this.events = events;
            this.filtering = filtering;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override CalendarOverviewModel Handle(CalendarOverview message)
        {
            var filterTaxon = this.filtering.GetCurrentFilter();
            var events = this.events.FindEventsWithinPeriod(
                DateTime.Now.Date,
                DateTime.Now.Date.AddDays(7),
                orgFilter: filterTaxon);

            var delayedEvents = this.events.FindDelayedQuittanceEvents(filterTaxon);

            var allEvents = delayedEvents.Concat(events);

            return new CalendarOverviewModel
            {
                OngoingEvents = allEvents.Where(x => x.NeedsQuittance && !x.IsQuittanced && x.StartTime.Date <= DateTime.Now.Date).OrderBy(x => x.EndTime).ToList(),
                CommingEvents = allEvents.Where(x => x.NeedsQuittance && !x.IsQuittanced && x.StartTime.Date > DateTime.Now.Date).OrderBy(x => x.StartTime).ToList()
            };
        }

        #endregion
    }
}