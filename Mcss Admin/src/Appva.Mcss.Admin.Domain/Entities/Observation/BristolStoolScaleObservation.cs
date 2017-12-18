﻿// <copyright file="BristolStoolScaleObservation.cs" company="Appva AB">
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
    /// A bristol observation.
    /// </summary>
    [DisplayName("Bristol")]
    [Description("Bristol Stool Scale (Type 1-7)")]
    //// [AccessControl("...")]
    [Loinc("11029-6", " Consistency of Stool")]
    public class BristolStoolScaleObservation : Observation
    {
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
        protected override IArbituraryValue NewMeasurement<T>(T value, IUnitOfMeasurement unit = null)
        {
            //// UNRESOLVED: VALIDATE value.
            // 1. unit != null 
            // 2. t != system.string || type.enum skiten
            // 3. t ! parse till tena enum skiten
            // samma i bristol och arbitrator skalan
            // (t måste vara number på weight) system.double
            // (t double.tryparse()
            // min max och argumentoutofrangeexception eller overflowexception

            //unit = unit ?? NonUnits.BristolStoolScale;

            return ArbituraryMeasuredValue.New(value, LevelOfMeasurement.Nominal, unit);
        }

        /// <inheritdoc />
        protected override IArbituraryValue NewMeasurement(string value, IUnitOfMeasurement unit = null)
        {
            //unit = unit ?? NonUnits.BristolStoolScale;
            //// UNRESOLVED: Check value.
            return ArbituraryMeasuredValue.New(value, LevelOfMeasurement.Nominal, unit);
        }
    }
}
