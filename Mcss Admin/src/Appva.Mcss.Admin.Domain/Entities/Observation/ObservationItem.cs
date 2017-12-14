// <copyright file="ObservationItem.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Mcss.Admin.Domain.VO;
    using Validation;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class ObservationItem : AggregateRoot
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservationItem"/> class.
        /// </summary>
        /// <param name="observation">The observation.</param>
        /// <param name="measurement">The measurement.</param>
        /// <param name="task">The Task</param>
        /// <param name="signature">The signature.</param>
        /// <param name="comment">The comment.</param>
        public ObservationItem(Observation observation, Measurement measurement, Task task = null, Signature signature = null, Comment comment = null)
        {
            Requires.NotNull(observation, "observation");
            Requires.NotNull(measurement, "measurement");
            this.Observation = observation;
            this.Measurement = measurement;
            this.Task        = task;
            this.Signature   = signature;
            this.Comment     = comment;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservationItem"/> class.
        /// </summary>
        /// <remarks>
        /// An NHibernate visible no-argument constructor.
        /// </remarks>
        internal protected ObservationItem()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The parent observation.
        /// </summary>
        public virtual Observation Observation
        {
            get;
            internal protected set;
        }

        /// <summary>
        /// The measurement.
        /// </summary>
        public virtual Measurement Measurement
        {
            get;
            internal protected set;
        }

        /// <summary>
        /// The signature.
        /// </summary>
        public virtual Signature Signature
        {
            get;
            internal protected set;
        }

        /// <summary>
        /// The comment / Note.
        /// </summary>
        public virtual Comment Comment
        {
            get;
            internal protected set;
        }

        /// <summary>
        /// The task.
        /// </summary>
        public virtual Task Task
        {
            get;
            internal protected set;
        }

        #endregion

        #region Public Static Builders.

        /// <summary>
        /// Creates a new instance of the <see cref="ObservationItem"/> class.
        /// </summary>
        /// <param name="observation">The observation.</param>
        /// <param name="measurement">The measurement.</param>
        /// <param name="signature">The signature.</param>
        /// <param name="task">The task.</param>
        /// <param name="comment">The comment.</param>
        /// <returns>A new <see cref="ObservationItem"/> instance.</returns>
        public static ObservationItem New(Observation observation, Measurement measurement, Task task = null, Signature signature = null, Comment comment = null)
        {
            return new ObservationItem(observation, measurement, task, signature, comment);
        }

        #endregion
    }
}