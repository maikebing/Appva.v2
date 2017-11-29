// <copyright file="MeasurementObservation.cs" company="Appva AB">
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
    /// An implementation of Observation for Measurement Values
    /// </summary>
    /// <seealso cref="Appva.Mcss.Admin.Domain.Entities.Observation" />
    public class MeasurementObservation : Observation
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="MeasurementObservation"/> class.
        /// </summary>
        /// <param name="patient">The patient.</param>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        /// <param name="scale">The scale.</param>
        /// <param name="delegation">The optional delegation.</param>
        public MeasurementObservation(Patient patient, string name, string description, string scale, Taxon delegation = null)
            : base (patient, name, description, scale)
        {
            this.Delegation = delegation;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MeasurementObservation"/> class.
        /// </summary>
        /// <remarks>An NHibernate visible no-argument constructor.</remarks>
        internal protected MeasurementObservation()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The delegation.
        /// </summary>
        public virtual Taxon Delegation
        {
            get;
            internal protected set;
        }

        #endregion

        #region Public Methods.

        /// <summary>
        /// Updates the current measurement observation.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="instruction">The instruction.</param>
        public virtual void Update(string name, string instruction)
        {
            Requires.NotNullOrWhiteSpace(name, "name");
            Requires.NotNullOrWhiteSpace(instruction, "description");
            this.Name        = name;
            this.Description = instruction;
        }

        /// <summary>
        /// Updates the current measurement observation.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="instruction">The instruction.</param>
        /// <param name="delegation">The delegation.</param>
        public virtual void Update(string name, string instruction, Taxon delegation)
        {
            Requires.NotNullOrWhiteSpace(name, "name");
            Requires.NotNullOrWhiteSpace(instruction, "description");
            Requires.NotNull(delegation, "delegation");
            this.Name = name;
            this.Description = instruction;
            this.Delegation = delegation;
        }

        #endregion
    }
}
