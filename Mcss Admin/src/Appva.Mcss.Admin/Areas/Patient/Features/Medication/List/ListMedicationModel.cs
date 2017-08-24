﻿// <copyright file="ListMedicationModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Patient.Models
{
    #region Imports.

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
        #region Properties.

        /// <summary>
        /// Gets or sets the patient.
        /// </summary>
        public PatientViewModel Patient
        {
            get;
            set;
        }

        #endregion

    }
}