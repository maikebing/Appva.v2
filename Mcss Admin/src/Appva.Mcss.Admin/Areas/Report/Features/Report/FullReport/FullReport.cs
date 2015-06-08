// <copyright file="FullReport.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Models
{
    #region Imports.

    using Appva.Cqrs;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class FullReport : IRequest<FullReportModel>
    {
        /// <summary>
        /// The start date, optional
        /// </summary>
        public DateTime? Start 
        { 
            get; 
            set;
        }

        /// <summary>
        /// The end date, optional
        /// </summary>
        public DateTime? End 
        {
            get;
            set;
        }

        /// <summary>
        /// The schedulesetting, otional
        /// </summary>
        public Guid? ScheduleSetting
        {
            get;
            set;
        }

        /// <summary>
        /// The current page
        /// </summary>
        public int Page 
        { 
            get;
            set;
        }
    }
}