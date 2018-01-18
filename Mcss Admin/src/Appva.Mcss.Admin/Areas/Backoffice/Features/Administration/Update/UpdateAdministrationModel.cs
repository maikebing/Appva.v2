using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Appva.Core.Extensions;
using Appva.Cqrs;
using Appva.Mcss.Admin.Application.Common;
using Appva.Mcss.Admin.Application.Models;
using Appva.Mcss.Admin.Domain.Entities;
using Appva.Mcss.Admin.Infrastructure.Models;
using Newtonsoft.Json;

namespace Appva.Mcss.Admin.Areas.Backoffice.Models
{
    public class UpdateAdministrationModel : IRequest<Parameterless<ListInventoriesModel>>
    {
        #region Constructors.

        public UpdateAdministrationModel()
        {
        }

        #endregion

        public Guid Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public UnitOfMeasurement Unit
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

        public string SpecificValues
        {
            get;
            set;
        }

        public static UpdateAdministrationModel New(AdministrationValueModel model)
        {
            var retval = new UpdateAdministrationModel();
            retval.Id = model.Id;
            retval.Name = model.Name;
            retval.Unit = model.CustomValues.Unit;

            if (model.CustomValues.SpecificValues != null)
            {
                retval.SpecificValues = JsonConvert.SerializeObject(model.CustomValues.SpecificValues.Values).Replace("[", "").Replace("]", "");
            }
            else
            {
                retval.Max = model.CustomValues.CustomFormula.Max;
                retval.Min = model.CustomValues.CustomFormula.Min;
                retval.Step = model.CustomValues.CustomFormula.Step;
                retval.Fractions = model.CustomValues.CustomFormula.Fractions;
            }
            return retval;
        }
    }
}