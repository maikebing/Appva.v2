// <copyright file="ArticleRepository.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Domain.Repositories
{
    #region Imports.

    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface IArticleRepository : 
        IRepository<Article>, 
        ISaveRepository<Article>, 
        IUpdateRepository<Article>
    {
        /// <summary>
        /// Get a category by id.
        /// </summary>
        /// <param name="categoryId">The category <see cref="Guid"/>.</param>
        /// <returns>An <see cref="ArticleCategory"/>.</returns>
        ArticleCategory GetCategory(Guid categoryId);

        /// <summary>
        /// Returns a collection of <see cref="ArticleCategory"/>.
        /// </summary>
        /// <returns>A collection of <see cref="ArticleCategory"/>.</returns>
        IList<ArticleCategory> GetCategories();

        /// <summary>
        /// Returns a collection of ordered <see cref="Article"/>.
        /// </summary>
        /// <param name="patientId">The patient <see cref="Guid"/>.</param>
        /// <param name="categories">A collection of <see cref="ArticleCategory"/>.</param>
        /// <returns>A collection of ordered <see cref="Article"/>.</returns>
        IList<Article> ListByOrderedArticles(Guid patientId, IList<Guid> categories = null);

        /// <summary>
        /// Returns a collection of refilled <see cref="Article"/>.
        /// </summary>
        /// <param name="patientId">The patient <see cref="Guid"/>.</param>
        /// <param name="articleCategoryPermissions">A collection of Guid.</param>
        /// <returns>A collection of <see cref="Article"/>.</returns>
        IList<Article> List(Guid patientId, ArticleStatus? filterBy, IList<Guid> articleCategoryPermissions = null);
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class ArticleRepository : Repository<Article>, IArticleRepository
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IPersistenceContext"/> instance.
        /// </summary>
        private readonly IPersistenceContext persistenceContext;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ArticleRepository"/> class.
        /// </summary>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public ArticleRepository(IPersistenceContext persistenceContext)
            :base(persistenceContext)
        {
            this.persistenceContext = persistenceContext;
        }

        #endregion

        #region IArticleRepository Members.

        public ArticleCategory GetCategory(Guid categoryId)
        {
            return this.persistenceContext.QueryOver<ArticleCategory>()
                .Where(x => x.Id == categoryId)
                    .And(x => x.IsActive == true)
                        .SingleOrDefault();
        }

        /// <inheritdoc />
        public IList<ArticleCategory> GetCategories()
        {
            return this.persistenceContext.QueryOver<ArticleCategory>()
                .Where(x => x.IsActive == true)
                    .OrderBy(x => x.Name).Asc
                        .List();
        }

        /// <inheritdoc />
        public IList<Article> ListByOrderedArticles(Guid patientId, IList<Guid> categories = null)
        {
            var query = this.persistenceContext.QueryOver<Article>()
                .Where(x => x.Patient.Id == patientId)
                    .And(x => x.Status != ArticleStatus.NotStarted)
                        .And(x => x.IsActive == true);

            if(categories != null)
            {
                query.JoinQueryOver(x => x.ArticleCategory)
                    .WhereRestrictionOn(x => x.Id)
                        .IsIn(categories.ToArray());
            }

            return query.List();
        }

        /// <inheritdoc />
        public IList<Article> List(Guid patientId, ArticleStatus? filterBy, IList<Guid> categories = null)
        {
            var query = this.persistenceContext.QueryOver<Article>()
                .Where(x => x.Patient.Id == patientId)
                .And(x => x.IsActive == true);

            if (filterBy.HasValue)
            {
                query.Where(x => x.Status == filterBy.GetValueOrDefault());
            }

            if (categories != null)
            {
                query.JoinQueryOver(x => x.ArticleCategory)
                    .WhereRestrictionOn(x => x.Id)
                        .IsIn(categories.ToArray());
            }

            query = query.OrderBy(x => x.Name).Asc;

            return query.List();
        }

        #endregion
    }
}