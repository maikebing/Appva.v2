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
    public sealed class AdministrationValueModel
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="AdministrationValueModel"/> class.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="unit"></param>
        /// <param name="max"></param>
        /// <param name="min"></param>
        /// <param name="step"></param>
        /// <param name="fractions"></param>
        public AdministrationValueModel(string name, UnitOfMeasurement unit, double max, double min = 0, double step = 1, int fractions = 0)
        {
            Requires.NotNullOrWhiteSpace(name, "name");
            Requires.NotNull(unit, "unit");
            Requires.ValidState(max >= 0, "max");
            Requires.ValidState(min >= 0 && min <= max, "min");
            Requires.ValidState(fractions <= 2, "decimals");
            Requires.ValidState(step >= 0.01, "step");
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.CustomValues = new AdministrationValues(unit, max, min, step, fractions);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdministrationValueModel"/> class.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="unit"></param>
        /// <param name="values"></param>
        public AdministrationValueModel(string name, UnitOfMeasurement unit, IList<double> values)
        {
            Requires.NotNullOrWhiteSpace(name, "name");
            Requires.NotNull(unit, "unit");
            Requires.NotNullOrEmpty(values, "values");
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.CustomValues = new AdministrationValues(unit, values);

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdministrationValueModel"/> class.
        /// </summary>
        /// <remarks>
        /// An ApplicationSettings visible no-argument constructor.
        /// </remarks>
        public AdministrationValueModel()
        {
        }

        #endregion

        #region Properties.

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

        /// <summary>
        /// The <see cref="AdministrationValues"/>.
        /// </summary>
        [JsonProperty("Values")]
        public AdministrationValues CustomValues
        {
            get;
            set;
        }

        #endregion

        #region Public members.

        /// <summary>
        /// Updates the AdministrationValueModel.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="max"></param>
        /// <param name="min"></param>
        /// <param name="step"></param>
        /// <param name="fractions"></param>
        /// <param name="unit"></param>
        public void Update(string name, double max, double min = 0, double step = 1, int fractions = 0, UnitOfMeasurement unit = null)
        {
            this.Name = name;
            this.CustomValues.Update(max, min, step, fractions, unit);
        }

        /// <summary>
        /// Updates the AdministrationValueModel.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="specificValues"></param>
        /// <param name="unit"></param>
        public void Update(string name, IList<double> specificValues, UnitOfMeasurement unit = null)
        {
            this.Name = name;
            this.CustomValues.Update(specificValues, unit);
        }

        /// <summary>
        /// Updates the AdministrationValueModel.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="customValues"></param>
        public void Update(string name, AdministrationValues customValues)
        {
            this.Name = name;
            this.CustomValues = customValues;
        }

        /// <summary>
        /// Gets a readable string for presentation purposes. e.g Milliliter (0-50ml)
        /// </summary>
        /// <returns>a string</returns>
        public string ToLongString()
        {
            return string.Format("{0} ({1})", this.Name, this.CustomValues.ToLongString());
        }

        #endregion
    }
}
