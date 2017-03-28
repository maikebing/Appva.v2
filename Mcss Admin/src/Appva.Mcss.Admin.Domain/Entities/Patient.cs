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
        #region Variables.

        /// <summary>
        /// The LastActivatedAt field
        /// TODO: Remove later
        /// </summary>
        private DateTime lastActivatedAt;

        #endregion

        #region Constructor 

        /// <summary>
        /// Initializes a new instance of the <see cref="Patient"/> class.
        /// </summary>
        public Patient()
            :base()
        {
            this.LastActivatedAt = DateTime.Now;
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
        /// If Archived.
        /// </summary>
        public virtual bool IsArchived
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

        /// <summary>
        /// When the patient was activated
        /// Sets default to 2001-01-01. Will automatically be updated to CreatedAt 
        /// (if LastActivatedAt less than CreatedAt) when entity is loaded
        /// </summary>
        public virtual DateTime LastActivatedAt
        {
            get
            {
                if (this.lastActivatedAt < this.CreatedAt)
                {
                    return this.CreatedAt;
                }
                return this.lastActivatedAt;
            }
            set
            {
                this.lastActivatedAt = value;
            }
        }

        /// <summary>
        /// When the patient was inactivated
        /// </summary>
        public virtual DateTime? LastInActivatedAt
        {
            get;
            set;
        }

        /// <summary>
        /// Whether or not the patient is a person of public interest (VIP).
        /// </summary>
        public virtual bool IsPersonOfPublicInterest
        {
            get;
            set;
        }

        /// <summary>
        /// Whether or not the patient is a person of which all demographic
        /// information is sensitive.
        /// </summary>
        public virtual bool IsAllDemographicInformationSensitive
        {
            get;
            set;
        }

        #endregion
    }
}