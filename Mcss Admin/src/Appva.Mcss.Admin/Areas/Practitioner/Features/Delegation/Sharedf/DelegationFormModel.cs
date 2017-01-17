// <copyright file="DelegationFormModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Models
{


    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Areas.Models;
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
    public class DelegationFormModel : IRequest<ListDelegation>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegationFormModel"/> class.
        /// </summary>
        public DelegationFormModel()
        {
            Delegations = new Guid[] { };
            Patients = new string[] { };
            PatientItems = new List<SelectListItem>();
            //DelegationsTaken = new HashSet<Guid>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// The id?
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// The account id
        /// </summary>
        public Guid AccountId
        {
            get;
            set;
        }

        /// <summary>
        /// The start date, when delegations becomes active
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
        /// The end date, when the delegationexpires
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
        /// Structural for choosing patient 
        /// </summary>
        [DisplayName("För boende")]
        public string Patient 
        { 
            get; 
            set;
        }

        /// <summary>
        /// The patients this delegation is valid for
        /// </summary>
        [Required(ErrorMessage = "Boende måste väljas.")]
        public string[] Patients 
        {
            get; 
            set;
        }
        
        /// <summary>
        /// The organization-taxon this delegation is valid for
        /// </summary>
        [DisplayName("För avdelning")]
        public string OrganizationTaxon 
        { 
            get;
            set;
        }

        /// <summary>
        /// The delegations to add
        /// </summary>
        [Required(ErrorMessage = "Delegering måste väljas.")]
        public Guid[] Delegations 
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

        #endregion

        #region Lists.

        /// <summary>
        /// All available patients
        /// </summary>
        public IEnumerable<SelectListItem> PatientItems 
        {
            get;
            set; 
        }

        /// <summary>
        /// All available organisation-taxons
        /// </summary>
        public IEnumerable<TaxonViewModel> OrganizationTaxons 
        { 
            get;
            set;
        }

        /// <summary>
        /// All available delegations
        /// </summary>
        public Dictionary<ITaxon, IList<ITaxon>> DelegationTemplate 
        { 
            get;
            set;
        }

        #endregion
    }
}