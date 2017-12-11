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

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class Article : AggregateRoot
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
        public virtual Category Category
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
        /// The refill order status.
        /// </summary>
        public virtual ArticleStatus Status
        {
            get;
            set;
        }

        /// <summary>
        /// The patient.
        /// </summary>
        public virtual Patient Patient
        {
            get;
            set;
        }

        #endregion

        #region Public members.

        /// <summary>
        /// Updates the status.
        /// </summary>
        /// <param name="withStatus">The with status.</param>
        /// <param name="byUser">The by user.</param>
        public virtual void UpdateStatus(ArticleStatus withStatus, Account byUser)
        {
            this.Status = withStatus;
            switch (withStatus)
            {
                case ArticleStatus.NotStarted:
                    break;
                case ArticleStatus.RefillRequested:
                    this.RefillOrderedBy = byUser;
                    this.RefillOrderDate = DateTime.Now;
                    break;
                case ArticleStatus.OrderedFromSupplier:
                    this.OrderedBy = byUser;
                    this.OrderDate = DateTime.Now;
                    break;
            }
        }

        #endregion

        #region Public static methods.

        /// <summary>
        /// Creates a new <see cref="Article"/>.
        /// </summary>
        /// <param name="name">The article name.</param>
        /// <param name="description">The article description.</param>
        /// <param name="patient">The <see cref="Patient"/>.</param>
        /// <param name="category">The <see cref="Category"/>.</param>
        /// <param name="orderStatus">The article order status.</param>
        /// <returns>A new <see cref="Article"/>.</returns>
        public static Article CreateNew(string name, string description, Patient patient, Category category, ArticleStatus status)
        {
            return new Article
            {
                Name = name,
                Description = description,
                RefillOrderDate = null,
                RefillOrderedBy = null,
                OrderDate = null,
                OrderedBy = null,
                Status = status,
                Patient = patient,
                Category = category
            };
        }

        #endregion
    }

    public enum ArticleStatus
    {
        NotStarted,
        RefillRequested,
        OrderedFromSupplier
    }
}