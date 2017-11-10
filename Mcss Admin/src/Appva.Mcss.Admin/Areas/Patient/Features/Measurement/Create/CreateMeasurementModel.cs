// <copyright file="CreateMeasurementModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>

namespace Appva.Mcss.Admin.Models
{
    #region Imports

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class CreateMeasurementModel : Identity<ListMeasurement>
    {
        #region Variables

        /// <summary>
        /// Patient Id
        /// </summary>
        public Guid PatientId
        {
            get; set;
        }

        /// <summary>
        /// The name.
        /// </summary>
        [DisplayName("Namn")]
        [Required]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [DisplayName("Instruktion")]
        [Required]
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// The Scale SelectList.
        /// </summary>
        [DisplayName("Skala")]
        public IEnumerable<SelectListItem> SelectScaleList
        {
            get;
            set;
        }

        /// <summary>
        /// The delegation select list.
        /// </summary>
        [DisplayName("Kräver delegering för")]
        public IEnumerable<SelectListItem> SelectDelegationList
        {
            get;
            set;
        }

        /// <summary>
        /// The selected delegation.
        /// </summary>
        [Required]
        public string SelectedDelegation
        {
            get;
            set;
        }

        /// <summary>
        /// The selected scale.
        /// </summary>
        [Required]
        public string SelectedScale
        {
            get;
            set;
        }

        #endregion
    }
}