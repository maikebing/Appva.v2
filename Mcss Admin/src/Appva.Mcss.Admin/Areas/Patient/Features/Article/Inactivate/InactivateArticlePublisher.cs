// <copyright file="InactivateArticlePublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Auditing;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Areas.Models;
    using Appva.Mcss.Admin.Domain.Repositories;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class InactivateArticlePublisher : RequestHandler<InactivateArticleModel, bool>
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

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="InactivateArticlePublisher"/> class.
        /// </summary>
        /// <param name="articleRepository">The <see cref="IArticleRepository"/>.</param>
        /// <param name="patientService">The <see cref="IPatientService"/>.</param>
        /// <param name="auditing">The <see cref="IAuditService"/>.</param>
        public InactivateArticlePublisher(IArticleRepository articleRepository, IPatientService patientService, IAuditService auditing)
        {
            this.articleRepository = articleRepository;
            this.patientService = patientService;
            this.auditing = auditing;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override bool Handle(InactivateArticleModel message)
        {
            var patient = this.patientService.Get(message.Id);
            var article = this.articleRepository.Get(message.Article);

            if(article == null || patient == null)
            {
                return false;
            }

            article.IsActive = false;
            this.articleRepository.Update(article);
            this.auditing.Delete(patient, "raderade artikeln {0} ({1})", article.Name, article.Id);

            return true;
        }

        #endregion
    }
}