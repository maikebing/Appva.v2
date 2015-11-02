// <copyright file="CalendarDetails.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class CalendarDetails : IRequest<CalendarTask>
    {

        #region Properties

        /// <summary>
        /// The task id
        /// </summary>
        public Guid TaskId
        {
            get;
            set;
        }

        /// <summary>
        /// The Sequence id
        /// </summary>
        public Guid SequenceId
        {
            get;
            set;
        }

        /// <summary>
        /// The Starttime of the event
        /// </summary>
        public DateTime StartTime
        {
            get;
            set;
        }

        /// <summary>
        /// The endtime of the event
        /// </summary>
        public DateTime EndTime
        {
            get;
            set;
        }

        #endregion
    }
}