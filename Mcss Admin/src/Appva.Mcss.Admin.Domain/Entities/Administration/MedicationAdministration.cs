// <copyright file="MedicationAdministration.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;
    using Validation;

    #endregion

    public class MedicationAdministration : Administration
    {
        public static readonly IReadOnlyList<UnitOfMeasurement> Units = new List<UnitOfMeasurement>
        {
            MassUnits.Gram,
            MassUnits.Milligram,
            VolumeUnits.Deciliter,
            VolumeUnits.Centiliter,
            VolumeUnits.Milliliter,
            NonUnits.Tablets
        };

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="MedicationAdministration"/> class.
        /// </summary>
        /// <param name="patient">The <see cref="Patient" />.</param>
        /// <param name="sequence">The <see cref="Sequence" />.</param>
        /// <param name="unit">The <see cref="UnitOfMeasurement" />.</param>
        /// <param name="customValues">The custom values.</param>
        public MedicationAdministration(string name, Sequence sequence, UnitOfMeasurement unit, IList<double> customValues)
            : base(name, sequence, unit, customValues)
        {
            Requires.ValidState(this.Validate(unit), "unit");
        }

        public MedicationAdministration(string name, Sequence sequence, UnitOfMeasurement unit, double max, double min, double step, int fractions)
            : base(name, sequence, unit, max, min, step, fractions)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MedicationAdministration"/> class.
        /// </summary>
        /// <remarks>
        /// An NHibernate visible no-argument constructor.
        /// </remarks>
        protected internal MedicationAdministration()
        {
        }

        #endregion

        #region Administration Overrides.

        /// <inheritdoc />
        protected override ArbitraryValue NewValue(double value)
        {
            Requires.ValidState(this.Validate(value), "value"); //// TODO: Insert validation.
            return ArbitraryValue.New(value, LevelOfMeasurement.Quantitative, this.Unit);
        }

        /// <inheritdoc />
        public override Administration Update(UnitOfMeasurement unit, IList<double> customValues)
        {
            Requires.ValidState(Validate(unit), "unit");
            Requires.NotNullOrEmpty(customValues, "customValues");
            this.Unit = unit;
            this.SpecificValues = JsonConvert.SerializeObject(customValues);

            //// TODO: Toggle between list or formula. Handle this in another way, a class for this functionality.
            this.MaxValue = 0;
            this.MinValue = 0;
            this.Step = 0;
            this.Fractions = 0;

            return this;
        }

        /// <inheritdoc />
        public override Administration Update(UnitOfMeasurement unit, double max, double min = 0, double step = 1, int fractions = 0)
        {
            Requires.NotNull(unit, "unit");
            Requires.ValidState((max >= min && max > 0), "max");
            Requires.ValidState((min <= max && min >= 0), "min");
            Requires.ValidState(step > 0, "step");
            Requires.ValidState((fractions == 0 || fractions == 1 || fractions == 2), "fractions");
            this.Unit = unit;
            this.MaxValue = max;
            this.MinValue = min;
            this.Step = step;
            this.Fractions = fractions;

            //// TODO: Toggle between list or formula. Handle this in another way, a class for this functionality.
            this.SpecificValues = string.Empty;

            return this;
        }

        #endregion

        /// <summary>
        /// Validates the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private bool Validate(double value)
        {
            //// TODO: Inside Range or In CustomValuesList
            return this.GetCustomValues().Any(x => x == value);
        }

        /// <summary>
        /// Validates the specified unit.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private bool Validate(UnitOfMeasurement unit)
        {
            return Units.Any(x => x == unit);
        }
    }
}
