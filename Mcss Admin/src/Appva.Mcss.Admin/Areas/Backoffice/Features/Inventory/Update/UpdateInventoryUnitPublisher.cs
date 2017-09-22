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
    using Appva.Persistence;
    using Newtonsoft.Json;
    using Appva.Mcss.Admin.Domain.Entities;

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

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateInventoryUnitPublisher"/> class.
        /// </summary>
        public UpdateInventoryUnitPublisher(ISettingsService settings, IPersistenceContext persistence)
        {
            this.settings = settings;
            this.persistence = persistence;
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

            var query = this.persistence.QueryOver<Sequence>()
                .Where(x => x.IsActive == true)
                    .JoinQueryOver(x => x.DosageObservation)
                        .Where(x => x.DosageScaleId == setting.Id)
                            .List();

            var amountToJson = JsonConvert.SerializeObject(setting.Amounts);

            foreach (var row in query)
            {
                row.DosageObservation.DosageScaleValues = setting.Name;
                row.DosageObservation.DosageScaleUnit = setting.Unit;
                row.DosageObservation.DosageScaleValues = amountToJson;
                this.persistence.Update(row);
            }

            return new Parameterless<ListInventoriesModel>();
        }

        #endregion
    }
}