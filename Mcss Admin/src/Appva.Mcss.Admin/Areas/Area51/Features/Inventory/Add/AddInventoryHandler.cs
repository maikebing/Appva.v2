// <copyright file="AddInventoryHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Area51.Models.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class AddInventoryHandler : RequestHandler<Parameterless<AddInventoryModel>, AddInventoryModel>
    {
        /// <inheritdoc />
        public override AddInventoryModel Handle(Parameterless<AddInventoryModel> message)
        {
            return new AddInventoryModel
            {
                Amounts = JsonConvert.SerializeObject(SettingsService.InventoryAmountDefaults)
            };
        }
    }
}