// <copyright file="ListAlert.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>

namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using Appva.Cqrs;
    using Appva.Mcss.Web.ViewModels;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ListAlert : IRequest<ListAlertModel>
    {
        /// <summary>
        /// The patient ID.
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// Optional year filtering parameter.
        /// </summary>
        public int? Year
        {
            get;
            set;
        }

        /// <summary>
        /// Optional month filtering parameter.
        /// </summary>
        public int? Month
        {
            get;
            set;
        }

        /// <summary>
        /// Optional start date filtering parameter.
        /// </summary>
        public DateTime? StartDate
        {
            get;
            set;
        }

        /// <summary>
        /// Optional end date filtering parameter.
        /// </summary>
        public DateTime? EndDate
        {
            get;
            set;
        }
    }
}