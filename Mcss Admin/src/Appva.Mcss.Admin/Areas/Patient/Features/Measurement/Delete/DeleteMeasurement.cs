using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appva.Mcss.Admin.Models
{
    public class DeleteMeasurement : Identity<DeleteMeasurementModel>
    {
        public Guid MeasurementId { get; set; }
    }
}