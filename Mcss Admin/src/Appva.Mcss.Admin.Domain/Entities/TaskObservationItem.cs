// <copyright file="TaskObservationItem.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>

namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using Appva.Mcss.Admin.Domain.VO;
    using Validation;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class TaskObservationItem : ObservationItem
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskObservationItem"/> class.
        /// </summary>
        /// <param name="task">The task</param>
        /// <param name="dosageObservation">The dosage observation</param>
        /// <param name="measurement">The measurement</param>
        /// <param name="signature">The signature</param>
        public TaskObservationItem(Task task, DosageObservation dosageObservation, Measurement measurement, Signature signature = null)
            :base(measurement, signature)
        {
            Requires.NotNull(dosageObservation, "dosageObservation");
            Requires.NotNull(task, "task");
            this.DosageObservation = dosageObservation;
            this.Task = task;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskObservationItem"/> class.
        /// </summary>
        /// <remarks>
        /// An NHibernate visible no-argument constructor.
        /// </remarks>
        protected TaskObservationItem()
        {
        }

        #endregion

        #region Properties

        public virtual DosageObservation DosageObservation
        {
            get;
            set;
        }

        /// <summary>
        /// The task
        /// </summary>
        public virtual Task Task
        {
            get;
            set;
        }

        #endregion

        #region Public static builders

        /// <summary>
        /// Creates a new instance of the <see cref="TaskObservationItem"/> class.
        /// </summary>
        /// <param name="task">The task</param>
        /// <param name="dosageObservation">The dosage observation</param>
        /// <param name="measurement">The measurement</param>
        /// <param name="signature">The signature</param>
        /// <returns><see cref="TaskObservationItem"/></returns>
        public static TaskObservationItem New(Task task, DosageObservation dosageObservation, Measurement measurement, Signature signature = null)
        {
            return new TaskObservationItem(task, dosageObservation, measurement, signature);
        }
        #endregion
    }
}
