// <copyright file="OverviewHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Models.Handlers
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
    internal sealed class DashboardCalendarHandler : RequestHandler<DashboardCalendar, DashboardCalendarModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="ITaskService"/>
        /// </summary>
        private readonly ITaskService tasks;

        /// <summary>
        /// The <see cref="IEventService"/>
        /// </summary>
        private readonly IEventService events;

        #endregion
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="DashboardCalendarHandler"/> class.
        /// </summary>
        public DashboardCalendarHandler(ITaskService tasks, IEventService events)
        {
            this.tasks = tasks;
            this.events = events;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override DashboardCalendarModel Handle(DashboardCalendar message)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}