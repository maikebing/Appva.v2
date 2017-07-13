// <copyright file="ArticleCategoryListHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Backoffice.Models.Handlers
{
    #region Imports.

    using System.Collections.Generic;
    using System.Globalization;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Repositories;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ArticleListHandler : RequestHandler<Parameterless<ArticleListModel>, ArticleListModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IArticleRepository"/>.
        /// </summary>
        private readonly IArticleRepository repository;

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistence;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="ArticleListHandler"/> class.
        /// </summary>
        /// <param name="repository">The <see cref="IArticleRepository"/>.</param>
        public ArticleListHandler(IArticleRepository repository, IPersistenceContext persistence)
        {
            this.repository = repository;
            this.persistence = persistence;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override ArticleListModel Handle(Parameterless<ArticleListModel> message)
        {
            var categories = this.repository.GetCategories();
            var categoryList = new List<ArticleCategoryList>();

            foreach (var category in categories)
            {
                int articleCount = this.persistence.QueryOver<Article>()
                    .Where(x => x.IsActive == true)
                        .And(x => x.ArticleCategory == category)
                            .RowCount();

                categoryList.Add(new ArticleCategoryList
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description,
                    ArticleCount = articleCount,
                    CreatedAt = category.CreatedAt,
                    UpdatedAt = category.UpdatedAt.ToString("yyyy-MM-dd, HH:mm:ss"),
                    HasArticles = articleCount > 0 ? true : false
                });
            }

            return new ArticleListModel
            {
                CategoryList = categoryList
            };
        }

        #endregion
    }
}