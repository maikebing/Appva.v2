// <copyright file="TenaObservation.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using Validation;

    #endregion

    /// <summary>
    /// A TENA Identifi scale <see cref="Observation"/> implementation.
    /// </summary>
    [DisplayName("TENA Identifi")]
    [Description("Registration of bathroom behavior")]
    public class TenaObservation : Observation
    {
        /// <summary>
        /// A list of valid tena values.
        /// </summary>
        private readonly List<string> valueList = new List<string> { "Toilet", "ToiletUnsuccessful", "Leakage", "Feces" };

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="TenaObservation"/> class.
        /// </summary>
        /// <param name="startAt">The start date.</param>
        /// <param name="endAt">The end date.</param>
        /// <param name="patient">The patient.</param>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        public TenaObservation(DateTime startAt, DateTime endAt, Patient patient, string name, string description) 
        {
            Requires.NotNull(patient,                 "patient");
            Requires.NotNullOrWhiteSpace(name,        "name");
            Requires.NotNullOrWhiteSpace(description, "description");
            this.Patient     = patient;
            this.Name        = name;
            this.Description = description;
            this.StartDate   = startAt;
            this.EndDate     = endAt;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TenaObservation"/> class.
        /// </summary>
        /// <remarks>
        /// An NHibernate visible no-argument constructor.
        /// </remarks>
        protected internal TenaObservation()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// Date at start of period.
        /// </summary>
        public virtual DateTime StartDate
        {
            get;
            protected internal set;
        }

        /// <summary>
        /// Date at end of period.
        /// </summary>
        public virtual DateTime EndDate
        {
            get;
            protected internal set;
        }

        /// <summary>
        /// Computes the current status of the period.
        /// E.g. if the period is pending, in progress or completed
        /// </summary>
        /// <value>
        /// The current status.
        /// </value>
        [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1501:StatementMustNotBeOnSingleLine", Justification = "Reviewed.")]
        public virtual string Status
        {
            get
            {
                if (DateTime.Now > this.EndDate  ) { return "completed"; }
                if (DateTime.Now < this.StartDate) { return "pending";   }
                return "in-progress";
            }
        }

        #endregion

        #region Methods.

        /// <summary>
        /// Updates the current tena period.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="instruction">The instruction.</param>
        /// <param name="startsAt">The starts at.</param>
        /// <param name="endsAt">The ends at.</param>
        public virtual void Update(string name, string instruction, DateTime startsAt, DateTime endsAt)
        {
            Requires.NotNullOrWhiteSpace(name, "name");
            Requires.NotNullOrWhiteSpace(instruction, "description");
            this.Name        = name;
            this.Description = instruction;
            this.StartDate   = startsAt;
            this.EndDate     = endsAt;
        }

        /// <inheritdoc />
        protected override ArbitraryValue NewMeasurement<T>(T value, UnitOfMeasurement unit = null)
        {
            Requires.ValidState(value.IsNull() == false, "value");
            unit = unit ?? NonUnits.TenaIdentifiScale;
            return ArbitraryValue.New(this.Validate(value), LevelOfMeasurement.Nominal, null);
        }

        /// <inheritdoc />
        protected override ArbitraryValue NewMeasurement(string value, UnitOfMeasurement unit = null)
        {
            Requires.NotNullOrWhiteSpace(value, "value");
            unit = unit ?? NonUnits.TenaIdentifiScale;
            return ArbitraryValue.New(this.Validate(value), LevelOfMeasurement.Nominal, null);
        }

        /// <summary>
        /// Validates 'value' against a list with valid values.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns>value if valid.</returns>
        private T Validate<T>(T value)
        {
            if (((value.Is<string>()) || (value.GetType().IsEnum)) == false)
            {
                throw new ArgumentOutOfRangeException("value", value, " is an invalid value.");
            }
            if (this.valueList.Contains(value.ToString()) == false)
            {
                throw new ArgumentOutOfRangeException("value", value, " is not a valid value.");
            }
            return value;
        }

        #endregion
    }
}
