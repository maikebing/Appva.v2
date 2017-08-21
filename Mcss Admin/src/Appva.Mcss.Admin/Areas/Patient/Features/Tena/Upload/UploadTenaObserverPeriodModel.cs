﻿// <copyright file="UploadTenaObserverPeriodModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Models
{
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
        /// HttpStatusCode
        /// </summary>
        public HttpStatusCode StatusCode
        {
            get;
            set;
        }

        /// <summary>
        /// HttpStatusMessage as String
        /// </summary>
        public string StatusMessage
        {
            get;
            set;
        }

        /// <summary>
        /// Tena Identifi ID
        /// </summary>
        public string TenaId
        {
            get;
            set;
        }

    }
}