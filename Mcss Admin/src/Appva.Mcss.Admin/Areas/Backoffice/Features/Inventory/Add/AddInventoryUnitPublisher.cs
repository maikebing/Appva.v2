// <copyright file="AddInventoryPublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Models.Handlers
{
    #region Imports.

    using System.Collections.Generic;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Areas.Area51.Models;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Persistence;
    using Newtonsoft.Json;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Application.Models;
    using System;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class AddInventoryUnitPublisher : RequestHandler<AddInventoryUnitModel, bool>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ISettingsService"/>
        /// </summary>
        private readonly ISettingsService settings;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AddInventoryUnitPublisher"/> class.
        /// </summary>
        /// <param name="persistence">The <see cref="IPersistenceContext"/></param>
        public AddInventoryUnitPublisher(ISettingsService settings)
        {
            this.settings = settings;
        }

        #endregion

        /// <inheritdoc />
        public override bool Handle(AddInventoryUnitModel message)
        {
            var name    = message.Name;
            var amounts = JsonConvert.DeserializeObject<List<double>>(string.Format("[{0}]", message.Amounts.Replace(" ","")));

            var units = this.settings.Find<List<InventoryAmountListModel>>(ApplicationSettings.InventoryUnitsWithAmounts);
            units.Add(new InventoryAmountListModel
            {
                Id = Guid.NewGuid(),
                Name = name,
                Field = message.Field,
                Unit = string.IsNullOrWhiteSpace(message.Unit) ? null : message.Unit,
                Amounts = amounts
            });

            this.settings.Upsert<List<InventoryAmountListModel>>(ApplicationSettings.InventoryUnitsWithAmounts, units);

            return true;
        }
    }
}