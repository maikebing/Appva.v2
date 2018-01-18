// <copyright file="Administration.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using System.Collections.Generic;
    using System.Linq;
    using Appva.Core.Extensions;
    using Appva.Mcss.Admin.Domain;
    using Newtonsoft.Json;
    using Validation;

    #endregion

    /// <summary>
    /// An abstract base administration.
    /// </summary>
    /// <remarks>
    /// A set of Instructions such as Route and Method might be added in the future.
    /// </remarks>
    public abstract class Administration : EventSourced
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="Administration"/> class.
        /// </summary>
        /// <param name="sequence">The <see cref="Sequence"/>. Required.</param>
        /// <param name="unit">The <see cref="UnitOfMeasurement"/>. Required.</param>
        /// <param name="maxValue">The maximum value. Must be greater than or equal the minmum value.</param>
        /// <param name="minValue">The minimum value, default 0. Must be lesser than or equal to the mimum value.</param>
        /// <param name="step">The step, incrementation, default 1</param>
        /// <param name="fractions">The amount of fractions, default 0.</param>
        protected internal Administration(string name, Sequence sequence, UnitOfMeasurement unit, double maxValue, double minValue = 0, double step = 1, int fractions = 0)
        {
            Requires.NotNullOrWhiteSpace(name, "name");
            Requires.NotNull(sequence, "sequence");
            Requires.NotNull(unit, "unit");
            Requires.ValidState(maxValue >= minValue, "maxValue");
            Requires.ValidState(minValue <= maxValue, "minValue");
            Requires.ValidState(step > 0.00, "step");
            Requires.ValidState((fractions == 0 || fractions == 1 || fractions == 2), "fractions");
            this.Name = name;
            this.Patient = sequence.Patient;
            this.Sequence = sequence;
            this.Unit = unit;
            this.MaxValue = maxValue;
            this.MinValue = minValue;
            this.Step = step;
            this.Fractions = fractions;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Administration"/> class.
        /// </summary>
        /// <param name="sequence">The <see cref="Sequence"/>.</param>
        /// <param name="unit">The <see cref="UnitOfMeasurement"/>.</param>
        /// <param name="specificValues">The specific list of values.</param>
        protected internal Administration(string name, Sequence sequence, UnitOfMeasurement unit, IEnumerable<double> specificValues)
        {
            Requires.NotNullOrWhiteSpace(name, "name");
            Requires.NotNull(sequence, "sequence");
            Requires.NotNull(unit, "unit");
            Requires.NotNullOrEmpty(specificValues, "specificValues");
            this.Name = name;
            this.Patient = sequence.Patient;
            this.Sequence = sequence;
            this.Unit = unit;
            this.SpecificValues = JsonConvert.SerializeObject(specificValues);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Administration"/> class.
        /// </summary>
        /// <remarks>
        /// An NHibernate visible no-argument constructor.
        /// </remarks>
        protected internal Administration()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public virtual string Name
        {
            get;
            protected internal set;
        }

        /// <summary>
        /// Gets or sets the patient.
        /// </summary>
        /// <value>The patient.</value>
        public virtual Patient Patient
        {
            get;
            protected internal set;
        }

        /// <summary>
        /// Gets or sets the sequence.
        /// </summary>
        /// <value>The sequence.</value>
        public virtual Sequence Sequence
        {
            get;
            protected internal set;
        }

        /// <summary>
        /// The unit.
        /// </summary>
        /// <value>The <see cref="UnitOfMeasurement"/>.</value>
        public virtual UnitOfMeasurement Unit
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the maximum value.
        /// </summary>
        public virtual double MaxValue
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the minimum value.
        /// </summary>
        public virtual double MinValue
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the step.
        /// </summary>
        public virtual double Step
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the fractions.
        /// </summary>
        public virtual int Fractions
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the custom values.
        /// </summary>
        /// <value>The custom values.</value>
        public virtual string SpecificValues
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the administered items.
        /// </summary>
        /// <value>The administered items.</value>
        public virtual IList<AdministeredItem> AdministeredItems
        {
            get;
            protected internal set;
        }

        /// <summary>
        /// Gets the unproxied Administration.
        /// </summary>
        /// <value>The unproxied Administration.</value>
        public virtual Administration Unproxied
        {
            get
            {
                return this;
            }
        }

        #endregion

        #region Public Methods.

        /// <summary>
        /// Administers the specified Administration.
        /// </summary>
        /// <param name="who">The <see cref="Account"/>.</param>
        /// <param name="task">The <see cref="Task"/>.</param>
        /// <param name="value">The value.</param>
        /// <param name="unit">The <see cref="UnitOfMeasurement"/>.</param>
        /// <returns><see cref="AdministeredItem"/>.</returns>
        public virtual AdministeredItem Administer(Account who, Task task, double value)
        {
            return this.AddItem(who, task, this.NewValue(value));
        }

        /// <summary>
        /// Get a custom values list
        /// </summary>
        /// <returns>A list of double</returns>
        public virtual IList<double> GetCustomValues()
        {
            return JsonConvert.DeserializeObject<List<double>>(this.SpecificValues);
        }

        /// <summary>
        /// Adds the <see cref="AdministeredItem"/>.
        /// </summary>
        /// <param name="who">The <see cref="Account"/>.</param>
        /// <param name="task">The <see cref="Task"/>.</param>
        /// <param name="value">The <see cref="ArbitraryValue"/>.</param>
        /// <returns><see cref="AdministeredItem"/>.</returns>
        protected virtual AdministeredItem AddItem(Account who, Task task, ArbitraryValue value)
        {
            Requires.NotNull(who, "who");
            Requires.NotNull(task, "task");
            Requires.NotNull(value, "arbitrary");
            var signature = Signature.New(who, value.ToSignedData());
            var item = AdministeredItem.New(this, task, value, signature);
            this.AdministeredItems.Add(item);
            return item;
        }

        /// <summary>
        /// Updates the administration with a unit and a specified list of values.
        /// </summary>
        /// <param name="unit">The <see cref="UnitOfMeasurement"/>.</param>
        /// <param name="customValues">The list of custom values.</param>
        /// <returns><see cref="Administration"/>.</returns>
        public abstract Administration Update(UnitOfMeasurement unit, IList<double> customValues);

        /// <summary>
        /// Updates the administration with a unit and a formula for building the values.
        /// </summary>
        /// <param name="unit">The <see cref="UnitOfMeasurement"/>.</param>
        /// <param name="max">The maximum value.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="step">The step/increment.</param>
        /// <param name="fractions">The fractions.</param>
        /// <returns>Administration.</returns>
        public abstract Administration Update(UnitOfMeasurement unit, double max, double min = 0, double step = 1, int fractions = 0);

        /// <summary>
        /// News the value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="unit">The unit.</param>
        /// <returns>ArbitraryValue.</returns>
        protected abstract ArbitraryValue NewValue(double value);

        #endregion
    }
}