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
    public class TenaObservationPeriod : Observation
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="TenaObservationPeriod"/> class.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="patient">The patient.</param>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        /// <param name="category">The category.</param>
        public TenaObservationPeriod(DateTime startDate, DateTime endDate, Patient patient, string name, string description) 
            : base(patient, name, description)
        {
            this.StartDate = startDate;
            this.EndDate = endDate;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TenaObservationPeriod"/> class.
        /// </summary>
        /// <remarks>An NHibernate visible no-argument constructor.</remarks>
        public TenaObservationPeriod()
        {
        }

        #endregion

        #region Properties
        /// <summary>
        /// Date at start of period
        /// </summary>
        public virtual DateTime StartDate
        {
            get;
            set;
        }

        /// <summary>
        /// Date at end of period
        /// </summary>
        public virtual DateTime EndDate
        {
            get;
            set;
        }

        #endregion
    }
}
