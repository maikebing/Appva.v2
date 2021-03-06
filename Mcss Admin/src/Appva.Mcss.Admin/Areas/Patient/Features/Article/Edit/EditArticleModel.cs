﻿// <copyright file="EditArticleModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Domain.Entities;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class EditArticleModel : IRequest<bool>
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

        /// <summary>
        /// The patient id.
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
        /// The selected category id.
        /// </summary>
        public string SelectedCategory
        {
            get;
            set;
        }

        /// <summary>
        /// A list of article categories.
        /// </summary>
        public IEnumerable<SelectListItem> Categories
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public ArticleStatus Status
        {
            get;
            set;
        }

        /// <summary>
        /// A list of article status options.
        /// </summary>
        public IDictionary<string, string> StatusOptions
        {
            get;
            set;
        }

        #endregion  
    }
}