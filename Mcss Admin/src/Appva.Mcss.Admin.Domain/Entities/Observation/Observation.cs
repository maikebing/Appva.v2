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
        /// <param name="category">Classification of type of observation.</param>
        public Observation(Patient patient, string name, string description, Taxon category = null)
        {
            Requires.NotNull            (patient,     "patient"    );
            Requires.NotNullOrWhiteSpace(name,        "name"       );
            Requires.NotNullOrWhiteSpace(description, "description");
            this.Patient     = patient;
            this.Name        = name;
            this.Description = description;
            this.Category    = category;
            this.RegisterEvent(ObservationCreatedEvent.New(this));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Observation"/> class.
        /// </summary>
        /// <remarks>
        /// An NHibernate visible no-argument constructor.
        /// </remarks>
        protected Observation()
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
        /// The observation category.
        /// </summary>
        public virtual Taxon Category
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

        #endregion

        #region Public Static Builders.

        /// <summary>
        /// Creates a new instance of the <see cref="Observation"/> class.
        /// </summary>
        /// <param name="patient">The patient which the observation is made for.</param>
        /// <param name="name">The name of the observation.</param>
        /// <param name="description">The description or instruction.</param>
        /// <param name="category">Classification of type of observation.</param>
        /// <returns>A new <see cref="Observation"/> instance.</returns>
        public static Observation New(Patient patient, string name, string description, Taxon category = null)
        {
            return new Observation(patient, name, description, category);
        }

        #endregion
    }
}