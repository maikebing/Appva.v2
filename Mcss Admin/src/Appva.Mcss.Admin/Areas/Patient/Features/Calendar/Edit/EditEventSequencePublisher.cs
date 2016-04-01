// <copyright file="EditEventSequencePublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Patient.Features.Calendar.Edit
{
    #region Imports.

    using Appva.Mcss.Admin.Models;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Cqrs;
    using System;
    using System.Collections.Generic;
    using System.Linq;
using Appva.Mcss.Admin.Application.Services;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class EditEventSequencePublisher : RequestHandler<EventViewModel, ListCalendar>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IEventService" />
        /// </summary>
        private readonly IEventService eventService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="EditEventSequencePublisher"/> class.
        /// </summary>
        public EditEventSequencePublisher(IEventService eventService)
        {
            this.eventService = eventService;
        }

        #endregion

        #region RequestHandler overrides.
        
        /// <inheritdoc />
        public override ListCalendar Handle(EventViewModel message)
        {
            if (message.Category.Equals("new"))
            {
                message.Category = this.eventService.CreateCategory(message.NewCategory).ToString();
            }
            this.eventService.Update(
                message.SequenceId,
                new Guid(message.Category),
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
                Date = message.ChoosedDate,
                Id = message.PatientId
            };
        }

        #endregion
    }
}