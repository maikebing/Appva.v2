// <copyright file="SignSchedule.cs" company="Appva AB">
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
    public sealed class SignSchedule : Identity<TaskListViewModel>
    {
        /// <summary>
        /// Optional schedule settings ID.
        /// </summary>
        public Guid? ScheduleSettingsId
        {
            get;
            set;
        }

        /// <summary>
        /// Optional year.
        /// </summary>
        public int? Year
        {
            get;
            set;
        }

        /// <summary>
        /// Optional month.
        /// </summary>
        public int? Month
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

        /// <summary>
        /// Optional anomalies query filter - defaults to false.
        /// </summary>
        public bool? FilterByAnomalies
        {
            get;
            set;
        }

        /// <summary>
        /// Optional on need based query filter - defaults to false.
        /// </summary>
        public bool? FilterByNeedsBasis
        {
            get;
            set;
        }

        /// <summary>
        /// Optional page number - defaults to 1.
        /// </summary>
        public int? Page
        {
            get;
            set;
        }

        /// <summary>
        /// Optional sort ordering - defaults to <see cref="OrderTasksBy.Day"/>.
        /// </summary>
        public OrderTasksBy Order
        {
            get;
            set;
        }
    }
}