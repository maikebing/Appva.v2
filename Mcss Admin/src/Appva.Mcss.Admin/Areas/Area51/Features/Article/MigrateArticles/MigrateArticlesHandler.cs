// <copyright file="MigrateArticlesHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Features.Accounts.List
{
    #region Imports.

    using System.Linq;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Features.Area51.ArticleOption;
    using Appva.Persistence;

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
            //var s = new System.Diagnostics.Stopwatch();
            //s.Start();

            /*
            foreach (var setting in scheduleSettings)
            {
                using (var transaction = persistence.BeginTransaction())
                {
                    var category = new ArticleCategory();
                    category.Name = setting.Name;
                    this.persistence.Save(category);

                    var schedules = this.persistence.QueryOver<Schedule>()
                        .Where(x => x.ScheduleSettings == setting)
                            .List();

                    foreach (var schedule in schedules)
                    {
                        if (schedule.ArticleCategory == null)
                        {
                            schedule.ArticleCategory = category;
                            this.persistence.Update(schedule);
                        }
                    }

                    transaction.Commit();
                }
            }
            */

            //s.Stop();
            //var result = string.Format("Time elapsed: {0}:{1}, memory usage: {2} KB", System.Math.Floor(s.Elapsed.TotalMinutes), s.Elapsed.ToString("ss\\.ff"), System.GC.GetTotalMemory(false) / 1024);

            return false;
        }

        #endregion
    }
}