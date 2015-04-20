using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appva.Mcss.Admin.Domain.Entities;
using DataAnnotationsExtensions;
using Appva.Mvc.Html.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace Appva.Mcss.Web.ViewModels {
    public class DelegationReportViewModel : DateSpanViewModel {
        public Guid AccountId { get; set; }
        public AccountViewModel Account { get; set; }
        public Guid? DelegationId { get; set; }
        public IList<SelectListItem> Delegations { get; set; }
        public Guid? Schedule { get; set; }
        public IList<ScheduleSettings> Schedules { get; set; }
        public ReportViewModel Report { get; set; }
    }
}