// <copyright file="ListMedicationTableRowViewModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
using Appva.Mcss.Admin.Application.Models;
using Appva.Mcss.Admin.Domain.Entities;
using System;
namespace Appva.Mcss.Admin.Models
{
    #region Imports.



    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ListMedicationTableRowViewModel
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ListMedicationTableRowViewModel"/> class.
        /// </summary>
        public ListMedicationTableRowViewModel()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// Gets or sets the medication.
        /// </summary>
        /// <value>
        /// The medication.
        /// </value>
        public Medication Medication
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
        public SequenceMedicationCompareModel Sequences
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the patient identifier.
        /// </summary>
        /// <value>
        /// The patient identifier.
        /// </value>
        public Guid PatientId
        {
            get;
            set;
        }

        #endregion

        public string RowStatus
        {
            get
            {
                var status = "";
                if (Sequences.Sequence != null)
                {
                    status = "ok";
                }
                else if (Sequences.History != null && Sequences.History.Count > 0)
                {
                    status = "updated";
                }
                if (Medication.EndsAt.GetValueOrDefault(DateTime.MaxValue) < DateTime.Now)
                {
                    status += " canceled";
                }
                
                return status;
                
            }
        }
    }
}