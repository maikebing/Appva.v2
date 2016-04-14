// <copyright file="RecountInventoryItemHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class RecountInventoryItemHandler : RequestHandler<RecountInventoryItem, InventoryTransactionItemViewModel>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistence;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="RecountInventoryItemHandler"/> class.
        /// </summary>
        /// <param name="persistence">The <see cref="IPersistenceContext"/></param>
        public RecountInventoryItemHandler(IPersistenceContext persistence)
        {
            this.persistence = persistence;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override InventoryTransactionItemViewModel Handle(RecountInventoryItem message)
        {
            var inventory = this.persistence.Get<Inventory>(message.InventoryId);
            return new InventoryTransactionItemViewModel
            {
                Operation     = "recount",
                InventoryId   = inventory.Id,
                InventoryName = inventory.Description,
                ReturnUrl     = message.ReturnUrl
            };
        }

        #endregion
    }
}