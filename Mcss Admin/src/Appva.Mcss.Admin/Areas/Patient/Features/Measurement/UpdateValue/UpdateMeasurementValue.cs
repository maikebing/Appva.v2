using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appva.Mcss.Admin.Models
{
    public class UpdateMeasurementValue : Identity<UpdateMeasurementValueModel>
    {
        public Guid ValueId { get; set; }
    }
}