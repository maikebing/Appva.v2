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
    using Appva.Mcss.Admin.Domain.Entities;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ListArticleHandler : RequestHandler<ListArticle, ListArticleModel>
    {
        #region Fields.

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
        /// <param name="accountService">The <see cref="IAccountService"/>.</param>
        /// <param name="identityService">The <see cref="IIdentityService"/>.</param>
        /// <param name="patientService">The <see cref="IPatientService"/></param>
        /// <param name="patientTransformer">The <see cref="IPatientTransformer"/></param>
        /// <param name="articleService">The <see cref="IAuditService"/>.</param>
        public ListArticleHandler(
            IArticleService articleService,
            IAccountService accountService,
            IIdentityService identityService, 
            IPatientService patientService, 
            IPatientTransformer patientTransformer, 
            IAuditService auditing)
        {
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
            var patient  = this.patientService.Get(message.Id);
            var account  = this.accountService.Find(this.identityService.PrincipalId);
            var articles = this.articleService.ListArticlesFor(patient, message.Status);

            this.auditing.Read(patient, "listade artiklar i beställningslistan");

            return new ListArticleModel
            {
                OrderedCount         = (message.Status.HasValue ? this.articleService.ListArticlesFor(patient, null) : articles).Count(x => x.Status == ArticleStatus.OrderedFromSupplier),
                RefillRequestedCount = (message.Status.HasValue ? this.articleService.ListArticlesFor(patient, null) : articles).Count(x => x.Status == ArticleStatus.RefillRequested),
                Articles             = articles,
                Patient              = this.patientTransformer.ToPatient(patient),
                OrderOptions         = this.articleService.GetArticleStatusOptions(),
                StatusFilter         = message.Status
            };
        }

        #endregion
    }
}