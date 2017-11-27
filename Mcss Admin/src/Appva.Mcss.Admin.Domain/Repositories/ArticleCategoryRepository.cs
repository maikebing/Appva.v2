// <copyright file="ArticleCategoryRepository.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Repositories
{
    #region Imports.

    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Persistence;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    public interface IArticleCategoryRepository : IRepository<ArticleCategory>
    {
        /// <summary>
        /// Lists the specified category ids.
        /// </summary>
        /// <param name="categoryIds">The category ids.</param>
        /// <returns></returns>
        IList<ArticleCategory> List(IList<Guid> categoryIds = null);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class ArticleCategoryRepository : Repository<ArticleCategory>, IArticleCategoryRepository
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ArticleCategoryRepository"/> class.
        /// </summary>
        public ArticleCategoryRepository(IPersistenceContext persistence)
            :base(persistence)
        {
        }

        #endregion

        #region IArticleCategoryRepository members.

        /// <inheritdoc />
        public IList<ArticleCategory> List(IList<Guid> categoryIds = null)
        {
            var query = this.Context.QueryOver<ArticleCategory>()
                .Where(x => x.IsActive == true);

            if(categoryIds != null)
            {
                query.WhereRestrictionOn(x => x.Id).IsIn(categoryIds.ToArray());
            }

            return query.List();
        }

        #endregion
    }
}