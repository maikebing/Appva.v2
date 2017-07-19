// <copyright file="MigrateArticlesHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Features.Accounts.List
{
    #region Imports.

    using System;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Features.Area51.ArticleOption;
    using Appva.Mcss.Application.Models;
    using Appva.Persistence;
    using NHibernate;

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

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="MigrateArticlesHandler"/> class.
        /// </summary>
        /// <param name="persistence">The <see cref="IPersistenceContext"/>.</param>
        public MigrateArticlesHandler(IPersistenceContext persistence)
        {
            this.persistence = persistence;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override bool Handle(MigrateArticles message)
        {
            var s = new System.Diagnostics.Stopwatch();
            s.Start();

            IQueryOver<Sequence, ScheduleSettings> query = null;
            int rowCount = 0;
            do
            {
                var session = this.persistence.Session.SessionFactory.OpenSession();
                using (var transaction = session.BeginTransaction())
                {
                    query = session.QueryOver<Sequence>()
                        .Where(x => x.Article == null)
                            .JoinQueryOver(x => x.Schedule)
                                .JoinQueryOver(x => x.ScheduleSettings)
                                    .Where(x => x.OrderRefill == true)
                                        .And(x => x.ArticleCategory != null);

                    rowCount = query.RowCount();
                    if (rowCount > 0)
                    {
                        var list = query.Take(10000).List();
                        foreach (var sequence in list)
                        {
                            var article = this.CreateArticle(sequence);
                            session.Save(article);
                            sequence.Article = article;
                            session.Update(sequence);
                        }

                        transaction.Commit();
                    }
                }
            }
            while (rowCount > 0);

            s.Stop();
            var result = string.Format("Time elapsed: {0}:{1} (m:s)", System.Math.Floor(s.Elapsed.TotalMinutes), s.Elapsed.ToString("ss\\.ff"));

            return false;
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
                Refill = false,
                RefillOrderDate = null,
                RefillOrderedBy = null,
                Ordered = true,
                OrderDate = null,
                OrderedBy = null,
                Status = ArticleStatus.Refilled.ToString(),
                Patient = sequence.Patient,
                ArticleCategory = sequence.Schedule.ScheduleSettings.ArticleCategory
            };

            if (refillIsNull == false)
            {
                if (sequence.RefillInfo.Refill == true)
                {
                    article.Refill = true;
                    article.Ordered = false;
                    article.Status = ArticleStatus.NotStarted.ToString();
                    article.RefillOrderedBy = sequence.RefillInfo.RefillOrderedBy;
                    article.RefillOrderDate = sequence.RefillInfo.RefillOrderedDate;
                }
                else if (sequence.RefillInfo.Ordered == true)
                {
                    article.OrderedBy = sequence.RefillInfo.OrderedBy;
                    article.OrderDate = sequence.RefillInfo.OrderedDate;
                }
            }

            return article;
        }

        #endregion
    }
}