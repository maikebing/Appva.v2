// <copyright file="HomeViewModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using System;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class HomeViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public DateTime SevenDayStartDate
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime SevenDayEndDate
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool HasCalendarOverview
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool HasOrderOverview
        {
            get;
            set;
        }

        public bool ArticleModuleIsInstalled { get; set; }
    }
}