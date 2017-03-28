// <copyright file="CategoryDetailsModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Models
{
    #region Imports.

    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Domain.Entities;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class CategoryDetailsModel
    {
        #region Properties.

        /// <summary>
        /// The id of the schedule
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// The name of the category
        /// </summary>
        [Required]
        [Display(Name="Namn")]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// The category-color
        /// </summary
        [Required]
        [Display(Name = "Färg")]
        public string Color
        {
            get;
            set;
        }

        /// <summary>
        /// If the category should be declared as an absence-category
        /// E.g. if the patient is away during the activity and the 
        /// possibility to pause all tasks should be active
        /// </summary>
        [Display(Name="Frånvaro")]
        public bool Absence
        {
            get;
            set;
        }

        /// <summary>
        /// Deviation-dialog on/off
        /// </summary>
        [Display(Name = "Kontrollruta vid avvikande signering")]
        public bool NurseConfirmDeviation
        {
            get;
            set;
        }

        /// <summary>
        /// The popup message when task signed as a deviation
        /// </summary>
        public ConfirmDeviationMessage DeviationMessage
        {
            get;
            set;
        }

        /// <summary>
        /// The status-taxon available for this category
        /// </summary>
        public IList<Taxon> StatusTaxons
        {
            get;
            set;
        }

        #endregion
    }
}