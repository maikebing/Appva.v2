// <copyright file="MigrateArticlesHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Features.Accounts.List
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.VO;
    using Appva.Mcss.Admin.Features.Area51.ArticleOption;
    using Appva.Mcss.Application.Models;
    using Appva.Persistence;
    using NHibernate;
    using System;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class MigrateArticlesHandler : RequestHandler<MigrateArticles, bool>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistence;

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService service;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="MigrateArticlesHandler"/> class.
        /// </summary>
        /// <param name="persistence">The <see cref="IPersistenceContext"/>.</param>
        /// <param name="service">The <see cref="ISettingsService"/>.</param>
        public MigrateArticlesHandler(IPersistenceContext persistence, ISettingsService service)
        {
            this.persistence = persistence;
            this.service = service;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override bool Handle(MigrateArticles message)
        {
            var maxNumberOfRowsPerTransaction = 10000;
            var list = this.persistence.QueryOver<Sequence>()
                .Where(x => x.IsActive == true)
                .And(x => x.Repeat.EndAt >= DateTime.Now || x.Repeat.EndAt == null)
                .And(x => x.Article == null)
                .JoinQueryOver(x => x.Schedule)
                    .JoinQueryOver(x => x.ScheduleSettings)
                        .Where(x => x.OrderRefill == true)
                        .And(x => x.ArticleCategory != null)
                        .Take(maxNumberOfRowsPerTransaction)
                        .List();

            foreach (var sequence in list)
            {
                var article = this.CreateArticle(sequence);
                this.persistence.Save(article);
                sequence.Article = article;
                this.persistence.Update(sequence);
            }

            if(list.Count == maxNumberOfRowsPerTransaction)
            {
                return false;
            }

            var settings = this.service.Find(ApplicationSettings.OrderListSettings);
            this.service.Upsert(ApplicationSettings.OrderListSettings, OrderListConfiguration.CreateNew(true, true));

            return true;
        }

        #endregion

        #region Private methods.

        /// <inheritdoc />
        private Article CreateArticle(Sequence sequence)
        {
            bool refillIsNull = sequence.RefillInfo == null ? true : false;
            var article = new Article
            {
                Name = sequence.Name,
                Description = sequence.Description,
                RefillOrderDate = null,
                RefillOrderedBy = null,
                OrderDate = null,
                OrderedBy = null,
                Status = ArticleStatus.NotStarted,
                Patient = sequence.Patient,
                Category = sequence.Schedule.ScheduleSettings.ArticleCategory
            };

            if (refillIsNull == false)
            {
                if (sequence.RefillInfo.Refill == true)
                {
                    article.Status = ArticleStatus.RefillRequested;
                    article.RefillOrderedBy = sequence.RefillInfo.RefillOrderedBy;
                    article.RefillOrderDate = sequence.RefillInfo.RefillOrderedDate;
                }
                if (sequence.RefillInfo.Ordered == true)
                {
                    article.Status = ArticleStatus.OrderedFromSupplier;
                    article.OrderedBy = sequence.RefillInfo.OrderedBy;
                    article.OrderDate = sequence.RefillInfo.OrderedDate;
                }
            }

            return article;
        }

        #endregion
    }
}