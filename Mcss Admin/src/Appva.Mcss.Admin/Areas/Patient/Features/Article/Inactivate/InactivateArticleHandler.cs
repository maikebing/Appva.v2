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
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Areas.Models;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class InactivateArticleHandler : RequestHandler<InactivateArticle, InactivateArticleModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistence;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="InactivateArticleHandler"/> class.
        /// </summary>
        /// <param name="persistence">The <see cref="IPersistenceContext"/>.</param>
        public InactivateArticleHandler(IPersistenceContext persistence)
        {
            this.persistence = persistence;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override InactivateArticleModel Handle(InactivateArticle message)
        {
            var sequence = this.persistence.QueryOver<Sequence>()
                .JoinQueryOver(x => x.Article)
                    .Where(x => x.Id == message.Article)
                        .SingleOrDefault();

            return new InactivateArticleModel
            {
                PatientId = message.Id,
                ArticleId = message.Article,
                SequenceId = sequence == null ? Guid.Empty : sequence.Id,
                SequenceName = sequence == null ? string.Empty : sequence.Name,
                ScheduleListName = sequence == null ? string.Empty : sequence.Schedule.ScheduleSettings.Name
            };
        }

        #endregion
    }
}