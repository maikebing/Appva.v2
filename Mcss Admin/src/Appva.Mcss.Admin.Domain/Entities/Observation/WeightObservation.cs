// <copyright file="WeightObservation.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Entities
{
    /// <summary>
    /// A weight observation.
    /// </summary>
    public class WeightObservation : Observation
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="WeightObservation"/> class.
        /// </summary>
        /// <param name="patient">The patient.</param>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        /// <param name="delegation">The delegation.</param>
        public WeightObservation(Patient patient, string name, string description, Taxon delegation = null)
            : base(patient, name, description, null, delegation)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WeightObservation"/> class.
        /// </summary>
        /// <remarks>An NHibernate visible no-argument constructor.</remarks>
        internal protected WeightObservation()
        {
        }

        #endregion

        #region Properties.

        //// UNRESOLVED: temporary solution

        /// <summary>
        /// The descriptive name of the scale in use.
        /// </summary>
        public virtual string ShortName
        {
            get
            {
                return "Weight";
            }
        }
        //// UNRESOLVED: temporary solution

        /// <summary>
        /// The descriptive name of the scale in use.
        /// </summary>
        public virtual string DescriptiveName
        {
            get
            {
                return "General Weight Scale (5-450 kg)";
            }
        }

        #endregion
    }
}
