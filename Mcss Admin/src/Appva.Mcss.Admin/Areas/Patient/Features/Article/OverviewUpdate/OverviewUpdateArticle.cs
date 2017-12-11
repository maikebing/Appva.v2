// <copyright file="OverviewUpdateArticle.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Web;
    using Appva.Mcss.Application.Models;
    using Microsoft.AspNet.Identity;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class OverviewUpdateArticle : Identity<bool>
    {
        #region Properties.

        /// <summary>
        /// A collection of articles.
        /// </summary>
        public IList<ArticleModel> Articles
        {
            get;
            set;
        }

        #endregion
    }
}