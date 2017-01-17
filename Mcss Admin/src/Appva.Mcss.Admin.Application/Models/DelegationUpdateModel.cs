// <copyright file="UpdateDelegationModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:your@email.address">Your name</a></author>
namespace Appva.Mcss.Admin.Application.Models
{
    #region Imports.

    using Appva.Mcss.Admin.Domain.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class DelegationUpdateModel
    {
        #region Properties.

        /// <summary>
        /// The name.
        /// </summary>
        public bool? IsActive
        {
            get;
            set;
        }

        /// <summary>
        /// Sets this delegation to all patients, new and old.
        /// </summary>
        public bool? IsGlobal
        {
            get;
            set;
        }

        /// <summary>
        /// List of <see cref="Patient"/>.
        /// </summary>
        public IList<Patient> Patients
        {
            get;
            set;
        }

        /// <summary>
        /// The delegation start date.
        /// </summary>
        public DateTime? StartDate
        {
            get;
            set;
        }

        /// <summary>
        /// The delagation end date.
        /// </summary>
        public DateTime? EndDate
        {
            get;
            set;
        }

        

        /// <summary>
        /// The organisation taxon
        /// </summary>
        public Taxon OrganisationTaxon
        {
            get;
            set;
        }

        /// <summary>
        /// <see cref="Account"/> which created the <see cref="Delegation"/>.
        /// </summary>
        public Account CreatedBy
        {
            get;
            set;
        }

        /// <summary>
        /// The change reason
        /// </summary>
        public string Reason
        {
            get;
            set;
        }

        #endregion
    }
}