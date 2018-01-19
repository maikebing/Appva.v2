// <copyright file="AdministrationValues.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using System.Collections.Generic;
    using System.Linq;
    using Appva.Mcss.Admin.Application.Models;
    using Newtonsoft.Json;
    using Validation;

    #endregion

    [JsonObject]
    public class AdministrationValues
    {
        #region Constructors.

        public AdministrationValues(UnitOfMeasurement unit, double max, double min = 0, double step = 1, int fractions = 0)
        {
            this.Unit = unit;
            this.CustomFormula = new CustomFormula(max, min, step, fractions);
        }

        public AdministrationValues(UnitOfMeasurement unit, IEnumerable<double> specificValues)
        {
            this.Unit = unit;
            this.SpecificValues = new CustomList(specificValues);
        }

        public AdministrationValues(UnitOfMeasurement unit, CustomFormula formula)
        {
            this.Unit = unit;
            this.CustomFormula = formula;
        }

        public AdministrationValues(UnitOfMeasurement unit, CustomList specificValue)
        {
            this.Unit = unit;
            this.SpecificValues = specificValue;
        }

        protected internal AdministrationValues()
        {
        }


        #endregion

        #region Properties.

        /// <summary>
        /// The <see cref="UnitOfMeasurement"/>.
        /// </summary>
        [JsonProperty("Unit")]
        public virtual UnitOfMeasurement Unit
        {
            get;
            set;
        }

        /// <summary>
        /// The <see cref="CustomList"/>.
        /// </summary>
        [JsonProperty("CustomValues")]
        public virtual CustomList SpecificValues
        {
            get;
            set;
        }

        /// <summary>
        /// The <see cref="CustomFormula"/>.
        /// </summary>
        [JsonProperty("CustomFormula")]
        public virtual CustomFormula CustomFormula
        {
            get;
            set;
        }

        #endregion

        #region Members.

        public virtual void Update(double max, double min = 0, double step = 1, int fractions = 0, UnitOfMeasurement unit = null)
        {
            this.CustomFormula = new CustomFormula(max, min, step, fractions);
            this.Unit = unit ?? this.Unit;
            this.SpecificValues = null;
        }

        public virtual void Update(IList<double> specificValues, UnitOfMeasurement unit = null)
        {
            this.SpecificValues = new CustomList(specificValues);
            this.Unit = unit ?? this.Unit;
            this.CustomFormula = null;
        }

        public virtual string ToLongString()
        {
            if (this.SpecificValues != null)
            {
                return string.Format("{0}-{1}{2}", this.SpecificValues.Values.Min(), this.SpecificValues.Values.Max(), this.Unit.Code);
            }
            return string.Format("{0}-{1}{2}", this.CustomFormula.Min, this.CustomFormula.Max, this.Unit.Code);
        }

        internal virtual bool Validate(double value)
        {
            if (this.SpecificValues == null)
            {
                return (value >= this.CustomFormula.Min && value <= this.CustomFormula.Max);
            }
            return this.SpecificValues.Values.Any(x => x == value);
        }

        #endregion
    }

    [JsonObject]
    public class CustomList
    {
        public CustomList(IEnumerable<double> specificValues)
        {
            this.Values = specificValues;
        }
        protected internal CustomList()
        {
        }

        [JsonProperty("ValueList")]
        private string values;
        public virtual IEnumerable<double> Values
        {
            get
            {
                return JsonConvert.DeserializeObject<IEnumerable<double>>(this.values);
            }
            set
            {
                this.values = JsonConvert.SerializeObject(value);
            }
        }

        public virtual void Udpate(IEnumerable<double> specificValues)
        {
            this.Values = specificValues;
        }
    }

    [JsonObject]
    public class CustomFormula
    {
        public CustomFormula(double max, double min = 0, double step = 1, int fractions = 0)
        {
            Requires.ValidState(max >= min, "max");
            this.Max = max;
            this.Min = min;
            this.Step = step;
            this.Fractions = fractions;
        }

        protected internal CustomFormula()
        {
        }

        [JsonProperty("Max")]
        public virtual double Max { get; set; }
        [JsonProperty("Min")]
        public virtual double Min { get; set; }
        [JsonProperty("Step")]
        public virtual double Step { get; set; }
        [JsonProperty("Fractions")]
        public virtual int Fractions { get; set; }

        public virtual void Update(double max, double min = 0, double step = 1, int fractions = 0)
        {
            Requires.ValidState(max >= min, "max");
            this.Max = max;
            this.Min = min;
            this.Step = step;
            this.Fractions = fractions;
        }
    }
}
