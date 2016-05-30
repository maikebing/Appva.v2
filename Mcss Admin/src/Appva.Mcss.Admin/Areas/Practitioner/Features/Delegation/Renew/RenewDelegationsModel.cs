// <copyright file="UpdateDelegationModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Practitioner.Models
{
    #region Imports.

    using Appva.Mvc;
    using DataAnnotationsExtensions;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class RenewDelegationsModel
    {
        #region Properties

        /// <summary>
        /// The renewal from-date
        /// </summary>
        [Required(ErrorMessage = "Datum måste fyllas i.")]
        [Date(ErrorMessage = "Datum måste fyllas i med åtta siffror och bindestreck, t. ex. 2012-12-21.")]
        [DateLessThan(Target = "EndDate", ErrorMessage = "Startdatum måste vara ett tidigare datum är slutdatum.")]
        [DataType(DataType.Date)]
        [DisplayName("Från")]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// The renewal to-date
        /// </summary>
        [Required(ErrorMessage = "Datum måste fyllas i.")]
        [Date(ErrorMessage = "Datum måste fyllas i med åtta siffror och bindestreck, t. ex. 2012-12-21.")]
        [DateGreaterThan(Target = "StartDate", ErrorMessage = "Slutdatum måste vara ett senare datum är startdatum.")]
        [DataType(DataType.Date)]
        [DisplayName("Till")]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// The account-id
        /// </summary>
        public Guid AccountId { get; set; }

        /// <summary>
        /// The delegation category id.
        /// </summary>
        public Guid DelegationCategoryId { get; set; }

        #endregion
    }
}