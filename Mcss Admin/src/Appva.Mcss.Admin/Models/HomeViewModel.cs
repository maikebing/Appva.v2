using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Appva.Mcss.Admin.Domain.Entities;

namespace Appva.Mcss.Web.ViewModels {
    
    public class HomeViewModel {

        //public Taxon Taxon { get; set; }

        //public List<TaxonViewModel> Taxons { get; set; }

        public IList<PatientViewModel> RaisedAlertsForPatients { get; set; }

        public IList<Sequence> StockCounts { get; set; }

        public IList<Sequence> DelayedStockCounts { get; set; }

        public int StockControlIntervalInDays { get; set; }


        public DateTime SevenDayStartDate { get; set; }
        public DateTime SevenDayEndDate { get; set; }

        public bool HasCalendarOverview { get; set; }
        public bool HasOrderOverview { get; set; }

    }

    

}