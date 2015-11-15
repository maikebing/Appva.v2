// <copyright file="DashboardNotification.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.alvegard@appva.se">Richard Alvegard</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class DashboardNotification : Notification
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="DashboardNotification"/> class.
        /// </summary>
        public DashboardNotification()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// The notification template
        /// </summary>
        public virtual string Template
        {
            get;
            set;
        }

        #endregion
    }
}