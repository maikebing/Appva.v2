using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Appva.Mcss.Admin.Domain.Entities;

namespace Appva.Mcss.Web.ViewModels
{
    public class PreparePrintViewModel
    {
        public Dictionary<DateTime, IDictionary<string, IDictionary<int, string>>> PrintSchedule { get; set; }
        public Patient Patient { get; set; }
        public Schedule Schedule { get; set; }
        public Dictionary<int, IDictionary<string, string>> Signatures { get; set; }

    }
}