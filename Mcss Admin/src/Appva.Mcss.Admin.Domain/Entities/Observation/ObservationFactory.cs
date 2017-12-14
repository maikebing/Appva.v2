// <copyright file="ObservationFactory.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using Validation;

    #endregion

    /// <summary>
    /// Class ObservationFactory.
    /// </summary>
    public static class ObservationFactory
    {
        /// <summary>
        /// Creates the new.
        /// </summary>
        /// <param name="patient">The patient.</param>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        /// <param name="scale">The scale.</param>
        /// <param name="delegation">The delegation.</param>
        /// <returns>Observation.</returns>
        public static Observation CreateNew(Patient patient, string name, string description, string scale, Taxon delegation = null)
        {
            Requires.NotNullOrEmpty(scale, "scale");
            switch (scale.ToLower())
            {
                case "feces"  :
                case "common" : return new FecesObservation  (patient, name, description, delegation);
                case "weight" : return new WeightObservation (patient, name, description, delegation);
                case "bristol": return new BristolObservation(patient, name, description, delegation);
                default: return null;
            }
        }
    }
}
