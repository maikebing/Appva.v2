// <copyright file="InactivateArticleHandler.cs" company="Appva AB">
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
    public sealed class InactivateArticleHandler : RequestHandler<InactivateArticle, ListArticle>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IArticleRepository"/>.
        /// </summary>
        private readonly IArticleRepository articleRepository;

        private readonly IPatientService patientService;

        /// <summary>
        /// The <see cref="IAuditService"/>.
        /// </summary>
        private readonly IAuditService auditing;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="InactivateArticleHandler"/> class.
        /// </summary>
        /// <param name="articleRepository">The <see cref="IArticleRepository"/>.</param>
        /// <param name="auditing">The <see cref="IAuditService"/>.</param>
        public InactivateArticleHandler(IArticleRepository articleRepository, IPatientService patientService, IAuditService auditing)
        {
            this.articleRepository = articleRepository;
            this.patientService = patientService;
            this.auditing = auditing;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override ListArticle Handle(InactivateArticle message)
        {
            var patient = this.patientService.Get(message.Id);
            var article = this.articleRepository.Get(message.Article);
            article.IsActive = false;
            this.articleRepository.Update(article);
            this.auditing.Delete(patient, "raderade artikeln {0} ({1})", article.Name, article.Id);


            return new ListArticle
            {
                Id = message.Id
            };
        }

        #endregion
    }
}