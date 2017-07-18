// <copyright file="ArticleOption.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Features.Area51.ArticleOption
{
    #region Imports.

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class ArticleOption
    {
        /// <summary>
        /// Check if article categories has been created.
        /// </summary>
        public bool HasCreatedCategories
        {
            get;
            set;
        }

        /// <summary>
        /// The article migration status.
        /// </summary>
        public bool HasMigratedArticles
        {
            get;
            set;
        }
    }
}