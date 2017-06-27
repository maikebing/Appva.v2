// <copyright file="EditArticlePublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Models.Handlers
{
    #region Imports.

    using System;
    using System.Linq;
    using Appva.Core.Extensions;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Models;
    using Appva.Mcss.Admin.Domain.Repositories;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class EditArticlePublisher : RequestHandler<EditArticleModel, bool>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IArticleRepository"/>.
        /// </summary>
        private readonly IArticleRepository articleRepository;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="EditArticlePublisher"/> class.
        /// <param name="articleRepository">The <see cref="IArticleRepository"/>.</param>
        /// </summary>
        public EditArticlePublisher(IArticleRepository articleRepository)
        {
            this.articleRepository = articleRepository;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override bool Handle(EditArticleModel message)
        {
            var article = this.articleRepository.Get(message.Article);

            if(article == null || string.IsNullOrEmpty(message.Name))
            {
                return false;
            }

            var category = this.articleRepository.GetCategory(new Guid(message.SelectedCategory));
            article.Name = message.Name;
            article.Description = message.Description;
            article.ArticleCategory = category;
            article.Version += 1;
            article.UpdatedAt = DateTime.Now;
            this.articleRepository.Update(article);

            return true;
        }

        #endregion
    }
}