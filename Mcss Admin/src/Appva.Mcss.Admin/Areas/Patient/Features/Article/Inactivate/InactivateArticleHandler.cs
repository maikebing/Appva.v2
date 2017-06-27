// <copyright file="InactivateArticleHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Areas.Models;
    using Appva.Mcss.Admin.Domain.Repositories;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class InactivateArticleHandler : RequestHandler<InactivateArticle, ListArticle>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IArticleRepository"/>.
        /// </summary>
        private readonly IArticleRepository articleRepository;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="InactivateArticleHandler"/> class.
        /// </summary>
        public InactivateArticleHandler(IArticleRepository articleRepository)
        {
            this.articleRepository = articleRepository;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override ListArticle Handle(InactivateArticle message)
        {
            var article = this.articleRepository.Get(message.Article);
            article.IsActive = false;
            this.articleRepository.Update(article);

            return new ListArticle
            {
                Id = message.Id
            };
        }

        #endregion
    }
}