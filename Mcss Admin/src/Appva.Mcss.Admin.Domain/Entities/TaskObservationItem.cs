//// <copyright file="TaskObservationItem.cs" company="Appva AB">
////     Copyright (c) Appva AB. All rights reserved.
//// </copyright>

//namespace Appva.Mcss.Admin.Domain.Entities
//{
//    #region Imports.

//    using Appva.Mcss.Admin.Domain.VO;
//    using Validation;

//    #endregion

//    /// <summary>
//    /// TODO: Add a descriptive summary to increase readability.
//    /// </summary>
//    public class TaskObservationItem : ObservationItem
//    {
//        #region Constructor

//        /// <summary>
//        /// Initializes a new instance of the <see cref="TaskObservationItem"/> class.
//        /// </summary>
//        /// <param name="task">The task. Required</param>
//        /// <param name="observation">The dosage observation. Required</param>
//        /// <param name="measurement">The measurement. Required</param>
//        /// <param name="signature">The signature</param>
//        public TaskObservationItem(Observation observation, Measurement measurement, Task task, Signature signature, Comment comment = null)
//            : base(observation, measurement, task, signature, comment)
//        {
//            Requires.NotNull(signature, "signature");
//            Requires.NotNull(task, "task");
//        }

//        /// <summary>
//        /// Initializes a new instance of the <see cref="TaskObservationItem"/> class.
//        /// </summary>
//        /// <remarks>
//        /// An NHibernate visible no-argument constructor.
//        /// </remarks>
//        protected TaskObservationItem()
//        {
//        }

//        #endregion

//        #region Properties

//        #endregion

//        #region Public static builders

//        /// <summary>
//        /// Creates a new instance of the <see cref="TaskObservationItem"/> class.
//        /// </summary>
//        /// <param name="task">The task</param>
//        /// <param name="dosageObservation">The dosage observation</param>
//        /// <param name="measurement">The measurement</param>
//        /// <param name="signature">The signature</param>
//        /// <returns><see cref="TaskObservationItem"/></returns>
//        public new static TaskObservationItem New(Observation observation, Measurement measurement, Task task, Signature signature, Comment comment = null)
//        {
//            return new TaskObservationItem(observation, measurement, task, signature, comment);
//        }
//        #endregion
//    }
//}
