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
        /// Updates the order status.
        /// </summary>
        /// <param name="list">A collection of <see cref="ArticleModel"/>.</param>
        /// <param name="userId">The user <see cref="Guid"/>.</param>
        void UpdateStatus(IList<ArticleModel> list, Guid userId);

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
        IList<Article> ListArticlesFor(Patient patient);
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
        /// The <see cref="IAccountRepository"/>.
        /// </summary>
        private readonly IAccountRepository accountRepository;

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
            IAccountRepository accountRepository, 
            IIdentityService identityService,
            IAuditService auditing)
        {
            this.articleRepository = articleRepository;
            this.accountRepository = accountRepository;
            this.identityService   = identityService;
            this.auditing          = auditing;
        }

        #endregion

        #region IArticleService Members.

        /// <inheritdoc />
        public void UpdateStatus(IList<ArticleModel> list, Guid userId)
        {
            var account = this.accountRepository.Get(userId);
            foreach (var orderedArticle in list)
            {
                var article = this.articleRepository.Get(orderedArticle.Id);

                if (article != null && article.Status != orderedArticle.Status)
                {
                    article.UpdateStatus(orderedArticle.Status, account);
                    this.articleRepository.Update(article);

                    this.auditing.Update(article.Patient, "ändrade orderstatus för {0} ({1}) till {2}", article.Name, article.Id, article.Status.ToString().ToLower());

                }
            }
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
        public IList<Article> ListArticlesFor(Patient patient)
        {
            var permissions = this.identityService.ArticleCategoryPermissions().Select(x => new Guid(x.Value)).ToList();
            return this.articleRepository.List(patient.Id, permissions);
        }

        #endregion
    }
}