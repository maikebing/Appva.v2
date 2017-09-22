// <copyright file="UpdateInventoryUnitPublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class UpdateInventoryUnitPublisher : RequestHandler<UpdateInventoryUnitModel, Parameterless<ListInventoriesModel>>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="ISettingsService"/>
        /// </summary>
        private readonly ISettingsService settings;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateInventoryUnitPublisher"/> class.
        /// </summary>
        public UpdateInventoryUnitPublisher(ISettingsService settings)
        {
            this.settings = settings;
        }

        #endregion

        #region RequestHandler Overrides

        /// <inheritdoc />
        public override Parameterless<ListInventoriesModel> Handle(UpdateInventoryUnitModel message)
        {
            var settings = this.settings.Find<List<InventoryAmountListModel>>(ApplicationSettings.InventoryUnitsWithAmounts);
            var setting  = settings.SingleOrDefault(x => x.Id == message.Id);
            
            setting.Name    = message.Name;
            setting.Unit = string.IsNullOrWhiteSpace(message.Unit) ? null : message.Unit;
            setting.Amounts = JsonConvert.DeserializeObject<List<double>>(string.Format("[{0}]", message.Amounts.Replace(" ", "")));

            this.settings.Upsert<List<InventoryAmountListModel>>(ApplicationSettings.InventoryUnitsWithAmounts, settings);

            return new Parameterless<ListInventoriesModel>();
        }

        #endregion
    }
}