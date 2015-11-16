// <copyright file="EditDelegationModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Models
{
    #region Imports.

    using Appva.Mcss.Web.ViewModels;
    using Appva.Mvc;
    using Appva.Mcss.Admin.Domain.Entities;
    using DataAnnotationsExtensions;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class EditDelegationModel
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="EditDelegationModel"/> class.
        /// </summary>
        public EditDelegationModel()
        {
            Patients = new string[] { };
            ConnectedPatients = new List<Patient>();
            PatientItems = new List<SelectListItem>();
        }

        #endregion

        #region Properties

        public Guid Id { get; set; }

        [Required(ErrorMessage = "Datum måste fyllas i.")]
        [Date(ErrorMessage = "Datum måste fyllas i med åtta siffror och bindestreck, t. ex. 2012-12-21.")]
        [DateLessThan(Target = "EndDate", ErrorMessage = "Startdatum måste vara ett tidigare datum är slutdatum.")]
        [PlaceHolder("T.ex. 2012-12-21")]
        [DisplayName("Gäller fr.o.m.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Datum måste fyllas i.")]
        [Date(ErrorMessage = "Datum måste fyllas i med åtta siffror och bindestreck, t. ex. 2012-12-21.")]
        [DateGreaterThan(Target = "StartDate", ErrorMessage = "Slutdatum måste vara ett senare datum är startdatum.")]
        [PlaceHolder("T.ex. 2012-12-21")]
        [DisplayName("Gäller t.o.m.")]
        public DateTime EndDate { get; set; }

        [DisplayName("För boende")]
        public string Patient { get; set; }
        public IEnumerable<SelectListItem> PatientItems { get; set; }

        [DisplayName("För avdelning")]
        public string Taxon { get; set; }
        public IEnumerable<TaxonViewModel> Taxons { get; set; }

        public IList<Patient> ConnectedPatients { get; set; }

        [Required(ErrorMessage = "Boende måste väljas.")]
        public string[] Patients { get; set; }

        #endregion
    }
}