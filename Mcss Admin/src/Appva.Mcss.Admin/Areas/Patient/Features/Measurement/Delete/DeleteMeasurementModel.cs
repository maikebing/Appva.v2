using Appva.Mcss.Admin.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appva.Mcss.Admin.Models
{
    public class DeleteMeasurementModel : Identity<ListMeasurementModel>
    {
        public MeasurementObservation Observation { get; set; }
    }
}