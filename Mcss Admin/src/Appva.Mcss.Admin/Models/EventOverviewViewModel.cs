using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appva.Mcss.Admin.Domain.Entities;

namespace Appva.Mcss.Web.ViewModels {
    public class EventOverviewViewModel {

        public IList<Task> Activities { get; set; }
        public IDictionary<int, ScheduleSettings> Categories { get; set; }
        public int CalendarColors { get; set; }
    }
}