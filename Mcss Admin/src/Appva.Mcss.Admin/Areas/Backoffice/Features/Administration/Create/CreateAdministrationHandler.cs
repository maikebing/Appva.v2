using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Appva.Cqrs;
using Appva.Mcss.Admin.Application.Common;
using Appva.Mcss.Admin.Application.Services.Settings;
using Appva.Mcss.Admin.Domain.Entities;

namespace Appva.Mcss.Admin.Areas.Backoffice.Models.Handlers
{
    #region Imports.


    #endregion

    internal sealed class CreateAdministrationHandler : RequestHandler<CreateAdministration, CreateAdministrationModel>
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

        #endregion

        #region Constructors.

        public CreateAdministrationHandler()
        {
        }

        #endregion

        #region RequestHandler Overrides.

        public override CreateAdministrationModel Handle(CreateAdministration message)
        {
            var model = new CreateAdministrationModel();
            return model;
        }

        #endregion
    }
}