// <copyright file="UpdateTenaObservationPeriod.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using Appva.Cqrs;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class UpdateTenaObservationPeriod : IRequest<UpdateTenaObservationPeriodModel>
    {
        #region Properties

        /// <summary>
        /// The patient id
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the period identifier.
        /// </summary>
        /// <value>
        /// The period identifier.
        /// </value>
        public Guid PeriodId
        {
            get;
            set;
        }

        #endregion
    }
}