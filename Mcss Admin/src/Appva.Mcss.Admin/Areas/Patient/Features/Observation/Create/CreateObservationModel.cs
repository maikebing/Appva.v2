﻿// <copyright file="CreateObservationModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// Class CreateObservationModel.
    /// </summary>
    /// <seealso cref="Appva.Mcss.Admin.Models.Identity{Appva.Mcss.Admin.Models.ListObservation}" />
    public class CreateObservationModel : Identity<ListObservation>
    {
        #region Properties.

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
        [Required]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
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