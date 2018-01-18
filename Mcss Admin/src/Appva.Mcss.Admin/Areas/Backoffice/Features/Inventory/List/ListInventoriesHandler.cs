// <copyright file="ListInventoriesHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Models.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Infrastructure.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class ListInventoriesHandler : RequestHandler<Parameterless<ListInventoriesModel>, ListInventoriesModel>
    {
        #region Variables

        /// <summary>
        /// The <see cref="ISettingsService"/>
        /// </summary>
        private readonly ISettingsService settings;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ListInventoriesHandler"/> class.
        /// </summary>
        public ListInventoriesHandler(ISettingsService settings)
        {
            this.settings = settings;
        }

        #endregion

        /// <inheritdoc />
        public override ListInventoriesModel Handle(Parameterless<ListInventoriesModel> models)
        {
            return new ListInventoriesModel
            {
                InventoryUnits = this.settings.GetIventoryAmountLists(),
                AdministrationUnits = this.settings.GetAdministrationAmountModelList()
            };
        }
    }
}