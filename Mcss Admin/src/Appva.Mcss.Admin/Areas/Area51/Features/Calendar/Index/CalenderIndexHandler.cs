// <copyright file="CalenderIndexHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Area51.Models.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class CalenderIndexHandler : RequestHandler<CalendarIndex, CalendarIndexModel>
    {
        #region Variables

        /// <summary>
        /// The <see cref="IEventService"/>.
        /// </summary>
        private readonly IEventService events;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CalenderIndexHandler"/> class.
        /// </summary>
        public CalenderIndexHandler(IEventService events)
        {
            this.events = events;
        }

        #endregion

        public override CalendarIndexModel Handle(CalendarIndex message)
        {
            return new CalendarIndexModel
            {
                Categories = this.events.GetCategories()
            };
        }
    }
}