﻿// <copyright file="CreateInventoryHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using System.Linq;
    using System.Web.Mvc;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services.Settings;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class CreateInventoryHandler : RequestHandler<Identity<CreateInventoryModel>, CreateInventoryModel>
    {
        #region Variabels.

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService settings;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateInventoryHandler"/> class.
        /// </summary>
        /// <param name="settings">The <see cref="ISettingsService"/></param>
        public CreateInventoryHandler(ISettingsService settings)
        {
            this.settings = settings;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override CreateInventoryModel Handle(Identity<CreateInventoryModel> message)
        {
            return new CreateInventoryModel
            {
                Id          = message.Id,
                AmountsList = this.settings.GetIventoryAmountLists().Select(x => new SelectListItem { Text = x.Name, Value = x.Name })
            };
        }

        #endregion
    }
}