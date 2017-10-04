using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appva.Mcss.Admin.Models
{
    public class UpdateMeasurementValueModel : Identity<ViewMeasurementModel>
    {
        public Guid ValueId { get; set; }
        public string Value { get; set; }
        public string Comment { get; set; }
    }
}