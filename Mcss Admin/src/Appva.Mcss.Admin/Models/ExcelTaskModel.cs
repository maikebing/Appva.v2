using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appva.Mcss.Web.Controllers {
    public class ExcelTaskModel {
        public string Task { get; set; }
        public DateTime TaskCompletedOnDate { get; set; }
        public string TaskCompletedOnTime { get; set; }
        public DateTime TaskScheduledOnDate { get; set; }
        public string TaskScheduledOnTime { get; set; }
        public int MinutesBefore { get; set; }
        public int MinutesAfter { get; set; }
        public string PatientFullName { get; set; }
        public string CompletedBy { get; set; }
        public string TaskCompletionStatus { get; set; }
    }
}