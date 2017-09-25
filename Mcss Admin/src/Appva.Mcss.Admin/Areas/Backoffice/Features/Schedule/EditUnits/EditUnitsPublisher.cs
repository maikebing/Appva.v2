// <copyright file="EditUnitsPublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>

namespace Appva.Mcss.Admin.Areas.Backoffice.Handlers
{
    #region Imports.

    using System.Collections.Generic;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using Appva.Mcss.Admin.Domain.Models;
    using Appva.Mcss.Admin.Domain.VO;
    using System.Globalization;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Models;
    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class EditUnitsPublisher : RequestHandler<EditUnitsModel, bool>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService service;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="EditUnitsPublisher"/> class.
        /// </summary>
        /// <param name="service">The <see cref="ISettingsService"/>.</param>
        public EditUnitsPublisher(ISettingsService service)
        {
            this.service = service;
        }

        #endregion

        #region RequestHandler overrides

        /// <inheritdoc />
        public override bool Handle(EditUnitsModel message)
        {
            var list = new List<InventoryAmountListModel>();

            //foreach (var item in message.Dosages)
            //{
            //    if(string.IsNullOrWhiteSpace(item.Name) || 
            //       string.IsNullOrWhiteSpace(item.Unit) ||
            //       string.IsNullOrWhiteSpace(item.Values))
            //    {
            //        return false;
            //    }

            //    var array = item.Values.Split(',');

            //    foreach (var value in array)
            //    {
            //        double result = 0;
            //        if(double.TryParse(value.Trim(), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out result) == false)
            //        {
            //            return false;
            //        }
            //    }

            //    list.Add(new InventoryAmountListModel
            //    {
            //        Id = item.Id,
            //        Name = item.Name,
            //        Unit = item.Unit,
            //        Amounts = item.Values
            //    });
            //}

            //this.service.Upsert(ApplicationSettings.InventoryUnitsWithAmounts, InventoryDefaults.CreateNew(list));

            return true;
        }

        #endregion
    }
}