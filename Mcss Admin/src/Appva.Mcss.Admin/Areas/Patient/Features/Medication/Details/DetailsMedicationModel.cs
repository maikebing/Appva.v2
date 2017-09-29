// <copyright file="DetailsMedicationModel.cs" company="Appva AB">
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
    public sealed class DetailsMedicationModel
    {
        #region Properties.

        /// <summary>
        /// The patient
        /// </summary>
        public PatientViewModel Patient
        {
            get;
            set;
        }

        /// <summary>
        /// The medication
        /// </summary>
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

        public Dictionary<int, string> IntervallTranslation
        {
            get
            {
                var intervalMap = new Dictionary<int, string>();
                intervalMap.Add(0, "Annan");
                intervalMap.Add(1, "Varje dag");
                intervalMap.Add(2, "Varannan dag");
                intervalMap.Add(3, "Var tredje dag");
                intervalMap.Add(4, "Var fjärde dag");
                intervalMap.Add(5, "Var femte dag");
                intervalMap.Add(6, "Var sjätte dag");
                intervalMap.Add(7, "Varje vecka");
                intervalMap.Add(14, "Varannan vecka");
                intervalMap.Add(21, "Var tredje vecka");
                intervalMap.Add(28, "Var fjärde vecka");
                intervalMap.Add(35, "Var femte vecka");
                intervalMap.Add(56, "Var åttonde vecka");
                intervalMap.Add(84, "Var tolfte vecka");
                return intervalMap;
            }
        }

        #endregion
    }
}