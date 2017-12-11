
using Appva.Mcss.Admin.Application.Models;
using Appva.Mcss.Admin.Domain.Entities;
using Appva.Mcss.Web.ViewModels;
using System;
// <copyright file="ListMedicationsTableViewModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
using System.Collections.Generic;
namespace Appva.Mcss.Admin.Models
{
    #region Imports.



    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ListMedicationsTableViewModel 
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ListMedicationsTableViewModel"/> class.
        /// </summary>
        public ListMedicationsTableViewModel()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// Gets or sets the medications.
        /// </summary>
        /// <value>
        /// The medications.
        /// </value>
        public IList<Medication> Medications
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the sequences.
        /// </summary>
        /// <value>
        /// The sequences.
        /// </value>
        public Dictionary<long, SequenceMedicationCompareModel> Sequences
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the patient.
        /// </summary>
        /// <value>
        /// The patient.
        /// </value>
        public Guid PatientId
        {
            get;
            set;
        }

        #endregion
    }
}