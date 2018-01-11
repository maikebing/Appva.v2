// <copyright file="MedicationAdministration.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using System.Collections.Generic;
    using System.Linq;
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
        public MedicationAdministration(Sequence sequence, UnitOfMeasurement unit, IList<double> customValues)
            : base(sequence, unit, customValues)
        {
            Requires.ValidState(this.Validate(unit), "unit");
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

        /// <summary>
        /// News the value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="unit">The unit.</param>
        /// <returns>ArbitraryValue.</returns>
        protected override ArbitraryValue NewValue(double value, UnitOfMeasurement unit = null)
        {
            Requires.ValidState(this.Validate(value), "value"); //// TODO: Insert validation.
            return ArbitraryValue.New(value, LevelOfMeasurement.Quantitative, this.Unit);
        }

        /// <summary>
        /// Updates the specified unit.
        /// </summary>
        /// <param name="unit">The <see cref="UnitOfMeasurement"/>.</param>
        /// <param name="customValues">The custom values.</param>
        /// <returns>Administration.</returns>
        public override Administration Update(UnitOfMeasurement unit, IList<double> customValues = null)
        {
            Requires.ValidState(Validate(unit), "unit");
            this.Unit = unit;
            this.CustomValues = ComposeCustomValues(customValues);
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
