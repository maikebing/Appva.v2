// <copyright file="ArticleService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Application.Services
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Mcss.Admin.Application.Auditing;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Repositories;
    using Appva.Mcss.Application.Models;
    using Appva.Mcss.Admin.Application.Security.Identity;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface IArticleService : IService
    {
        /// <summary>
        /// Finds the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Article Find(Guid id);

        /// <summary>
        /// Updates the status for.
        /// </summary>
        /// <param name="articleId">The article identifier.</param>
        /// <param name="withStatus">The with status.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Article UpdateStatusFor(Guid articleId, ArticleStatus withStatus);

        /// <summary>
        /// Returns a collection of order options.
        /// </summary>
        /// <returns>An <see cref="IDictionary{string, string}"/>.</returns>
        IDictionary<string, string> GetArticleStatusOptions();

        /// <summary>
        /// Lists the articles for.
        /// </summary>
        /// <param name="patient">The patient.</param>
        /// <returns></returns>
        IList<Article> ListArticlesFor(Patient patient, ArticleStatus? filterBy);

        /// <summary>
        /// Lists the categories.
        /// </summary>
        /// <returns></returns>
        IList<ArticleCategory> ListCategories();

        /// <summary>
        /// Inactivates the specified article.
        /// </summary>
        /// <param name="article">The article.</param>
        void InactivateArticle(Guid articleId);

        /// <summary>
        /// Creates the article.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        /// <param name="patient">The patient.</param>
        /// <param name="articleCategory">The article category.</param>
        /// <returns></returns>
        Article CreateArticle(string name, string description, Patient patient, ArticleCategory articleCategory);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ArticleService : IArticleService
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IArticleRepository"/>.
        /// </summary>
        private readonly IArticleRepository articleRepository;

        /// <summary>
        /// The <see cref="IArticleCategoryRepository"/>.
        /// </summary>
        private readonly IArticleCategoryRepository articleCategoryRepository;

        /// <summary>
        /// The <see cref="IAccountRepository"/>.
        /// </summary>
        private readonly IAccountRepository accountRepository;

        /// <summary>
        /// The <see cref="ISequenceService"/>.
        /// </summary>
        private readonly ISequenceService sequenceService;

        /// <summary>
        /// The <see cref="IIdentityService"/>
        /// </summary>
        private readonly IIdentityService identityService;

        /// <summary>
        /// The <see cref="IAuditService"/>.
        /// </summary>
        private readonly IAuditService auditing;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="ArticleService"/> class.
        /// </summary>
        /// <param name="articleRepository">The <see cref="IArticleRepository"/>.</param>
        /// <param name="accountRepository">The <see cref="IAccountRepository"/>.</param>
        /// <param name="auditing">The <see cref="IAuditService"/>.</param>
        public ArticleService(
            IArticleRepository articleRepository, 
            IArticleCategoryRepository articleCategoryRepository,
            IAccountRepository accountRepository, 
            ISequenceService sequenceService,
            IIdentityService identityService,
            IAuditService auditing)
        {
            this.articleRepository = articleRepository;
            this.articleCategoryRepository = articleCategoryRepository;
            this.accountRepository = accountRepository;
            this.sequenceService   = sequenceService;
            this.identityService   = identityService;
            this.auditing          = auditing;
        }

        #endregion

        #region IArticleService Members.

        /// <inheritdoc />
        public Article Find(Guid id)
        {
            return this.articleRepository.Get(id);
        }

        /// <inheritdoc />
        public Article UpdateStatusFor(Guid articleId, ArticleStatus withStatus)
        {
            var account = this.accountRepository.Get(this.identityService.PrincipalId);
            var article = this.articleRepository.Get(articleId);

            if(article == null)
            {
                return null;
            }

            //// Can't update if article already has the given status
            if (article.Status == withStatus)
            {
                return article;
            }

            article.UpdateStatus(withStatus, account);
            this.articleRepository.Update(article);
            this.auditing.Update(article.Patient, "ändrade orderstatus för {0} ({1}) till {2}", article.Name, article.Id, article.Status.ToString().ToLower());

            return article;
        }

        /// <inheritdoc />
        public IDictionary<string, string> GetArticleStatusOptions()
        {
            return new Dictionary<string, string>
            {
                { ArticleStatus.RefillRequested.ToString(), "Påfyllning begärd" },
                { ArticleStatus.OrderedFromSupplier.ToString(), "Beställd" },
                { ArticleStatus.NotStarted.ToString(), "Påfylld" }
            };
        }

        /// <inheritdoc />
        public IList<Article> ListArticlesFor(Patient patient, ArticleStatus? filterBy)
        {
            var permissions = this.identityService.ArticleCategoryPermissions().Select(x => new Guid(x.Value)).ToList();
            return this.articleRepository.List(patient.Id, filterBy, permissions);
        }

        /// <inheritdoc />
        public IList<ArticleCategory> ListCategories()
        {
            var categoryIds = this.identityService.ArticleCategoryPermissions().Select(x => new Guid(x.Value)).ToList();
            return this.articleCategoryRepository.List(categoryIds);
        }

        /// <inheritdoc />
        public void InactivateArticle(Guid articleId)
        {
            var article = this.Find(articleId);
            if (article == null)
            {
                return;
            }

            var sequences = this.sequenceService.ListByArticle(articleId);
            foreach (var sequence in sequences)
            {
                sequence.Article = null;
                this.sequenceService.Update(sequence);
            }

            article.IsActive = false;
            this.articleRepository.Update(article);

            this.auditing.Delete(article.Patient, "raderade artikeln {0} ({1})", article.Name, article.Id);
        }

        /// <inheritdoc />
        public Article CreateArticle(string name, string description, Patient patient, ArticleCategory articleCategory)
        {
            var article = Article.CreateNew(name, description, patient, articleCategory, ArticleStatus.NotStarted);
            this.articleRepository.Save(article);
            this.auditing.Create(patient, "skapade artikeln {0} ({1})", article.Name, article.Id);

            return article;
        }

        #endregion


        
    }
}