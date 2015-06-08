// <copyright file="CreateInventoryItemHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Core.Extensions;
    using Appva.Core.Utilities;
    using Appva.Mcss.Admin.Commands;
    using Appva.Persistence;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Infrastructure;
    using Appva.Mcss.Admin.Application.Security.Identity;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class CreateInventoryItemHandler : RequestHandler<CreateInventoryItem, bool>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPatientService"/>.
        /// </summary>
        private readonly IPatientService patientService;

        /// <summary>
        /// The <see cref="ITaskService"/>.
        /// </summary>
        private readonly ITaskService taskService;

        /// <summary>
        /// The <see cref="ILogService"/>.
        /// </summary>
        private readonly ILogService logService;

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistence;

        /// <summary>
        /// The <see cref="IPatientTransformer"/>.
        /// </summary>
        private readonly IPatientTransformer transformer;

        /// <summary>
        /// The <see cref="IIdentityService"/>.
        /// </summary>
        private readonly IIdentityService identity;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateInventoryItemHandler"/> class.
        /// </summary>
        /// <param name="settings">The <see cref="IPatientService"/> implementation</param>
        /// <param name="settings">The <see cref="ITaskService"/> implementation</param>
        /// <param name="settings">The <see cref="ILogService"/> implementation</param>
        public CreateInventoryItemHandler(
            IPatientService patientService, ITaskService taskService, ILogService logService, IPersistenceContext persistence,
            IPatientTransformer transformer, IIdentityService identity)
        {
            this.patientService = patientService;
            this.taskService = taskService;
            this.logService = logService;
            this.persistence = persistence;
            this.transformer = transformer;
            this.identity = identity;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override bool Handle(CreateInventoryItem message)
        {
            var inventory = this.persistence.Get<Inventory>(message.InventoryId);
            var previousInventoryLevel = inventory.CurrentLevel;
            switch (message.Operation)
            {
                case "add":
                    inventory.CurrentLevel += message.Value;
                    break;
                case "recount":
                    inventory.CurrentLevel = message.Value;
                    inventory.LastRecount = DateTime.Now;
                    break;
                case "withdrawal":
                    inventory.CurrentLevel = inventory.CurrentLevel - message.Value;
                    break;
                default:
                    break;
            }
            var user = this.persistence.Get<Account>(this.identity.PrincipalId);
            var transaction = new InventoryTransactionItem
            {
                Account = user,
                Description = message.Description,
                Operation = message.Operation,
                Value = message.Value,
                Sequence = message.SequenceId != Guid.Empty ? this.persistence.Get<Sequence>(message.SequenceId) : null,
                Task = message.TaskId.IsEmpty() ? null : this.persistence.Get<Task>(message.TaskId),
                Inventory = inventory,
                CurrentInventoryValue = inventory.CurrentLevel,
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