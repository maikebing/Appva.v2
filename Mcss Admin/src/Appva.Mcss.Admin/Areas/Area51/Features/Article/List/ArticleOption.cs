// <copyright file="ArticleOption.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Features.Area51.ArticleOption
{
    #region Imports.

    using Appva.Mcss.Admin.Domain.VO;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class ArticleOption
    {
        /// <summary>
        /// If categories has been migrated.
        /// </summary>
        public bool HasCreatedCategories
        {
            get;
            set;
        }

        /// <summary>
        /// If articles has been migrated.
        /// </summary>
        public bool HasMigratedArticles
        {
            get;
            set;
        }

        /// <summary>
        /// If there are any migratable items.
        /// </summary>
        public bool HasMigratableItems
        {
            get;
            set;
        }
    }
}