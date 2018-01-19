// <copyright file="CreateAdministrationPublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Models.Handlers
{
    #region Imports.

    using System.Collections.Generic;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Newtonsoft.Json;
    using Validation;

    #endregion

    /// <summary>
    /// The create administration publisher.
    /// </summary>
    public class CreateAdministrationPublisher : RequestHandler<CreateAdministrationModel, Parameterless<ListInventoriesModel>>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ISettingsService"/>
        /// </summary>
        private readonly ISettingsService settingsService;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateAdministrationPublisher"/> class.
        /// </summary>
        /// <param name="settingsService">The<see cref="ISettingsService"/>.</param>
        public CreateAdministrationPublisher(ISettingsService settingsService)
        {
            this.settingsService = settingsService;
        }

        #endregion

        #region RequestHandlers Overrides.

        /// <inheritdoc />
        public override Parameterless<ListInventoriesModel> Handle(CreateAdministrationModel message)
        {
            var units = this.settingsService.Find(ApplicationSettings.AdministrationUnitsWithAmounts);
            var unit = UnitOfMeasurement.Parse(message.SelectedUnit);
            var name = message.Name;
            if (message.IsCustomList == true)
            {
                var specificValues = JsonConvert.DeserializeObject<List<double>>(string.Format("[{0}]", message.SpecificValues.Replace(" ", "")));
                units.Add(new AdministrationValueModel(name, unit, specificValues));
            }
            else
            {
                Requires.ValidState(message.Max.HasValue && message.Max.Value > 0, "Max");
                var max = message.Max.Value;
                var min = message.Min.HasValue ? message.Min.Value : 0;
                var step = message.Step.HasValue ? message.Step.Value : 1;
                var fractions = message.Fractions.HasValue ? message.Fractions.Value : 0;
                units.Add(new AdministrationValueModel(name, unit, max, min, step, fractions));
            }
            this.settingsService.Upsert(ApplicationSettings.AdministrationUnitsWithAmounts, units);
            return new Parameterless<ListInventoriesModel>();
        }

        #endregion
    }
}