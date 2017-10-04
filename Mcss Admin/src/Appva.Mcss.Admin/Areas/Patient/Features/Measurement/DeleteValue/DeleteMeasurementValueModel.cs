using Appva.Mcss.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appva.Mcss.Admin.Models
{
    public class DeleteMeasurementValueModel : Identity<ViewMeasurementModel>
    {
        public Guid ValueId { get; set; }
        public bool DeleteConfirmed { get; set; }
    }
}