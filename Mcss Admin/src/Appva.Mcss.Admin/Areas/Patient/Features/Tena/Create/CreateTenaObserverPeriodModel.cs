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
        public DateTime StartDate
        {
            get;
            set;
        }

        /// <summary>
        /// EndDate
        /// </summary>
        public DateTime EndDate
        {
            get;
            set;
        }
    }
}