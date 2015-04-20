using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appva.Mcss.Admin.Domain.Entities;

namespace Appva.Mcss.Web.ViewModels {
    public class PrepareSchemaViewModel {
        public PatientViewModel Patient { get; set; }
        public Schedule Schedule { get; set; }
        public IList<PreparedSequence> Sequences { get; set; }
        public IList<PreparedTask> Tasks { get; set; }
        public DateTime StartDate { get; set; }
        public int Week { get; set; }
        public bool ArchivedWeek { get; set; }
    }
}