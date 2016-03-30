// <copyright file="CreateDelegationModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Models
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Mvc;
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
    public sealed class CreateDelegationModel : IRequest<ListDelegation>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateDelegationModel"/> class.
        /// </summary>
        public CreateDelegationModel()
        {
            Delegations = new Guid[]{};
            Patients = new string[] { };
            PatientItems = new List<SelectListItem>();
            DelegationsTaken = new HashSet<Guid>();
        }

        #endregion

        /// <summary>
        /// The Guid of the current Account to be delegated
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public HashSet<Guid> DelegationsTaken
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<SelectListItem> DelegationTypes
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "Datum måste fyllas i.")]
        [Date(ErrorMessage = "Datum måste fyllas i med åtta siffror och bindestreck, t. ex. 2012-12-21.")]
        [DateLessThan(Target = "EndDate", ErrorMessage = "Startdatum måste vara ett tidigare datum är slutdatum.")]
        [PlaceHolder("T.ex. 2012-12-21")]
        [DisplayName("Gäller fr.o.m.")]
        public DateTime StartDate
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "Datum måste fyllas i.")]
        [Date(ErrorMessage = "Datum måste fyllas i med åtta siffror och bindestreck, t. ex. 2012-12-21.")]
        [DateGreaterThan(Target = "StartDate", ErrorMessage = "Slutdatum måste vara ett senare datum är startdatum.")]
        [PlaceHolder("T.ex. 2012-12-21")]
        [DisplayName("Gäller t.o.m.")]
        public DateTime EndDate
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("För boende")]
        public string Patient
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<SelectListItem> PatientItems
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("För avdelning")]
        public string Taxon
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<TaxonViewModel> Taxons
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "Boende måste väljas.")]
        public string[] Patients
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "Delegering måste väljas.")]
        public Guid[] Delegations { get; set; }
        public Dictionary<ITaxon, IList<ITaxon>> DelegationTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// If this delegation should be valid for a specific 
        /// patient or part of the organisation
        /// </summary>
        public bool ValidForSpecificPatients
        {
            get;
            set;
        }
    }
}