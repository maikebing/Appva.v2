namespace Appva.Mcss.Admin.Areas.Backoffice.Models.Handlers
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Appva.Core.Extensions;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Newtonsoft.Json;

    #endregion

    public class CreateAdministrationPublisher : RequestHandler<CreateAdministrationModel, Parameterless<ListInventoriesModel>>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ISettingsService"/>
        /// </summary>
        private readonly ISettingsService settingsService;

        #endregion

        #region Constructors.

        public CreateAdministrationPublisher(ISettingsService settingsService)
        {
            this.settingsService = settingsService;
        }

        #endregion

        #region RequestHandlers Overrides.

        /// <inheritdoc />
        public override Parameterless<ListInventoriesModel> Handle(CreateAdministrationModel message)
        {
            var units = this.settingsService.Find<List<AdministrationAmountModel>>(ApplicationSettings.AdministrationUnitsWithAmounts);
            if (string.IsNullOrWhiteSpace(message.SpecificValues) && message.Max != null)
            {
                var max = message.Max;
                var min = message.Min.HasValue ? message.Min.Value : 0;
                var step = message.Step.HasValue ? message.Step.Value : 1;
                var fractions = message.Fractions.HasValue ? message.Fractions.Value : 0;
                units.Add(new AdministrationAmountModel(message.Name, UnitOfMeasurement.Parse(message.SelectedUnit), max.Value, min, fractions, step));
            }
            if (!string.IsNullOrWhiteSpace(message.SpecificValues) && message.Max == null)
            {
                var specificValues = JsonConvert.DeserializeObject<List<double>>(string.Format("[{0}]", message.SpecificValues.Replace(" ", "")));
                units.Add(new AdministrationAmountModel(message.Name, UnitOfMeasurement.Parse(message.SelectedUnit), specificValues));
            }
            this.settingsService.Upsert<List<AdministrationAmountModel>>(ApplicationSettings.AdministrationUnitsWithAmounts, units);
            return new Parameterless<ListInventoriesModel>();
        }

        #endregion
    }
}