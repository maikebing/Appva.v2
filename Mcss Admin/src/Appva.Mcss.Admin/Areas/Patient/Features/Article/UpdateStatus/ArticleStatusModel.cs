// <copyright file="ArticleStatusModel.cs" company="Appva AB">
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
    using System.Web;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Models;
    using Appva.Mcss.Application.Models;
    using Microsoft.AspNet.Identity;
    using Appva.Mvc;
    using Appva.Mcss.Admin.Domain.Entities;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ArticleStatusModel : IRequest<ListArticle>
    {
        #region Properties.

        /// <summary>
        /// The patient id.
        /// </summary>
        public Guid Id
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
        /// Gets or sets the articles.
        /// </summary>
        /// <value>
        /// The articles.
        /// </value>
        public IList<Tickable> Articles
        {
            get;
            set;
        }

        #endregion
    }
}