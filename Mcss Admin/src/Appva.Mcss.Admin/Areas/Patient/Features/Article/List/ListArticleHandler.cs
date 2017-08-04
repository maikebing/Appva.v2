// <copyright file="ListArticleHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Auditing;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Transformers;
    using Appva.Mcss.Admin.Domain.Repositories;
    using Appva.Mcss.Admin.Infrastructure;
    using Appva.Mcss.Application.Models;

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
        /// The <see cref="IAccountService"/>.
        /// </summary>
        private readonly IAccountService accountService;

        /// <summary>
        /// The <see cref="IIdentityService"/>.
        /// </summary>
        private readonly IIdentityService identityService;

        /// <summary>
        /// The <see cref="IPatientService"/>.
        /// </summary>
        private readonly IPatientService patientService;

        /// <summary>
        /// The <see cref="IPatientTransformer"/>.
        /// </summary>
        private readonly IPatientTransformer patientTransformer;

        /// <summary>
        /// The <see cref="IAuditService"/>.
        /// </summary>
        private readonly IAuditService auditing;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="ListArticleHandler"/> class.
        /// </summary>
        /// <param name="articleRepository">The <see cref="IArticleRepository"/></param>
        /// <param name="articleTransformer">The <see cref="IArticleTransformer"/></param>
        /// <param name="accountService">The <see cref="IAccountService"/>.</param>
        /// <param name="identityService">The <see cref="IIdentityService"/>.</param>
        /// <param name="patientService">The <see cref="IPatientService"/></param>
        /// <param name="patientTransformer">The <see cref="IPatientTransformer"/></param>
        /// <param name="articleService">The <see cref="IAuditService"/>.</param>
        public ListArticleHandler(
            IArticleRepository articleRepository, 
            IArticleTransformer articleTransformer, 
            IArticleService articleService,
            IAccountService accountService,
            IIdentityService identityService, 
            IPatientService patientService, 
            IPatientTransformer patientTransformer, 
            IAuditService auditing)
        {
            this.articleRepository = articleRepository;
            this.articleTransformer = articleTransformer;
            this.articleService = articleService;
            this.accountService = accountService;
            this.identityService = identityService;
            this.patientService = patientService;
            this.patientTransformer = patientTransformer;
            this.auditing = auditing;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override ListArticleModel Handle(ListArticle message)
        {
            var listArticleModel = new ListArticleModel();
            var articleModelList = new List<ArticleModel>();
            var account = this.accountService.Find(this.identityService.PrincipalId);
            var categories = this.articleService.GetRoleArticleCategoryList(account);
            var orderedArticles = this.articleRepository.ListByOrderedArticles(message.Id, categories).OrderByDescending(x => x.RefillOrderDate);
            var refilledArticles = this.articleRepository.ListByRefilledArticles(message.Id, categories).OrderByDescending(x => x.CreatedAt).ToList();
            var patient = this.patientService.Get(message.Id);
            var patientViewModel = this.patientTransformer.ToPatient(patient);

            foreach (var article in orderedArticles)
            {
                var options = new List<SelectListItem>();
                articleModelList.Add(articleTransformer.ToArticleModel(article));
            }

            this.auditing.Read(patient, "läste beställningslistan");

            return new ListArticleModel
            {
                OrderedArticles = articleModelList,
                RefilledArticles = refilledArticles,
                HasArticles = articleModelList.Count > 0 || refilledArticles.Count > 0 ? true : false,
                Patient = patientViewModel,
                OrderOptions = this.articleService.GetOrderOptions()
            };
        }

        #endregion
    }
}