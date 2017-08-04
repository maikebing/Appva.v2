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
        IDictionary<string, string> GetOrderOptions();

        /// <summary>
        /// Returns a collection of <see cref="ArticleCategory"/> filtered by role permissions.
        /// </summary>
        /// <param name="account">The <see cref="Account"/>.</param>
        /// <returns>A collection of <see cref="ArticleCategory"/>.</returns>
        IList<ArticleCategory> GetRoleArticleCategoryList(Account account);
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
        public ArticleService(IArticleRepository articleRepository, IAccountRepository accountRepository, IAuditService auditing)
        {
            this.articleRepository = articleRepository;
            this.accountRepository = accountRepository;
            this.auditing = auditing;
        }

        #endregion

        #region IArticleService Members.

        /// <inheritdoc />
        public void UpdateStatus(IList<ArticleModel> list, Guid userId)
        {
            foreach (var orderedArticle in list)
            {
                var article = this.articleRepository.Get(orderedArticle.Id);
                var account = this.accountRepository.Find(userId);

                if (article != null && account != null && article.Status != orderedArticle.SelectedOrderOptionKey)
                {
                    bool isRefilled = orderedArticle.SelectedOrderOptionKey == ArticleStatus.Refilled.ToString();
                    string selectedOrderOption = this.GetOrderOptions().FirstOrDefault(x => x.Key == orderedArticle.SelectedOrderOptionKey).Value;

                    article.Refill = isRefilled ? false : true;
                    article.RefillOrderDate = isRefilled ? (DateTime?)null : article.RefillOrderDate;
                    article.RefillOrderedBy = isRefilled ? null : article.RefillOrderedBy;
                    article.Ordered = isRefilled ? true : false;
                    article.OrderDate = isRefilled ? DateTime.Now : article.OrderDate;
                    article.OrderedBy = isRefilled ? account : null;
                    article.Status = isRefilled ? ArticleStatus.Refilled.ToString() : orderedArticle.SelectedOrderOptionKey;
                    this.articleRepository.Update(article);

                    if (isRefilled)
                    {
                        this.auditing.Update(article.Patient, "fyllde på {0} ({1})", article.Name, article.Id);
                    }
                    else
                    {
                        this.auditing.Update(article.Patient, "ändrade orderstatus för {0} ({1}) till {2}", article.Name, article.Id, selectedOrderOption.ToLower());
                    }
                }
            }
        }

        /// <inheritdoc />
        public IDictionary<string, string> GetOrderOptions()
        {
            return new Dictionary<string, string>
            {
                { ArticleStatus.NotStarted.ToString(), "Påfyllning begärd" },
                { ArticleStatus.OrderedFromSupplier.ToString(), "Beställd" },
                { ArticleStatus.Refilled.ToString(), "Påfylld" }
            };
        }

        /// <inheritdoc />
        public IList<ArticleCategory> GetRoleArticleCategoryList(Account account)
        {
            var categories = new List<ArticleCategory>();
            foreach (var role in account.Roles)
            {
                foreach (var category in role.ArticleCategories)
                {
                    if (!categories.Contains(category))
                    {
                        categories.Add(category);
                    }
                }
            }

            return categories;
        }

        #endregion
    }
}