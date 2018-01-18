using System;
using System.Collections.Generic;
using System.Linq;
using Appva.Core.Extensions;
using Appva.Mcss.Admin.Domain.Entities;
using Newtonsoft.Json;
using Validation;

namespace Appva.Mcss.Admin.Application.Models
{
    [JsonObject]
    public sealed class AdministrationAmountModel
    {
        public AdministrationAmountModel(string name, UnitOfMeasurement unit, double max, double min = 0, int fractions = 0, double step = 1)
        {
            Requires.NotNullOrWhiteSpace(name, "name");
            Requires.NotNull(unit, "unit");
            Requires.ValidState(max >= 0, "max");
            Requires.ValidState(min >= 0 && min <= max, "min");
            Requires.ValidState(fractions <= 2, "decimals");
            Requires.ValidState(step >= 0.01, "step");
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.Unit = unit;
            this.Max = max;
            this.Min = min;
            this.Fractions = fractions;
            this.Step = step;
            this.SpecificValues = null;
        }

        public AdministrationAmountModel(string name, UnitOfMeasurement unit, IList<double> values)
        {
            Requires.NotNullOrWhiteSpace(name, "name");
            Requires.NotNull(unit, "unit");
            Requires.NotNullOrEmpty(values, "values");
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.Unit = unit;
            this.SpecificValues = values;
            this.Max = null;
            this.Min = null;
            this.Step = null;
            this.Fractions = null;
        }

        public AdministrationAmountModel()
        {
        }

        [JsonProperty("Id")]
        public Guid Id
        {
            get;
            set;
        }

        [JsonProperty("Name")]
        public string Name
        {
            get;
            set;
        }

        [JsonProperty("Unit")]
        public UnitOfMeasurement Unit
        {
            get;
            set;
        }

        [JsonProperty("Max")]
        public double? Max
        {
            get;
            set;
        }

        [JsonProperty("Min")]
        public double? Min
        {
            get;
            set;
        }

        [JsonProperty("Fractions")]
        public int? Fractions
        {
            get;
            set;
        }

        [JsonProperty("Step")]
        public double? Step
        {
            get;
            set;
        }

        [JsonProperty("SpecificValues")]
        public IList<double> SpecificValues
        {
            get;
            set;
        }

        public string ToLongString()
        {
            if (this.SpecificValues != null || this.SpecificValues.IsNotEmpty())
            {
                return string.Format("{0} ({1}-{2} {3})", this.Name, this.SpecificValues.Max(), this.SpecificValues.Min(), this.Unit.Code);
            }
            if (this.SpecificValues == null || this.SpecificValues.IsEmpty())
            {
                return string.Format("{0} ({1}-{2} {3})", this.Name, this.Max, this.Min, this.Unit.Code);
            }
            return null;
        }
    }
}
