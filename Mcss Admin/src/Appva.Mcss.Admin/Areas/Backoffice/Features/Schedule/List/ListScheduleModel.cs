// <copyright file="ListScheduleModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Models
{
    #region Imports.

    using Appva.Mcss.Admin.Domain.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ListScheduleModel
    {
        #region Properties.

        /// <summary>
        /// The schedulesettings
        /// </summary>
        public IList<ScheduleSettings> Schedules
        {
            get;
            set;
        }

        public IList<Schedule> SchedulesUsedBy { get; set; }

        public List<Guid> PatientFilterIdList { get; set; }

        #endregion
    }
}