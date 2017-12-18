﻿// <copyright file="DosageObservation.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using Newtonsoft.Json;
    using Validation;

    #endregion

    /// <summary>
    /// An implementation of <see cref="Observation"/> for administered dosages.
    /// </summary>
    public class DosageObservation : Observation
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="DosageObservation"/> class.
        /// </summary>
        /// <param name="patient">The patient which the observation is made for.</param>
        /// <param name="name">The name of the observation.</param>
        /// <param name="description">The description or instruction.</param>
        /// <param name="scale">The scale used in the observation.</param>
        public DosageObservation(Patient patient, string name, string description, object scale) 
            : base(patient, name, description, scale)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DosageObservation"/> class.
        /// </summary>
        /// <remarks>An NHibernate visible no-argument constructor.</remarks>
        protected internal DosageObservation()
        {
        }

        #endregion

        #region Public Methods.

        /// <summary>
        /// Updates the DosageObservationScale.
        /// </summary>
        /// <param name="scale">The scale.</param>
        public virtual void Update(object scale)
        {
            Requires.NotNull(scale, "scale");
            this.Scale = JsonConvert.SerializeObject(scale); 
        }

        #endregion

        /// <inheritdoc />
        protected override ArbitraryValue NewMeasurement<T>(T value, UnitOfMeasurement unit = null)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        protected override ArbitraryValue NewMeasurement(string value, UnitOfMeasurement unit = null)
        {
            throw new System.NotImplementedException();
        }
    }
}