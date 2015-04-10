// <copyright file="IPagingAndSortingRepository.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Repository
{
    /// <summary>
    /// Generic repository.
    /// </summary>
    /// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
    /// <typeparam name="TEntity">The Entity</typeparam>
    public interface IPagingAndSortingRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Returns a collection of <code>TEntity</code>.
        /// </summary>
        /// <param name="pageable">The <see cref="Pagable"/> entity</param>
        /// <returns>The <see cref="PageableSet"/></returns>
        PageableSet<TEntity> List(Pageable<TEntity> pageable);

        /// <summary>
        /// Returns a collection of <code>TEntity</code>.
        /// </summary>
        /// <param name="searchable">The <see cref="Searchable"/> entity</param>
        /// <returns>The <see cref="PageableSet"/></returns>
        PageableSet<TEntity> Search(Searchable<TEntity> searchable);
    }
}
