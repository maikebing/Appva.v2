﻿// <copyright file="NotificationReadByAccount.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.alvegard@appva.se">Richard Alvegard</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using Appva.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class NotificationViewedBy : AggregateRoot<NotificationViewedBy>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationViewedBy"/> class.
        /// </summary>
        public NotificationViewedBy()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// The account which read the notification
        /// </summary>
        public virtual Account Account
        {
            get;
            set;
        }

        /// <summary>
        /// The notification
        /// </summary>
        public virtual Notification Notification
        {
            get;
            set;
        }

        #endregion
    }
}