// <copyright file="AddArticlePublisher.cs" company="Appva AB">
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
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Domain.Repositories;
    using Appva.Mcss.Admin.Domain.Entities;

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

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="AddArticlePublisher"/> class.
        /// <param name="articleRepository">The <see cref="IArticleRepository"/>.</param>
        /// <param name="patientService">The <see cref="IPatientService"/>.</param>
        /// </summary>
        public AddArticlePublisher(IArticleRepository articleRepository, IPatientService patientService)
        {
            this.articleRepository = articleRepository;
            this.patientService = patientService;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override bool Handle(AddArticleModel message)
        {
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
                Patient = patient
            };

            this.articleRepository.Save(article);

            return true;
        }

        #endregion
    }
}