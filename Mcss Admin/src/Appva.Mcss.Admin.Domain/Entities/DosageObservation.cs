// <copyright file="TenaObservationPeriod.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports

    using System;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class DosageObservation : Observation
    {
        #region Constructor

        ///// <summary>
        ///// Initializes a new instance of the <see cref="DosageObservation"/> class.
        ///// </summary>
        ///// <param name="startDate">The start date.</param>
        ///// <param name="endDate">The end date.</param>
        ///// <param name="patient">The patient.</param>
        ///// <param name="name">The name.</param>
        ///// <param name="description">The description.</param>
        ///// <param name="category">The category.</param>
        //public DosageObservation(Patient patient, string name, string description, Taxon category = null)
        //    : base(patient, name, description, category)
        //{
        //}

        ///// <summary>
        ///// Initializes a new instance of the <see cref="DosageObservation"/> class.
        ///// </summary>
        ///// <remarks>An NHibernate visible no-argument constructor.</remarks>
        //public DosageObservation()
        //{
        //}

        #endregion

        #region Properties

        public virtual string TestString { get; set; }


        #endregion
    }
}