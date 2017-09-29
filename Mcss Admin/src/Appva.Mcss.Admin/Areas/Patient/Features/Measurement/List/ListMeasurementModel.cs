using Appva.Mcss.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appva.Mcss.Admin.Models
{
    public class ListMeasurementModel
    {
        public PatientViewModel Patient { get; set; }
        public IList<string> Names { get; set; }
        public IList<Domain.Entities.MeasurementObservation> MeasurementList { get; set; }
    }
}