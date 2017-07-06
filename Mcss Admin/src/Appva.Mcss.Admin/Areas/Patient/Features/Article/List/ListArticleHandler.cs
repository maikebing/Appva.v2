// <copyright file="ListArticleHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Domain.Repositories;
    using Appva.Mcss.Admin.Infrastructure;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ListArticleHandler : RequestHandler<ListArticle, ListArticleModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IArticleRepository"/>.
        /// </summary>
        private readonly IArticleRepository articleRepository;

        /// <summary>
        /// The <see cref="IPatientService"/>.
        /// </summary>
        private readonly IPatientService patientService;

        /// <summary>
        /// The <see cref="IPatientTransformer"/>.
        /// </summary>
        private readonly IPatientTransformer transformer;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ListArticleHandler"/> class.
        /// </summary>
        /// <param name="articleService">The <see cref="IArticleRepository"/></param>
        /// <param name="patientService">The <see cref="IPatientService"/></param>
        /// <param name="transformer">The <see cref="IPatientTransformer"/></param>
        public ListArticleHandler(IArticleRepository articleRepository, IPatientService patientService, IPatientTransformer transformer)
        {
            this.articleRepository = articleRepository;
            this.patientService = patientService;
            this.transformer = transformer;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override ListArticleModel Handle(ListArticle message)
        {
            var articleModelList = new List<ArticleModel>();
            var orderedArticles = this.articleRepository.ListByOrderedArticles(message.Id).OrderByDescending(x => x.RefillOrderDate);
            var refilledArticles = this.articleRepository.ListByRefilledArticles(message.Id).OrderByDescending(x => x.CreatedAt).ToList();
            var orderOptions = new Dictionary<string, string> {
                { "not-started", "Påfyllning begärd" },
                { "ordered-from-supplier", "Beställd" },
                { "refilled", "Påfylld" } };

            foreach (var article in orderedArticles)
            {
                var options = new List<SelectListItem>();
                var articleModel = new ArticleModel
                {
                    Id = article.Id,
                    Name = article.Name,
                    Description = article.Description,
                    Category = article.ArticleCategory,
                    OrderedBy = article.RefillOrderedBy,
                    OrderDate = article.RefillOrderDate,
                    SelectedOrderOptionKey = article.Status == null ? "not-started" : article.Status
                };

                foreach (var option in orderOptions)
                {
                    if(articleModel.SelectedOrderOptionKey == option.Key)
                    {
                        articleModel.SelectedOrderOptionValue = option.Value;
                    }

                    options.Add(new SelectListItem
                    {
                        Text = option.Value,
                        Value = option.Key,
                        Selected = option.Key == articleModel.SelectedOrderOptionKey ? true : false
                    });
                }

                articleModel.OrderOptions = options;
                articleModelList.Add(articleModel);
            }

            return new ListArticleModel
            {
                OrderedArticles = articleModelList,
                RefilledArticles = refilledArticles,
                HasArticles = articleModelList.Count > 0 || refilledArticles.Count > 0 ? true : false,
                Patient = this.transformer.ToPatient(this.patientService.Get(message.Id))
            };
        }

        #endregion
    }
}