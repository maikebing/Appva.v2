using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appva.Mcss.Admin.Domain.Entities;

namespace Appva.Mcss.Web.ViewModels {
    public class ScheduleListViewModel {
        public PatientViewModel Patient { get; set; }
        public IList<Schedule> Schedules { get; set; }
    }
}