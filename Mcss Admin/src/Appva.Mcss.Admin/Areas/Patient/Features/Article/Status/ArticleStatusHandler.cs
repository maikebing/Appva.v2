// <copyright file="ArticleStatusHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using System;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Areas.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ArticleStatusHandler : RequestHandler<ArticleStatusModel, ListArticle>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IArticleService"/>.
        /// </summary>
        private readonly IArticleService articleService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="InactivateArticleHandler"/> class.
        /// </summary>
        /// <param name="articleService">The <see cref="IArticleService"/>.</param>
        public ArticleStatusHandler(IArticleService articleService)
        {
            this.articleService = articleService;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override ListArticle Handle(ArticleStatusModel message)
        {
            this.articleService.UpdateStatus(message.OrderedArticles, message.UserId);

            return new ListArticle
            {
                Id = message.PatientId
            };
        }

        #endregion
    }
}