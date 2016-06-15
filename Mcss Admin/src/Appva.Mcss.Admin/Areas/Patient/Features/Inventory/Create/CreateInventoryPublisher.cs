// <copyright file="CreateInventoryPublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using System.Linq;
    using Appva.Core.Extensions;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class CreateInventoryPublisher : RequestHandler<CreateInventoryModel, ListInventory>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IInventoryService"/>.
        /// </summary>
        private readonly IInventoryService inventoryService;

        /// <summary>
        /// The <see cref="IPatientService"/>.
        /// </summary>
        private readonly IPatientService patientService;

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService settingService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateInventoryPublisher"/> class.
        /// </summary>
        /// <param name="inventoryService">The <see cref="IInventoryService"/></param>
        /// <param name="patientService">The <see cref="IPatientService"/></param>
        /// <param name="settingService">The <see cref="ISettingsService"/></param>
        public CreateInventoryPublisher(IInventoryService inventoryService, IPatientService patientService, ISettingsService settingService)
        {
            this.inventoryService = inventoryService;
            this.patientService   = patientService;
            this.settingService   = settingService;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override ListInventory Handle(CreateInventoryModel message)
        {
            var amounts   = message.Amounts.IsNotEmpty() ? this.settingService.GetIventoryAmountLists().Where(x => x.Name == message.Amounts).FirstOrDefault() : new InventoryAmountListModel();
            var patient   = this.patientService.Load(message.Id);
            var inventory = this.inventoryService.Create(message.Name, amounts.Name, amounts.Amounts, patient);
            return new ListInventory
            {
                Id          = patient.Id,
                InventoryId = inventory
            };
        }

        #endregion
    }
}