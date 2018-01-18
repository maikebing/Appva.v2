namespace Appva.Mcss.Admin.Areas.Backoffice.Models.Handlers
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Domain.Entities;

    #endregion

    public class UpdateAdministrationHandler : RequestHandler<UpdateAdministration, UpdateAdministrationModel>
    {
        #region Variables.

        private readonly IReadOnlyList<UnitOfMeasurement> Units = new List<UnitOfMeasurement>
        {
            MassUnits.Kilogram,
            MassUnits.Hektogram,
            MassUnits.Gram,
            MassUnits.Milligram,
            VolumeUnits.Liter,
            VolumeUnits.Deciliter,
            VolumeUnits.Centiliter,
            VolumeUnits.Milliliter,
            NonUnits.Tablets
        };

        private readonly ISettingsService settingsService;

        #endregion

        #region Constructors.

        public UpdateAdministrationHandler(ISettingsService settingsService)
        {
            this.settingsService = settingsService;
        }

        #endregion

        #region RequestHandler Overrides.

        public override UpdateAdministrationModel Handle(UpdateAdministration message)
        {
            var administration = this.settingsService.Find<List<AdministrationValueModel>>(ApplicationSettings.AdministrationUnitsWithAmounts)
                .SingleOrDefault(x => x.Id == message.Id);
            return UpdateAdministrationModel.New(administration);
        }

        #endregion
    }
}