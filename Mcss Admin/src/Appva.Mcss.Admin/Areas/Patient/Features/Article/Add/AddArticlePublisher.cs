﻿// <copyright file="AddArticlePublisher.cs" company="Appva AB">
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
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Repositories;
    using Appva.Mcss.Application.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class AddArticlePublisher : RequestHandler<AddArticleModel, bool>
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
        /// The
        /// </summary>
        private readonly IAuditService auditing;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="AddArticlePublisher"/> class.
        /// <param name="articleRepository">The <see cref="IArticleRepository"/>.</param>
        /// <param name="patientService">The <see cref="IPatientService"/>.</param>
        /// </summary>
        public AddArticlePublisher(IArticleRepository articleRepository, IPatientService patientService, IAuditService auditing)
        {
            this.articleRepository = articleRepository;
            this.patientService = patientService;
            this.auditing = auditing;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override bool Handle(AddArticleModel message)
        {
            if(string.IsNullOrEmpty(message.SelectedCategory))
            {
                return false;
            }

            var category = this.articleRepository.GetCategory(new Guid(message.SelectedCategory));
            var patient = this.patientService.Get(message.Id);

            if(patient == null || string.IsNullOrEmpty(message.Name))
            {
                return false;
            }

            var article = new Article
            {
                Name = message.Name,
                Description = message.Description,
                ArticleCategory = category,
                Patient = patient,
                Status = ArticleStatus.NotStarted
            };

            this.articleRepository.Save(article);
            this.auditing.Create(patient, "skapade artikeln {0} ({1})", article.Name, article.Id);

            return true;
        }

        #endregion
    }
}