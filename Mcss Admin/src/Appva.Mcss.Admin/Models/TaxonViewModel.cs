using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Appva.Mcss.Web.ViewModels {
    public class TaxonViewModel {
        public TaxonViewModel() {
            Taxons = new List<SelectListItem>();
        }
        public string Id { get; set; }
        public string Selected { get; set; }
        public string Label { get; set; }
        public string OptionLabel { get; set; }
        public IEnumerable<SelectListItem> Taxons { get; set; }
    }
}