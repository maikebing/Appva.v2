// <copyright file="Patient.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Common.Domain;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class Patient : Person<Patient>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Patient"/> class.
        /// </summary>
        /*public Patient(string firstName, string lastName, string personalIdentityNumber)
            : base(firstName, lastName, personalIdentityNumber)
        {
        }

        protected Patient()
        {
        }
         * */

        #endregion

        #region Person overrides.

        /// <summary>
        /// FIXME: Move to person! 
        /// </summary>
        public virtual PersonalIdentityNumber PersonalIdentityNumber
        {
            get;
            set;
        }

        #endregion

        #region Properties.

        /// <summary>
        /// Second unique identifier for a patient - e.g. NFC-tag.
        /// </summary>
        public virtual string Identifier
        {
            get;
            set;
        }

        /// <summary>
        /// The taxonomy <see cref="Taxon"/>.
        /// </summary>
        public virtual Taxon Taxon
        {
            get;
            set;
        }

        /// <summary>
        /// If deceased.
        /// </summary>
        public virtual bool Deceased
        {
            get;
            set;
        }

        /// <summary>
        /// If the patient has delayed <see cref="Task"/> which are not quittenced.
        /// </summary>
        public virtual bool HasUnattendedTasks
        {
            get;
            set;
        }

        /// <summary>
        /// List of <see cref="Delegation"/> for this patient.
        /// </summary>
        public virtual IList<Delegation> Delegations
        {
            get;
            set;
        }

        /// <summary>
        /// List of <see cref="Task"/> for this patient.
        /// </summary>
        public virtual IList<Task> Tasks
        {
            get;
            set;
        }

        /// <summary>
        /// List of <see cref=" Taxon"/> representing Senior Alerts for this patient
        /// </summary>
        public virtual IList<Taxon> SeniorAlerts
        {
            get;
            set;
        }

        #endregion
    }
}