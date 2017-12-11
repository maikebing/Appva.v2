// <copyright file="ArticleListHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Backoffice.Models.Handlers
{
    #region Imports.

    using System.Linq;
    using System.Collections.Generic;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Repositories;
    using Appva.Mcss.Admin.Domain.VO;
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

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService settingsService;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="ArticleListHandler"/> class.
        /// </summary>
        /// <param name="repository">The <see cref="IArticleRepository"/>.</param>
        /// <param name="persistence">The <see cref="IPersistenceContext"/>.</param>
        /// <param name="settingService">The <see cref="ISettingsService"/>.</param>
        public ArticleListHandler(IArticleRepository repository, IPersistenceContext persistence, ISettingsService settingsService)
        {
            this.repository = repository;
            this.persistence = persistence;
            this.settingsService = settingsService;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override ArticleListModel Handle(Parameterless<ArticleListModel> message)
        {
            var setting = this.settingsService.Find(ApplicationSettings.OrderListSettings);

            return new ArticleListModel
            {
                IsInstalled = setting.IsInstalled,
                CategoryList = this.GetArticleCategoryList()
            };
        }

        #endregion

        #region Private methods.

        /// <inheritdoc />
        private IList<ArticleCategoryList> GetArticleCategoryList()
        {
            var categoryList = new List<ArticleCategoryList>();

            foreach (var category in this.repository.GetCategories())
            {
                int listCount = this.persistence.QueryOver<ScheduleSettings>()
                    .Where(x => x.OrderRefill == true)
                        .And(x => x.ArticleCategory != null)
                            .And(x => x.ArticleCategory == category)
                                .JoinQueryOver(x => x.ArticleCategory)
                                    .Where(x => x.IsActive == true)
                                        .RowCount();

                int articleCount = this.persistence.QueryOver<Article>()
                    .Where(x => x.IsActive == true)
                        .And(x => x.Category == category)
                            .RowCount();

                categoryList.Add(new ArticleCategoryList
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description,
                    ListCount = listCount,
                    ArticleCount = articleCount,
                    UpdatedAt = category.UpdatedAt,
                    HasLists = listCount > 0,
                    HasArticles = articleCount > 0
                });
            }

            return categoryList;
        }

        #endregion
    }
}