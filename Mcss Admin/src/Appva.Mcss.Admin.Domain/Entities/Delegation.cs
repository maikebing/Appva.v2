// <copyright file="Delegation.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class Delegation : AggregateRoot
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="Delegation"/> class.
        /// </summary>
        public Delegation()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The name.
        /// </summary>
        public virtual string Name
        {
            get;
            set;
        }

        /// <summary>
        /// The <see cref="Account"/>.
        /// </summary>
        public virtual Account Account
        {
            get;
            set;
        }

        /// <summary>
        /// Sets this delegation to all patients, new and old.
        /// </summary>
        public virtual bool IsGlobal
        {
            get;
            set;
        }

        /// <summary>
        /// List of <see cref="Patient"/>.
        /// </summary>
        public virtual IList<Patient> Patients
        {
            get;
            set;
        }

        /// <summary>
        /// The delegation start date.
        /// </summary>
        public virtual DateTime StartDate
        {
            get;
            set;
        }

        /// <summary>
        /// The delagation end date.
        /// </summary>
        public virtual DateTime EndDate
        {
            get;
            set;
        }

        /// <summary>
        /// If not yet activated the delegation is pending.
        /// </summary>
        public virtual bool Pending
        {
            get;
            set;
        }

        /// <summary>
        /// The delegation taxon.
        /// </summary>
        public virtual Taxon Taxon
        {
            get;
            set;
        }

        /// <summary>
        /// The organisation taxon
        /// </summary>
        public virtual Taxon OrganisationTaxon
        {
            get;
            set;
        }

        /// <summary>
        /// <see cref="Account"/> which created the <see cref="Delegation"/>.
        /// </summary>
        public virtual Account CreatedBy
        {
            get;
            set;
        }

        #endregion

        #region Public Methods.

        /// <summary>
        /// Adds a <see cref="Patient"/>.
        /// </summary>
        /// <param name="patient"></param>
        public virtual void Add(Patient patient)
        {
            Patients.Add(patient);
        }

        #endregion
    }
}