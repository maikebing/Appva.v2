// <copyright file="Observation.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using Validation;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class Observation : EventSourced
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="Observation"/> class.
        /// </summary>
        /// <param name="patient">The patient which the observation is made for.</param>
        /// <param name="name">The name of the observation.</param>
        /// <param name="description">The description or instruction.</param>
        /// <param name="scale">The scale used in the observation.</param>
        public Observation(Patient patient, string name, string description, object scale, Taxon delegation = null) //// UNRESOLVED: custom scale used by DosageObservation
        {
            Requires.NotNull            (patient,     "patient"    );
            Requires.NotNullOrWhiteSpace(name,        "name"       );
            Requires.NotNullOrWhiteSpace(description, "description");
            this.Patient     = patient;
            this.Name        = name;
            this.Description = description;
            this.Delegation  = delegation;
            this.Scale       = scale == null ? null : scale.ToString();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Observation"/> class.
        /// </summary>
        /// <remarks>
        /// An NHibernate visible no-argument constructor.
        /// </remarks>
        internal protected Observation()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The observation patient reference.
        /// </summary>
        public virtual Patient Patient
        {
            get;
            internal protected set;
        }

        /// <summary>
        /// The observation name.
        /// </summary>
        public virtual string Name
        {
            get;
            internal protected set;
        }

        /// <summary>
        /// The observation description.
        /// </summary>
        public virtual string Description
        {
            get;
            internal protected set;
        }

        /// <summary>
        /// The delegation.
        /// </summary>
        public virtual Taxon Delegation
        {
            get;
            internal protected set;
        }

        /// <summary>
        /// The scale
        /// TODO: Break out to a new entity : IUnit
        /// </summary>
        public virtual string Scale
        {
            get;
            internal protected set;
        }

        /// <summary>
        /// The collection of observation items.
        /// </summary>
        /// <remarks>Populated by NHibernate.</remarks>
        public virtual IList<ObservationItem> Items
        {
            get;
            internal protected set;
        }

        /// <summary>
        /// Gets or sets the sequences.
        /// </summary>
        /// <remarks>Populated by NHibernate.</remarks>
        public virtual IList<Sequence> Sequences
        {
            get;
            internal protected set;
        }

        /// <summary>
        /// Gets the unproxied Observation.
        /// </summary>
        /// <value>The unproxied Obaervation.</value>
        public virtual Observation UnProxied
        {
            get
            {
                return this;
            }                
        }

        #endregion

        #region Public Methods.

        /// <summary>
        /// Updates the current tena period.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="instruction">The instruction.</param>
        public virtual void Update(string name, string instruction)
        {
            Requires.NotNullOrWhiteSpace(name,        "name");
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
            Requires.NotNullOrWhiteSpace(name,        "name");
            Requires.NotNullOrWhiteSpace(instruction, "description");
            Requires.NotNull(            delegation,  "delegation");
            this.Name        = name;
            this.Description = instruction;
            this.Delegation  = delegation;
        }

        #endregion
    }
}