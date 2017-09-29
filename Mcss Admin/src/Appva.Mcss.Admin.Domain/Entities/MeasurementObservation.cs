using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validation;

namespace Appva.Mcss.Admin.Domain.Entities
{
    public class MeasurementObservation : Observation
    {
        public MeasurementObservation(string scale, Taxon delegation, Patient patient, string name, string description, Taxon category = null)
            : base (patient, name, description, category)
        {
            Requires.NotNullOrWhiteSpace(scale, "scale");
            Requires.NotNull(delegation, "delegation");

            this.Scale = scale;
            this.Delegation = delegation;
        }
        public MeasurementObservation()
        {

        }

        public virtual string Scale { get; set; }
        public virtual Taxon Delegation { get; set; }

        public static MeasurementObservation New(string scale, Taxon delegation, Patient patient, string name, string description, Taxon category = null)
        {
            return new MeasurementObservation(scale, delegation, patient, name, description, category);
        }
    }
}
