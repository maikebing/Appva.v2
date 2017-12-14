// <copyright file="FecesObservation.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Entities
{
    /// <summary>
    /// A feces observation.
    /// </summary>
    public class FecesObservation : Observation
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="FecesObservation"/> class.
        /// </summary>
        /// <param name="patient">The patient.</param>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        /// <param name="delegation">The delegation.</param>
        public FecesObservation(Patient patient, string name, string description, Taxon delegation = null)
            : base(patient, name, description, null, delegation)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FecesObservation"/> class.
        /// </summary>
        /// <remarks>An NHibernate visible no-argument constructor.</remarks>
        internal protected FecesObservation()
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
                return "Feces";
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
                return "General stool scale (Type AAA-d)";
            }
        }

        #endregion
    }
}
