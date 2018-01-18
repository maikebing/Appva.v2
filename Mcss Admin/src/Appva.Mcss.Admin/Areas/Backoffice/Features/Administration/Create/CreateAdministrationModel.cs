using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Appva.Cqrs;
using Appva.Mcss.Admin.Application.Common;
using Appva.Mcss.Admin.Domain.Entities;
using Appva.Mcss.Admin.Infrastructure.Models;

namespace Appva.Mcss.Admin.Areas.Backoffice.Models
{
    public class CreateAdministrationModel : IRequest<Parameterless<ListInventoriesModel>>
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

        public CreateAdministrationModel()
        {
            this.UnitSelectList = Units.Select(x => new SelectListItem { Text = x.Name, Value = x.Code }).ToList();
        }

        public string Name
        {
            get;
            set;
        }

        public List<SelectListItem> UnitSelectList
        {
            get;
            set;
        }

        public string SelectedUnit
        {
            get;
            set;
        }

        public string SpecificValues
        {
            get;
            set;
        }

        public double? Max
        {
            get;
            set;
        }

        public double? Min
        {
            get;
            set;
        }

        public double? Step
        {
            get;
            set;
        }

        public int? Fractions
        {
            get;
            set;
        }
    }
}