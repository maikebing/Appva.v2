﻿// <copyright file="ArticleModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Application.Models
{
    #region Imports.

    using System;
    using Appva.Mcss.Admin.Domain.Entities;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class ArticleModel
    {
        /// <summary>
        /// The article id.
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// The article name.
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// The article description.
        /// </summary>
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// The article category.
        /// </summary>
        public Category Category
        {
            get;
            set;
        }

        /// <summary>
        /// The user that ordered the refill.
        /// </summary>
        public Account OrderedBy
        {
            get;
            set;
        }

        /// <summary>
        /// The refill order date.
        /// </summary>
        public DateTime? OrderDate
        {
            get;
            set;
        }

        /// <summary>
        /// The selected order option key.
        /// </summary>
        public ArticleStatus Status
        {
            get;
            set;
        }

       

        /// <summary>
        /// Displays a formatted order date.
        /// </summary>
        public string FormattedOrderDate
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
    }
}