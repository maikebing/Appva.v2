// <copyright file="TenaObservationItem.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>

namespace Appva.Mcss.Admin.Domain.Entities
{
    using Appva.Mcss.Admin.Domain.VO;
    #region Imports

    using System;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class TenaObservationItem : AggregateRoot
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
        /// TenaObservationPeriod, ForeignKeyId
        /// </summary>
        public virtual TenaObservationPeriod TenaObservationPeriod
        {
            get;
            set;
        }

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

        #endregion

        #region Public Static Builders.

        /// <summary>
        /// Creates a new instance of the <see cref="TenaObservationItem"/> class.
        /// </summary>
        /// <param name="observation">The observation.</param>
        /// <param name="measurement">The measurement.</param>
        /// <param name="signature">The signature.</param>
        /// <returns>A new <see cref="ObservationItem"/> instance.</returns>

        #endregion
    }
}
