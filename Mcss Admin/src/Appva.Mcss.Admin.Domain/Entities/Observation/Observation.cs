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
    public abstract class Observation : EventSourced
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
            //// Code <-- 
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
        /// <remarks>
        /// Populated by NHibernate.
        /// </remarks>
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
        /// Takes a measurement value, e.g. measures the length or weight, as either
        /// structs or enumerations.
        /// </summary>
        /// <typeparam name="T">The value type.</typeparam>
        /// <param name="who">The practitioner who performed the measurement operation.</param>
        /// <param name="value">The struct value quantity.</param>
        /// <param name="unit">Optional unit, if the observation allows it.</param>
        public virtual void TakeMeasurement<T>(Account who, T value, IUnitOfMeasurement unit = null) where T : struct
        {
            this.AddObservationMeasurement(who, this.NewMeasurement(value, unit));
        }

        /// <summary>
        /// Takes a measurement value, e.g. measures the length or weight, as a string.
        /// </summary>
        /// <typeparam name="T">The value type.</typeparam>
        /// <param name="who">The practitioner who performed the measurement operation.</param>
        /// <param name="value">The struct value quantity.</param>
        /// <param name="unit">Optional unit, if the observation allows it.</param>
        public virtual void TakeMeasurement(Account who, string value, IUnitOfMeasurement unit = null)
        {
            this.AddObservationMeasurement(who, this.NewMeasurement(value, unit));
        }

        /// <summary>
        /// Creates and signs a new observation item.
        /// </summary>
        /// <param name="who">The practitioner who performed the measurement operation.</param>
        /// <param name="measurement">The measured value.</param>
        protected void AddObservationMeasurement(Account who, IArbituraryValue measurement)
        {
            var signature = Signature.New(who, measurement.ToSignedData());
            var item      = ObservationItem.New(this, measurement, null, signature, null);
            this.Items.Add(item);
        }

        /// <summary>
        /// Creates a new measurement value.
        /// </summary>
        /// <typeparam name="T">The type of value.</typeparam>
        /// <param name="value">The value.</param>
        /// <param name="unit">Optional unit, if allowed.</param>
        /// <returns>A new <see cref="IArbituraryValue"/> instance.</returns>
        protected abstract IArbituraryValue NewMeasurement<T>(T value, IUnitOfMeasurement unit = null) where T : struct;

        /// <summary>
        /// Creates a new measurement value.
        /// </summary>
        /// <typeparam name="T">The type of value.</typeparam>
        /// <param name="value">The value.</param>
        /// <param name="unit">Optional unit, if allowed.</param>
        /// <returns>A new <see cref="IArbituraryValue"/> instance.</returns>
        protected abstract IArbituraryValue NewMeasurement(string value, IUnitOfMeasurement unit = null);

        /// <summary>
        /// Updates the current measurement observation.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="instruction">The instruction.</param>
        /// <param name="delegation">The delegation.</param>
        public virtual void Update(string name, string instruction, Taxon delegation = null)
        {
            Requires.NotNullOrWhiteSpace(name,        "name");
            Requires.NotNullOrWhiteSpace(instruction, "description");
            this.Name        = name;
            this.Description = instruction;
            this.Delegation  = delegation;
        }

        #endregion
    }
}