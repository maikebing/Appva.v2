// <copyright file="TenaObservationPeriod.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports

    using System;
    using Validation;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class TenaObservationPeriod : Observation
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="TenaObservationPeriod"/> class.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="patient">The patient.</param>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        /// <param name="category">The category.</param>
        public TenaObservationPeriod(DateTime startDate, DateTime endDate, Patient patient, string name, string description) 
        {
            Requires.NotNull(patient, "patient");
            Requires.NotNullOrWhiteSpace(name, "name");
            Requires.NotNullOrWhiteSpace(description, "description");
            this.Patient     = patient;
            this.Name        = name;
            this.Description = description;
            this.StartDate   = startDate;
            this.EndDate     = endDate;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TenaObservationPeriod"/> class.
        /// </summary>
        /// <remarks>An NHibernate visible no-argument constructor.</remarks>
        public TenaObservationPeriod()
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
        public virtual string Status
        {
            get
            {
                if (DateTime.Now > EndDate  ) { return "completed"; }
                if (DateTime.Now < StartDate) { return "pending";   }
                return "in-progress";
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
        /// Updates the current tena period.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="instruction">The instruction.</param>
        /// <param name="startsAt">The starts at.</param>
        /// <param name="endsAt">The ends at.</param>
        public virtual void Update(string name, string instruction, DateTime startsAt, DateTime endsAt)
        {
            Requires.NotNullOrWhiteSpace(name,        "name");
            Requires.NotNullOrWhiteSpace(instruction, "description");
            this.Name        = name;
            this.Description = instruction;
            this.StartDate   = startsAt;
            this.EndDate     = endsAt;
        }

        #endregion
    }
}
