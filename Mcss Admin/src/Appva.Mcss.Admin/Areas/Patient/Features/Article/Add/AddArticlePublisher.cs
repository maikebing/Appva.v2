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
        /// The <see cref="IArticleService"/>.
        /// </summary>
        private readonly IArticleService articleService;

        /// <summary>
        /// The <see cref="ICategoryService"/>
        /// </summary>
        private readonly ICategoryService categoryService;

        /// <summary>
        /// The <see cref="IPatientService"/>.
        /// </summary>
        private readonly IPatientService patientService;


        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="AddArticlePublisher"/> class.
        /// </summary>
        /// <param name="categoryService">The category service.</param>
        /// <param name="articleService">The article service.</param>
        /// <param name="patientService">The patient service.</param>
        public AddArticlePublisher(
            ICategoryService categoryService,
            IArticleService articleService,
            IPatientService patientService)
        {
            this.categoryService = categoryService;
            this.articleService  = articleService;
            this.patientService  = patientService;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override bool Handle(AddArticleModel message)
        {
            var category = this.categoryService.Find(new Guid(message.SelectedCategory));
            var patient  = this.patientService.Get(message.Id);

            this.articleService.CreateArticle(message.Name, message.Description, patient, category);

            return true;
        }

        #endregion
    }
}