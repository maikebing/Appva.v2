// <copyright file="CreateInventoryTransactionItemHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using System;
    using Appva.Core.Extensions;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class CreateInventoryTransactionItemHandler : RequestHandler<CreateInventoryTransactionItem, bool>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IIdentityService"/>.
        /// </summary>
        private readonly IIdentityService identityService;

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistence;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateInventoryTransactionItemHandler"/> class.
        /// </summary>
        /// <param name="identityService">The <see cref="IIdentityService"/></param>
        /// <param name="persistence">The <see cref="IPersistenceContext"/></param>
        public CreateInventoryTransactionItemHandler(IIdentityService identityService, IPersistenceContext persistence)
        {
            this.identityService = identityService;
            this.persistence     = persistence;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override bool Handle(CreateInventoryTransactionItem message)
        {
            var inventory = this.persistence.Get<Inventory>(message.InventoryId);
            var previousInventoryLevel = inventory.CurrentLevel;
            switch (message.Operation)
            {
                case "add":
                    inventory.CurrentLevel += Math.Round(Convert.ToDouble(message.Value), 4);
                    break;
                case "recount":
                    inventory.CurrentLevel = Math.Round(Convert.ToDouble(message.Value), 4);
                    inventory.LastRecount = DateTime.Now;
                    break;
                case "withdrawal":
                    inventory.CurrentLevel = inventory.CurrentLevel - Math.Round(Convert.ToDouble(message.Value), 4);
                    break;
                default:
                    break;
            }
            var user = this.persistence.Get<Account>(this.identityService.PrincipalId);
            var transaction = new InventoryTransactionItem
            {
                Account                = user,
                Description            = message.Description,
                Operation              = message.Operation,
                Value                  = Math.Round(Convert.ToDouble(message.Value), 4),
                Sequence               = message.SequenceId != Guid.Empty ? this.persistence.Get<Sequence>(message.SequenceId) : null,
                Task                   = message.TaskId.IsEmpty() ? null : this.persistence.Get<Task>(message.TaskId),
                Inventory              = inventory,
                CurrentInventoryValue  = inventory.CurrentLevel,
                PreviousInventoryValue = previousInventoryLevel
            };
            this.persistence.Save(transaction);
            inventory.Transactions.Add(transaction);
            this.persistence.Update(inventory);
            return true;
        }

        #endregion
    }
}