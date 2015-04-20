using System;
using System.Collections.Generic;
using Appva.Mcss.Admin.Domain.Entities;

namespace Appva.Mcss.Web.ViewModels {
    public class DelegationRevisionViewModel {
        public Guid AccountId { get; set; }
        public AccountViewModel Account { get; set; }
        public ChangeSet ChangeSet { get; set; }
        public IList<ChangeSet> ChangeSets { get; set; }
        public Delegation Delegation { get; set; }
        public DateTime? Date { get; set; }
    }
}