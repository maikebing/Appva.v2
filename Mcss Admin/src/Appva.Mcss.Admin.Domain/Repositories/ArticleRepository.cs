﻿// <copyright file="ArticleRepository.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Domain.Repositories
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface IArticleRepository : IRepository
    {
        /// <summary>
        /// Get an article by id.
        /// </summary>
        /// <param name="articleId">The article <see cref="Guid"/>.</param>
        /// <returns>An <see cref="Article"/>.</returns>
        Article Get(Guid articleId);

        /// <summary>
        /// Get a category by id.
        /// </summary>
        /// <param name="categoryId">The category <see cref="Guid"/>.</param>
        /// <returns>An <see cref="ArticleCategory"/>.</returns>
        ArticleCategory GetCategory(Guid categoryId);

        /// <summary>
        /// Returns a collection of <see cref="ArticleCategory"/>.
        /// </summary>
        /// <returns>A collection of <see cref="ArticleCategory"/>.</returns>
        IList<ArticleCategory> GetCategories();

        /// <summary>
        /// Returns a collection of ordered <see cref="Article"/>.
        /// </summary>
        /// <param name="patientId">The patient <see cref="Guid"/>.</param>
        /// <returns>A collection of ordered <see cref="Article"/>.</returns>
        IList<Article> ListByOrderedArticles(Guid patientId);

        /// <summary>
        /// Returns a collection of refilled <see cref="Article"/>.
        /// </summary>
        /// <param name="patientId">The patient <see cref="Guid"/>.</param>
        /// <returns>A collection of refilled <see cref="Article"/>.</returns>
        IList<Article> ListByRefilledArticles(Guid patientId);

        /// <summary>
        /// Updates an <see cref="Article"/>.
        /// </summary>
        /// <param name="article">The <see cref="Article"/>.</param>
        void Update(Article article);

        /// <summary>
        /// Saves an <see cref="Article"/>.
        /// </summary>
        /// <param name="article">The <see cref="Article"/>.</param>
        void Save(Article article);
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class ArticleRepository : IArticleRepository
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IPersistenceContext"/> instance.
        /// </summary>
        private readonly IPersistenceContext persistenceContext;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ArticleRepository"/> class.
        /// </summary>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public ArticleRepository(IPersistenceContext persistenceContext)
        {
            this.persistenceContext = persistenceContext;
        }

        #endregion

        #region IArticleRepository Members.

        /// <inheritdoc />
        public Article Get(Guid articleId)
        {
            return this.persistenceContext.QueryOver<Article>()
                .Where(x => x.Id == articleId)
                    .And(x => x.IsActive == true)
                        .SingleOrDefault();
        }

        public ArticleCategory GetCategory(Guid categoryId)
        {
            return this.persistenceContext.QueryOver<ArticleCategory>()
                .Where(x => x.Id == categoryId)
                    .And(x => x.IsActive == true)
                        .SingleOrDefault();
        }

        /// <inheritdoc />
        public IList<ArticleCategory> GetCategories()
        {
            return this.persistenceContext.QueryOver<ArticleCategory>()
                .Where(x => x.IsActive == true)
                    .OrderBy(x => x.Name).Asc
                        .List();
        }

        /// <inheritdoc />
        public IList<Article> ListByOrderedArticles(Guid patientId)
        {
            return this.persistenceContext.QueryOver<Article>()
                .Where(x => x.Patient.Id == patientId)
                    .And(x => x.Refill == true)
                        .And(x => x.IsActive == true)
                            .List();
        }

        /// <inheritdoc />
        public IList<Article> ListByRefilledArticles(Guid patientId)
        {
            return this.persistenceContext.QueryOver<Article>()
                .Where(x => x.Patient.Id == patientId)
                    .And(x => x.Refill == false)
                        .And(x => x.IsActive == true)
                            .List();
        }

        /// <inheritdoc />
        public void Update(Article article)
        {
            this.persistenceContext.Update(article);
        }

        /// <inheritdoc />
        public void Save(Article article)
        {
            this.persistenceContext.Save(article);
        }

        #endregion
    }
}