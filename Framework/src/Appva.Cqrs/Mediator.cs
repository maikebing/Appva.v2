// <copyright file="Mediator.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Cqrs
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Reflection;
    using System.Threading.Tasks;

    #endregion

    /// <summary>
    /// Intermediary to decouple many peers.
    /// </summary>
    public interface IMediator
    {
        /// <summary>
        /// Sends a request.
        /// </summary>
        /// <typeparam name="TResponse">The response type</typeparam>
        /// <param name="request">The request</param>
        /// <returns>The responce {T}</returns>
        TResponse Send<TResponse>(IRequest<TResponse> request);

        /// <summary>
        /// Sends a request asynchronously.
        /// </summary>
        /// <typeparam name="TResponse">The response type</typeparam>
        /// <param name="request">The request</param>
        /// <returns>The responce {T}</returns>
        Task<TResponse> SendAsync<TResponse>(IAsyncRequest<TResponse> request);

        /// <summary>
        /// Publish a notification.
        /// </summary>
        /// <typeparam name="TNotification">The notification type</typeparam>
        /// <param name="notification">The notification to be published</param>
        void Publish<TNotification>(TNotification notification) where TNotification : INotification;

        /// <summary>
        /// Publish a notification asynchronously.
        /// </summary>
        /// <typeparam name="TNotification">The notification type</typeparam>
        /// <param name="notification">The notification to be published</param>
        /// <returns>Task</returns>
        Task PublishAsync<TNotification>(TNotification notification) where TNotification : IAsyncNotification;
    }

    /// <summary>
    /// Intermediary to decouple many peers.
    /// </summary>
    public class Mediator : IMediator
    {
        #region Variables.

        /// <summary>
        /// The service locator.
        /// </summary>
        private readonly IDependencyResolver dependencyResolver;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Mediator"/> class.
        /// </summary>
        /// <param name="dependencyResolver">The <see cref="IDependencyResolver"/></param>
        public Mediator(IDependencyResolver dependencyResolver)
        {
            this.dependencyResolver = dependencyResolver;
        }

        #endregion

        /// <inheritdoc />
        public TResponse Send<TResponse>(IRequest<TResponse> request)
        {
            var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
            var handler = this.dependencyResolver.GetInstance(handlerType);
            if (handler == null)
            {
                throw new InvalidOperationException("Handler was not found for request of type " + handlerType);
            }
            return (TResponse) this.GetHandlerMethod(handlerType, "Handle", request.GetType()).Invoke(handler, new object[] { request });
        }

        /// <inheritdoc />
        [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1008:OpeningParenthesisMustBeSpacedCorrectly", Justification = "Reviewed.")]
        public async Task<TResponse> SendAsync<TResponse>(IAsyncRequest<TResponse> request)
        {
            var handlerType = typeof(IAsyncRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
            var handler = this.dependencyResolver.GetInstance(handlerType);
            if (handler == null)
            {
                throw new InvalidOperationException("Handler was not found for request of type " + handlerType);
            }
            return await (Task<TResponse>) this.GetHandlerMethod(handlerType, "Handle", request.GetType())
                .Invoke(handler, new object[] { request });
        }

        /// <inheritdoc />
        public void Publish<TNotification>(TNotification notification) where TNotification : INotification
        {
            var notificationHandlers = this.NotificationHandlers<TNotification>();
            foreach (var handler in notificationHandlers)
            {
                handler.Handle(notification);
            }
        }

        /// <inheritdoc />
        public async Task PublishAsync<TNotification>(TNotification notification) where TNotification : IAsyncNotification
        {
            var notificationHandlers = this.AsyncNotificationHandlers<TNotification>();
            foreach (var handler in notificationHandlers)
            {
                await handler.Handle(notification);
            }
        }

        /// <summary>
        /// Dispatch the notification handler.
        /// </summary>
        /// <typeparam name="TNotification">The notification type</typeparam>
        /// <returns>An enumerable notification handler collection</returns>
        private IEnumerable<INotificationHandler<TNotification>> NotificationHandlers<TNotification>()
            where TNotification : INotification
        {
            return this.dependencyResolver.GetInstances<INotificationHandler<TNotification>>();
        }

        /// <summary>
        /// Dispatch the notification handler asynchronously.
        /// </summary>
        /// <typeparam name="TNotification">The notification type</typeparam>
        /// <returns>An enumerable notification handler collection</returns>
        private IEnumerable<IAsyncNotificationHandler<TNotification>> AsyncNotificationHandlers<TNotification>()
            where TNotification : IAsyncNotification
        {
            return this.dependencyResolver.GetInstances<IAsyncNotificationHandler<TNotification>>();
        }

        /// <summary>
        /// Returns the handler method.
        /// </summary>
        /// <param name="handlerType">The handler type</param>
        /// <param name="handlerMethodName">The handler method name</param>
        /// <param name="messageType">The message type</param>
        /// <returns>The <see cref="MethodInfo"/></returns>
        private MethodInfo GetHandlerMethod(Type handlerType, string handlerMethodName, Type messageType)
        {
            return handlerType.GetMethod(
                    handlerMethodName,
                    BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod,
                    null, 
                    CallingConventions.HasThis,
                    new[] { messageType },
                    null);
        }
    }
}
