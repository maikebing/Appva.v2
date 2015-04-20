using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;
using Appva.Mvc.Html.DataAnnotations;
using Appva.Mcss.Admin.Domain.Entities;

namespace Appva.Mcss.Web.ViewModels {

    public class PatientFormViewModel {

        public Guid? PatientId { get; set; }

        [Required(ErrorMessage = "Förnamn måste fyllas i.")]
        [DisplayName("Förnamn")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Efternamn måste fyllas i.")]
        [DisplayName("Efternamn")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Personnummer måste fyllas i.")]
        [RegularExpression(@"^(18|19|20)\d{2}((0[1-9])|(1[0-2]))(([0-2][0-9])|(3[0-1])|([6-8][0-9])|(9[0-1]))-?[0-9pPtTfF][0-9]{3}$", ErrorMessage = "Personnummer måste fyllas i med tolv siffror och bindestreck, t. ex. 19010101-0001.")]
        [DisplayName("Personnummer")]
        public string UniqueIdentifier { get; set; }

        [DisplayName("Avliden")]
        public bool Deceased { get; set; }

        [Required(ErrorMessage = "Address måste väljas.")]
        [Remote("VerifyTaxon", "Taxonomy", HttpMethod = "POST", ErrorMessage = "Address måste väljas.")]
        public string Taxon { get; set; }

        [DisplayName("Tag")]
        public string Tag { get; set; }

        public IEnumerable<TaxonViewModel> Taxons { get; set; }

        public IEnumerable<Taxon> SeniorAlerts { get; set; }
        public Guid[] PatientSeniorAlerts { get; set; }

        //Settings
        public bool HasTagIdentifier { get; set; }

    }
}