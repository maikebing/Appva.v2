namespace Appva.Repository
{

    /// <summary>
    /// Generic repository.
    /// </summary>
    /// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
    /// <typeparam name="TEntity"></typeparam>
    public interface IPagingAndSortingRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {

        /// <summary>
        /// Returns a collection of <code>TEntity</code>.
        /// </summary>
        /// <param name="pageable"></param>
        /// <returns></returns>
        PageableSet<TEntity> List(Pageable<TEntity> pageable);

        /// <summary>
        /// Returns a collection of <code>TEntity</code>.
        /// </summary>
        /// <param name="searchable"></param>
        /// <returns></returns>
        PageableSet<TEntity> Search(Searchable<TEntity> searchable);

    }

}
