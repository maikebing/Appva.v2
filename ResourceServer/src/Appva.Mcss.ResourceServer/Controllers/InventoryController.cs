// <copyright file="InventoryController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.ResourceServer.Controllers
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Web.Http;
    using Appva.Core.Extensions;
    using Appva.Mcss.Domain.Entities;
    using Appva.Mcss.ResourceServer.Application;
    using Appva.Mcss.ResourceServer.Application.Authorization;
    using Appva.Mcss.ResourceServer.Domain.Repositories;
    using Appva.WebApi.Filters;
    using Models;

    #endregion

    /// <summary>
    /// Inventory endpoint.
    /// </summary>
    [RoutePrefix("v1/inventory")]
    public class InventoryController : ApiController
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ISequenceRepository"/>.
        /// </summary>
        private readonly ISequenceRepository sequenceRepository;

        /// <summary>
        /// The <see cref="IInventoryRepository"/>.
        /// </summary>
        private readonly IInventoryRepository inventoryRepository;

        /// <summary>
        /// The <see cref="IInventoryTransactionItemRepository"/>.
        /// </summary>
        private readonly IInventoryTransactionItemRepository inventoryTransactionItemRepository;

        /// <summary>
        /// The <see cref="IAccountRepository"/>.
        /// </summary>
        private readonly IAccountRepository accountRepository;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryController"/> class.
        /// </summary>
        /// <param name="sequenceRepository">The <see cref="ISequenceRepository"/></param>
        /// <param name="inventoryRepository">The <see cref="IInventoryRepository"/></param>
        /// <param name="inventoryTransactionItemRepository">The <see cref="IInventoryTransactionItemRepository"/></param>
        /// <param name="accountRepository">The <see cref="IAccountRepository"/></param>
        public InventoryController(
            ISequenceRepository sequenceRepository, 
            IInventoryRepository inventoryRepository, 
            IInventoryTransactionItemRepository inventoryTransactionItemRepository,
            IAccountRepository accountRepository)
        {
            this.sequenceRepository = sequenceRepository;
            this.inventoryRepository = inventoryRepository;
            this.inventoryTransactionItemRepository = inventoryTransactionItemRepository;
            this.accountRepository = accountRepository;
        }

        #endregion

        #region Routes.

        /// <summary>
        /// Creates an inventory-transaction
        /// </summary>
        /// <param name="model">The model</param>
        /// <returns>TODO: Return value</returns>
        [AuthorizeToken(Scope.ReadWrite)]
        [HttpPost, Validate, Route]
        public IHttpActionResult Create(InventoryTransactionModel model)
        {
            var inventory = this.inventoryRepository.Get(model.InventoryId);
            if (inventory.IsNull())
            {
                return this.NotFound();
            }
            var sequence = this.sequenceRepository.Get(model.SequenceId);
            if (sequence.IsNull())
            {
                return this.NotFound();
            }
            var prevInventoryValue = inventory.CurrentLevel;
            switch (model.Type) 
            {
                case "add":
                    inventory.CurrentLevel += model.Amount;
                    break;
                case "recount":
                    inventory.CurrentLevel = model.Amount;
                    inventory.LastRecount = DateTime.Now;
                    break;
                case "withdrawal":
                    inventory.CurrentLevel = inventory.CurrentLevel - model.Amount;
                    break;
                default:
                    break;
            }
            var transaction = new InventoryTransactionItem
            { 
                Description = model.Reason,
                Inventory = inventory,
                Operation = model.Type,
                Sequence = sequence,
                Value = model.Amount,
                CurrentInventoryValue = inventory.CurrentLevel,
                PreviousInventoryValue = prevInventoryValue                    
            };
            transaction.Account = this.accountRepository.Get(this.User.Identity.Id());
            this.inventoryTransactionItemRepository.Save(transaction);
            inventory.Transactions.Add(transaction);
            this.inventoryRepository.Update(inventory);
            //// TODO: Semantically incorrect HTTP response, should be 201 Created.
            return this.Ok(new { id = transaction.Id });            
        }

        /// <summary>
        /// OBS. Structural. Updates an inventory-transaction
        /// TODO: Transaction cannot be updated from clients for the moment. Therefore not implemented.
        /// </summary>
        /// <param name="id">TODO: id</param>
        /// <returns>TODO: returns</returns>
        [AuthorizeToken(Scope.ReadWrite)]
        [HttpPut, Route("{id:guid}")]
        public IHttpActionResult Update(Guid id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Removes an inventory transaction
        /// </summary>
        /// <param name="id">TODO: id</param>
        /// <returns>TODO: returns</returns>
        [AuthorizeToken(Scope.ReadWrite)]
        [HttpPut, Route("{id:guid}/delete")]
        public IHttpActionResult Delete(Guid id)
        {
            var transaction = this.inventoryTransactionItemRepository.Get(id);
            if (transaction.IsNull())
            {
                return this.NotFound();
            }
            if (!transaction.Active)
            {
                //// TODO: Should not return 200 ok. Probably 500 or something, needs to be fixed in app. Can't delete an deleted inventory-transaction
                return this.Ok();
            }
            transaction.Active = false;
            switch (transaction.Operation)
            {
                case "add":
                    transaction.Inventory.CurrentLevel -= transaction.Value;
                    break;
                case "recount":
                    var previousTransaction = this.inventoryTransactionItemRepository.GetLastActiveTransaction(transaction);
                    if (previousTransaction.Operation != "recount")
                    {
                        var previousRecount = this.inventoryTransactionItemRepository.GetLastActiveTransaction(transaction, "recount"); 
                    }   
                    transaction.Inventory.CurrentLevel = transaction.Value;
                    transaction.Inventory.LastRecount = DateTime.Now;
                    break;
                case "withdrawal":
                    transaction.Inventory.CurrentLevel += transaction.Value;
                    break;
                default:
                    break;
            }
            //// TODO: 200 is success? 
            return this.Ok(new { success = true });
        }

        /// <summary>
        /// TODO: ??? Lists inventory-transactions depending on optional parameters
        /// </summary>
        /// <param name="inventoryId">TODO: inventoryId</param>
        /// <param name="taskId">TODO: taskId</param>
        /// <param name="sequenceId">TODO: sequenceId</param>
        /// <returns>TODO: returns</returns>
        [AuthorizeToken(Scope.ReadWrite)]
        [HttpGet, Route("list")]
        public IHttpActionResult List([FromUri(Name="inventory_id")] Guid? inventoryId = null, [FromUri(Name="task_id")] Guid? taskId = null, [FromUri(Name="sequence_id")] Guid? sequenceId = null)
        {
            var transactions = new List<Guid> { Guid.NewGuid() };
            return this.Ok(transactions);
        }

        #endregion
    }
}