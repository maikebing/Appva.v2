﻿// <copyright file="ListProfileModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:h4nsson@gmail.com">Emmanuel Hansson</a>
// </author>
// <author>
//     <a href="mailto:ziemanncarl@gmail.com">Carl Ziemann</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Backoffice.Models
{
    #region Imports.

    using System.Collections.Generic;
    using Appva.Mcss.Web.ViewModels;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ListProfileModel : SearchViewModel<ProfileAssessment>
    {
        #region Properties.

        /// <summary>
        /// List of assessments.
        /// </summary>
        public List<ProfileAssessment> Assessments
        {
            get;
            set;
        }

        /// <summary>
        /// Filter by.
        /// </summary>
        public bool? IsActive
        {
            get; set;
        }

        /// <summary>
        /// Number of new assessments to install.
        /// </summary>
        public int NotInstalledAssesments
        {
            get; set;
        }

        #endregion
    }
}