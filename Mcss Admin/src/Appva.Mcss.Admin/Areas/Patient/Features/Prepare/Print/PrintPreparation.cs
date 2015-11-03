// <copyright file="PrintPreparation.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using System;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class PrintPreparation : Identity<FileContentResult>
    {
        /// <summary>
        /// The <c>Schedule</c> ID.
        /// </summary>
        public Guid ScheduleId
        {
            get;
            set;
        }

        /// <summary>
        /// The start date.
        /// </summary>
        public DateTime StartDate
        {
            get;
            set;
        }

        /// <summary>
        /// The end date.
        /// </summary>
        public DateTime EndDate
        {
            get;
            set;
        }
    }
}