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

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class DosageObservation : Observation
    {
        public DosageObservation(string dosageScaleUnit, string dosageScaleValues, Patient patient, string name, string description, Taxon category = null) 
            : base(patient, name, description, category)
        {
            this.DosageScaleUnit = dosageScaleUnit;
            this.DosageScaleValues = dosageScaleValues;
        }

        public DosageObservation()
        {
        }

        #region Properties

        /// <summary>
        /// Gets or sets the dosage scale unit.
        /// </summary>
        public virtual string DosageScaleUnit
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the dosage scale values.
        /// </summary>
        public virtual string DosageScaleValues
        {
            get;
            set;
        }

        #endregion
    }
}