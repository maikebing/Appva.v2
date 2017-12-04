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
    using NHibernate.Criterion;

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
        /// <returns>An <see cref="Category"/>.</returns>
        Category GetCategory(Guid categoryId);

        /// <summary>
        /// Returns a collection of <see cref="Category"/>.
        /// </summary>
        /// <returns>A collection of <see cref="Category"/>.</returns>
        IList<Category> GetCategories();

        /// <summary>
        /// Returns a collection of refilled <see cref="Article"/>.
        /// </summary>
        /// <param name="patientId">The patient <see cref="Guid"/>.</param>
        /// <param name="categoryPermissions">A collection of Guid.</param>
        /// <returns>A collection of <see cref="Article"/>.</returns>
        IList<Article> List(Guid patientId, ArticleStatus? filterBy, IList<Guid> categoryPermissions = null);

        /// <summary>
        /// Searches the specified taxon filter.
        /// </summary>
        /// <param name="taxonFilter">The taxon filter.</param>
        /// <param name="statuses">The statuses.</param>
        /// <param name="categoryPermissions">The article category permissions.</param>
        /// <returns></returns>
        IList<Article> Search(string taxonFilter, IList<ArticleStatus> statuses = null, IList<Guid> categoryPermissions = null);
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

        public Category GetCategory(Guid categoryId)
        {
            return this.persistenceContext.QueryOver<Category>()
                .Where(x => x.Id == categoryId)
                    .And(x => x.IsActive == true)
                        .SingleOrDefault();
        }

        /// <inheritdoc />
        public IList<Category> GetCategories()
        {
            return this.persistenceContext.QueryOver<Category>()
                .Where(x => x.IsActive == true)
                    .OrderBy(x => x.Name).Asc
                        .List();
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

            

            query = query.OrderBy(x => x.Name).Asc;

            return query.List();
        }

        /// <inheritdoc />
        public IList<Article> Search(string taxonFilter, IList<ArticleStatus> statuses = null, IList<Guid> categoryPermissions = null)
        {
            var query = this.persistenceContext.QueryOver<Article>()
                .Where(x => x.IsActive == true);

            //// Filter by permissions to categories
            if (categoryPermissions != null)
            {
                query.JoinQueryOver(x => x.Category)
                    .WhereRestrictionOn(x => x.Id)
                        .IsIn(categoryPermissions.ToArray());
            }

            //// Filter by statuses
            if (statuses != null)
            {
                query.WhereRestrictionOn(x => x.Status).IsIn(statuses.ToArray());
            }

            //// Filter by org-taxon
            query.JoinQueryOver(x => x.Patient)
                    .JoinQueryOver(x => x.Taxon)
                    .WhereRestrictionOn(x => x.Path).IsLike(taxonFilter, MatchMode.Start);

            return query.List();
        }

        #endregion
    }
}