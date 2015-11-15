// <copyright file="NotificationDashboardHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.alvegard@appva.se">Richard Alvegard</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Models.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Application.Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class NotificationDashboardHandler : RequestHandler<NotificationDashboard, NotificationDashboardModel>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="INotificationService" />
        /// </summary>
        private readonly INotificationService notifications;

        /// <summary>
        /// The <see cref="IAccountService" />
        /// </summary>
        private readonly IAccountService accounts;

        /// <summary>
        /// <see cref="IIdentityService"/>
        /// </summary>
        private readonly IIdentityService identity;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationDashboardHandler"/> class.
        /// </summary>
        public NotificationDashboardHandler(INotificationService notifications, IAccountService accounts, IIdentityService identity)
        {
            this.notifications = notifications;
            this.accounts = accounts;
            this.identity = identity;
        }

        #endregion

        #region RequestHandler overrides.

        public override NotificationDashboardModel Handle(NotificationDashboard message)
        {
            var notification = this.notifications.GetFirstVisibleDashboardNotificationByAccount(this.identity.PrincipalId);
            if(notification != null)
            { 
                var account = this.accounts.Find(this.identity.PrincipalId);
                this.notifications.AddNotificationViewedBy(notification, account);
                return new NotificationDashboardModel 
                {
                    Template = notification.Template
                };
            }
            return null;
        }

        #endregion
    }
}