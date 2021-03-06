﻿// <copyright file="AddPatientsHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Area51.Models
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Repositories;
    using Appva.Persistence;
    using System;
    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class AddPatientsHandler : NotificationHandler<AddPatientsNotice>
    {
        #region Fields

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext context;

        /// <summary>
        /// The <see cref="IInventoryRepository"/>
        /// </summary>
        private readonly IInventoryRepository inventories;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AddPatientsHandler"/> class.
        /// </summary>
        /// <param name="context">The <see cref="IPersistenceContext"/></param>
        /// <param name="inventories">The <see cref="IInventoryRepository"/></param>
        public AddPatientsHandler(IPersistenceContext context, IInventoryRepository inventories)
        {
            this.context     = context;
            this.inventories = inventories;
        }

        #endregion

        /// <inheritdoc />
        public override void Handle(AddPatientsNotice notification)
        {
            var sequences = this.context.QueryOver<Sequence>()
                .Where(x => x.Inventory != null).List();
            foreach (var sequence in sequences)
            {
                var inventory     = sequence.Inventory;
                inventory.Patient = sequence.Patient;
                inventory.IsActive = sequence.IsActive;
                if (inventory.LastRecount == null)
                {
                    inventory.LastRecount = inventory.UpdatedAt;
                }
                this.inventories.Update(inventory);
            }

            //// Creates the update-notice
            /*var notice = new DashboardNotification
            {
                IsActive = true,
                IsVisibleToEveryone = true,
                Name = "Nyheter - Saldo",
                Published = true,
                PublishedDate = DateTime.Now.Date,
                Template = "Templates/inventory-news",
                ViewedBy = new List<NotificationViewedBy>(),
                VisibleTo = new List<Account>()
            };

            this.context.Save<DashboardNotification>(notice);*/
        }
    }
}