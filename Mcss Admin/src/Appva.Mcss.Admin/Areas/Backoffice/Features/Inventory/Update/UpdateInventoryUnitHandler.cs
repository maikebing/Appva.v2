// <copyright file="UpdateInventoryUnitHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Handlers
{
    #region Imports.

    using Appva.Core.Extensions;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using Appva.Mcss.Admin.Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class UpdateInventoryUnitHandler : RequestHandler<Identity<UpdateInventoryUnitModel>, UpdateInventoryUnitModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService settings;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateInventoryUnitHandler"/> class.
        /// </summary>
        public UpdateInventoryUnitHandler(ISettingsService settings)
        {
            this.settings = settings;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override UpdateInventoryUnitModel Handle(Identity<UpdateInventoryUnitModel> message)
        {
            var setting = this.settings.Find<List<InventoryAmountListModel>>(ApplicationSettings.InventoryUnitsWithAmounts)
                .SingleOrDefault(x => x.Id == message.Id);
            if(setting.IsNull())
            {
                return new UpdateInventoryUnitModel();
            }
            return new UpdateInventoryUnitModel() {
                Amounts = JsonConvert.SerializeObject(setting.Amounts).Replace("[", "").Replace("]", ""),
                Name    = setting.Name,
                Id      = setting.Id
            };
        }

        #endregion
    }
}