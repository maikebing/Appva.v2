﻿// <copyright file="MeasurementRepository.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>

namespace Appva.Mcss.Admin.Domain.Repositories
{
    #region Imports

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface IMeasurementRepository : IRepository<MeasurementObservation>
    {
        #region Fields

        /// <summary>
        /// Gets the measurement categories.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns>IList&lt;MeasurementObservation&gt;.</returns>
        IList<MeasurementObservation> GetList(Guid patientId);

        /// <summary>
        /// Creates the measurement observation.
        /// </summary>
        /// <param name="measurementObservation">The measurement observation<see cref="MeasurementObservation"/>.</param>
        void Create(MeasurementObservation measurementObservation);

        /// <summary>
        /// Updates the specified observation.
        /// </summary>
        /// <param name="observation">The observation<see cref="MeasurementObservation"/>.</param>
        void Update(MeasurementObservation observation);

        #endregion
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class MeasurementRepository : Repository<MeasurementObservation>, IMeasurementRepository
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MeasurementRepository"/> class.
        /// </summary>
        /// <param name="context">The <see cref="T:Appva.Persistence.IPersistenceContext" />.</param>
        public MeasurementRepository(IPersistenceContext context) 
            : base(context)
        {
        }

        #endregion

        #region IMeasurementRepository Members

        /// <inheritdoc />
        public IList<MeasurementObservation> GetList(Guid patientId)
        {
            return this.Context.QueryOver<MeasurementObservation>()
                .Where(x => x.IsActive)
                .And(x => x.Patient.Id == patientId)
                .List();
        }

        /// <inheritdoc />
        public void Create(MeasurementObservation measurementObservation)
        {
            this.Save(measurementObservation);
        }

        #endregion
    }
}