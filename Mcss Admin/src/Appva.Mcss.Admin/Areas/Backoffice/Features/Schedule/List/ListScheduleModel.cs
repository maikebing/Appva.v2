// <copyright file="ListScheduleModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
//     <a href="mailto:ziemanncarl@gmail.com">Carl Ziemann</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Models
{
    #region Imports.
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Appva.Mcss.Admin.Domain.Entities;
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
        public IList<ScheduleSettings> ScheduleSettings
        {
            get;
            set;
        }

        /// <summary>
        /// All the Schedules 
        /// </summary>
        public IList<Schedule> Schedules { get; set; }

        /// <summary>
        /// List of sequences
        /// </summary>
        public IList<Sequence> SequenceList { get; set; }
        #endregion
    }
}