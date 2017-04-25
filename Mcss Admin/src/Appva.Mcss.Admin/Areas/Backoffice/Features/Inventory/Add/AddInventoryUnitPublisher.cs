// <copyright file="AddInventoryUnitPublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Models.Handlers
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Newtonsoft.Json;

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
        /// <param name="settings">The <see cref="ISettingsService"/></param>
        public AddInventoryUnitPublisher(ISettingsService settings)
        {
            this.settings = settings;
        }

        #endregion

        /// <inheritdoc />
        public override bool Handle(AddInventoryUnitModel message)
        {
            var name = message.Name;
            List<double> amounts = null;

            try
            {
                amounts = JsonConvert.DeserializeObject<List<double>>(string.Format("[{0}]", message.Amounts.Replace(" ", string.Empty)));
            }
            catch
            {
                return false;
            }

            var units = this.settings.Find<List<InventoryAmountListModel>>(ApplicationSettings.InventoryUnitsWithAmounts);
            units.Add(new InventoryAmountListModel
            {
                Id = Guid.NewGuid(),
                Name = name,
                Amounts = amounts
            });

            this.settings.Upsert<List<InventoryAmountListModel>>(ApplicationSettings.InventoryUnitsWithAmounts, units);

            return true;
        }
    }
}