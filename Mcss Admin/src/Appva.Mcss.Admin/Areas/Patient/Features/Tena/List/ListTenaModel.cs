// <copyright file="ListTenaModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>

namespace Appva.Mcss.Admin.Models
{
    #region imports

    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Web.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: 
    /// </summary>
    public sealed class ListTenaModel
    {
        /// <summary>
        /// Patient
        /// </summary>
        public PatientViewModel Patient
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the periods.
        /// </summary>
        /// <value>
        /// The periods.
        /// </value>
        public IList<TenaObservationPeriod> Periods 
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the current period identifier.
        /// </summary>
        /// <value>
        /// The current period identifier.
        /// </value>
        public Guid CurrentPeriodId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the period.
        /// </summary>
        /// <value>
        /// The period.
        /// </value>
        public TenaObservationPeriod Period
        {
            get
            {
                return Periods.FirstOrDefault(x => x.Id == CurrentPeriodId);
            }
        }
    }
}