﻿// <copyright file="ListMedicationModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Web.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ListMedicationModel
    {
        #region Properties

        /// <summary>
        /// The patient
        /// </summary>
        public PatientViewModel Patient
        {
            get;
            set;
        }

        /// <summary>
        /// The dispensed medications
        /// </summary>
        public IList<Medication> DispensedMedications
        {
            get;
            set;
        }

        /// <summary>
        /// The original package medications
        /// </summary>
        public IList<Medication> OriginalPackageMedications
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

        #endregion
    }
}