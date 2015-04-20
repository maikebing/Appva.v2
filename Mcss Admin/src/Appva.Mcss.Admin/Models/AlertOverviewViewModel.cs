using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appva.Mcss.Admin.Domain.Entities;

namespace Appva.Mcss.Web.ViewModels {
    public class AlertOverviewViewModel {

        public IList<PatientViewModel> Patients { get; set; }

        public int CountAll { get; set; }

        public int CountNotSigned { get; set; }
    }
}