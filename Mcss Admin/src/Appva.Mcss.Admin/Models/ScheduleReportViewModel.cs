using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appva.Mcss.Admin.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;
using Appva.Mvc;
using System.ComponentModel;
using Appva.Mcss.Admin.Application.Models;
using Appva.Repository;

namespace Appva.Mcss.Web.ViewModels {

    public class ScheduleReportViewModel : DateSpanViewModel {
        public PatientViewModel Patient { get; set; }
        public Guid? Schedule { get; set; }
        public IList<ScheduleSettings> Schedules { get; set; }
        public ReportData Report { get; set; }
        public PageableSet<Task> Tasks { get; set; }
    }

}