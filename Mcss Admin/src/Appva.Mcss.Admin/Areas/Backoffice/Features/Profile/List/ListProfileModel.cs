// <copyright file="ListProfileModel.cs" company="Appva AB">
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
    using Appva.Mcss.Admin.Application.Models;
    #region Imports.

    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using Appva.Mcss.Web.ViewModels;
    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ListProfileModel : SearchViewModel<ProfileAssessment>
    {
        #region Properties

        /// <summary>
        /// List of assessments.
        /// </summary>
        /// 

        public List<ProfileAssessment> Assessments
        {
            get;
            set;
        }

        public bool IsActive { get; set; }

        //public List<ProfileAssessment> Assessments { get; set; }

        #endregion
    }
}