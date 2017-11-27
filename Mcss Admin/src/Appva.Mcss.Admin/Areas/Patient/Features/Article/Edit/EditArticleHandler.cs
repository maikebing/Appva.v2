// <copyright file="EditArticleHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using System.Collections.Generic;
    using System.Web.Mvc;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Areas.Models;
    using Appva.Mcss.Admin.Domain.Repositories;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Security.Identity;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class EditArticleHandler : RequestHandler<EditArticle, EditArticleModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IArticleService"/>.
        /// </summary>
        private readonly IArticleService articleService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="EditArticleHandler"/> class.
        /// </summary>
        /// <param name="articleRepository">The <see cref="IArticleRepository"/>.</param>
        public EditArticleHandler(IArticleService articleService)
        {
            this.articleService = articleService;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override EditArticleModel Handle(EditArticle message)
        {
            var article = this.articleService.Find(message.Article);
            var categories = this.articleService.ListCategories();
            var categoryList = new List<SelectListItem>();

            foreach (var category in categories)
            {
                categoryList.Add(new SelectListItem
                {
                    Text = category.Name,
                    Value = category.Id.ToString(),
                    Selected = article.ArticleCategory.Id == category.Id ? true : false
                });
            }

            return new EditArticleModel
            {
                Article     = message.Article,
                Id          = message.Id,
                Name        = article.Name,
                Description = article.Description,
                Categories  = categoryList,
                StatusOptions = this.articleService.GetArticleStatusOptions(),
                Status      = article.Status
            };
        }

        #endregion
    }
}