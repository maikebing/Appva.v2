// <copyright file="Observation.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
namespace Appva.Mcss.Admin.Domain.Entities
{
    using Newtonsoft.Json;
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
        public Observation(Patient patient, string name, string description, object scale)
        {
            Requires.NotNull            (patient,     "patient"    );
            Requires.NotNullOrWhiteSpace(name,        "name"       );
            Requires.NotNullOrWhiteSpace(description, "description");
            Requires.NotNull(scale,       "scale");
            this.Patient     = patient;
            this.Name        = name;
            this.Description = description;
            this.Scale       = JsonConvert.SerializeObject(scale);
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
    }
}