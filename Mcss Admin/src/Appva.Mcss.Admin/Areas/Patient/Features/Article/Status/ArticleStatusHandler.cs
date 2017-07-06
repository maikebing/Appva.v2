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
    using System.Linq;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Areas.Models;
    using Appva.Mcss.Admin.Domain.Repositories;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ArticleStatusHandler : RequestHandler<ArticleStatusModel, ListArticle>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IArticleRepository"/>.
        /// </summary>
        private readonly IArticleRepository articleRepository;

        /// <summary>
        /// The <see cref="IAccountRepository"/>.
        /// </summary>
        private readonly IAccountRepository accountRepository;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="InactivateArticleHandler"/> class.
        /// </summary>
        public ArticleStatusHandler(IArticleRepository articleRepository, IAccountRepository accountRepository)
        {
            this.articleRepository = articleRepository;
            this.accountRepository = accountRepository;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override ListArticle Handle(ArticleStatusModel message)
        {
            //var selectedArticle = message.OrderedArticles.Where(x => x.Id != Guid.Empty).FirstOrDefault();

            foreach (var orderedArticle in message.OrderedArticles)
            {
                var article = this.articleRepository.Get(orderedArticle.Id);
                var account = this.accountRepository.Find(message.UserId);

                if (article != null && account != null)
                {
                    bool isRefilled = orderedArticle.SelectedOrderOptionKey == "refilled";
                    article.Refill = isRefilled ? false : true;
                    article.RefillOrderDate = isRefilled ? (DateTime?)null : article.RefillOrderDate;
                    article.RefillOrderedBy = isRefilled ? null : article.RefillOrderedBy;
                    article.Ordered = isRefilled ? true : false;
                    article.OrderDate = isRefilled ? DateTime.Now : article.OrderDate;
                    article.OrderedBy = isRefilled ? account : null;
                    article.Status = isRefilled ? "refilled" : orderedArticle.SelectedOrderOptionKey;
                    this.articleRepository.Update(article);
                }
            }

            return new ListArticle
            {
                Id = message.PatientId
            };
        }

        #endregion
    }
}