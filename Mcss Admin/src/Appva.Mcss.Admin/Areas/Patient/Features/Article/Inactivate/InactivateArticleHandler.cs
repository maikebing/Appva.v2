// <copyright file="InactivateArticleHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using System;
    using System.Linq;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Areas.Models;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Persistence;
    using Appva.Mcss.Admin.Application.Services;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class InactivateArticleHandler : RequestHandler<InactivateArticle, InactivateArticleModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="ISequenceService"/>.
        /// </summary>
        private readonly ISequenceService sequenceService;

        /// <summary>
        /// The <see cref="IArticleService"/>.
        /// </summary>
        private readonly IArticleService articleService;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="InactivateArticleHandler"/> class.
        /// </summary>
        /// <param name="persistence">The <see cref="IPersistenceContext"/>.</param>
        public InactivateArticleHandler(
            ISequenceService sequenceService,
            IArticleService articleService)
        {
            this.sequenceService = sequenceService;
            this.articleService  = articleService;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override InactivateArticleModel Handle(InactivateArticle message)
        {
            return new InactivateArticleModel
            {
                PatientId = message.Id,
                ArticleId = message.Article,
                Sequences = this.sequenceService.ListByArticle(message.Article).ToList(),
                Article   = this.articleService.Find(message.Article)
            };
        }

        #endregion
    }
}