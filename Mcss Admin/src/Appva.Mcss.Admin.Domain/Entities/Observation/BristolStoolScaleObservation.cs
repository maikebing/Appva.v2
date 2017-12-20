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
    using System.Linq;
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
        private readonly IReadOnlyList<string> valueList = new List<string> {  "type1", "type2", "type3", "type4", "type5", "type6", "type7" };

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

        #region Observation Overrides.

        /// <inheritdoc />
        protected override ArbitraryValue NewMeasurement<T>(T value, UnitOfMeasurement unit = null)
        {
            throw new InvalidOperationException("Value must be of a type 'string'.");
        }

        /// <inheritdoc />
        protected override ArbitraryValue NewMeasurement(string value, UnitOfMeasurement unit = null)
        {
            Requires.NotNullOrWhiteSpace(value, "value");
            Requires.ValidState(this.valueList.Any(x => x.Equals(value.ToString(), StringComparison.InvariantCultureIgnoreCase)), "value");
            Requires.ValidState(unit == null || unit == NonUnits.BristolStoolScale, "unit must be a bristol stool scale unit.");
            return ArbitraryValue.New(value, LevelOfMeasurement.Nominal, NonUnits.BristolStoolScale);
        }

        #endregion
    }
}
