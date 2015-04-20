using System;
using System.Collections.Generic;
using Appva.Mcss.Admin.Domain.Entities;

namespace Appva.Mcss.Web.ViewModels {

    public class ReportViewModel {

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public double TasksOnTime { get; set; }
        public double TasksNotOnTime { get; set; }

        public double ComparedDateSpanTasksOnTime { get; set; }
        public double ComparedDateSpanTasksNotOnTime { get; set; }

        public double AverageDifferenceInTime { get; set; }
        public double ComparedAverageDifferenceInTime { get; set; }

        public SearchViewModel<Task> Search { get; set; }

    }

}