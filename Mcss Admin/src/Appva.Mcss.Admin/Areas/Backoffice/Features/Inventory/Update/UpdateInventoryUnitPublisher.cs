// <copyright file="UpdateInventoryUnitPublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Handlers
{
    #region Imports.

    using System.Collections.Generic;
    using System.Linq;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Newtonsoft.Json;

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
        /// <param name="settings">The <see cref="ISettingsService"/></param>
        public UpdateInventoryUnitPublisher(ISettingsService settings)
        {
            this.settings = settings;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override Parameterless<ListInventoriesModel> Handle(UpdateInventoryUnitModel message)
        {
            var settings = this.settings.Find<List<InventoryAmountListModel>>(ApplicationSettings.InventoryUnitsWithAmounts);
            var setting  = settings.SingleOrDefault(x => x.Id == message.Id);
            List<double> amounts = null;

            try
            {
                amounts = JsonConvert.DeserializeObject<List<double>>(string.Format("[{0}]", message.Amounts.Replace(" ", string.Empty)));
            }
            catch{}
            
            setting.Name    = string.IsNullOrEmpty(message.Name) ? setting.Name : message.Name;
            setting.Amounts = amounts != null ? amounts : setting.Amounts;

            this.settings.Upsert<List<InventoryAmountListModel>>(ApplicationSettings.InventoryUnitsWithAmounts, settings);

            return new Parameterless<ListInventoriesModel>();
        }

        #endregion
    }
}