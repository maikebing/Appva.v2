// <copyright file="ListInventoriesHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Models.Handlers
{
    #region Imports.

    using Application.Common;
    using Application.Services;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Infrastructure.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class ListInventoriesHandler : RequestHandler<Parameterless<ListInventoriesModel>, ListInventoriesModel>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ISettingsService"/>
        /// </summary>
        private readonly ISettingsService settings;

        /// <summary>
        /// The <see cref="ITaxonomyService"/>
        /// </summary>
        private readonly ITaxonomyService taxonomyService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ListInventoriesHandler"/> class.
        /// <param name="settings">The <see cref="ISettingsService"/></param>
        /// <param name="taxonomyService">The <see cref="ITaxonomyService"/></param>
        /// </summary>
        /// <param name="settings">The <see cref="ISettingsService"/></param>
        /// <param name="taxonomyService">The <see cref="ITaxonomyService"/></param>
        public ListInventoriesHandler(ISettingsService settings, ITaxonomyService taxonomyService)
        {
            this.taxonomyService = taxonomyService;
            this.settings = settings;
        }

        #endregion

        /// <inheritdoc />
        public override ListInventoriesModel Handle(Parameterless<ListInventoriesModel> models)
        {
            return new ListInventoriesModel
            {
                Withdrawals = this.taxonomyService.List(TaxonomicSchema.Withdrawal),
                Units = this.settings.GetIventoryAmountLists()
            };
        }
    }
}