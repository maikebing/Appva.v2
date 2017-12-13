﻿// <copyright file="UpdateObservationModel.cs" company="Appva AB">
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
    using Appva.Mcss.Admin.Domain.Entities;

    #endregion

    /// <summary>
    /// Class UpdateObservationModel.
    /// </summary>
    /// <seealso cref="Appva.Mcss.Admin.Models.Identity{Appva.Mcss.Admin.Models.ListObservation}" />
    public class UpdateObservationModel : Identity<ListObservation>
    {
        #region Properties.

        /// <summary>
        /// The MeasurementObservation.
        /// </summary>
        public Observation Observation
        {
            get;
            set;
        }

        /// <summary>
        /// The observation id.
        /// </summary>
        public Guid ObservationId
        {
            get;
            set;
        }

        /// <summary>
        /// The name.
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// The instruction.
        /// </summary>
        public string Instruction
        {
            get;
            set;
        }

        /// <summary>
        /// The delegation SelectList
        /// </summary>
        [DisplayName("Kräver delegation för")]
        public IEnumerable<SelectListItem> SelectDelegationList
        {
            get;
            set;
        }

        /// <summary>
        /// The selected unit.
        /// </summary>
        public string SelectedUnit
        {
            get;
            set;
        }

        /// <summary>
        /// The selected scale.
        /// </summary>
        public string SelectedScale
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

        #endregion
    }
}