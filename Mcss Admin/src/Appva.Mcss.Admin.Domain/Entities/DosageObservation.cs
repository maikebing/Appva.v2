// <copyright file="DosageObservation.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports

    using System;
    using System.Collections.Generic;
    using Validation;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class DosageObservation : Observation
    {
        public DosageObservation(Sequence sequence, Patient patient, string name, string description, string dosageScale, Taxon category = null) 
            : base(patient, name, description, category)
        {
            Requires.NotNull(sequence, "sequence");
            this.DosageScale = dosageScale;
            this.Sequence = sequence;
        }

        //public DosageObservation(Patient patient, string name, string description, string scale, Taxon category = null)
        //    : base(patient, name, description, category)
        //{
        //    this.DosageScale = scale;
        //}

        public DosageObservation()
        {
        }

        #region Properties

        /// <summary>
        /// Gets or sets the dosage scale.
        /// </summary>
        public virtual string DosageScale
        {
            get;
            set;
        }

        /// <summary>
        /// The Task
        /// </summary>
        /// <value>The sequence<see cref="Sequence"/>.</value>
        public virtual Sequence Sequence
        {
            get;
            set;
        }

        #endregion

        #region Public Static Builders.

        /// <summary>
        /// Creates a new instance of the <see cref="Observation"/> class.
        /// </summary>
        /// <param name="patient">The patient which the observation is made for.</param>
        /// <param name="name">The name of the observation.</param>
        /// <param name="description">The description or instruction.</param>
        /// <param name="category">Classification of type of observation.</param>
        /// <returns>A new <see cref="Observation"/> instance.</returns>
        //public static DosageObservation New(string dosageScale, Sequence sequence, Patient patient, string name, string description, Taxon category = null)
        //{
        //    return new DosageObservation(dosageScale, sequence, patient, name, description, category);
        //}

        public static DosageObservation New(Sequence sequence, Patient patient, string name, string description, string scale, Taxon category = null)
        {
            return new DosageObservation(sequence, patient, name, description, scale, category);
        }

        #endregion
    }
}