// <copyright file="InventoryRepository.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a></author>
namespace Appva.Mcss.ResourceServer.Domain.Repositories
{
    #region Imports.

    using Appva.Mcss.Domain.Entities;
    using Appva.Persistence;
    using Appva.Repository;

    #endregion

    /// <summary>
    /// The Inventory repository.
    /// </summary>
    public interface IInventoryRepository : IPagingAndSortingRepository<Inventory>
    {
    }

    /// <summary>
    /// Implementation of <see cref="IInventoryRepository"/>.
    /// </summary>
    public class InventoryRepository : PagingAndSortingRepository<Inventory>, IInventoryRepository
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryRepository"/> class.
        /// </summary>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public InventoryRepository(IPersistenceContext persistenceContext)
            : base(persistenceContext)
        {
        }

        #endregion
    }
}