// <copyright file="ArticleCategory.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using System;
    using Appva.Common.Domain;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class ArticleCategory : AggregateRoot<ArticleCategory>
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="ArticleCategory"/> class.
        /// </summary>
        public ArticleCategory()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The name.
        /// </summary>
        public virtual string Name
        {
            get;
            set;
        }

        /// <summary>
        /// The category description.
        /// </summary>
        public virtual string Description
        {
            get;
            set;
        }

        #endregion
    }
}