// <copyright file="Article.cs" company="Appva AB">
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
    public class Article : AggregateRoot<Article>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Article"/> class.
        /// </summary>
        public Article()
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
        /// The description.
        /// </summary>
        public virtual string Description
        {
            get;
            set;
        }

        /// <summary>
        /// The article category.
        /// </summary>
        public virtual ArticleCategory ArticleCategory
        {
            get;
            set;
        }

        /// <summary>
        /// Check if the article needs to be refilled.
        /// </summary>
        public virtual bool Refill
        {
            get;
            set;
        }

        /// <summary>
        /// The account which ordered the last refill.
        /// </summary>
        public virtual Account RefillOrderedBy
        {
            get;
            set;
        }

        /// <summary>
        /// The date last refill was ordered.
        /// </summary>
        public virtual DateTime? RefillOrderDate
        {
            get;
            set;
        }

        /// <summary>
        /// Check if the article needs to be refilled and material have been ordered from manufacturer.
        /// </summary>
        public virtual bool Ordered
        {
            get;
            set;
        }

        /// <summary>
        /// The account which made the order from manufacturer.
        /// </summary>
        public virtual DateTime? OrderDate
        {
            get;
            set;
        }

        /// <summary>
        /// When the article was ordered form manufacturer.
        /// </summary>
        public virtual Account OrderedBy
        {
            get;
            set;
        }

        /// <summary>
        /// Last update.
        /// </summary>
        public new virtual DateTime? UpdatedAt
        {
            get;
            set;
        }

        /// <summary>
        /// The refill order status.
        /// </summary>
        public virtual string Status
        {
            get;
            set;
        }

        /// <summary>
        /// The patient.
        /// </summary>
        public Patient Patient
        {
            get;
            set;
        }

        #endregion
    }
}