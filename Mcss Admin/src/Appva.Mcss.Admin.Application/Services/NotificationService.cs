// <copyright file="INotificationService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Services
{
    #region Imports.

    using Appva.Mcss.Admin.Application.Auditing;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Repositories;
    using Appva.Repository;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web.Hosting;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface INotificationService : IService
    {
        /// <summary>
        /// Gets a Dashboard Notification by account id
        /// </summary>
        /// <param name="accountId">The account id</param>
        /// <returns><see cref="DashboardNotification"/></returns>
        DashboardNotification GetFirstVisibleDashboardNotificationByAccount(Guid accountId);

        /// <summary>
        /// Adds a viewer to a notification
        /// </summary>
        /// <param name="notification">The <see cref="Notification" /></param>
        /// <param name="viewedBy">The <see cref="Account" /></param>
        void AddNotificationViewedBy(Notification notification, Account viewedBy);

        /// <summary>
        /// Lists tasks by given criterias
        /// </summary>
        /// <returns>A <see cref="PageableSet"/> of Notifications</returns>
        PageableSet<Notification> List(int page = 1, int pageSize = 10);

        /// <summary>
        /// Lists all installed and availabel templates
        /// </summary>
        /// <returns></returns>
        IList<string> GetTemplates();
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class NotificationService : INotificationService
    {
        #region Variables.

        /// <summary>
        /// The <see cref="INotificationRepository"/>
        /// </summary>
        private readonly INotificationRepository notifications;

        /// <summary>
        /// The <see cref="IIdentityService" /> implementation
        /// </summary>
        private readonly IIdentityService identity;

        /// <summary>
        /// The <see cref="IAuditService" /> implementation
        /// </summary>
        private readonly IAuditService audit;

        #endregion

        #region Constructor. 

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationService"/> class.
        /// </summary>
        /// <param name="notifications">
        /// The <see cref="INotificationRepository"/> implementation
        /// </param>
        public NotificationService(INotificationRepository notifications, IIdentityService identity, IAuditService audit)
        {
            this.notifications = notifications;
            this.identity = identity;
            this.audit = audit;
        }

        #endregion

        #region INotificationService

        /// <inheritdoc />
        public DashboardNotification GetFirstVisibleDashboardNotificationByAccount(Guid accountId)
        {
            var notification = this.notifications.GetFirstVisibleDashboardNotificationByAccount(accountId);
            if (notification != null)
            {
                this.audit.Read("Notifikation på översikt (ref. {0}) visades för användare {1}", notification.Id, accountId);
            }

            return notification;
        }

        /// <inheritdoc />
        public void AddNotificationViewedBy(Notification notification, Account viewedBy)
        {
            notification.AddView(viewedBy);
            notifications.Update(notification);
        }

        /// <inheritdoc />
        public PageableSet<Notification> List(int page = 1, int pageSize = 10)
        {
            this.audit.Read("Användare {0} läste lista över notifieringar", this.identity.Principal.Identity.Name);
            return this.notifications.List((ulong)page, (ulong)pageSize);
        }

        /// <inheritdoc />
        public IList<string> GetTemplates()
        {
            var path = string.Format("{0}Areas/Notification/Features/Notification/Partials/Templates/", HostingEnvironment.ApplicationPhysicalPath);
            if (Directory.Exists(path))
            {
                return Directory.GetFiles(path).Select(x => x.Replace(path, "").Replace(".cshtml", "")).ToList();
            }

            return new List<string>();
        }

        #endregion
    }
}