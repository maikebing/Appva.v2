// <copyright file="Schedule.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class Schedule : AggregateRoot
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="Schedule"/> class.
        /// </summary>
        public Schedule()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// Name of the schedule, e.g. "Medication schedule"
        /// </summary>
        public virtual string Name
        {
            get;
            set;
        }

        /// <summary>
        /// A Schedule has a Many-to-One relationship to a Patient, 
        /// meaning that for each patient there can be 1 or N schedules.
        /// </summary>
        public virtual Patient Patient
        {
            get;
            set;
        }

        /// <summary>
        /// Specific settings for the schedule.
        /// </summary>
        public virtual ScheduleSettings ScheduleSettings
        {
            get;
            set;
        }

        /// <summary>
        /// A schedule has N items, such as an activity
        /// </summary>
        public virtual IList<Sequence> Sequences
        {
            get;
            set;
        }

        #endregion

        #region Public Methods.

        /// <summary>
        /// Adds a <see cref="Sequence"/> to the <see cref="Schedule"/>
        /// </summary>
        /// <param name="sequence"></param>
        public virtual void Add(Sequence sequence)
        {
            Sequences.Add(sequence);
        }

        #endregion
    }
}