// <copyright file="ListAlertModel.cs" company="Appva AB">
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
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Web.ViewModels;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class ListAlertModel
    {
        /// <summary>
        /// 
        /// </summary>
        public PatientViewModel Patient
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<DateTime, Dictionary<Schedule, IList<Task>>> TaskEarlierMap
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<DateTime, Dictionary<Schedule, IList<Task>>> TaskCurrentMap
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<SelectListItem> Years
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<SelectListItem> Months
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime StartDate
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime EndDate
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int? Year
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int? Month
        {
            get;
            set;
        }
    }
}