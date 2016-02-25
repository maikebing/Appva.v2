// <copyright file="CreateInventoryPublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Core.Extensions;
    using Appva.Mcss.Admin.Application.Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class CreateInventoryPublisher : RequestHandler<CreateInventoryModel, ListInventory>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IInventoryService"/>
        /// </summary>
        private readonly IInventoryService inventories;

        /// <summary>
        /// The <see cref="IPatientService"/>
        /// </summary>
        private readonly IPatientService patients;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateInventoryPublisher"/> class.
        /// </summary>
        public CreateInventoryPublisher(IInventoryService inventories, IPatientService patients)
        {
            this.inventories = inventories;
            this.patients = patients;
        }

        #endregion

        #region RequestHandler Overrides

        /// <inheritdoc />
        public override ListInventory Handle(CreateInventoryModel message)
        {
            var amounts = message.Amounts.IsNotEmpty() ? message.Amounts.Split(';').ToList() : null;
            var patient = this.patients.Load(message.Id);
            var inventory = this.inventories.Create(message.Name, message.Unit, amounts, patient);
            return new ListInventory
            {
                Id = patient.Id,
                InventoryId = inventory
            };
        }

        #endregion
    }
}