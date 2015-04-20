using System;
using System.Collections.Generic;
using Appva.Mcss.Admin.Domain.Entities;

namespace Appva.Mcss.Web.ViewModels {
    public class DelegationListViewModel {
        public Guid AccountId { get; set; }
        public AccountViewModel Account { get; set; }
        public IDictionary<string, IList<Delegation>> DelegationMap { get; set; }
        public IDictionary<string, IList<KnowledgeTest>> KnowledgeTestMap { get; set; }
        public IList<string> HasKnowledgeTestList { get; set; }
        public IList<Patient> Patients { get; set; }
        public Dictionary<Taxon, IList<Taxon>> Template { get; set; }
    }
}