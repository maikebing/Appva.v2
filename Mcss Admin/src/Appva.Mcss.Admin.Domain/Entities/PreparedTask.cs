// <copyright file="PreparedTask.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Common.Domain;

    #endregion

    /// <summary>
    /// Represents a sequence of tasks that should be prepared
    /// </summary>
    public class PreparedTask : AggregateRoot<PreparedTask>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PreparedTask"/> class.
        /// </summary>
        public PreparedTask()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The account that prepared this task
        /// </summary>
        public virtual Account PreparedBy
        {
            get;
            set;
        }

        /// <summary>
        /// The PrepareSequence this task belongs to
        /// </summary>
        public virtual PreparedSequence PreparedSequence
        {
            get;
            set;
        }

        /// <summary>
        /// The date the task was perepared for
        /// </summary>
        public virtual DateTime Date
        {
            get;
            set;
        }

        /// <summary>
        /// The schedule the task belongs to
        /// </summary>
        public virtual Schedule Schedule
        {
            get;
            set;
        }

        #endregion
    }
}