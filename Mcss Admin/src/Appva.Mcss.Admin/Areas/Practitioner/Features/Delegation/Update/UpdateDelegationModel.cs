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
    using Appva.Cqrs;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class UpdateDelegationModel : IRequest<ListDelegation>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateDelegationModel"/> class.
        /// </summary>
        public UpdateDelegationModel()
        {
            Patients = new string[] { };
            ConnectedPatients = new List<Patient>();
            PatientItems = new List<SelectListItem>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// The account id
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// The startdate
        /// </summary>
        [Required(ErrorMessage = "Datum måste fyllas i.")]
        [Date(ErrorMessage = "Datum måste fyllas i med åtta siffror och bindestreck, t. ex. 2012-12-21.")]
        [DateLessThan(Target = "EndDate", ErrorMessage = "Startdatum måste vara ett tidigare datum är slutdatum.")]
        [DisplayName("Gäller fr.o.m.")]
        public DateTime StartDate
        {
            get;
            set;
        }

        /// <summary>
        /// The enddate
        /// </summary>
        [Required(ErrorMessage = "Datum måste fyllas i.")]
        [Date(ErrorMessage = "Datum måste fyllas i med åtta siffror och bindestreck, t. ex. 2012-12-21.")]
        [DateGreaterThan(Target = "StartDate", ErrorMessage = "Slutdatum måste vara ett senare datum är startdatum.")]
        [DisplayName("Gäller t.o.m.")]
        public DateTime EndDate
        {
            get;
            set;
        }

        /// <summary>
        /// The patients
        /// </summary>
        [DisplayName("För boende")]
        public string Patient
        {
            get;
            set;
        }

        /// <summary>
        /// The oganization-taxon
        /// </summary>
        [DisplayName("För avdelning")]
        public string OrganizationTaxon
        {
            get;
            set;
        }

        /// <summary>
        /// The patients, helper for view
        /// </summary>
        public IList<Patient> ConnectedPatients
        {
            get;
            set;
        }

        /// <summary>
        /// The patients, hidden field
        /// </summary>
        [Required(ErrorMessage = "Boende måste väljas.")]
        public string[] Patients
        {
            get;
            set;
        }

        /// <summary>
        /// If the delegation is valid for specific patient
        /// </summary>
        public bool ValidForSpecificPatients
        {
            get;
            set;
        }

        #region Collections.

        /// <summary>
        /// Available patiens
        /// </summary>
        public IEnumerable<SelectListItem> PatientItems
        {
            get;
            set;
        }

        /// <summary>
        /// The organization taxons
        /// </summary>
        public IEnumerable<TaxonViewModel> OrganizationTaxons
        {
            get;
            set;
        }

        #endregion

        #endregion
    }
}