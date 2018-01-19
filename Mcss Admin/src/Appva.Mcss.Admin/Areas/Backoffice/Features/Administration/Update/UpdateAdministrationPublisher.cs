// <copyright file="UpdateAdministrationPublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Models.Handlers
{
    #region Imports.

    using System.Collections.Generic;
    using System.Linq;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// The update administration publisher.
    /// </summary>
    public class UpdateAdministrationPublisher : RequestHandler<UpdateAdministrationModel, Parameterless<ListInventoriesModel>>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService settingsService;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateAdministrationPublisher"/> class.
        /// </summary>
        /// <param name="settingsService"><see cref="ISettingsService"/>.</param>
        public UpdateAdministrationPublisher(ISettingsService settingsService)
        {
            this.settingsService = settingsService;
        }

        #endregion

        #region Requesthandler overrides.

        /// <inheritdoc />
        public override Parameterless<ListInventoriesModel> Handle(UpdateAdministrationModel message)
        {
            var settings = this.settingsService.Find(ApplicationSettings.AdministrationUnitsWithAmounts);                
            var administration = settings.SingleOrDefault(x => x.Id == message.Id);
            var unit = administration.CustomValues.Unit;
            var name = message.Name;
            if (message.IsCustomList == true)
            {
                var values = new AdministrationValues(unit, new CustomList(JsonConvert.DeserializeObject<List<double>>(string.Format("[{0}]", message.SpecificValues.Replace(" ", ""))).ToList()));
                administration.Update(name, values);
            }
            else
            {
                var max = message.Max.Value;
                var min = message.Min.HasValue ? message.Min.Value : 0;
                var step = message.Step.HasValue ? message.Step.Value : 1;
                var fractions = message.Fractions.HasValue ? message.Fractions.Value : 0;
                var values = new AdministrationValues(unit, new CustomFormula(max, min, step, fractions));
                administration.Update(name, values);
            }
            this.settingsService.Upsert<List<AdministrationValueModel>>(ApplicationSettings.AdministrationUnitsWithAmounts, settings);
            return new Parameterless<ListInventoriesModel>();
        }

        #endregion
    }
}