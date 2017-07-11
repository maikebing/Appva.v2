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
    using Appva.Mcss.Application.Models;
    using Appva.Mcss.Admin.Application.Transformers;

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
        /// The <see cref="IArticleTransformer"/>.
        /// </summary>
        private readonly IArticleTransformer articleTransformer;

        /// <summary>
        /// The <see cref="IArticleService"/>.
        /// </summary>
        private readonly IArticleService articleService;

        /// <summary>
        /// The <see cref="IPatientService"/>.
        /// </summary>
        private readonly IPatientService patientService;

        /// <summary>
        /// The <see cref="IPatientTransformer"/>.
        /// </summary>
        private readonly IPatientTransformer patientTransformer;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="ListArticleHandler"/> class.
        /// </summary>
        /// <param name="articleRepository">The <see cref="IArticleRepository"/></param>
        /// <param name="articleTransformer">The <see cref="IArticleTransformer"/></param>
        /// <param name="patientService">The <see cref="IPatientService"/></param>
        /// <param name="patientTransformer">The <see cref="IPatientTransformer"/></param>
        public ListArticleHandler(IArticleRepository articleRepository, IArticleTransformer articleTransformer, IArticleService articleService, IPatientService patientService, IPatientTransformer patientTransformer)
        {
            this.articleRepository = articleRepository;
            this.articleTransformer = articleTransformer;
            this.articleService = articleService;
            this.patientService = patientService;
            this.patientTransformer = patientTransformer;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override ListArticleModel Handle(ListArticle message)
        {
            var listArticleModel = new ListArticleModel();
            var articleModelList = new List<ArticleModel>();
            var orderedArticles = this.articleRepository.ListByOrderedArticles(message.Id).OrderByDescending(x => x.RefillOrderDate);
            var refilledArticles = this.articleRepository.ListByRefilledArticles(message.Id).OrderByDescending(x => x.CreatedAt).ToList();

            foreach (var article in orderedArticles)
            {
                var options = new List<SelectListItem>();
                articleModelList.Add(articleTransformer.ToArticleModel(article));
            }

            return new ListArticleModel
            {
                OrderedArticles = articleModelList,
                RefilledArticles = refilledArticles,
                HasArticles = articleModelList.Count > 0 || refilledArticles.Count > 0 ? true : false,
                Patient = this.patientTransformer.ToPatient(this.patientService.Get(message.Id)),
                OrderOptions = this.articleService.GetOrderOptions()
            };
        }

        #endregion
    }
}