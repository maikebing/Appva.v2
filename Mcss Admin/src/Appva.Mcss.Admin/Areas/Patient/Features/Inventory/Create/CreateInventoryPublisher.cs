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
    using Appva.Mcss.Admin.Application.Services.Settings;
using Appva.Mcss.Admin.Application.Models;

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

        /// <summary>
        /// The <see cref="ISettingsService"/>
        /// </summary>
        private readonly ISettingsService settings;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateInventoryPublisher"/> class.
        /// </summary>
        public CreateInventoryPublisher(IInventoryService inventories, IPatientService patients, ISettingsService settings)
        {
            this.inventories = inventories;
            this.patients = patients;
            this.settings = settings;
        }

        #endregion

        #region RequestHandler Overrides

        /// <inheritdoc />
        public override ListInventory Handle(CreateInventoryModel message)
        {
            var amounts = message.Amounts.IsNotEmpty() ? this.settings.GetIventoryAmountLists().Where(x => x.Name == message.Amounts).FirstOrDefault() : new InventoryAmountListModel();
            var patient = this.patients.Load(message.Id);
            var inventory = this.inventories.Create(message.Name, amounts.Name, amounts.Amounts, patient);
            return new ListInventory
            {
                Id = patient.Id,
                InventoryId = inventory
            };
        }

        #endregion
    }
}