// <copyright file="FecesObservation.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using System.ComponentModel;
    using Appva.Mcss.Admin.Domain.Common;

    #endregion

    /// <summary>
    /// A arbitrary stool weight scale <see cref="Observation"/> implementation.
    /// </summary>
    [DisplayName("Feces")]
    [Description("General stool scale (Type AAA-d)")]
    [Loinc("17609-9", "Weight [Mass/time] of 72 hour Stool")]
    public class FecesObservation : Observation
    {
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
            unit = unit ?? NonUnits.ArbitraryStoolScale;
            //// UNRESOLVED: Check value.
            return ArbitraryValue.New(value, LevelOfMeasurement.Nominal, unit);
        }

        /// <inheritdoc />
        protected override ArbitraryValue NewMeasurement(string value, UnitOfMeasurement unit = null)
        {
            unit = unit ?? NonUnits.ArbitraryStoolScale;
            //// UNRESOLVED: Check value.
            return ArbitraryValue.New(value, LevelOfMeasurement.Nominal, unit);
        }
    }
}
