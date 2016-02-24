﻿// <copyright file="InventoryService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Services
{
    #region Imports.

    using Appva.Mcss.Admin.Application.Auditing;
using Appva.Mcss.Admin.Application.Security.Identity;
using Appva.Mcss.Admin.Domain.Entities;
using Appva.Mcss.Admin.Domain.Repositories;
using Appva.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

    #endregion

    public interface IInventoryService : IService
    {
        /// <summary>
        /// Finds the inventory with given id
        /// </summary>
        /// <param name="id">The Id</param>
        /// <returns><see cref="Inventory"/></returns>
        Inventory Find(Guid id);

        /// <summary>
        /// Updates the inventory
        /// </summary>
        /// <param name="id">The inventory id</param>
        /// <param name="name">The updated name of the inventory</param>
        /// <param name="unit">The inventory unit</param>
        /// <param name="amounts">The available amounts</param>
        void Update(Guid id, string name, string unit, IList<string> amounts);

        /// <summary>
        /// Returns all inventories which belongs to a patient
        /// </summary>
        /// <param name="patientId">The patient Id</param>
        /// <param name="active">Get only active/inactive inventories</param>
        /// <returns>List of <see cref="Inventory"/></returns>
        IList<Inventory> Search(Guid patientId, bool? active = null);

        /// <summary>
        /// Lists all transcations for an inventory
        /// </summary>
        /// <param name="inventory">Id of the <see cref="Inventory"/></param>
        /// <param name="fromDate">Filter from date</param>
        /// <param name="toDate">Filter to date</param>
        /// <param name="page">Current page</param>
        /// <param name="page">Current page siźe</param>
        /// <returns>List of <see cref="Inventory"/></returns>
        PageableSet<InventoryTransactionItem> ListTransactionsFor(Guid inventory, DateTime? fromDate = null, DateTime? toDate = null, int page = 0, int pageSize = 10);

        /// <summary>
        /// Inactivates the inventory
        /// </summary>
        /// <param name="inventory">The <see cref="Inventory"/></param>
        void Inactivate(Inventory inventory);

        /// <summary>
        /// Reacivates the inventory
        /// </summary>
        /// <param name="inventory">The <see cref="Inventory"/></param>
        void Reactivate(Inventory inventory);

        /// <summary>
        /// Creates a new inventory
        /// </summary>
        /// <param name="name">The inventory name</param>
        /// <param name="unit">The inventory unit</param>
        /// <param name="amounts">The available amounts</param>
        /// <param name="patient">The inventory patient</param>
        /// <returns>Id of created inventory</returns>
        Guid Create(string name, string unit, IList<string> amounts, Patient patient);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class InventoryService : IInventoryService
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IInventoryRepository"/>
        /// </summary>
        private readonly IInventoryRepository repository;

        /// <summary>
        /// The <see cref="IIdentityService"/>.
        /// </summary>
        private readonly IIdentityService identity;

        /// <summary>
        /// The <see cref="IAuditService"/>.
        /// </summary>
        private readonly IAuditService audit;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryService"/> class.
        /// </summary>
        public InventoryService(IInventoryRepository repository, IIdentityService identity, IAuditService audit)
        {
            this.repository = repository;
            this.identity = identity;
            this.audit = audit;
        }

        #endregion

        #region IInventoryService Members

        /// <inheritdoc />
        public Inventory Find(Guid id)
        {
            return this.repository.Find(id);
        }

        /// <inheritdoc />
        public void Update(Guid id, string name, string unit, IList<string> amounts)
        {
            var inventory = this.repository.Find(id);

            inventory.Description = name;
            inventory.Amounts = amounts;
            inventory.Unit = unit;

            this.repository.Update(inventory);

            this.audit.Update("{0} uppdaterade saldo {1} (ref. {2})", this.identity.Principal.Identity.Name, name, id);
        }

        /// <inheritdoc />
        public IList<Inventory> Search(Guid patientId, bool? active = null)
        {
            var scheduleSettings = this.identity.SchedulePermissions().Select(x => new Guid(x.Value)).ToList();
            return this.repository.Search(patientId, scheduleSettings, active);
        }

        public PageableSet<InventoryTransactionItem> ListTransactionsFor(Guid inventory, DateTime? fromDate = null, DateTime? toDate = null, int page = 0, int pageSize = 10)
        {
            return this.repository.ListTransactionsFor(inventory, fromDate, toDate, page, pageSize);
        }

        /// <inheritdoc />
        public void Inactivate(Inventory inventory)
        {
            inventory.IsActive = false;
            this.repository.Update(inventory);
            this.audit.Delete("{0} inaktiverade saldo {1} (ref. {2})", this.identity.Principal.Identity.Name, inventory.Description, inventory.Id);
        }

        /// <inheritdoc />
        public void Reactivate(Inventory inventory)
        {
            inventory.IsActive = true;
            this.repository.Update(inventory);
            this.audit.Delete("{0} återaktiverade saldo {1} (ref. {2})", this.identity.Principal.Identity.Name, inventory.Description, inventory.Id);
        }

        /// <inheritdoc />
        public Guid Create(string name, string unit, IList<string> amounts, Patient patient)
        {
            var inventory = new Inventory
            {
                Description = name,
                Amounts = amounts,
                Patient = patient,
                Unit = unit
            };
            var id = this.repository.Save(inventory);

            this.audit.Create("{0} skapade saldot {1} (ref. {2})", this.identity.Principal.Identity.Name, inventory.Description, id);

            return id;
        }

        #endregion
    }
}