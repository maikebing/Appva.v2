// <copyright file="InstallColorsPublisher.cs" company="Appva AB">
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
    internal sealed class InstallColorsPublisher : RequestHandler<InstallColors, CalendarIndex>
    {
        #region Variables

        /// <summary>
        /// The <see cref="IEventService"/>.
        /// </summary>
        private readonly IEventService events;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="InstallColorsPublisher"/> class.
        /// </summary>
        public InstallColorsPublisher(IEventService events)
        {
            this.events = events;
        }

        #endregion

        public override CalendarIndex Handle(InstallColors message)
        {
            var categories = this.events.GetCategories();

            var colors = new List<string>() {
                "#5d5d5d",
                "#1ed288",
                "#e28a00",
                "#b668ca",
                "#47597f",
                "#349d00",
                "#e9d600",
                "#0091ce"};

            foreach (var cat in categories)
            {
                var index = categories.IndexOf(cat) % (colors.Count);
                cat.Color = colors[index];
            }

            return new CalendarIndex();
        }
    }
}