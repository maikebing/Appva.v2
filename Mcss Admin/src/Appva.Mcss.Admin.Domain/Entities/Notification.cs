// <copyright file="Notification.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
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
    public class Notification : AggregateRoot
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="Notification"/> class.
        /// </summary>
        public Notification()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The discriptive name of the notification
        /// </summary>
        public virtual string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Discriminator to set the type of the notification
        /// </summary>
        public virtual NotificationType NotificationType 
        {
            get;
            set;
        }

        /// <summary>
        /// If the notification is published
        /// </summary>
        public virtual bool Published
        {
            get;
            set;
        }

        /// <summary>
        /// When the notification should be active
        /// </summary>
        public virtual DateTime PublishedDate 
        {
            get;
            set;
        }

        /// <summary>
        /// When the notificaton should be inactive
        /// </summary>
        public virtual DateTime? UnPublishedDate 
        {
            get;
            set;
        }

        /// <summary>
        /// If the notification is vissible to everyone
        /// </summary>
        public virtual bool IsVisibleToEveryone
        {
            get;
            set;
        }

        /// <summary>
        /// Accounts which the notification should be avaliable to
        /// </summary>
        public virtual IList<Account> VisibleTo
        {
            get;
            set;
        }
        
        /// <summary>
        /// Users who read the notification
        /// </summary>
        public virtual IList<NotificationViewedBy> ViewedBy
        {
            get;
            set;
        }

        #endregion

        #region Public Methods.

        /// <summary>
        /// Adds a view by a user to the current notification
        /// </summary>
        /// <param name="account"></param>
        public virtual void AddView(Account account)
        {
            if (this.ViewedBy == null)
            {
                this.ViewedBy = new List<NotificationViewedBy>();
            }
            this.ViewedBy.Add(new NotificationViewedBy { Account = account, Notification = this });
        }

        #endregion
    }

    #region Enums.

    /// <summary>
    /// Defines the notification-type
    /// </summary>
    public enum NotificationType
    {
        DashboardNotification
    }

    #endregion
}