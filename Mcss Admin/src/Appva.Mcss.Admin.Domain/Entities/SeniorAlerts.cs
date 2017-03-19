using System;
using Appva.Common.Domain;

namespace Appva.Mcss.Admin.Domain.Entities
{

    /// <summary>
    /// Represents a change event for an instance.
    /// </summary>
    public class SeniorAlerts : Entity<SeniorAlerts>
    {

        #region Public Fields.

        /// <summary>
        /// The patient id.
        /// </summary>
        public virtual Guid? PatientId { get; set; }

        /// <summary>
        /// The taxon id.
        /// </summary>
        public virtual Guid? TaxonId { get; set; }

        #endregion
    }
}
