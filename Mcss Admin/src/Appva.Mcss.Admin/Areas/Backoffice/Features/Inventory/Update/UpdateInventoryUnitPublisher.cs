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
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Appva.Persistence;
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

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistence;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateInventoryUnitPublisher"/> class.
        /// </summary>
        public UpdateInventoryUnitPublisher(ISettingsService settings, IPersistenceContext persistence)
        {
            this.settings = settings;
            this.persistence = persistence;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override Parameterless<ListInventoriesModel> Handle(UpdateInventoryUnitModel message)
        {
            var settings = this.settings.Find<List<InventoryAmountListModel>>(ApplicationSettings.InventoryUnitsWithAmounts);
            var setting  = settings.SingleOrDefault(x => x.Id == message.Id);

            setting.Name = message.Name;
            setting.Feature = message.Feature;
            setting.Unit = string.IsNullOrWhiteSpace(message.Unit) ? null : message.Unit;
            setting.Amounts = JsonConvert.DeserializeObject<List<double>>(string.Format("[{0}]", message.Amounts.Replace(" ", "")));

            this.settings.Upsert<List<InventoryAmountListModel>>(ApplicationSettings.InventoryUnitsWithAmounts, settings);

            var dosageQuery = this.persistence.QueryOver<DosageObservation>()
                .Where(x => x.IsActive == true)
                  .And(x => string.IsNullOrEmpty(x.Scale))
                .List();

            foreach (var row in dosageQuery)
            {
                var scale = JsonConvert.DeserializeObject<InventoryAmountListModel>(row.Scale);
                if (setting.Id == scale.Id)
                {
                    //// UNRESOLVED: Change me!!
                    /*row.Scale = JsonConvert.SerializeObject(setting);
                    this.persistence.Update<DosageObservation>(row);*/
                }
            }

            return new Parameterless<ListInventoriesModel>();
        }

        #endregion
    }
}