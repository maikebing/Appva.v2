using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Appva.Mcss.Admin.Domain.Entities;

namespace Appva.Mcss.Web.ViewModels {
    public class TaskListViewModel {
        public PatientViewModel Patient { get; set; }
        public ScheduleSettings Schedule { get; set; }
        public IList<ScheduleSettings> ActiveScheduleSettings
        {
            get;
            set;
        }
        public IList<ScheduleSettings> InactiveScheduleSettings
        {
            get;
            set;
        }
        public IList<ScheduleSettings> Schedules { get; set; }
        public SearchViewModel<Task> Search { get; set; }
        public bool FilterByAnomalies { get; set; }
        public bool FilterByNeedsBasis { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public OrderTasksBy Order { get; set; }
        public int? Year { get; set; }
        public int? Month { get; set; }
        public IEnumerable<SelectListItem> Years { get; set; }
        public IEnumerable<SelectListItem> Months { get; set; }
    }

    public enum OrderTasksBy { 
        Day,
        Medecin,
        Time,
        Scheduled,
        SignedBy,
        Status
    }
}