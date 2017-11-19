// <copyright file="SubTask.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Validation;

    #endregion

    /// <summary>
    /// Represents a sub task.
    /// </summary>
    public class SubTask : AggregateRoot
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="SubTask"/> class.
        /// </summary>
        /// <param name="task">The task which is sub tasked.</param>
        /// <param name="observationItem">The observation item.</param>
        public SubTask(Task task, ObservationItem observationItem)
        {
            Requires.NotNull(task,            "task"           );
            Requires.NotNull(observationItem, "observationItem");
            this.Task            = task;
            this.ObservationItem = observationItem;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SubTask"/> class.
        /// </summary>
        /// <remarks>
        /// An NHibernate visible no-argument constructor.
        /// </remarks>
        protected SubTask()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The task which is sub tasked.
        /// </summary>
        public virtual Task Task
        {
            get;
            internal protected set;
        }

        /// <summary>
        /// The observation item.
        /// </summary>
        public virtual ObservationItem ObservationItem
        {
            get;
            internal protected set;
        }

        #endregion
    }
}