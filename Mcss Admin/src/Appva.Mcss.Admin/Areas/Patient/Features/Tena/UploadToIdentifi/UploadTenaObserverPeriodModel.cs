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
            get
            {
                if (this.TotalNumberOfEvents == this.EventsCreated + this.EventsUpdated)
                {
                    return "Uppladdning klar";
                }
                return "Uppladdning klar";
            }
        }

        public string Type
        {
            get
            {
                if (this.TotalNumberOfEvents == this.EventsCreated + this.EventsUpdated)
                {
                    return "positive";
                }
                return "neutral";
            }
        }


        /// <summary>
        /// Gets or sets the events created.
        /// </summary>
        /// <value>
        /// The events created.
        /// </value>
        public int EventsCreated { get; set; }

        /// <summary>
        /// Gets or sets the events updated.
        /// </summary>
        /// <value>
        /// The events updated.
        /// </value>
        public int EventsUpdated { get; set; }

        /// <summary>
        /// Gets or sets the events with error.
        /// </summary>
        /// <value>
        /// The events with error.
        /// </value>
        public int EventsWithError { get; set; }

        /// <summary>
        /// Gets or sets the events wthout assesments.
        /// </summary>
        /// <value>
        /// The events wthout assesments.
        /// </value>
        public int EventsWthoutAssesments { get; set; }

        public int TotalNumberOfEvents
        {
            get
            {
                return this.EventsCreated + this.EventsUpdated + this.EventsWithError + this.EventsWthoutAssesments;
            }
        }
    }
}