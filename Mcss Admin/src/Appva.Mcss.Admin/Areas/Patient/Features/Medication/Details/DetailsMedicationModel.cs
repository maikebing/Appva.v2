// <copyright file="DetailsMedicationModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Models
{
    #region Imports.

    using Appva.Hip.Model;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Models;
    using Appva.Mcss.Web.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class DetailsMedicationModel 
    {
        public MedicationItem Medication
        {
            get;
            set;
        }

        public PatientViewModel Patient
        {
            get;
            set;
        }

        public DateTime MedicationLastChanged
        {
            get;
            set;
        }

        public IList<Sequence> Sequences
        {
            get;
            set;
        }

        public IEnumerable<Schedule> Schedules
        {
            get;
            set;
        }
    }
}