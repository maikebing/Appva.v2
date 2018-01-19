// <copyright file="AdministrationValueModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using Appva.Mcss.Admin.Domain.Entities;
    using Newtonsoft.Json;
    using Validation;

    #endregion

    [JsonObject]
    public sealed class AdministrationValueModel
    {
        #region Variables.

        /// <summary>
        /// The list of units available.
        /// </summary>
        public static readonly IReadOnlyList<UnitOfMeasurement> Units = new List<UnitOfMeasurement>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="AdministrationValueModel"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="unit">The <see cref="UnitOfMeasurement"/>.</param>
        /// <param name="max">The maximum value.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="step">The step/increment.</param>
        /// <param name="fractions">The number of fractions.</param>
        public AdministrationValueModel(string name, UnitOfMeasurement unit, double max, double min = 0, double step = 1, int fractions = 0)
        {
            Requires.NotNullOrWhiteSpace(name, "name");
            Requires.NotNull(unit, "unit");
            Requires.ValidState(max >= 0, "max");
            Requires.ValidState(min >= 0 && min <= max, "min");
            Requires.ValidState(fractions <= 2, "decimals");
            Requires.ValidState(Math.Round(step,2) >= 0.01, "step");
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

        /// <summary>
        /// The identifier
        /// </summary>
        [JsonProperty("Id")]
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// The visible name
        /// </summary>
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
        /// <param name="name">The name.</param>
        /// <param name="customValues">The <see cref="AdministrationValues"/>.</param>
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
