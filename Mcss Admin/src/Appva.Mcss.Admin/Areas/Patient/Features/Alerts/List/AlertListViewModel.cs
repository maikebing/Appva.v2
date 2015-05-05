using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Appva.Mcss.Admin.Domain.Entities;

namespace Appva.Mcss.Web.ViewModels {
    public class AlertListViewModel {
        public PatientViewModel Patient { get; set; }
        public Dictionary<DateTime, Dictionary<Schedule, IList<Task>>> TaskEarlierMap { get; set; }
        public Dictionary<DateTime, Dictionary<Schedule, IList<Task>>> TaskCurrentMap { get; set; }
        public IEnumerable<SelectListItem> Years { get; set; }
        public IEnumerable<SelectListItem> Months { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int? Year
        {
            get;
            set;
        }
        public int? Month
        {
            get;
            set;
        }
    }
}