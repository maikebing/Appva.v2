// <copyright file="Dosage.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
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
    public class Dosage : AggregateRoot
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Dosage"/> class.
        /// </summary>
        public Dosage()
        {
        }

        #endregion

        #region Properties

        public virtual int DayInPeriod
        {
            get;
            set;
        }

        public virtual double? Amount
        {
            get;
            set;
        }

        public virtual int Time
        {
            get;
            set;
        }

        #endregion
    }
}