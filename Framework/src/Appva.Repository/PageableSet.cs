namespace Appva.Repository
{
    #region Imports.

    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// A collection of pageable entities.
    /// </summary>
    /// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
    /// <typeparam name="TEntity"></typeparam>
    public class PageableSet<TEntity> where TEntity : class
    {
        /// <summary>
        /// The number of items to be returned.
        /// </summary>
        public long PageSize { get; set; }

        /// <summary>
        /// The current page number.
        /// </summary>
        public long CurrentPage { get; set; }

        /// <summary>
        /// The next page number.
        /// </summary>
        public long NextPage { get; set; }

        /// <summary>
        /// The total size of items.
        /// </summary>
        public long TotalCount { get; set; }

        /// <summary>
        /// The filtered set of entities.
        /// </summary>
        public IList<TEntity> Entities { get; set; }
    }
}