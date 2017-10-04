﻿// <copyright file="ListArticleModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using System.Collections.Generic;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Application.Models;
    using Appva.Mcss.Web.ViewModels;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ListArticleModel
    {
        /// <summary>
        /// A list of articles with a refill request.
        /// </summary>
        public IList<ArticleModel> OrderedArticles
        {
            get;
            set;
        }

        /// <summary>
        /// A list of refilled articles.
        /// </summary>
        public IList<Article> Articles
        {
            get;
            set;
        }

        /// <summary>
        /// A list of article order options.
        /// </summary>
        public IDictionary<string, string> OrderOptions
        {
            get;
            set;
        }

        /// <summary>
        /// Check if any refilled and ordered articles exists.
        /// </summary>
        public bool HasArticles
        {
            get;
            set;
        }

        /// <summary>
        /// The patient view model.
        /// </summary>
        public PatientViewModel Patient
        {
            get;
            set;
        }
    }
}