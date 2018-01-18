// <copyright file="BodyWeightObservation.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using Appva.Mcss.Admin.Domain.Common;
    using Validation;

    #endregion

    /// <summary>
    /// A body weight <see cref="Observation"/> implementation.
    /// </summary>
    [DisplayName("Body weight")]
    [Description("General weight scale (5-450 kg)")]
    [Loinc("29463-7", "Body weight")]
    public class BodyWeightObservation : Observation
    {
        #region Variables.

        /// <summary>
        /// The valid units of measurement.
        /// </summary>
        private static readonly IReadOnlyList<UnitOfMeasurement> Units = new List<UnitOfMeasurement>
        {
            MassUnits.Kilogram, MassUnits.Gram, MassUnits.Pound
        };

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="BodyWeightObservation"/> class.
        /// </summary>
        /// <param name="patient">The patient.</param>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        /// <param name="delegation">The delegation.</param>
        public BodyWeightObservation(Patient patient, string name, string description, Taxon delegation = null)
            : base(patient, name, description, delegation)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BodyWeightObservation"/> class.
        /// </summary>
        /// <remarks>
        /// An NHibernate visible no-argument constructor.
        /// </remarks>
        protected internal BodyWeightObservation()
        {
        }

        #endregion

        #region Observation Overrides.

        /// <inheritdoc />
        protected override ArbitraryValue NewMeasurement<T>(T value, UnitOfMeasurement unit = null)
        {
            Requires.ValidState(this.Validate(value), "value");
            Requires.ValidState(this.Validate(unit),  "unit");
            return ArbitraryValue.New(value, LevelOfMeasurement.Quantitative, unit);
        }

        /// <inheritdoc />
        protected override ArbitraryValue NewMeasurement(string value, UnitOfMeasurement unit = null)
        {
            Requires.NotNullOrWhiteSpace(value, "value");
            ////TODO: Locale differences in double datatype. Replaces '.' with ','
            value = value.Replace('.', ',');
            return this.NewMeasurement(this.TryParseToDouble(value), unit?? MassUnits.Kilogram);
        }

        #endregion

        #region Private Methods.

        /// <summary>
        /// Tries to parse the value type string to a result type double.
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns>Result as type <double>double</double>.</returns>
        private double TryParseToDouble(string value)
        {
            double result = 0.0;
            double.TryParse(value, out result);
            return result;
        }

        /// <summary>
        /// Validates a body weight value.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns><bool>true</bool> if valid.</returns>
        /// <exception cref="ArgumentNullException">if value is null.</exception>
        /// <exception cref="NotFiniteNumberException">if value is not a number.</exception>
        /// <exception cref="ArgumentOutOfRangeException">if value is not valid.</exception>
        private bool Validate<T>(T value)
        {
            if (value.IsNull())
            {
                throw new ArgumentNullException("value");
            }
            if (value.IsNot<double>())
            {
                throw new NotFiniteNumberException("value is not of type 'double'.");
            }
            var mass = (double) Convert.ChangeType(value, typeof(double));
            if (mass < 5 || mass > 450)
            {
                throw new ArgumentOutOfRangeException("value", value, "is out of range.");
            }
            return true;
        }

        /// <summary>
        /// Validates the body weight unit.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <returns><bool>true</bool>if valid.</returns>
        /// <exception cref="ArgumentNullException">if unit is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">if unit is not valid.</exception>
        private bool Validate(UnitOfMeasurement unit = null)
        {
            if (unit == null)
            {
                throw new ArgumentNullException("unit");
            }
            if (! Units.Any(valid => valid == unit))
            {
                throw new ArgumentOutOfRangeException("unit");
            }
            return true;
        }

        #endregion
    }
}
