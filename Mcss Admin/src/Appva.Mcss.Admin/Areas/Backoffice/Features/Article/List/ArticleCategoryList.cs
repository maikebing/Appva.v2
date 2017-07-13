// <copyright file="ArticleCategoryList.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Backoffice.Models
{
    #region Imports.

    using System;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ArticleCategoryList
    {
        #region Properties.

        /// <summary>
        /// The article category id.
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// The article category name.
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// The article category description.
        /// </summary>
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// The amount of active articles in the current category.
        /// </summary>
        public int ArticleCount
        {
            get;
            set;
        }

        /// <summary>
        /// The date the category was created.
        /// </summary>
        public DateTime CreatedAt
        {
            get;
            set;
        }

        /// <summary>
        /// Date and time when the category was updated.
        /// </summary>
        public string UpdatedAt
        {
            get;
            set;
        }

        /// <summary>
        /// Check if the category has any articles.
        /// </summary>
        public bool HasArticles
        {
            get;
            set;
        }

        #endregion
    }
}