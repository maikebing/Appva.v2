// <copyright file="ArticleOverviewHandler.cs" company="Appva AB">
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
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Application.Models;
    using Appva.Mcss.Admin.Application.Transformers;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Persistence;
    using NHibernate.Criterion;
    using Appva.Mcss.Admin.Application.Auditing;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class ArticleOverviewHandler : RequestHandler<ArticleOverview, ArticleOverviewViewModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IIdentityService"/>.
        /// </summary>
        private readonly IIdentityService identityService;

        /// <summary>
        /// The <see cref="IArticleService"/>.
        /// </summary>
        private readonly IArticleService articleService;

        /// <summary>
        /// The <see cref="IArticleTransformer"/>.
        /// </summary>
        private readonly IArticleTransformer articleTransformer;

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistence;

        /// <summary>
        /// The <see cref="ITaxonFilterSessionHandler"/>.
        /// </summary>
        private readonly ITaxonFilterSessionHandler filtering;

        /// <summary>
        /// The <see cref="IAuditService"/>.
        /// </summary>
        private readonly IAuditService auditing;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="ArticleOverviewHandler"/> class.
        /// </summary>
        /// <param name="identityService">The <see cref="IIdentityService"/>.</param>
        /// <param name="articleService">The <see cref="IArticleService"/>.</param>
        /// <param name="articleTransformer">The <see cref="IArticleTransformer"/>.</param>
        /// <param name="persistence">The <see cref="IPersistenceContext"/>.</param>
        /// <param name="filtering">The <see cref="ITaxonFilterSessionHandler"/>.</param>
        /// <param name="auditing">The <see cref="IAuditService"/>.</param>
        public ArticleOverviewHandler(IIdentityService identityService, IArticleService articleService, IArticleTransformer articleTransformer, IPersistenceContext persistence, ITaxonFilterSessionHandler filtering, IAuditService auditing)
        {
            this.identityService = identityService;
            this.articleService = articleService;
            this.articleTransformer = articleTransformer;
            this.persistence = persistence;
            this.filtering = filtering;
            this.auditing = auditing;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override ArticleOverviewViewModel Handle(ArticleOverview message)
        {
            var userId = this.identityService.PrincipalId;
            var orderedArticles = new List<ArticleModel>();
            var filterTaxon = this.filtering.GetCurrentFilter();
            var orders = this.persistence.QueryOver<Article>()
                .Where(x => x.IsActive)
                    .And(x => x.Refill == true)
                        .Fetch(x => x.RefillOrderedBy).Eager;

            orders.JoinQueryOver<Patient>(x => x.Patient)
                .Where(x => x.IsActive)
                    .And(x => x.Deceased == false)
                        .JoinQueryOver<Taxon>(x => x.Taxon)
                            .Where(Restrictions.On<Taxon>(x => x.Path)
                                .IsLike(filterTaxon.Id.ToString(), MatchMode.Anywhere));

            foreach (var article in orders.List().OrderBy(x => x.RefillOrderDate))
            {
                orderedArticles.Add(this.articleTransformer.ToArticleModel(article));
            }

            this.auditing.Read("läste beställningslistan från översiktsvyn");

            return new ArticleOverviewViewModel
            {
                OrderedArticles = orderedArticles,
                OrderOptions = this.articleService.GetOrderOptions(),
                UserId = userId
            };
        }

        #endregion
    }
}