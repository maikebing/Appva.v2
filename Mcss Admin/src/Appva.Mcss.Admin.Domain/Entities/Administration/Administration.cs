// <copyright file="Administration.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Domain;
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
        /// <param name="name"></param>
        /// <param name="sequence"><see cref="Sequence"/></param>
        /// <param name="valueModel"><see cref="AdministrationValueModel"/></param>
        protected Administration(Sequence sequence, AdministrationValueModel valueModel)
        {
            Requires.NotNull(valueModel, "valueModel");
            this.Sequence = sequence;
            this.Patient = sequence.Patient;
            this.Name = valueModel.Name;
            this.CustomValues = valueModel.CustomValues;
            this.CustomValues.ValueId = valueModel.Id;
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
        /// The <see cref="AdministrationValues"/>.
        /// </summary>
        public virtual AdministrationValues CustomValues
        {
            get;
            protected internal set;
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
        /// News the value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="unit">The unit.</param>
        /// <returns>ArbitraryValue.</returns>
        protected abstract ArbitraryValue NewValue(double value);

        #endregion
    }

}