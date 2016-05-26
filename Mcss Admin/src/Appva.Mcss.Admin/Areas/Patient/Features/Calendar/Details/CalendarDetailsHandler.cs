// <copyright file="CalenderDetailsHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Core.Extensions;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Transformers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Mcss.Admin.Domain.Entities;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class CalenderDetailsHandler : RequestHandler<CalendarDetails, CalendarTask>
    {
        #region Fields

        /// <summary>
        /// The <see cref="ITaskService"/>
        /// </summary>
        private readonly ITaskService tasks;

        /// <summary>
        /// The <see cref="IEventService"/>
        /// </summary>
        private readonly IEventService events;

        /// <summary>
        /// The <see cref="IScheduleService"/>
        /// </summary>
        private readonly IScheduleService schedules;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CalenderDetailsHandler"/> class.
        /// </summary>
        public CalenderDetailsHandler(ITaskService tasks, IEventService events, IScheduleService schedules)
        {
            this.tasks = tasks;
            this.events = events;
            this.schedules = schedules;
        }

        #endregion

        #region RequestHandler Overrides

        public override CalendarTask Handle(CalendarDetails message)
        {
            if (message.TaskId.IsNotEmpty())
            {
                return EventTransformer.TasksToEvent(this.tasks.Get(message.TaskId));
            }

            return this.events.GetActivityInSequence(message.SequenceId, message.EndTime);
        }

        #endregion
    }
}