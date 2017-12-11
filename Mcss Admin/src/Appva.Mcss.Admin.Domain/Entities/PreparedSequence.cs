// <copyright file="PreparedSequence.cs" company="Appva AB">
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
    /// Represents a sequence of tasks that should be prepared.
    /// </summary>
    public class PreparedSequence : AggregateRoot
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="PreparedSequence"/> class.
        /// </summary>
        public PreparedSequence()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The name of the sequence
        /// </summary>
        public virtual string Name
        {
            get;
            set;
        }

        /// <summary>
        /// The schedule this sequence belongs to
        /// </summary>
        public virtual Schedule Schedule
        {
            get;
            set;
        }

        #endregion
    }
}