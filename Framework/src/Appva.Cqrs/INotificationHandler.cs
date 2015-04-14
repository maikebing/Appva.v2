// <copyright file="INotificationHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Cqrs
{
    #region Imports.

    using System.Threading.Tasks;

    #endregion

    /// <summary>
    /// Notification handler.
    /// </summary>
    /// <typeparam name="TNotification">The notification type</typeparam>
    public interface INotificationHandler<in TNotification>
        where TNotification : INotification
    {
        /// <summary>
        /// Executes the notification.
        /// </summary>
        /// <param name="notification">The notification</param>
        void Handle(TNotification notification);
    }

    /// <summary>
    /// Asyncronous Notification handler.
    /// </summary>
    /// <typeparam name="TNotification">The notification type</typeparam>
    public interface IAsyncNotificationHandler<in TNotification>
        where TNotification : IAsyncNotification
    {
        /// <summary>
        /// Executes the notification.
        /// </summary>
        /// <param name="notification">The notification</param>
        /// <returns>A task</returns>
        Task Handle(TNotification notification);
    }

    /// <summary>
    /// Abstract base <see cref="INotificationHandler{TNotification}"/> 
    /// implementation.
    /// </summary>
    /// <typeparam name="TNotification">The request type</typeparam>
    public abstract class NotificationHandler<TNotification> : INotificationHandler<TNotification>
         where TNotification : INotification
    {
        #region INotificationHandler<TNotification> Members.

        /// <inheritdoc />
        public abstract void Handle(TNotification notification);

        #endregion
    }

    /// <summary>
    /// Abstract base <see cref="IAsyncNotificationHandler{TNotification}"/> 
    /// implementation.
    /// </summary>
    /// <typeparam name="TNotification">The request type</typeparam>
    public abstract class AsyncNotificationHandler<TNotification> : IAsyncNotificationHandler<TNotification>
         where TNotification : IAsyncNotification
    {
        #region IAsyncNotificationHandler<TNotification> Members.

        /// <inheritdoc />
        public abstract Task Handle(TNotification notification);

        #endregion
    }
}
