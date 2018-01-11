// <copyright file="Administration.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using System.Collections.Generic;
    using Appva.Core.Extensions;
    using Appva.Mcss.Admin.Domain;
    using Newtonsoft.Json;
    using Validation;

    #endregion

    /// <summary>
    /// An abstract base administration.
    /// </summary>
    public abstract class Administration : EventSourced
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="Administration"/> class.
        /// </summary>
        /// <param name="patient">The <see cref="Patient"/>.</param>
        /// <param name="sequence">The <see cref="Sequence"/>.</param>
        /// <param name="unit">The <see cref="UnitOfMeasurement"/>.</param>
        protected internal Administration(Sequence sequence, UnitOfMeasurement unit, IList<double> customValues = null)
        {
            Requires.NotNull(sequence, "sequence");
            Requires.NotNull(unit, "unit");
            this.Patient = sequence.Patient;
            this.Sequence = sequence;
            this.Unit = unit;
            this.Name = sequence.Name;
            this.CustomValues = ComposeCustomValues(customValues);
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
        /// Gets or sets the unit.
        /// </summary>
        /// <value>The unit.</value>
        public virtual UnitOfMeasurement Unit
        {
            get;
            protected internal set;
        }

        /// <summary>
        /// Gets or sets the route.
        /// </summary>
        /// <value>The route.</value>
        public virtual string Route
        {
            get;
            protected internal set;
        }

        /// <summary>
        /// Gets or sets the method.
        /// </summary>
        /// <value>The method.</value>
        public virtual string Method
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
        /// Gets or sets the custom values.
        /// </summary>
        /// <value>The custom values.</value>
        public virtual string CustomValues
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
        public virtual AdministeredItem Administer(Account who, Task task, double value, UnitOfMeasurement unit = null)
        {
            return this.AddItem(who, task, this.NewValue(value, unit));
        }

        /// <summary>
        /// Get a custom values list
        /// </summary>
        /// <returns>A list of double</returns>
        public virtual IList<double> GetCustomValues()
        {
            return JsonConvert.DeserializeObject<List<double>>(this.CustomValues);
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
        /// Composes the custom values list to a string.
        /// </summary>
        /// <param name="customValues">The list of custom values.</param>
        /// <returns>A json-serialized string containing custom or default values.</returns>
        protected string ComposeCustomValues(IList<double> customValues = null)
        {
            if (customValues.IsEmpty())
            {
                customValues = new List<double> { 0, 1, 2, 3, 5, 6, 7, 8, 9, 10 };
            }
            return JsonConvert.SerializeObject(customValues); //// TODO: Handle the list values.
        }

        /// <summary>
        /// Updates the administration with a specified unit and a list of values.
        /// </summary>
        /// <param name="unit">The <see cref="UnitOfMeasurement"/>.</param>
        /// <param name="customValues">The list of custom values.</param>
        /// <returns><see cref="Administration"/>.</returns>
        public abstract Administration Update(UnitOfMeasurement unit, IList<double> customValues = null);

        /// <summary>
        /// News the value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="unit">The unit.</param>
        /// <returns>ArbitraryValue.</returns>
        protected abstract ArbitraryValue NewValue(double value, UnitOfMeasurement unit = null);

        #endregion
    }
}