// <copyright file="DeleteInventoryUnitHandler.cs" company="Appva AB">
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
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Appva.Mcss.Admin.Models;
    using Appva.Persistence;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class DeleteInventoryUnitHandler : RequestHandler<Identity<Parameterless<ListInventoriesModel>>, Parameterless<ListInventoriesModel>>
    {
        #region Fields. 

        /// <summary>
        /// The <see cref="IsettingsService"/>
        /// </summary>
        private readonly ISettingsService settings;

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistence;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteInventoryUnitHandler"/> class.
        /// </summary>
        public DeleteInventoryUnitHandler(ISettingsService settings, IPersistenceContext persistence)
        {
            this.settings = settings;
            this.persistence = persistence;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override Parameterless<ListInventoriesModel> Handle(Identity<Parameterless<ListInventoriesModel>> message)
        {
            var settings = this.settings.Find<List<InventoryAmountListModel>>(ApplicationSettings.InventoryUnitsWithAmounts);
            var setting = settings.SingleOrDefault(x => x.Id == message.Id);

            settings.Remove(setting);

            this.settings.Upsert<List<InventoryAmountListModel>>(ApplicationSettings.InventoryUnitsWithAmounts, settings);


            //// Do we really want to remove the scale setting for each related signlist? Answer is no. Will save this snippet in case.
            /*
            var query = this.persistence.QueryOver<Sequence>()
                .Where(x => x.IsActive == true)
                .JoinQueryOver(x => x.DosageObservation)
                .Where(x => x.DosageScaleId == setting.Id)
                .List();

            var amountToJson = JsonConvert.SerializeObject(setting.Amounts);

            foreach (var row in query)
            {
                row.DosageObservation.DosageScaleId = Guid.Empty;
                row.DosageObservation.Name = string.Empty;
                row.DosageObservation.DosageScaleUnit = string.Empty;
                row.DosageObservation.DosageScaleValues = string.Empty;
                this.persistence.Update(row);
            }
            */
            return new Parameterless<ListInventoriesModel>();
        }

        #endregion
    }
}