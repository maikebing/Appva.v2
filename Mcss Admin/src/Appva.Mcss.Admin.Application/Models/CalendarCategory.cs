// <copyright file="CalendarCategory.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksosn@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using Appva.Mcss.Admin.Domain.Entities;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class CalendarCategory
    {
        #region Properties.

        /// <summary>
        /// The Id for the category
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// The name of the category
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// The color of the category
        /// </summary>
        public string Color
        {
            get;
            set;
        }

        /// <summary>
        /// If category contains absence-activities
        /// </summary>
        public bool Absence
        {
            get;
            set;
        }

        /// <summary>
        /// The sign-statuses available for the category activities
        /// </summary>
        public IList<Taxon> StatusTaxons 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// If the deviations should be confirmed by nurses
        /// </summary>
        public bool NurseConfirmDeviation
        {
            get;
            set;
        }

        /// <summary>
        /// The specific deviation-message
        /// </summary>
        public ConfirmDeviationMessage ConfirmDevitationMessage
        {
            get;
            set;
        }

        #endregion

        
    }
}