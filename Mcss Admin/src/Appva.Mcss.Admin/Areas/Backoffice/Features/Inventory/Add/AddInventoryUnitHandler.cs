// <copyright file="AddInventoryHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Models.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class AddInventoryUnitHandler : RequestHandler<Parameterless<AddInventoryUnitModel>, AddInventoryUnitModel>
    {
        /// <inheritdoc />
        public override AddInventoryUnitModel Handle(Parameterless<AddInventoryUnitModel> message)
        {
            return new AddInventoryUnitModel
            {
                SelectField = new List<SelectListItem> {
                    new SelectListItem { Text = "Inventarier", Value = InventoryDefaults.Feature.inventory.ToString() },
                    new SelectListItem { Text = "Dosering", Value = InventoryDefaults.Feature.dosage.ToString() },
                    //new SelectListItem { Text = "Mätning", Value = InventoryDefaults.Feature.measurement.ToString() }
                },
                Amounts = JsonConvert.SerializeObject(InventoryDefaults.AmountList).Replace("[","").Replace("]","")
            };
        }
    }
}