using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;
using Appva.Mcss.Admin.Domain.Entities;
using Appva.Mvc.Html.DataAnnotations;

namespace Appva.Mcss.Web.ViewModels {
    
    public class DelegationViewModel {

        public DelegationViewModel() { 
            Delegations = new Guid[]{};
            Patients = new string[] { };
            PatientItems = new List<SelectListItem>();
            DelegationsTaken = new HashSet<Guid>();
        }

        public Guid Id { get; set; }
        public Guid AccountId { get; set; }

        public HashSet<Guid> DelegationsTaken { get; set; }

        [RequiredIf(Target = "CreateNew", Value = false, ErrorMessage = "Delegering måste väljas.")]
        [DisplayName("Ny Delegering")]
        public string Delegation { get; set; }

        [RequiredIf(Target = "CreateNew", Value = false, ErrorMessage = "Delegeringstyp måste väljas.")]
        [DisplayName("Delegeringstyp")]
        public string DelegationType { get; set; }

        public IEnumerable<SelectListItem> DelegationTypes { get; set; }

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

        [Required(ErrorMessage = "Boende måste väljas.")]
        public string[] Patients { get; set; }

        [RequiredIf(Target = "CreateNew", Value = true, ErrorMessage = "Delegering måste väljas.")]
        public Guid[] Delegations { get; set; }
        public Dictionary<Taxon, IList<Taxon>> DelegationTemplate { get; set; }

        public bool CreateNew { get; set; }
    }

    public class DelegationEditViewModel {

        public DelegationEditViewModel() {
            Patients = new string[] { };
            ConnectedPatients = new List<Patient>();
            PatientItems = new List<SelectListItem>();
        }

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


    }

}