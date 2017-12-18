// <copyright file="BodyWeightObservation.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using System.Linq;
    using System.Collections.Generic;
    using Appva.Mcss.Admin.Domain.Common;
    using System;

    #endregion
    
    /// <summary>
    /// A body weight observation.
    /// </summary>
    [Loinc("29463-7", "Body weight")]
    public class BodyWeightObservation : Observation
    {
        #region Variables.

        /// <summary>
        /// The valid units of measurement.
        /// </summary>
        private readonly IReadOnlyList<IUnitOfMeasurement> Units = new List<IUnitOfMeasurement>
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

        #region Properties.

        //// UNRESOLVED: temporary solution

        /// <summary>
        /// The descriptive name of the scale in use.
        /// </summary>
        public virtual string ShortName
        {
            get
            {
                return "Weight";
            }
        }
        //// UNRESOLVED: temporary solution

        /// <summary>
        /// The descriptive name of the scale in use.
        /// </summary>
        public virtual string DescriptiveName
        {
            get
            {
                return "General Weight Scale (5-450 kg)";
            }
        }

        #endregion

        /// <inheritdoc />
        protected override IArbituraryValue NewMeasurement<T>(T value, IUnitOfMeasurement unit = null)
        {
            unit = unit ?? MassUnits.Kilogram;
            if (! this.Units.Any(valid => valid == unit))
            {
                throw new OverflowException("The unit is not one of the ");
            }
            return ArbituraryMeasuredValue.New(value, LevelOfMeasurement.Quantitative, MassUnits.Kilogram);
        }

        /// <inheritdoc />
        protected override IArbituraryValue NewMeasurement(string value, IUnitOfMeasurement unit = null)
        {
            return ArbituraryMeasuredValue.New(value, LevelOfMeasurement.Quantitative, MassUnits.Kilogram);
        }
    }
}
