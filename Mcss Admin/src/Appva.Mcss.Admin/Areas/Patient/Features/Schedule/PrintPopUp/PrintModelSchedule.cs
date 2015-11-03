// <copyright file="PrintModelSchedule.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using System;
    using Appva.Mcss.Web.ViewModels;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class PrintModelSchedule : Identity<SchedulePrintPopOverViewModel>
    {
        /// <summary>
        /// The schedule settings ID.
        /// </summary>
        public Guid ScheduleSettingsId
        {
            get;
            set;
        }

        /// <summary>
        /// Optional start date.
        /// </summary>
        public DateTime? StartDate
        {
            get;
            set;
        }

        /// <summary>
        /// Optional end date.
        /// </summary>
        public DateTime? EndDate
        {
            get;
            set;
        }
    }
}