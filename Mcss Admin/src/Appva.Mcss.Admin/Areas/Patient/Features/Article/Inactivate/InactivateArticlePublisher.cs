// <copyright file="InactivateArticlePublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Auditing;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Areas.Models;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Repositories;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class InactivateArticlePublisher : RequestHandler<InactivateArticleModel, bool>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IArticleService"/>.
        /// </summary>
        private readonly IArticleService articleService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="InactivateArticlePublisher"/> class.
        /// </summary>
        /// <param name="articleRepository">The <see cref="IArticleService"/>.</param>
        public InactivateArticlePublisher(IArticleService articleService)
        {
            this.articleService = articleService;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override bool Handle(InactivateArticleModel message)
        {
            var article = this.articleService.Find(message.ArticleId);
            
            if (article == null)
            {
                return false;
            }

            this.articleService.InactivateArticle(article.Id);
            return true;
        }

        #endregion
    }
}