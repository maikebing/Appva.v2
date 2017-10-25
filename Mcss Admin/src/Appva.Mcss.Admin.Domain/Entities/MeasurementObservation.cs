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
        public MeasurementObservation(Patient patient, string name, string description, string scale, Taxon delegation, Sequence sequence = null, Taxon category = null)
            : base (patient, name, description, scale, sequence, category)
        {
            Requires.NotNullOrWhiteSpace(scale, "scale");
            Requires.NotNull(delegation, "delegation");
            this.Delegation = delegation;
        }
        public MeasurementObservation()
        {
        }

        /// <summary>
        /// The delegation.
        /// </summary>
        public virtual Taxon Delegation
        {
            get;
            set;
        }

        /// <summary>
        /// News the specified patient.
        /// </summary>
        /// <param name="patient">The patient.</param>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        /// <param name="scale">The scale.</param>
        /// <param name="delegation">The delegation.</param>
        /// <param name="sequence">The sequence.</param>
        /// <param name="category">The category.</param>
        /// <returns>MeasurementObservation.</returns>
        public static MeasurementObservation New(Patient patient, string name, string description, string scale, Taxon delegation, Sequence sequence = null, Taxon category = null)
        {
            return new MeasurementObservation(patient, name, description, scale, delegation, sequence, category);
        }
    }
}
