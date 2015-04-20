using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appva.Mcss.Admin.Domain.Entities;

namespace Appva.Mcss.Web.ViewModels {
    public class DelegationOverviewViewModel {

        public IEnumerable<DelegationExpired> DelegationsExpiresWithin50Days { get; set; }

        public IEnumerable<DelegationExpired> DelegationsExpired { get; set; }
    }

    public class DelegationExpired {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public int DaysLeft { get; set; }
    }
}