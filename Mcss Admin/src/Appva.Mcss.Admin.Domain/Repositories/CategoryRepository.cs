// <copyright file="CategoryRepository.cs" company="Appva AB">
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

    public interface ICategoryRepository : IRepository<Category>
    {
        /// <summary>
        /// Lists the specified category ids.
        /// </summary>
        /// <param name="categoryIds">The category ids.</param>
        /// <returns></returns>
        IList<Category> List(IList<Guid> categoryIds = null);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryRepository"/> class.
        /// </summary>
        public CategoryRepository(IPersistenceContext persistence)
            :base(persistence)
        {
        }

        #endregion

        #region ICategoryRepository members.

        /// <inheritdoc />
        public IList<Category> List(IList<Guid> categoryIds = null)
        {
            var query = this.Context.QueryOver<Category>()
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