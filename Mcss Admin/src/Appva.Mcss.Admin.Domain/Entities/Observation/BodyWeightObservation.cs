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
            : base(patient, name, description, null, delegation)
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

        /// <inheritdoc />
        protected override ArbitraryValue NewMeasurement<T>(T value, UnitOfMeasurement unit = null)
        {
            //// Move this piece of validation to a separate private function ->
            if (! (value is double))
            {
                ///...
            }
            if (unit != null && Units.Any(valid => valid == unit) == false)
            {
                throw new ArgumentOutOfRangeException("unit");
            }
            var quantity = (double)Convert.ChangeType(value, typeof(double));
            return ArbitraryValue.New(quantity, LevelOfMeasurement.Quantitative, unit ?? MassUnits.Kilogram);
        }

        /// <inheritdoc />
        protected override ArbitraryValue NewMeasurement(string value, UnitOfMeasurement unit = null)
        {
            //// Add validation (as separate method), TryParse, check unit as previous, within range (the value)
            /// Just to make stuff work, parse and go to the method above -->
            var quantity = 0.0;
            double.TryParse(value, out quantity);
            return this.NewMeasurement(quantity, unit);
        }
    }
}
