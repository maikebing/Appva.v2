﻿// <copyright file="EditArticlePublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Models.Handlers
{
    #region Imports.

    using System;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Auditing;
    using Appva.Mcss.Admin.Application.Services;
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

        /// <summary>
        /// The <see cref="IPatientService"/>.
        /// </summary>
        private readonly IPatientService patientService;

        /// <summary>
        /// The <see cref="IAuditService"/>.
        /// </summary>
        private readonly IAuditService auditing;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="EditArticlePublisher"/> class.
        /// <param name="articleRepository">The <see cref="IArticleRepository"/>.</param>
        /// </summary>
        public EditArticlePublisher(IArticleRepository articleRepository, IPatientService patientService, IAuditService auditing)
        {
            this.articleRepository = articleRepository;
            this.patientService = patientService;
            this.auditing = auditing;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override bool Handle(EditArticleModel message)
        {
            var article = this.articleRepository.Get(message.Article);
            var currentArticleName = article.Name;
            var patient = this.patientService.Get(message.Id);

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
            this.auditing.Update(patient, "redigerade {0} ({1})", currentArticleName, article.Id);

            return true;
        }

        #endregion
    }
}