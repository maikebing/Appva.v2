// <copyright file="ListTenaModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>

namespace Appva.Mcss.Admin.Models
{
    #region imports

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Appva.Cqrs;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Mcss.Admin.Domain.Entities;

    #endregion


    /// <summary>
    /// TODO: 
    /// </summary>

    public class ListTenaModel
    {
        //Patient

        /// <summary>
        /// Patient Id
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// [TenaObservationPeriod]
        /// Tena Id
        /// StartDate
        /// EndDate
        /// List<TenaObservation>
        /// </summary>
        public string TenaId
        {
            get;
            set;
        }

        public IList<TenaObservation> TenaObservationList { get; set; }
        public IList<TenaObservationPeriod> TenaObservationPeriodsList { get; set; }


        /// <summary>
        /// [TenaObservation]
        /// TenaObservationId
        /// Created
        /// Value
        /// CreatedBy
        /// </summary>
    }
}