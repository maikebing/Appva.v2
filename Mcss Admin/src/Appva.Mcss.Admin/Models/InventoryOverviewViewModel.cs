using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appva.Mcss.Admin.Domain.Entities;

namespace Appva.Mcss.Web.ViewModels {
    public class InventoryOverviewViewModel {

        public IList<RecountOverviewItemViewModel> StockCounts { get; set; }
        public IList<RecountOverviewItemViewModel> DelayedStockCounts { get; set; }
        public int StockControlIntervalInDays { get; set; }

    }
}