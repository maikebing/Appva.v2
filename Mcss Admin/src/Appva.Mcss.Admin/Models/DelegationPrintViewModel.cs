using System;
using System.Collections.Generic;
using Appva.Mcss.Admin.Domain.Entities;

namespace Appva.Mcss.Web.ViewModels {
    public class DelegationPrintViewModel {
        public Guid AccountId { get; set; }
        public Account Account { get; set; }
        public Account Identity { get; set; }
        public IList<Delegation> Delegations { get; set; }
        public IList<KnowledgeTest> KnowledgeTests { get; set; }
        public IList<Patient> Patients { get; set; }
    }
}