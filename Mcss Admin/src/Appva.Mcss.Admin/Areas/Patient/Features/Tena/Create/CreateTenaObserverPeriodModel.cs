// <copyright file="CreateTenaObserverPeriodModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>

namespace Appva.Mcss.Admin.Models
{
    #region imports

    using System;
    using Appva.Cqrs;
    using DataAnnotationsExtensions;
    using System.ComponentModel.DataAnnotations;

    #endregion

    /// <summary>
    /// TODO: 
    /// </summary>
    public class CreateTenaObserverPeriodModel : Identity<ListTena>
    {
        /// <summary>
        /// PatientId
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// StartDate
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime StartDate
        {
            get;
            set;
        }

        /// <summary>
        /// EndDate
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime EndDate
        {
            get;
            set;
        }
    }
}