// <copyright file="EditArticle.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Models
{
    #region Imports.

    using System;
    using Appva.Mcss.Admin.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class EditArticle : Identity<EditArticleModel>
    {
        #region Properties.

        /// <summary>
        /// The article id.
        /// </summary>
        public Guid Article
        {
            get;
            set;
        }

        #endregion
    }
}