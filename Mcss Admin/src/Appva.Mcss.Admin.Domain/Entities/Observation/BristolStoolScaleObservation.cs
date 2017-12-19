// <copyright file="BristolStoolScaleObservation.cs" company="Appva AB">
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
    /// A Bristol stool scale <see cref="Observation"/> implementation.
    /// </summary>
    [DisplayName("Bristol")]
    [Description("Bristol Stool Scale (Type 1-7)")]
    [Loinc("11029-6", " Consistency of Stool")]
    public class BristolStoolScaleObservation : Observation
    {
        /// <summary>
        /// A list of valid bristol values.
        /// </summary>
        private readonly List<string> valueList = new List<string> {  "type1", "type2", "type3", "type4", "type5", "type6", "type7" };

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="BristolStoolScaleObservation"/> class.
        /// </summary>
        /// <param name="patient">The patient.</param>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        /// <param name="delegation">The delegation.</param>
        public BristolStoolScaleObservation(Patient patient, string name, string description, Taxon delegation = null)
            : base(patient, name, description, null, delegation)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BristolStoolScaleObservation"/> class.
        /// </summary>
        /// <remarks>
        /// An NHibernate visible no-argument constructor.
        /// </remarks>
        protected internal BristolStoolScaleObservation()
        {
        }

        #endregion

        /// <inheritdoc />
        protected override ArbitraryValue NewMeasurement<T>(T value, UnitOfMeasurement unit = null)
        {
            Requires.ValidState(value.IsNull() == false, "value");
            unit = unit ?? NonUnits.BristolStoolScale;
            return ArbitraryValue.New(this.Validate(value), LevelOfMeasurement.Nominal, unit);
        }

        /// <inheritdoc />
        protected override ArbitraryValue NewMeasurement(string value, UnitOfMeasurement unit = null)
        {
            Requires.NotNullOrWhiteSpace(value, "value");
            unit = unit ?? NonUnits.BristolStoolScale;
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
            if (this.valueList.Contains(value.ToString().ToLower()) == false)
            {
                throw new ArgumentOutOfRangeException("value", value, " is not a valid value.");
            }
            return value;
        }
    }
}
