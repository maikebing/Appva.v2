using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appva.Mcss.Admin.Domain.Entities;

namespace Appva.Mcss.Web.ViewModels {
    public class ScheduleDetailsViewModel {
        public PatientViewModel Patient { get; set; }
        public Schedule Schedule { get; set; }
        public IList<Sequence> ScheduleItems { get; set; }
        //public IList<Delegation> Delegations { get; set; }
    }
}