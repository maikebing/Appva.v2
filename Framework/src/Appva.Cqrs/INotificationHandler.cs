// <copyright file="INotificationHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
// Copyright (c) 2013 Matt Hinze
// Permission is hereby granted, free of charge, to any person obtaining a copy of 
// this software and associated documentation files (the "Software"), to deal in 
// the Software without restriction, including without limitation the rights to 
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of 
// the Software, and to permit persons to whom the Software is furnished to do so, 
// subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all 
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
namespace Appva.Cqrs
{
    #region Imports.

    using System.Diagnostics.CodeAnalysis;
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
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public abstract class AsyncNotificationHandler<TNotification> : IAsyncNotificationHandler<TNotification>
         where TNotification : IAsyncNotification
    {
        #region IAsyncNotificationHandler<TNotification> Members.

        /// <inheritdoc />
        public abstract Task Handle(TNotification notification);

        #endregion
    }
}
