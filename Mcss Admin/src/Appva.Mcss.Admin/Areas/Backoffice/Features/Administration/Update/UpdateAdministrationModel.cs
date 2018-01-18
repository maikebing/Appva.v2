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

        public UpdateAdministrationModel(AdministrationAmountModel model)
        {
            this.Id = model.Id;
            this.Name = model.Name;
            this.Unit = model.Unit;
            this.Max = model.Max;
            this.Min = model.Min;
            this.Step = model.Step;
            this.Fractions = model.Fractions;            
            this.SpecificValues = (model.SpecificValues.IsNotNull() || model.SpecificValues.IsNotEmpty()) ? JsonConvert.SerializeObject(model.SpecificValues).Replace("[", "").Replace("]", "") : string.Empty;
        }

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
    }
}