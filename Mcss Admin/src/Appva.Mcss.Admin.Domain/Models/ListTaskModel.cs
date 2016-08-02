// <copyright file="ListTaskModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.alvegard@appva.se">Richard Alvegard</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class ListTaskModel
    {
        /// <summary>
        /// Whether or not to override any paging parameters.
        /// </summary>
        public bool SkipPaging
        {
            get;
            set;
        }

        /// <summary>
        /// Whether or not to include only active tasks or inactive tasks.
        /// If not set it will select both active and inactive.
        /// </summary>
        public bool? IsActive
        {
            get;
            set;
        }

        /// <summary>
        /// The startdate
        /// </summary>
        public DateTime StartDate 
        {
            get;
            set;
        }

        /// <summary>
        /// The end date
        /// </summary>
        public DateTime EndDate
        { 
            get; 
            set;
        }

        /// <summary>
        /// The Account id, optional
        /// </summary>
        public Guid? AccountId
        {
            get;
            set;
        }

        /// <summary>
        /// The Patient id, optional
        /// </summary>
        public Guid? PatientId
        {
            get; 
            set;
        }

        /// <summary>
        /// The organisation Taxon, optional
        /// </summary>
        public Guid? TaxonId
        {
            get;
            set;
        }

        /// <summary>
        /// The schedulesetting, optional
        /// </summary>
        public Guid? ScheduleSettingId
        {
            get;
            set;
        }

        /// <summary>
        /// The Sequence, optional
        /// </summary>
        public Guid? SequenceId
        {
            get;
            set;
        }

        /// <summary>
        /// Include calendar-events
        /// </summary>
        public bool IncludeCalendarTasks
        { 
            get;
            set;
        }
    }
}