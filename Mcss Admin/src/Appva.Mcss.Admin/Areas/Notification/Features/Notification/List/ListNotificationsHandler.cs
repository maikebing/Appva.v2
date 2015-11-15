// <copyright file="ListNotificationsHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Models.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ListNotificationsHandler : RequestHandler<ListNotifications, ListNotifictationsModel>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="INotificationService"/> implementation
        /// </summary>
        private readonly INotificationService notifications;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ListNotificationsHandler"/> class.
        /// </summary>
        public ListNotificationsHandler(INotificationService notifications)
        {
            this.notifications = notifications;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override ListNotifictationsModel Handle(ListNotifications message)
        {
            var page = message.Page < 1 ? 1 : message.Page;
            var pageSize = message.Page < 10 ? 10 : message.Page;

            var notifications = this.notifications.List(page, pageSize);

            return new ListNotifictationsModel
            {
                Page = (int)notifications.CurrentPage,
                PageSize = (int)notifications.PageSize,
                Total = (int)notifications.TotalCount,
                Notifications = notifications.Entities
            };
        }

        #endregion
    }
}