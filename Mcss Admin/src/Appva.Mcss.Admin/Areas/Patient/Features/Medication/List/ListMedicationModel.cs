// <copyright file="ListMedicationModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Web.ViewModels;
using Appva.Hip.Model;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ListMedicationModel
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ListMedicationModel"/> class.
        /// </summary>
        public ListMedicationModel()
        {
        }

        #endregion

        public PatientViewModel Patient
        {
            get;
            set;
        }

        public Schedule Schedule
        {
            get;
            set;
        }

        public IList<ResponseItem<MedicationItem>> Medications 
        { 
            get;
            set;
        }

        public int PageNumber
        {
            get;
            set;
        }

        public int PageSize
        {
            get;
            set;
        }

        public int TotalItemCount
        {
            get;
            set;
        }
    }
}