﻿// <copyright file="TenaObservationPeriod.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>

namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports

    using System;
    using System.Collections.Generic;
    using Appva.Common.Domain;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class TenaObservationPeriod : AggregateRoot<TenaObservationPeriod>
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="TenaObservationPeriod"/> class.
        /// </summary>
        public TenaObservationPeriod()
        {
        }

        #endregion

        #region Properties
        /// <summary>
        /// Date at start of period
        /// </summary>
        public virtual DateTime StartDate
        {
            get;
            set;
        }

        /// <summary>
        /// Date at end of period
        /// </summary>
        public virtual DateTime EndDate
        {
            get;
            set;
        }

        /// <summary>
        /// Date at end of period
        /// </summary>
        public virtual Patient Patient
        {
            get;
            set;
        }

        /// <summary>
        /// TenaObservationItem
        /// </summary>
        public virtual IList<TenaObservationItem> TenaObservationItems
        {
            get;
            set;
        }

        #endregion
    }
}
