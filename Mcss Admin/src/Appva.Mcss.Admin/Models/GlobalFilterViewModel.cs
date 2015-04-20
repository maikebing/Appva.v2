using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appva.Mcss.Web.ViewModels {
    public class GlobalFilterViewModel {
        public string RootName { get; set; }
        public Guid RootId { get; set; }
        public IList<TaxonViewModel> Items { get; set; }
    }
}