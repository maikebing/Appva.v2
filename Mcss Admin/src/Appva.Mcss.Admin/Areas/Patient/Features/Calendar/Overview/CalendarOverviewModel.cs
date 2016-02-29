// <copyright file="CalendarOverviewModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Models
{
    #region Imports.

    using Appva.Mcss.Admin.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class CalendarOverviewModel
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CalendarOverviewModel"/> class.
        /// </summary>
        public CalendarOverviewModel()
        {
        }

        #endregion

        /// <summary>
        /// The ongoing events
        /// </summary>
        public IList<CalendarTask> OngoingEvents
        {
            get;
            set;
        }

        /// <summary>
        /// The comming events
        /// </summary>
        public IList<CalendarTask> CommingEvents
        {
            get;
            set;
        }
    }
}