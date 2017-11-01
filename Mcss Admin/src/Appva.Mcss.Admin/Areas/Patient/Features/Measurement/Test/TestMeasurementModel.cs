using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appva.Mcss.Admin.Domain.Entities;
using Appva.Mcss.Web.ViewModels;

namespace Appva.Mcss.Admin.Models
{
    public class TestMeasurementModel
    {
        public Guid Id { get; set; }
        public Guid MeasurementId { get; set; }
        public PatientViewModel PatientViewModel { get; set; }
        public IList<MeasurementObservation> MeasurementList { get; set; }
        public IList<ObservationItem> MeasurementValueList { get; set; }
        public string MeasurementUnit { get; set; }
        public string MeasurementLongScale { get; set; }
        public string MeasurementScale { get; set; }
        public Taxon Delegation { get; set; }
        public string MeasurementName { get; set; }
        public string MeasurementInstructions { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}