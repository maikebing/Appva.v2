using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appva.Mcss.Web.ViewModels;
using DataAnnotationsExtensions;
using Appva.Mvc;
using System.ComponentModel;
using System.Web.Mvc;
using Appva.Mcss.Admin.Domain.Entities;

namespace Appva.Mcss.Web.ViewModels {

    public class FullReportViewModel : DateSpanViewModel {

        //public Guid? Taxon { get; set; }

        //public List<TaxonViewModel> Taxons { get; set; }

        public Guid? Schedule { get; set; }

        public IList<ScheduleSettings> Schedules { get; set; }

        public ReportViewModel Report { get; set; }

    }

}