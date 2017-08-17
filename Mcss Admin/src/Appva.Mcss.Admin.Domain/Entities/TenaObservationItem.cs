// <copyright file="TenaObservationItem.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>

namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports

    using System;
    using Appva.Common.Domain;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class TenaObservationItem : AggregateRoot<TenaObservationItem>
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="TenaObservationItem"/> class.
        /// </summary>
        public TenaObservationItem()
        {
        }
        
        #endregion

        #region Properties
        /// <summary>
        /// Measurement
        /// </summary>
        public virtual string Measurement
        {
            get;
            set;
        }

        /// <summary>
        /// Signature
        /// </summary>
        public virtual string Signature
        {
            get;
            set;
        }

        /// <summary>
        /// Comment
        /// </summary>
        public virtual string Comment
        {
            get;
            set;
        }

        /// <summary>
        /// TenaObservationPeriod, ForeignKeyd
        /// </summary>
        public virtual TenaObservationPeriod TenaObservationPeriod
        {
            get;
            set;
        }
        #endregion
    }
}
