using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appva.Mcss.Admin.Domain.Entities;

namespace Appva.Mcss.Web.ViewModels {
    public class KnowledgeTestViewModel {
        public Guid AccountId { get; set; }
        public Guid TaxonId { get; set; }
        public IList<KnowledgeTest> Tests { get; set; }
    }
}