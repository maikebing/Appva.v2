// <copyright file="InventoryTransactionItemRepository.cs" company="Appva AB">
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
    /// The InventoryTransactionItem repository.
    /// </summary>
    public interface IInventoryTransactionItemRepository : IPagingAndSortingRepository<InventoryTransactionItem>
    {
        /// <summary>
        /// TODO: Add a descriptive summary to increase readability.
        /// </summary>
        /// <param name="item">TODO: item</param>
        /// <param name="type">TODO: type</param>
        /// <returns>TODO: returns</returns>
        InventoryTransactionItem GetLastActiveTransaction(InventoryTransactionItem item, string type = null);
    }

    /// <summary>
    /// Implementation of <see cref="IInventoryTransactionItemRepository"/>.
    /// </summary>
    public class InventoryTransactionItemRepository : PagingAndSortingRepository<InventoryTransactionItem>, IInventoryTransactionItemRepository
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryTransactionItemRepository"/> class.
        /// </summary>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public InventoryTransactionItemRepository(IPersistenceContext persistenceContext)
            : base(persistenceContext)
        {
        }

        #endregion

        #region IInventoryTransactionItemRepository Members.

        /// <inheritdoc />
        public InventoryTransactionItem GetLastActiveTransaction(InventoryTransactionItem item, string type = null)
        {
            var previous = this.Where(x => x.Active)
                .And(x => x.Inventory == item.Inventory)
                .And(x => x.Id != item.Id);
            if (type != null)
            {
                previous.Where(x => x.Operation == type);
            }
            return previous.OrderBy(x => x.Created).Asc.SingleOrDefault();
        }
        
        #endregion
    }
}