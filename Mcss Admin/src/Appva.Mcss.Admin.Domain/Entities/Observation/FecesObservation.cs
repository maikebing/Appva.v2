// <copyright file="FecesObservation.cs" company="Appva AB">
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
    using Appva.Mcss.Admin.Domain.Common;
    using Validation;

    #endregion

    /// <summary>
    /// A arbitrary stool weight scale <see cref="Observation"/> implementation.
    /// </summary>
    [DisplayName("Feces")]
    [Description("General stool scale (Type AAA-d)")]
    [Loinc("17609-9", "Weight [Mass/time] of 72 hour Stool")]
    public class FecesObservation : Observation
    {
        /// <summary>
        /// A list of valid feces values.
        /// </summary>
        private readonly List<string> valueList = new List<string> { "AAA", "AA", "A", "aaa", "a", "d", "D", "k" };

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="FecesObservation"/> class.
        /// </summary>
        /// <param name="patient">The patient.</param>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        /// <param name="delegation">The delegation.</param>
        public FecesObservation(Patient patient, string name, string description, Taxon delegation = null)
            : base(patient, name, description, null, delegation)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FecesObservation"/> class.
        /// </summary>
        /// <remarks>
        /// An NHibernate visible no-argument constructor.
        /// </remarks>
        protected internal FecesObservation()
        {
        }

        #endregion

        /// <inheritdoc />
        protected override ArbitraryValue NewMeasurement<T>(T value, UnitOfMeasurement unit = null)
        {
            Requires.ValidState(value.IsNull() == false, "value");
            unit = unit ?? NonUnits.ArbitraryStoolScale;
            return ArbitraryValue.New(this.Validate(value), LevelOfMeasurement.Nominal, unit);
        }

        /// <inheritdoc />
        protected override ArbitraryValue NewMeasurement(string value, UnitOfMeasurement unit = null)
        {
            Requires.NotNullOrWhiteSpace(value, "value");
            unit = unit ?? NonUnits.ArbitraryStoolScale;
            return ArbitraryValue.New(this.Validate(value), LevelOfMeasurement.Nominal, unit);
        }

        /// <summary>
        /// Validates 'value' against a list with valid values.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns>value if valid.</returns>
        private T Validate<T>(T value)
        {
            if (((value.Is<string>()) || (value.GetType().IsEnum)) == false)
            {
                throw new ArgumentOutOfRangeException("value", value, " is an invalid value.");
            }
            if (this.valueList.Contains(value.ToString()) == false)
            {
                throw new ArgumentOutOfRangeException("value", value, " is not a valid value.");
            }
            return value;
        }
    }
}
