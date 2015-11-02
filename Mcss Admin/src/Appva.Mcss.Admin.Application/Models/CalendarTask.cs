// <copyright file="CalendarTask.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class CalendarTask
    {
        #region Properties

        /// <summary>
        /// The id
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        public Guid SequenceId
        {
            get;
            set;
        }

        /// <summary>
        /// The event starttime
        /// </summary>
        public DateTime StartTime
        {
            get;
            set;
        }

        /// <summary>
        /// The event endtime
        /// </summary>
        public DateTime EndTime
        {
            get;
            set;
        }

        /// <summary>
        /// The category name
        /// </summary>
        public string CategoryName
        {
            get;
            set;
        }

        /// <summary>
        /// The category name
        /// </summary>
        public Guid CategoryId
        {
            get;
            set;
        }

        /// <summary>
        /// The event description
        /// </summary>
        public string Description
        {
            get;
            set;
        }
             
        #endregion

        public string Color { get; set; }

        public bool IsFullDayEvent { get; set; }

        public Guid TaskId { get; set; }

        public bool NeedsQuittance { get; set; }

        public bool IsQuittanced { get; set; }

        public Domain.Entities.Account QuittancedBy { get; set; }
    }
}