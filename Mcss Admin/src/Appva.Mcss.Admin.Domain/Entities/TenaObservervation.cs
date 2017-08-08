﻿// <copyright file="TenaObservervation.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>

namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports

    using Appva.Common.Domain;
    using System;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class TenaObservation : AggregateRoot<TenaObservation>
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="TenaObservation"/> class.
        /// </summary>
        public TenaObservation()
        {
        }
        
        #endregion

        #region Properties

        /// <summary>
        /// TenaId
        /// </summary>
        public virtual Guid TenaObservationPeriodId
        {
            get;
            set;
        }

        /// <summary>
        /// Measured value
        /// </summary>
        public virtual string Value
        {
            get;
            set;
        }

        /// <summary>
        /// Registered by
        /// </summary>
        public virtual string CreatedBy
        {
            get;
            set;
        }

        #endregion
    }
}
