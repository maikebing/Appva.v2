// <copyright file="UploadTenaObserverPeriodModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Models
{
    using Appva.Mcss.Admin.Domain.Entities;
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class UploadTenaObserverPeriodModel
    {
        /// <summary>
        /// Title.
        /// </summary>
        public string Title
        {
            get;
            set;
        }
        /// <summary>
        /// Message
        /// </summary>
        public string Message
        {
            get;
            set;
        }
        /// <summary>
        /// Symbol
        /// </summary>
        public string Icon
        {
            get;
            set;
        }

        public string Type
        {
            get;
            set;
        }

        public TenaObservationPeriod Period
        {
            get;
            set;
        }

    }
}