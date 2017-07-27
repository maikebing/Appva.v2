// <copyright file="ArticleListModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Backoffice.Models
{
    #region Imports.

    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ArticleListModel
    {
        #region Properties.

        /// <summary>
        /// If the order list has been activated.
        /// </summary>
        public bool IsOrderListEnabled
        {
            get;
            set;
        }

        /// <summary>
        /// If the order list function is enableable.
        /// </summary>
        public bool IsOrderListEnableable
        {
            get;
            set;
        }

        /// <summary>
        /// A list of <see cref="ArticleCategoryList"/>.
        /// </summary>
        public IList<ArticleCategoryList> CategoryList
        {
            get;
            set;
        }

        #endregion
    }
}