using System;

namespace Appva.Mcss.Web.ViewModels {

    public class ChartDataPointViewModel {
        public DateTime Date { get; set; }
        public double OnTime { get; set; }
        public double NotOnTime { get; set; }
    }

}
