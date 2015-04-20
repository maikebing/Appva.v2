using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appva.Mcss.Web.ViewModels
{
    public class RecountOverviewItemViewModel
    {
        public string Patient { get; set; }
        public Guid InventoryId { get; set; }
        public DateTime? LastRecount { get; set; }
        public string SequenceName { get; set; }

        public Guid PatientId { get; set; }
    }
}