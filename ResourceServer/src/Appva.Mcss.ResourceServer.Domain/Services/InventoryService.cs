// <copyright file="InventoryService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.ResourceServer.Domain.Services
{
    #region Imports.

    using Appva.Core.Extensions;
    using Appva.Mcss.Domain.Entities;
    using Appva.Mcss.ResourceServer.Domain.Repositories;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface IInventoryService
    {
        /// <summary>
        /// TODO: Summary.
        /// </summary>
        /// <param name="task">TODO: task</param>
        /// <param name="account">TODO: account</param>
        /// <returns>TODO: returns</returns>
        InventoryTransactionItem RedoInventoryWithdrawal(Task task, Account account);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal class InventoryService : IService, IInventoryService
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IInventoryRepository"/>.
        /// </summary>
        private readonly IInventoryRepository inventoryRepository;

        /// <summary>
        /// The <see cref="IInventoryTransactionItemRepository"/>.
        /// </summary>
        private readonly IInventoryTransactionItemRepository inventoryTransactionRepository;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryService"/> class.
        /// </summary>
        /// <param name="inventoryRepository">The <see cref="IInventoryRepository"/></param>
        /// <param name="inventoryTransactionRepository">The <see cref="IInventoryTransactionItemRepository"/></param>
        public InventoryService(IInventoryRepository inventoryRepository, IInventoryTransactionItemRepository inventoryTransactionRepository)
        {
            this.inventoryRepository = inventoryRepository;
            this.inventoryTransactionRepository = inventoryTransactionRepository;
        }

        #endregion

        #region IInventoryService Members.

        /// <inheritdoc />
        public InventoryTransactionItem RedoInventoryWithdrawal(Task task, Account account)
        {
            if (task.InventoryTransactions.IsEmpty())
            {
                return null;
            }
            var sequence = task.Sequence;
            var inventory = sequence.Inventory;
            double redoValue = 0;
            foreach (var t in task.InventoryTransactions)
            {
                if (t.Operation.Equals("withdrawal"))
                {
                    redoValue += t.Value;
                }
            }
            var item = new InventoryTransactionItem
            {
                Account = account,
                CurrentInventoryValue = inventory.CurrentLevel + redoValue,
                PreviousInventoryValue = inventory.CurrentLevel,
                Operation = "readd", 
                Value = redoValue,
                Sequence = sequence,
                Task = task,
                Inventory = inventory,
                Description = string.Format("Återförde saldo efter ångrad signering {0:yyyy-MM-dd HH:mm}", task.Scheduled)
            };
            this.inventoryTransactionRepository.Save(item);
            inventory.CurrentLevel = item.CurrentInventoryValue;
            inventory.Transactions.Add(item);
            this.inventoryRepository.Update(inventory);
            return item;
        }

        #endregion
    }
}