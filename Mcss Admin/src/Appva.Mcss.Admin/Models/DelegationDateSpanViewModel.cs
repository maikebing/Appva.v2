using System;

namespace Appva.Mcss.Web.ViewModels {

    public class DelegationDateSpanViewModel : DateSpanViewModel {
        public DelegationDateSpanViewModel() {
            StartDate = DateTime.Now.Date;
            EndDate = DateTime.Now.AddYears(1);

        }
        public Guid Id { get; set; }
        public Guid TaxonId { get; set; }
    }

}