// <copyright file="PrintSchemaSchedule.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
using Appva.Mcss.Admin.Models;
    using Appva.Mcss.Web.ViewModels;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class PrintSchemaSchedule : Identity<PrintViewModel>
    {
        /// <summary>
        /// The schedule settings ID.
        /// </summary>
        public Guid? ScheduleSettingsId
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

        /// <summary>
        /// Whether or not on needs base.
        /// </summary>
        public bool OnNeedBasis
        {
            get;
            set;
        }

        /// <summary>
        /// TODO: This is unknown.
        /// </summary>
        public bool StandardSequences
        {
            get;
            set;
        }
    }
}