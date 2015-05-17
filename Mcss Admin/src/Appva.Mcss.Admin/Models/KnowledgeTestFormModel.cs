using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;
using Appva.Mvc;

namespace Appva.Mcss.Web.ViewModels {
    public class KnowledgeTestFormModel {
        public Guid AccountId { get; set; }
        public Guid TaxonId { get; set; }

        [Required(ErrorMessage = "Aktivitetens namn måste fyllas i.")]
        [DisplayName("Kunskapstest")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Datum måste fyllas i.")]
        [Date(ErrorMessage = "Datum måste fyllas i med åtta siffror och bindestreck, t. ex. 2012-12-21.")]
        [PlaceHolder("T.ex. 2012-12-21")]
        [DisplayName("Genomfördes")]
        public DateTime? CompletedDate { get; set; }
    }
}